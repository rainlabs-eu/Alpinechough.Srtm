using System;

namespace NSrtm.Core
{
    static internal class AdfUtils
    {
        private const int egm1CellWidthPoints = 21600;
        private const int egm25CellWidthPoints = 8640;
        private const int egm1CellHightPoints = 10801;
        private const int egm25CellHightPoints = 4321;
        private const int egm1PointsCount = egm1CellWidthPoints * egm1CellHightPoints;
        private const int egm25PointsCount = egm25CellWidthPoints * egm25CellHightPoints;


        internal static int PointsPerCellFromDataLength(int length)
        {
            int pointsPerCell;
            switch (length)
            {
                case egm1PointsCount: // EGM 2008 2.5'
                    pointsPerCell = egm1CellHightPoints;
                    break;
                case egm25PointsCount: // EGM 2008 1'
                    pointsPerCell = egm25CellHightPoints;
                    break;
                default:
                    throw new ArgumentException(String.Format("Unsupported data length {0}", length), "length");
            }
            return pointsPerCell;
        }

        internal static bool IsDataLengthValid(long length)
        {
            return length == egm1PointsCount || length == egm25PointsCount;
        }

        public static bool IsDataLengthValid(int length)
        {
            return IsDataLengthValid((long)length);
        }
    }
}