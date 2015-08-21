using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal sealed class AdfDataLoaderFromRaw : AdfDataLoaderFromFileStreamBase
    {

        public AdfDataLoaderFromRaw([NotNull] IAdfPathResolver pathResolver) : base(pathResolver)
        {
        }

        protected override byte[] LoadAdfDataFromFile(AdfCellCoords coords, string filePath)
        {
            using (var fileStream = File.Open(filePath,FileMode.Open,FileAccess.Read,FileShare.Read))
            {
                return LoadAdfDataFromStream(fileStream);
            }
        }

        protected override async Task<byte[]> LoadAdfDataFromFileAsync(AdfCellCoords coords, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return await LoadAdfDataFromStreamAsync(fileStream);
            }
        }
    }
}
