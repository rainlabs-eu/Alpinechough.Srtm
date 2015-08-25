using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal interface IPgmPathResolver
    {
        [NotNull]
        string FindFilePath();
    }
}
