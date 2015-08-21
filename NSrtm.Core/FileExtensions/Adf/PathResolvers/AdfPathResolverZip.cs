using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal class AdfPathResolverZip : AdfPathResolverCaching
    {
        public AdfPathResolverZip([NotNull] string directory)
            : base(directory)
        {
        }

        protected override string coordsToFilename(AdfCellCoords coords)
        {
            var baseName = coords.ToBaseName();
            return baseName + ".zip" + "\\" + baseName + "\\" + baseName + "\\" + "w001001.adf";
        }
    }
}
