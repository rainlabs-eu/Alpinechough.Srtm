using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal class AdfPathResolverRaw : AdfPathResolverCaching
    {
        public AdfPathResolverRaw([NotNull] string directory) : base(directory)
        {
        }

        protected override string coordsToFilename(AdfCellCoords coords)
        {
            var baseName = coords.ToBaseName();
            return baseName + baseName + baseName + "w001001.adf";
        }
    }
}