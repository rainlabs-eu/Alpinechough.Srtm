using System;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal class NSrtmFileException : Exception
    {
        private readonly ICellCoords _coords;

        protected NSrtmFileException(ICellCoords coords, [NotNull] string message)
            : base(message)
        {
            _coords = coords;
        }

        public NSrtmFileException(ICellCoords coords, string message, Exception innerException)
            : base(message, innerException)
        {
            _coords = coords;
        }

        public ICellCoords Coords { get { return _coords; } }
    }
}
