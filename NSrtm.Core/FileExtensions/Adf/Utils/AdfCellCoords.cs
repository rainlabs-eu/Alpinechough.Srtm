using System;
using JetBrains.Annotations;
using NSrtm.Core.Core.Utils;

namespace NSrtm.Core
{
    internal struct AdfCellCoords
    {
        private readonly CellCoords _cornerCoords;

        private AdfCellCoords(int lat, int lon)
        {
            _cornerCoords = new CellCoords(lat, lon);
        }

        public CellCoords CornerCoords { get { return _cornerCoords; } }

        #region Static Members

        public static AdfCellCoords ForLatLon(double latitude, double longitude)
        {
            return new AdfCellCoords((int)Math.Floor(latitude / 45.0) * 45, (int)Math.Floor(longitude / 45.0) * 45);
        }

        #endregion
    }
}
