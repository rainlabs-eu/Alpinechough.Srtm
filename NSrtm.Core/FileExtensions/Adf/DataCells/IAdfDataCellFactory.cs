using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal interface IAdfDataCellFactory
    {
        [NotNull]
        IDataCell GetCellFor(AdfCellCoords coords);

        [NotNull]
        Task<IDataCell> GetCellForAsync(AdfCellCoords coords);
    }
}
