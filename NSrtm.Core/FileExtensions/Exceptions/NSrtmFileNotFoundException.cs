using NSrtm.Core.Core.Utils;

namespace NSrtm.Core
{
    internal class NSrtmFileNotFoundException : NSrtmFileException
    {
        public NSrtmFileNotFoundException(CellCoords coords)
            : base(coords, string.Format("Cannot find file for coordinates [{0}, {1}]", coords.LeftUpperCornerLat, coords.LeftUpperCornerLon))
        {
        }
    }
}
