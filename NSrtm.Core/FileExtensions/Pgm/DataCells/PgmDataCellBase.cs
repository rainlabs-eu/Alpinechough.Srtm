using System;
using System.Threading.Tasks;
using NSrtm.Core.FileExtensions.Adf.Utils;

namespace NSrtm.Core.FileExtensions.Pgm.DataCells
{
    internal abstract class PgmDataCellBase : IDataCell
    {
        protected readonly PgmCellParameters PgmParameters;

        protected PgmDataCellBase(PgmCellParameters pgmParameters)
        {
            PgmParameters = pgmParameters;
        }

        protected abstract double ElevationAtOffset(int pointPos);

        public double GetElevation(double latitude, double longitude)
        {
            var lonStep = PgmParameters.CellWidthPoints / 360.0;
            var latStep = (PgmParameters.CellHightPoints -1 ) / 180.0;

            int localLat = (int)((PgmParameters.Orgin.LeftUpperCornerLat - latitude) * latStep);
            int localLon = (int)((longitude - PgmParameters.Orgin.LeftUpperCornerLon) * lonStep);
            int pointPos = (localLon+ localLat * (int)PgmParameters.CellWidthPoints);

            if (pointPos < 0 || pointPos > PgmParameters.CellWidthPoints * PgmParameters.CellHightPoints)
                throw new ArgumentException("latitude or longitude out of range");

            return ElevationAtOffset(pointPos);
        }

        public abstract long MemorySize { get; }
        public abstract Task<double> GetElevationAsync(double latitude, double longitude);
    }
}
