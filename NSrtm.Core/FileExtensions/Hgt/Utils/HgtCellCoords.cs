using System;
using JetBrains.Annotations;
using NSrtm.Core.Core.Utils;

namespace NSrtm.Core
{
    internal struct HgtCellCoords
    {
        private readonly CellCoords _cornerCoords;

        private HgtCellCoords(int lat, int lon)
        {
            _cornerCoords = new CellCoords(lat, lon);
        }

        public CellCoords CornerCoords { get { return _cornerCoords; } }

        #region Static Members

        public static HgtCellCoords ForLatLon(double latitude, double longitude)
        {
            return new HgtCellCoords((int)Math.Floor(latitude), (int)Math.Floor(longitude));
        }

        #endregion
    }
}