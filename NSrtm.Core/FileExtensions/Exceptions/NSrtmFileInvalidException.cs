namespace NSrtm.Core
{
    internal class NSrtmFileInvalidException : NSrtmFileException
    {
        public NSrtmFileInvalidException(ICellCoords coords, string reason)
            : base(coords, string.Format("Invalid file ({1}) for coordinates [{0}]", coords.ToBaseName(), reason))
        {
        }
    }
}
