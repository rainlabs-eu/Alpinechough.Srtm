using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NSrtm.Core
{
    /// <summary>
    ///     Provides elevation from SRTM3 and SRTM1 format files (Adf or Adf.ZIP) downloaded from:
    ///     http://dds.cr.usgs.gov/srtm/version2_1/
    /// </summary>
    public class AdfElevationProvider : IElevationProvider
    {
        private readonly IAdfDataCellFactory _cellFactory;
        private readonly ConcurrentDictionary<AdfCellCoords, IDataCell> _cache = new ConcurrentDictionary<AdfCellCoords, IDataCell>();

        internal AdfElevationProvider([NotNull] IAdfDataCellFactory cellFactory)
        {
            if (cellFactory == null) throw new ArgumentNullException("cellFactory");
            _cellFactory = cellFactory;
            Name = "Unknown";
            Description = "Unknown";
        }

        /// <summary>
        ///     Short name describing implementation. Used for UI/Demos where different implementations are available.
        /// </summary>
        [NotNull] public string Name { get; set; }

        /// <summary>
        ///     More descriptive info about implementation. Used for UI/Demos where different implementations are available.
        /// </summary>
        [NotNull] public string Description { get; set; }

        /// <summary>
        ///     Gets elevation above MSL
        /// </summary>
        /// <param name="latitude">Latitude in degrees in WGS84 datum</param>
        /// <param name="longitude">Longitude in degrees in WGS84 datum</param>
        /// <returns></returns>
        public double GetElevation(double latitude, double longitude)
        {
            var coords = AdfCellCoords.ForLatLon(latitude, longitude);

            var cell = _cache.GetOrAdd(coords, buildCellFor(coords));

            return cell.GetElevation(latitude, longitude);
        }

        /// <summary>
        ///     Gets elevation above MSL
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public Task<double> GetElevationAsync(double latitude, double longitude)
        {
            var coords = AdfCellCoords.ForLatLon(latitude, longitude);
            IDataCell cellFromCache;
            if (_cache.TryGetValue(coords, out cellFromCache))
            {
                cellFromCache.GetElevationAsync(latitude, longitude);
            }

            return buildAndCacheCellAndReturnElevationAsync(coords, latitude, longitude);
        }

        private async Task<double> buildAndCacheCellAndReturnElevationAsync(AdfCellCoords coords, double latitude, double longitude)
        {
            IDataCell ret;
            try
            {
                ret = await _cellFactory.GetCellForAsync(coords);
            }
            catch (NSrtmFileException)
            {
                ret = DataCellInvalid.Invalid;
            }

            var cell = _cache.GetOrAdd(coords, ret);

            return await cell.GetElevationAsync(latitude, longitude);
        }

        [NotNull]
        private IDataCell buildCellFor(AdfCellCoords coords)
        {
            try
            {
                return _cellFactory.GetCellFor(coords);
            }
            catch (NSrtmFileException)
            {
                return DataCellInvalid.Invalid;
            }
        }

        /// <summary>
        ///     Creates elevation provider which loads Adf files to memory on demand.
        /// </summary>
        /// <remarks>Files are loaded and cached forever, so for "whole world coverage" it can use up to 16GB of RAM.</remarks>
        /// <param name="directory"></param>
        /// <returns>Created provider.</returns>
        [NotNull]
        public static IElevationProvider CreateInMemoryFromRawFiles([NotNull] string directory)
        {
            IAdfPathResolver pathResolver = new AdfPathResolverRaw(directory);
            IAdfDataLoader loader = new AdfDataLoaderFromRaw(pathResolver);
            return new AdfElevationProvider(new AdfDataCellInMemoryFactory(loader))
                   {
                       Name = "EGM files",
                       Description = string.Format("Unpacked EGM files (Adf) from directory {0}", directory)
                   };
        }

        /// <summary>
        ///     Creates elevation provider which loads Adf.ZIP files to memory on demand.
        /// </summary>
        /// <remarks>Files are loaded and cached forever, so for "whole world coverage" it can use up to 16GB of RAM.</remarks>
        /// <param name="directory"></param>
        /// <returns>Created provider.</returns>
        [NotNull]
        public static IElevationProvider CreateInMemoryFromZipFiles([NotNull] string directory)
        {
            IAdfPathResolver pathResolver = new AdfPathResolverZip(directory);
            IAdfDataLoader loader = new AdfDataLoaderFromZip(pathResolver);
            return new AdfElevationProvider(new AdfDataCellInMemoryFactory(loader))
                   {
                       Name = "EGM 2008 files",
                       Description = string.Format("ZIP packed EGM 2008 files (Adf.ZIP) from directory {0}", directory)
                   };
        }

        /// <summary>
        ///     Creates elevation provider which reads Adf files from disk.
        /// </summary>
        /// <remarks>It is 1000 - 10000 times slower than in memory implementations, but it uses almost no RAM.</remarks>
        /// <param name="directory"></param>
        /// <returns>Created provider.</returns>
        [NotNull]
        public static IElevationProvider CreateDirectDiskAccessFromRawFiles([NotNull] string directory)
        {
            IAdfPathResolver pathResolver = new AdfPathResolverRaw(directory);
            return new AdfElevationProvider(new AdfDataCellInFileFactory(pathResolver))
                   {
                       Name = "SRTM files",
                       Description = string.Format("Memory mapped SRTM files (Adf) from directory {0}", directory)
                   };
        }
    }
}
