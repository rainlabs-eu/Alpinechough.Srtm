using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal abstract class AdfPathResolverCaching : IAdfPathResolver
    {
        private readonly ConcurrentDictionary<AdfCellCoords, string> _cache = new ConcurrentDictionary<AdfCellCoords, string>();
        private readonly string _directory;

        protected AdfPathResolverCaching(string directory)
        {
            _directory = directory;
        }

        public string FindFilePath(AdfCellCoords coords)
        {
            return _cache.GetOrAdd(coords, findPathForFile);
        }

        [NotNull]
        private string findPathForFile(AdfCellCoords coords)
        {
            string filename = coordsToFilename(coords);
            var path = Path.Combine(_directory, filename);
            if (File.Exists(path)) return path;

            var foundfile = new DirectoryInfo(_directory).EnumerateFiles(filename, SearchOption.AllDirectories)
                                                        .FirstOrDefault();
            if (foundfile != null) return foundfile.FullName;
            else throw new FileNotFoundException(coords);
        }

        [NotNull]
        protected abstract string coordsToFilename(AdfCellCoords coords);
    }
}
