using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal interface IAdfDataLoader
    {
        [NotNull]
        byte[] LoadFromFile(AdfCellCoords coords);

        Task<byte[]> LoadFromFileAsync(AdfCellCoords coords);
    }
}