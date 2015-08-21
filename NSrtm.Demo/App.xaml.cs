using System;
using System.Windows;
using JetBrains.Annotations;
using NSrtm.Core;

namespace NSrtm.Demo
{
    internal partial class App
    {
        protected override void OnStartup([NotNull] StartupEventArgs e)
        {
            if (e == null) throw new ArgumentNullException("e");

            base.OnStartup(e);
            var mainWindow = new MainWindow();
            MainWindow = mainWindow;
            MainWindow.Show();
            mainWindow.ViewModel = new DemoViewModel(new IElevationProvider[]
                                                     {
                                                         HgtElevationProvider.CreateInMemoryFromZipFiles(@"C:\mc\SRTM3ZIP"),
                                                         HgtElevationProvider.CreateInMemoryFromRawFiles(@"C:\mc\SRTM3HGT"),
                                                         HgtElevationProvider.CreateDirectDiskAccessFromRawFiles(@"C:\mc\SRTM3HGT"),
                                                         AdfElevationProvider.CreateInMemoryFromZipFiles(@"C:\mc\EGS2008ZIP"),
                                                     });
        }
    }
}
