using CommunityToolkit.Mvvm.DependencyInjection;
using GroupCalendar.Core.Helpers;
using GroupCalendar.Core.Repository;
using GroupCalendar.Core.Services;
using GroupCalendar.ViewModels;
using GroupCalendar.ViewModels.Account;
using GroupCalendar.Views.Account;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace GroupCalendar
{
    /// <summary>
    /// community toolkit - mvvm toolkit
    /// </summary>
    public partial class App : Application
    {
        private const string APPLICATION_NAME = "GroupCalendar";
        private static readonly string Title = "GroupCalendar";

        public App()
        {
            var services = ConfigureServices();
            Ioc.Default.ConfigureServices(services);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // 중복 실행 됨.
            if (this.CheckDuplicateProcess() is true)
            {
                MessageBox.Show("이미 실행중 입니다.");
            }

            if (ShowLoginWindow() == false)
            {
                Current.Shutdown();
                return;
            }

            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            ShowMainWindow();
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //views
            services.AddSingleton<MainWindow>();

            //viewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<AccountWindowViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();

            //Service
            services.AddTransient<MemberService>();
            services.AddTransient<LoginService>();

            //Repository
            services.AddTransient<IMemberRepository, LocalMemberRepository>();

            //DB
            services.AddSingleton<DbConnectionFactory>();
            services.AddTransient<TransactionManager>();


            return services.BuildServiceProvider();
        }


        private bool CheckDuplicateProcess()
        {
            bool result = false;

            // 프로세스 중복 체크
            // 이미 실행 중이라면 해당 Window Activate 처리
            if (ProcessChecker.Do(APPLICATION_NAME))
            {
                result = true;

                string processName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                int currentProcess = System.Diagnostics.Process.GetCurrentProcess().Id;
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(processName);
                foreach (System.Diagnostics.Process process in processes)
                {
                    if (currentProcess == process.Id)
                    {
                        continue;
                    }

                    // find MainWindow Title
                    IntPtr hwnd = ProcessChecker.FindWindow(null, Title);
                    if (hwnd.ToInt32() > 0)
                    {
                        //Activate it
                        ProcessChecker.SetForegroundWindow(hwnd);

                        WindowShowStyle command = ProcessChecker.IsIconicNative(hwnd) ? WindowShowStyle.Restore : WindowShowStyle.Show;
                        ProcessChecker.ShowWindow(hwnd, command);
                    }
                }

                Current.Shutdown();
            }

            return result;
        }

        private bool ShowLoginWindow()
        {
            var isLogin = false;

            var loginWindow = new AccountWindow();
            var vm = Ioc.Default.GetService<AccountWindowViewModel>();
            vm.SuccessLogin += (_, __) => 
            {
                isLogin = true;
                loginWindow.Close();
            };
            loginWindow.DataContext = vm;
            loginWindow.ShowDialog();

            return isLogin;
        }

        private void ShowMainWindow()
        {
            var main = new MainWindow();
            main.DataContext = Ioc.Default.GetService<MainViewModel>();
            main?.ShowDialog();
        }
    }
}
