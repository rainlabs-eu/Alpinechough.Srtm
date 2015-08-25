using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core.FileExtensions.Pgm.DataCells
{
    internal interface IPgmDataCellFactory
    {
        [NotNull]
        IDataCell GetCell();

        [NotNull]
        Task<IDataCell> GetCellAsync();
    }
}
