using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NSrtm.Core.FileExtensions.Adf.Utils;

namespace NSrtm.Core.FileExtensions.Pgm.DataCells
{
    internal class PgmDataCellInFileFactory : IPgmDataCellFactory
    {
        private readonly IPgmPathResolver _pathResolver;

        public PgmDataCellInFileFactory([NotNull] IPgmPathResolver pathResolver)
        {
            _pathResolver = pathResolver;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",Justification = "Returned cell is disposable")]
        public IDataCell GetCell()
        {
            var path = _pathResolver.FindFilePath();

            FileStream file = null;
            try
            {
                file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var cellParameters = PgmCellParser.GetParametersFrom(file);
                return new PgmDataCellInFile(file, cellParameters);
            }
            catch (Exception)
            {
                if (file != null) file.Dispose();
                throw;
            }
        }

        public Task<IDataCell> GetCellAsync() //TODO GetPArametersAsync
        {
            return Task.FromResult(GetCell());
        }

        public sealed class PgmDataCellInFile : PgmDataCellBase, IDisposable
        {
            private readonly FileStream _file;
            private readonly object _lock = new object();

            internal PgmDataCellInFile([NotNull] FileStream file, PgmCellParameters fileSize) : base(fileSize)
            {
                _file = file;
            }

            public override long MemorySize { get { return 0; } }

            public void Dispose()
            {
                _file.Dispose();
            }

            public override Task<double> GetElevationAsync(double latitude, double longitude)
            {
                return Task.FromResult(GetElevation(latitude, longitude));
            }

            protected override double ElevationAtOffset(int pointPos)
            {
                lock (_lock)
                {
                    _file.Seek(pointPos + PgmParameters.SkippedBytes, SeekOrigin.Begin);
                    UInt16 rawElevation = (UInt16)(_file.ReadByte() << 8 | _file.ReadByte());
                    if (rawElevation > PgmParameters.MaxValue)
                    {
                        throw new ArgumentOutOfRangeException(rawElevation.ToString());
                    }
                    return rawElevation * PgmParameters.Scale + PgmParameters.Offset;
                }
            }
        }
    }
}
