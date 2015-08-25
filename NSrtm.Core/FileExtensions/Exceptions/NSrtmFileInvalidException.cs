namespace NSrtm.Core
{
    internal class NSrtmFileInvalidException : NSrtmFileException
    {
        public NSrtmFileInvalidException(string reason)
            : base(string.Format("Invalid file ({0})", reason))
        {
        }
    }
}
