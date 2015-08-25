using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal abstract class PgmPathResolverCaching : IPgmPathResolver
    {
        private readonly string _directory;

        protected PgmPathResolverCaching(string directory)
        {
            _directory = directory;
        }

        [NotNull]
        public string FindFilePath()
        {
            string filename = coordsToFilename();
            var path = Path.Combine(_directory, filename);
            if (File.Exists(path)) return path;

            var foundfile = new DirectoryInfo(_directory).EnumerateFiles(filename, SearchOption.AllDirectories)
                                                        .FirstOrDefault();
            if (foundfile != null) return foundfile.FullName;
            else throw new FileNotFoundException();
        }

        [NotNull]
        protected abstract string coordsToFilename();
    }
}
