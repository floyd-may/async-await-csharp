namespace AsyncAwait {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;

    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();

            container.PerRequest<MainViewModel>().Activated += (o) => {
                if (!(o is MainViewModel))
                    return;

                var vm = (MainViewModel)o;

                var timer = new System.Timers.Timer
                {
                    Interval = 100
                };

                timer.Elapsed += (x, args) =>
                {
                    Application.Dispatcher.BeginInvoke(new System.Action(() => vm.RotationAngle = (int)(DateTime.Now.Millisecond * .36)));
                };
                timer.Start();
            };
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}