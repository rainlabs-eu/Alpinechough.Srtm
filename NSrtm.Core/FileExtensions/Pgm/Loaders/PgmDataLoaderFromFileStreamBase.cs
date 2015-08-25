using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal abstract class PgmDataLoaderFromFileStreamBase : IPgmDataLoader
    {
        private readonly IPgmPathResolver _pathResolver;

        protected PgmDataLoaderFromFileStreamBase([NotNull] IPgmPathResolver pathResolver)
        {
            _pathResolver = pathResolver;
        }

        [NotNull]
        public byte[] LoadFromFile()
        {
            var filePath = _pathResolver.FindFilePath();

            return LoadAdfDataFromFile(filePath);
        }

        public Task<byte[]> LoadFromFileAsync()
        {
            var filePath = _pathResolver.FindFilePath();

            return LoadAdfDataFromFileAsync(filePath);
        }

        protected abstract byte[] LoadAdfDataFromFile([NotNull] string filePath);
        protected abstract Task<byte[]> LoadAdfDataFromFileAsync([NotNull] string filePath);

        protected static byte[] LoadAdfDataFromStream(Stream zipStream)
        {
            byte[] adfData;
            using (var memory = new MemoryStream())
            {
                zipStream.CopyTo(memory);
                adfData = memory.ToArray();
            }
            return adfData;
        }

        protected static async Task<byte[]> LoadAdfDataFromStreamAsync(Stream zipStream)
        {
            byte[] adfData;
            using (var memory = new MemoryStream())
            {
                await zipStream.CopyToAsync(memory);
                adfData = memory.ToArray();
            }
            return adfData;
        }
    }
}
