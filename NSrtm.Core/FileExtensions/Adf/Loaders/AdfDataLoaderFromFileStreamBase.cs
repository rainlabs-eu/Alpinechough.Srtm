using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal abstract class AdfDataLoaderFromFileStreamBase : IAdfDataLoader
    {
        private readonly IAdfPathResolver _pathResolver;

        protected AdfDataLoaderFromFileStreamBase([NotNull] IAdfPathResolver pathResolver)
        {
            _pathResolver = pathResolver;
        }

        [NotNull]
        public byte[] LoadFromFile(AdfCellCoords coords)
        {
            var filePath = _pathResolver.FindFilePath(coords);

            return LoadAdfDataFromFile(coords, filePath);
        }

        public Task<byte[]> LoadFromFileAsync(AdfCellCoords coords)
        {
            var filePath = _pathResolver.FindFilePath(coords);

            return LoadAdfDataFromFileAsync(coords, filePath);
        }

        protected abstract byte[] LoadAdfDataFromFile(AdfCellCoords coords, [NotNull] string filePath);
        protected abstract Task<byte[]> LoadAdfDataFromFileAsync(AdfCellCoords coords, [NotNull] string filePath);

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
