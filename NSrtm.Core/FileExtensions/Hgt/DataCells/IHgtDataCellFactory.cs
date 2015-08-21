using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal interface IHgtDataCellFactory
    {
        [NotNull]
        IDataCell GetCellFor(HgtCellCoords coords);

        [NotNull]
        Task<IDataCell> GetCellForAsync(HgtCellCoords coords);
    }
}
