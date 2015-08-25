using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSrtm.Core.FileExtensions.Adf.Utils
{
    public struct PgmCellParameters
    {
        private readonly PgmCellCoords _orgin;
        private readonly double _offset;
        private readonly uint _cellWidthPoints;
        private readonly uint _cellHightPoints;
        private readonly double _scale;
        private readonly uint _maxValue;
        private readonly uint _skippedBytes;

        [CLSCompliantAttribute(false)]
        public PgmCellParameters(PgmCellCoords orgin, double offset, uint cellWidthPoints, uint cellHightPoints, double scale, uint maxValue, uint skippedBytes)
        {
            _orgin = orgin;
            _offset = offset;
            _cellWidthPoints = cellWidthPoints;
            _cellHightPoints = cellHightPoints;
            _scale = scale;
            _maxValue = maxValue;
            _skippedBytes = skippedBytes;
        }

        public double Scale { get { return _scale; } }

        [CLSCompliantAttribute(false)]
        public uint MaxValue { get { return _maxValue; } }

        [CLSCompliantAttribute(false)]
        public uint CellHightPoints { get { return _cellHightPoints; } }

        [CLSCompliantAttribute(false)]
        public uint CellWidthPoints { get { return _cellWidthPoints; } }

        public double Offset { get { return _offset; } }

        public PgmCellCoords Orgin { get { return _orgin; } }

        [CLSCompliantAttribute(false)]
        public uint SkippedBytes { get { return _skippedBytes; } }
    }
}
