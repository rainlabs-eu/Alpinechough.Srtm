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

        protected abstract double ElevationAtOffset(int bytesPos);

        public double GetElevation(double latitude, double longitude)
        {
            int localLat = (int)((_coords.LeftUpperCornerLat - latitude) / 180.0 * _pointsPerCell);
            int localLon = (int)((_coords.LeftUpperCornerLon - longitude) / 360.0 * (_pointsPerCell - 1) * 2);
            int bytesPos = (localLat * (_pointsPerCell - 1) * 2) + localLon;

            if (bytesPos < 0 || bytesPos > _pointsPerCell * (_pointsPerCell-1) * 2)
                throw new ArgumentException("latitude or longitude out of range");

            return ElevationAtOffset(bytesPos);
        }

        public abstract long MemorySize { get; }
        public abstract Task<double> GetElevationAsync(double latitude, double longitude);
    }
}
