using System;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    public struct PgmCellCoords : IEquatable<PgmCellCoords>, ICellCoords
    {
        private readonly double _leftUpperCornerLat;
        private readonly double _leftUpperCornerLon;

        private PgmCellCoords(double leftUpperCornerLat, double leftUpperCornerLon)
        {
            _leftUpperCornerLat = leftUpperCornerLat;
            _leftUpperCornerLon = leftUpperCornerLon;
        }

        public double LeftUpperCornerLat { get { return _leftUpperCornerLat; } }
        public double LeftUpperCornerLon { get { return _leftUpperCornerLon; } }

        public bool Equals(PgmCellCoords other)
        {
            return _leftUpperCornerLat == other._leftUpperCornerLat && _leftUpperCornerLon == other._leftUpperCornerLon;
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return obj is PgmCellCoords && Equals((PgmCellCoords)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)_leftUpperCornerLat * 397) ^ (int)_leftUpperCornerLon;
            }
        }

        public static bool operator ==(PgmCellCoords left, PgmCellCoords right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PgmCellCoords left, PgmCellCoords right)
        {
            return !left.Equals(right);
        }

        public static PgmCellCoords ForLatLon(double latitude, double longitude)
        {
            return new PgmCellCoords(latitude, longitude);
        }

        public string ToBaseName()
        {
            return String.Format("{0}{1:D2}{2}{3:D2}",
                                 _leftUpperCornerLat < 0 ? "S" : "N",
                                 Math.Abs(_leftUpperCornerLat),
                                 _leftUpperCornerLon < 0 ? "W" : "E",
                                 Math.Abs(_leftUpperCornerLon));
        }
    }
}