using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal sealed class PgmDataLoaderFromZip : PgmDataLoaderFromFileStreamBase
    {
        public PgmDataLoaderFromZip([NotNull] IPgmPathResolver pathResolver) : base(pathResolver)
        {
        }

        protected override byte[] LoadAdfDataFromFile(string filePath)
        {
            using (var zipArchive = ZipFile.OpenRead(filePath))
            {
                var entry = zipArchive.Entries.Single();
                    using (var zipStream = entry.Open())
                {
                    return LoadAdfDataFromStream(zipStream);
                }
            }
        }

        protected override async Task<byte[]> LoadAdfDataFromFileAsync(string filePath)
        {
            using (var zipArchive = ZipFile.OpenRead(filePath))
            {
                var entry = zipArchive.Entries.Single();
                using (var zipStream = entry.Open())
                {
                    return await LoadAdfDataFromStreamAsync(zipStream);
                }
            }
        }
    }
}
