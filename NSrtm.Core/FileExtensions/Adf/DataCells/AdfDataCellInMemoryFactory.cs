using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal class AdfDataCellInMemoryFactory : IAdfDataCellFactory
    {
        private readonly IAdfDataLoader _loader;

        public AdfDataCellInMemoryFactory([NotNull] IAdfDataLoader loader)
        {
            _loader = loader;
        }

        public IDataCell GetCellFor(AdfCellCoords coords)
        {
            var data = _loader.LoadFromFile(coords);
            return new AdfDataCellInMemory(data, HgtUtils.PointsPerCellEdgeFromDataLength(data.Length), coords);
        }

        public async Task<IDataCell> GetCellForAsync(AdfCellCoords coords)
        {
            var data = await _loader.LoadFromFileAsync(coords);

            return new AdfDataCellInMemory(data, HgtUtils.PointsPerCellEdgeFromDataLength(data.Length), coords);
        }

        private class AdfDataCellInMemory : AdfDataCellBase
        {
            private readonly byte[] _adfData;

            internal AdfDataCellInMemory([NotNull] byte[] adfData, int pointsPerCell, AdfCellCoords coords) : base(pointsPerCell, coords)
            {
                _adfData = adfData;
            }

            public override long MemorySize { get { return _adfData.Length; } }

            [NotNull]
            public override Task<double> GetElevationAsync(double latitude, double longitude)
            {
                return Task.FromResult(GetElevation(latitude, longitude));
            }

            protected override double ElevationAtOffset(int bytesPos)
            {
                // Motorola "big endian" order with the most significant byte first
                Int16 elevation = (short)((_adfData[bytesPos]) << 8 | _adfData[bytesPos + 1]);
                if (elevation > short.MinValue)
                    return elevation;
                else return Double.NaN;
            }
        }
    }
}
