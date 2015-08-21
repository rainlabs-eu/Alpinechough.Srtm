using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal class DataCellInvalid : IDataCell
    {
        private static readonly DataCellInvalid invalid = new DataCellInvalid();
        private static readonly Task<double> invalidElevationTask = Task.FromResult(Double.NaN);

        private DataCellInvalid()
        {
        }

        [NotNull] public static DataCellInvalid Invalid { get { return invalid; } }

        public double GetElevation(double latitude, double longitude)
        {
            return Double.NaN;
        }

        public long MemorySize { get { return 0; } }

        [NotNull]
        public Task<double> GetElevationAsync(double latitude, double longitude)
        {
            return invalidElevationTask;
        }
    }
}
