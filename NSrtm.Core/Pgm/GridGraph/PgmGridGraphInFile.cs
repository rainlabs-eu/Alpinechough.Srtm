﻿using System;
using System.IO;
using JetBrains.Annotations;

namespace NSrtm.Core.Pgm.GridGraph
{
    public sealed class PgmGridGraphInFile : PgmGridGraphBase, IDisposable
    {
        private readonly FileStream _fileStream;
        private readonly Object _thisLock = new Object();

        public PgmGridGraphInFile([NotNull] FileStream stream, PgmDataDescription pgmParameters)
            : base(pgmParameters)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            _fileStream = stream;
        }

        protected override double GetUndulationFrom(int position)
        {
            lock (_thisLock)
            {
                var offset = 2 * position + Parameters.PreambleLength + 2;
                _fileStream.Seek(offset, SeekOrigin.Begin);
                UInt16 rawData = (UInt16)(_fileStream.ReadByte() << 8 | _fileStream.ReadByte());
                return rawData.ToEgmFormat(Parameters);
            }
        }

        public void Dispose()
        {
            _fileStream.Dispose();
        }
    }
}
