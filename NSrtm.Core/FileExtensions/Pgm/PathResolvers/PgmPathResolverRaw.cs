using System.IO;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal class PgmPathResolverRaw : PgmPathResolverCaching
    {
        public PgmPathResolverRaw([NotNull] string directory) : base(directory)
        {
        }

        protected override string coordsToFilename()
        {
            return Path.Combine("geoids","egm2008-2_5.pgm");
        }
    }
}