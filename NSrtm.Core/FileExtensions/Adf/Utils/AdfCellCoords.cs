using System;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    internal struct AdfCellCoords : IEquatable<AdfCellCoords>, ICellCoords
    {
        private readonly int _leftUpperCornerLat;
        private readonly int _leftUpperCornerLon;

        private AdfCellCoords(int leftUpperCornerLat, int leftUpperCornerLon)
        {
            _leftUpperCornerLat = leftUpperCornerLat;
            _leftUpperCornerLon = leftUpperCornerLon;
        }

        public int LeftUpperCornerLat { get { return _leftUpperCornerLat; } }
        public int LeftUpperCornerLon { get { return _leftUpperCornerLon; } }

        public bool Equals(AdfCellCoords other)
        {
            return _leftUpperCornerLat == other._leftUpperCornerLat && _leftUpperCornerLon == other._leftUpperCornerLon;
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return obj is AdfCellCoords && Equals((AdfCellCoords)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_leftUpperCornerLat * 397) ^ _leftUpperCornerLon;
            }
        }

        public static bool operator ==(AdfCellCoords left, AdfCellCoords right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AdfCellCoords left, AdfCellCoords right)
        {
            return !left.Equals(right);
        }

        public static AdfCellCoords ForLatLon(double latitude, double longitude)
        {
            return new AdfCellCoords((int)Math.Floor(latitude), (int)Math.Floor(longitude));
        }

        public string ToBaseName()
        {
            return String.Format("{0}{1:D2}{2}{3:D2}",
                                 _leftUpperCornerLat < 0 ? "s" : "n",
                                 Math.Abs(_leftUpperCornerLat),
                                 _leftUpperCornerLon < 0 ? "w" : "e",
                                 Math.Abs(_leftUpperCornerLon));
        }
    }
}