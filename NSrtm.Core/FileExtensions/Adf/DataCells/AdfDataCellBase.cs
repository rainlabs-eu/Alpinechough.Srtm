using System;
using System.Threading.Tasks;

namespace NSrtm.Core
{
    internal abstract class AdfDataCellBase : IDataCell
    {
        private readonly int _pointsPerCell;
        private readonly AdfCellCoords _coords;

        protected AdfDataCellBase(int pointsPerCell, AdfCellCoords coords)
        {
            _pointsPerCell = pointsPerCell;
            _coords = coords;
        }

        public double GetElevation(double latitude, double longitude)
        {
            int localLat = (int)((latitude - _coords.CornerCoords.LeftUpperCornerLat) * _pointsPerCell);
            int localLon = (int)((longitude - _coords.CornerCoords.LeftUpperCornerLon) * _pointsPerCell);
            int bytesPos = ((_pointsPerCell - localLat - 1) * _pointsPerCell * 2) + localLon * 2;

            if (bytesPos < 0 || bytesPos > _pointsPerCell * _pointsPerCell * 2)
                throw new ArgumentException("latitude or longitude out of range");

            return ElevationAtOffset(bytesPos);
        }

        public abstract long MemorySize { get; }
        public abstract Task<double> GetElevationAsync(double latitude, double longitude);
        protected abstract double ElevationAtOffset(int bytesPos);
    }
}
