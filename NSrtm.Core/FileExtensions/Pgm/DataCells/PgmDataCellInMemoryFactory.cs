using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MiscUtil.Conversion;
using MiscUtil.IO;
using NSrtm.Core.FileExtensions.Adf.Utils;
using NSrtm.Core.FileExtensions.Pgm.DataCells;

namespace NSrtm.Core
{
    internal class PgmDataCellInMemoryFactory : IPgmDataCellFactory
    {
        private readonly IPgmDataLoader _loader;

        public PgmDataCellInMemoryFactory([NotNull] IPgmDataLoader loader)
        {
            _loader = loader;
        }

        public IDataCell GetCell()
        {
            var data = _loader.LoadFromFile();
            var cellParameters = PgmCellParser.GetParametersFrom(data);
            var elevationData = PgmCellParser.GetElevationFrom(data);
            return new PgmDataCellInMemory(elevationData, cellParameters);
        }

        public async Task<IDataCell> GetCellAsync()
        {
            var data = await _loader.LoadFromFileAsync();
            var cellParameters = await PgmCellParser.GetParametersFromAsync(data);
            var elevationData = await PgmCellParser.GetElevationFromAsync(data);
            return new PgmDataCellInMemory(elevationData, cellParameters);
        }

        private class PgmDataCellInMemory : PgmDataCellBase
        {
            private readonly IReadOnlyList<UInt16> _adfData;

            internal PgmDataCellInMemory([NotNull] byte[] adfData, PgmCellParameters pgmParameters)
                : base(pgmParameters)
            {
                var data = new List<UInt16>();
                using (var stream = new MemoryStream(adfData))
                using (EndianBinaryReader reader = new EndianBinaryReader(EndianBitConverter.Big, stream))
                {
                    var numberOfPoints = pgmParameters.CellHightPoints * pgmParameters.CellWidthPoints;
                    while (data.Count < numberOfPoints)
                    {
                        data.Add(reader.ReadUInt16());
                    }
                }
                _adfData = data.AsReadOnly();
            }

            public override long MemorySize { get { return _adfData.Count; } }

            [NotNull]
            public override Task<double> GetElevationAsync(double latitude, double longitude)
            {
                return Task.FromResult(GetElevation(latitude, longitude));
            }

            protected override double ElevationAtOffset(int pointPos)
            {
                var rawElevation = _adfData[pointPos];
                if (rawElevation > PgmParameters.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("rawElevation");
                }
                return rawElevation * PgmParameters.Scale + PgmParameters.Offset;
            }
        }
    }
}
