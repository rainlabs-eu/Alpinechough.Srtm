using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal sealed class PgmDataLoaderFromRaw : PgmDataLoaderFromFileStreamBase
    {

        public PgmDataLoaderFromRaw([NotNull] IPgmPathResolver pathResolver) : base(pathResolver)
        {
        }

        protected override byte[] LoadAdfDataFromFile(string filePath)
        {
            using (var fileStream = File.Open(filePath,FileMode.Open,FileAccess.Read,FileShare.Read))
            {
                return LoadAdfDataFromStream(fileStream);
            }
        }

        protected override async Task<byte[]> LoadAdfDataFromFileAsync(string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return await LoadAdfDataFromStreamAsync(fileStream);
            }
        }
    }
}
