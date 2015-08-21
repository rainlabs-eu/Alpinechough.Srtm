using JetBrains.Annotations;

namespace NSrtm.Core.Core.Utils
{
    public interface ICellCoords
    {
        int LeftUpperCornerLat { get; }
        int LeftUpperCornerLon { get; }
        ICellCoords ForLatLon(double latitude, double longitude);
        string ToBaseName();
    }
}
