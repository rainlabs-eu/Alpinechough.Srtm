using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal class AdfPathResolverZip : AdfPathResolverCaching
    {
        public AdfPathResolverZip([NotNull] string directory) : base(directory)
        {
        }

        protected override string coordsToFilename(AdfCellCoords coords)
        {
            return coords.CornerCoords.ToBaseName() + ".adf.zip";
        }
    }
}