using System;

namespace NSrtm.Core
{
    static internal class AdfUtils
    {
        private const int egm1PointsPerCell = 2701;
        private const int egm25PointsPerCell = 1081;
        private const int egm1PointsCount = egm25PointsPerCell * egm25PointsPerCell * 2;
        private const int egm25PointsCount = egm1PointsPerCell * egm1PointsPerCell * 2;


        internal static int PointsPerCellFromDataLength(int length)
        {
            int pointsPerCell;
            switch (length)
            {
                case egm1PointsCount: // EGM 2008 2.5'
                    pointsPerCell = egm25PointsPerCell;
                    break;
                case egm25PointsCount: // EGM 2008 1'
                    pointsPerCell = egm1PointsPerCell;
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