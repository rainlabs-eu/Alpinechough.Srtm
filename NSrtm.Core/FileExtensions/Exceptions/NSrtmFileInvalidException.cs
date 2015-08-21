using NSrtm.Core.Core.Utils;

namespace NSrtm.Core
{
    internal class NSrtmFileInvalidException : NSrtmFileException
    {
        public NSrtmFileInvalidException(CellCoords coords, string reason)
            : base(coords, string.Format("Invalid file ({2}) for coordinates [{0}, {1}]", coords.LeftUpperCornerLat, coords.LeftUpperCornerLon, reason))
        {
        }
    }
}