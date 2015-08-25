using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NSrtm.Core.FileExtensions.Pgm.DataCells;

namespace NSrtm.Core.FileExtensions.Pgm
{
    /// <summary>
    ///     Provides elevation from SRTM3 and SRTM1 format files (PGM or PGM.ZIP) downloaded from:
    ///     http://dds.cr.usgs.gov/srtm/version2_1/
    /// </summary>
    public class PgmElevationProvider : IElevationProvider
    {
        private readonly IPgmDataCellFactory _cellFactory;
        private IDataCell _cell;

        internal PgmElevationProvider([NotNull] IPgmDataCellFactory cellFactory)
        {
            if (cellFactory == null) throw new ArgumentNullException("cellFactory");
            _cellFactory = cellFactory;
            Name = "Unknown";
            Description = "Unknown";
        }

        private async Task<IDataCell> builCellAsync()
        {
            try
            {
                return await _cellFactory.GetCellAsync();
            }
            catch (NSrtmFileException)
            {
                return DataCellInvalid.Invalid;
            }
        }

        [NotNull]
        private IDataCell buildCell()
        {
            try
            {
                return _cellFactory.GetCell();
            }
            catch (NSrtmFileException)
            {
                return DataCellInvalid.Invalid;
            }
        }

        /// <summary>
        ///     Short name describing implementation. Used for UI/Demos where different implementations are available.
        /// </summary>
        [NotNull]
        public string Name { get; set; }

        /// <summary>
        ///     More descriptive info about implementation. Used for UI/Demos where different implementations are available.
        /// </summary>
        [NotNull]
        public string Description { get; set; }

        /// <summary>
        ///     Gets elevation above MSL
        /// </summary>
        /// <param name="latitude">Latitude in degrees in WGS84 datum</param>
        /// <param name="longitude">Longitude in degrees in WGS84 datum</param>
        /// <returns></returns>
        public double GetElevation(double latitude, double longitude)
        {
            if (_cell == null)
            {
                _cell = buildCell();
            }
            return _cell.GetElevation(latitude, longitude);
        }

        /// <summary>
        ///     Gets elevation above MSL
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public async Task<double> GetElevationAsync(double latitude, double longitude)
        {
            if (_cell == null)
            {
                _cell = await builCellAsync();
            }
            return await _cell.GetElevationAsync(latitude, longitude);
        }

        #region Static Members

        /// <summary>
        ///     Creates elevation provider which loads PGM files to memory on demand.
        /// </summary>
        /// <remarks>Files are loaded and cached forever, so for "whole world coverage" it can use up to 16GB of RAM.</remarks>
        /// <param name="directory"></param>
        /// <returns>Created provider.</returns>
        [NotNull]
        public static IElevationProvider CreateInMemoryFromRawFiles([NotNull] string directory)
        {
            IPgmPathResolver pathResolver = new PgmPathResolverRaw(directory);
            IPgmDataLoader loader = new PgmDataLoaderFromRaw(pathResolver);
            return new PgmElevationProvider(new PgmDataCellInMemoryFactory(loader))
                   {
                       Name = "EGM files",
                       Description = string.Format("Unpacked EGM files (PGM) from directory {0}", directory)
                   };
        }

        /// <summary>
        ///     Creates elevation provider which loads PGM.ZIP files to memory on demand.
        /// </summary>
        /// <remarks>Files are loaded and cached forever, so for "whole world coverage" it can use up to 16GB of RAM.</remarks>
        /// <param name="directory"></param>
        /// <returns>Created provider.</returns>
        [NotNull]
        public static IElevationProvider CreateInMemoryFromZipFiles([NotNull] string directory)
        {
            IPgmPathResolver pathResolver = new PgmPathResolverZip(directory);
            IPgmDataLoader loader = new PgmDataLoaderFromZip(pathResolver);
            return new PgmElevationProvider(new PgmDataCellInMemoryFactory(loader))
                   {
                       Name = "EGM 2008 files",
                       Description = string.Format("ZIP packed EGM 2008 files (PGM.ZIP) from directory {0}", directory)
                   };
        }

        /// <summary>
        ///     Creates elevation provider which reads PGM files from disk.
        /// </summary>
        /// <remarks>It is 1000 - 10000 times slower than in memory implementations, but it uses almost no RAM.</remarks>
        /// <param name="directory"></param>
        /// <returns>Created provider.</returns>
        [NotNull]
        public static IElevationProvider CreateDirectDiskAccessFromRawFiles([NotNull] string directory)
        {
            IPgmPathResolver pathResolver = new PgmPathResolverRaw(directory);
            return new PgmElevationProvider(new PgmDataCellInFileFactory(pathResolver))
                   {
                       Name = "SRTM files",
                       Description = string.Format("Memory mapped SRTM files (PGM) from directory {0}", directory)
                   };
        }

        #endregion
    }
}
