using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core.Core.Utils
{
    public abstract class CellCoords : ICellCoords
    {
        private readonly int _cornerLat;
        private readonly int _cornerLon;

        private CellCoords(int cornerLat, int cornerLon)
        {
            _cornerLat = cornerLat;
            _cornerLon = cornerLon;
        }

        public int LeftUpperCornerLat { get { return _cornerLat; } }
        public int LeftUpperCornerLon { get { return _cornerLon; } }

        public bool Equals(CellCoords other)
        {
            return _cornerLat == other._cornerLat && _cornerLon == other._cornerLon;
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return obj is CellCoords && Equals((CellCoords)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_cornerLat * 397) ^ _cornerLon;
            }
        }

        public static bool operator ==(CellCoords left, CellCoords right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CellCoords left, CellCoords right)
        {
            return !left.Equals(right);
        }

        public abstract ICellCoords ForLatLon(double latitude, double longitude);

        public string ToBaseName()
        {
            return String.Format("{0}{1:D2}{2}{3:D3}",
                                 _cornerLat < 0 ? "S" : "N",
                                 Math.Abs(_cornerLat),
                                 _cornerLon < 0 ? "W" : "E",
                                 Math.Abs(_cornerLon));
        }
    }
}
