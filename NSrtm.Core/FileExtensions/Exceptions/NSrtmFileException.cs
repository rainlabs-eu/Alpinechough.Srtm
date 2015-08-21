using System;
using JetBrains.Annotations;
using NSrtm.Core.Core.Utils;

namespace NSrtm.Core
{
    internal class NSrtmFileException : Exception
    {
        private readonly CellCoords _coords;

        protected NSrtmFileException(CellCoords coords, [NotNull] string message)
            : base(message)
        {
            _coords = coords;
        }

        public NSrtmFileException(CellCoords coords, string message, Exception innerException)
            : base(message, innerException)
        {
            _coords = coords;
        }

        public CellCoords Coords { get { return _coords; } }
    }
}
