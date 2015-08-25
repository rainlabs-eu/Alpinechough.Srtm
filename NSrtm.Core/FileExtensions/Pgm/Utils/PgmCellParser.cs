using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NSrtm.Core.FileExtensions.Adf.Utils
{

    public static class PgmCellParser
    {
        public static PgmCellParameters GetParametersFrom(byte[] data)
        {
         return new PgmCellParameters( PgmCellCoords.ForLatLon(90,0), -108, 8640, 4321, 0.003, 65535, 404 );
        }

        public static byte[] GetElevationFrom(byte[] data)
        {
          return data.Skip(404).ToArray();
        }

        public static Task<PgmCellParameters> GetParametersFromAsync(byte[] data)
        {
            return Task.FromResult(GetParametersFrom(data));
        }

        public static Task<byte[]> GetElevationFromAsync(byte[] data)
        {
            return Task.FromResult(GetElevationFrom(data));
        }

        internal static PgmCellParameters GetParametersFrom(FileStream file)
        {
            byte[] result = new byte[404];
            file.Read(result, 0, 404);
            return GetParametersFrom(result);
        }
    }
}
