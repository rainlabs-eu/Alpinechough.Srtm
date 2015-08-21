namespace NSrtm.Core
{
    internal class NSrtmFileNotFoundException : NSrtmFileException
    {
        public NSrtmFileNotFoundException(ICellCoords coords)
            : base(coords, string.Format("Cannot find file for coordinates [{0}]", coords.ToBaseName()))
        {
        }
    }
}
