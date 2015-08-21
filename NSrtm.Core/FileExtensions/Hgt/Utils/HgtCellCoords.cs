using System;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal struct HgtCellCoords : IEquatable<HgtCellCoords>, ICellCoords
    {
        private readonly int _leftDownCornerLat;
        private readonly int _leftDownCornerLon;

        private HgtCellCoords(int leftDownCornerLat, int leftDownCornerLon)
        {
            _leftDownCornerLat = leftDownCornerLat;
            _leftDownCornerLon = leftDownCornerLon;
        }

        public int LeftDownCornerLat { get { return _leftDownCornerLat; } }
        public int LeftDownCornerLon { get { return _leftDownCornerLon; } }

        public bool Equals(HgtCellCoords other)
        {
            return _leftDownCornerLat == other._leftDownCornerLat && _leftDownCornerLon == other._leftDownCornerLon;
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return obj is HgtCellCoords && Equals((HgtCellCoords)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_leftDownCornerLat * 397) ^ _leftDownCornerLon;
            }
        }

        public static bool operator ==(HgtCellCoords left, HgtCellCoords right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HgtCellCoords left, HgtCellCoords right)
        {
            return !left.Equals(right);
        }

        public static HgtCellCoords ForLatLon(double latitude, double longitude)
        {
            return new HgtCellCoords((int)Math.Floor(latitude), (int)Math.Floor(longitude));
        }

        public string ToBaseName()
        {
            return String.Format("{0}{1:D2}{2}{3:D3}",
                                 _leftDownCornerLat < 0 ? "S" : "N",
                                 Math.Abs(_leftDownCornerLat),
                                 _leftDownCornerLon < 0 ? "W" : "E",
                                 Math.Abs(_leftDownCornerLon));
        }
    }
}