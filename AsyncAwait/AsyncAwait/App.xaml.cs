using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwait
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            var viewModel = new ViewModel();

            var timer = new System.Timers.Timer
            {
                Interval = 100
            };
            
            timer.Elapsed += (o, args) =>
            {
                Dispatcher.BeginInvoke(new Action(() => viewModel.RotationAngle = (int)(DateTime.Now.Millisecond * .36)));
            };
            timer.Start();

            MainWindow.DataContext = viewModel;
        }
    }
}
