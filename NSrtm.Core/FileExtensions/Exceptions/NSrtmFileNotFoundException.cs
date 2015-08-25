namespace NSrtm.Core
{
    internal class NSrtmFileNotFoundException : NSrtmFileException
    {
        public NSrtmFileNotFoundException()
            : base("Cannot find file")
        {
        }
    }
}
