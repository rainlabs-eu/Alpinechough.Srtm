using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal interface IPgmDataLoader
    {
        [NotNull]
        byte[] LoadFromFile();

        Task<byte[]> LoadFromFileAsync();
    }
}