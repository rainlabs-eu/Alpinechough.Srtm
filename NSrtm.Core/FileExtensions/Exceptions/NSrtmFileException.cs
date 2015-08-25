using System;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal class NSrtmFileException : Exception
    {

        protected NSrtmFileException([NotNull] string message)
            : base(message)
        {
        }

        public NSrtmFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
