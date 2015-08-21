using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal sealed class AdfDataLoaderFromZip : AdfDataLoaderFromFileStreamBase
    {
        public AdfDataLoaderFromZip([NotNull] IAdfPathResolver pathResolver) : base(pathResolver)
        {
        }

        protected override byte[] LoadAdfDataFromFile(AdfCellCoords coords, string filePath)
        {
            using (var zipArchive = ZipFile.OpenRead(filePath))
            {
                var entry = zipArchive.Entries.Single();

                long length = entry.Length;
                if (!AdfUtils.IsDataLengthValid(length))
                    throw new NSrtmFileInvalidException(coords, string.Format("Invalid length - {0} bytes", length));

                using (var zipStream = entry.Open())
                {
                    return LoadAdfDataFromStream(zipStream);
                }
            }
        }

        protected override async Task<byte[]> LoadAdfDataFromFileAsync(AdfCellCoords coords, string filePath)
        {
            using (var zipArchive = ZipFile.OpenRead(filePath))
            {
                var entry = zipArchive.Entries.Single();

                long length = entry.Length;
                if (!HgtUtils.IsDataLengthValid(length))
                    throw new NSrtmFileInvalidException(coords, string.Format("Invalid length - {0} bytes", length));

                using (var zipStream = entry.Open())
                {
                    return await LoadAdfDataFromStreamAsync(zipStream);
                }
            }
        }
    }
}
