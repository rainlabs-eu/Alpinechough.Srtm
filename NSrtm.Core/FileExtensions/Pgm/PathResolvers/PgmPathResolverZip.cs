using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal class PgmPathResolverZip : PgmPathResolverCaching
    {
        public PgmPathResolverZip([NotNull] string directory)
            : base(directory)
        {
        }

        protected override string coordsToFilename()
        {
            return "Und_min2.5x2.5_egm2008_isw = 82_WGS84_TideFree_SE.gz";
        }
    }
}
