using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal interface IAdfPathResolver
    {
        [NotNull]
        string FindFilePath(AdfCellCoords coords);
    }
}
