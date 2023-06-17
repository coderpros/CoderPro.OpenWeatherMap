// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Interaction logic for App.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.UI.Wpf
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Forms;

    using CoderPro.OpenWeatherMap.UI.Wpf.ViewModels;

    using MaterialDesignThemes.Wpf;

    using Microsoft.Extensions.Configuration;
    
    #endregion

    /// <summary>
    /// Interaction logic for the App.
    /// </summary>
    public partial class App
    {
        #region Properties & Fields

        /// <summary>
        /// The system tray icon.
        /// </summary>
        private static NotifyIcon? notifyIcon;

        /// <summary>
        /// The flag that dictates of the app is in the process of exiting.
        /// </summary>
        private static bool isExiting;

        /// <summary>
        /// The application settings manager.
        /// </summary>
        private readonly SettingsManager<ViewModels.ApplicationSettings> applicationSettingsManager;

        /// <summary>
        /// The user settings manager.
        /// </summary>
        private readonly SettingsManager<ViewModels.UserSettings> userSettingsManager;

        /// <summary>
        /// The application settings.
        /// </summary>
        private readonly ViewModels.ApplicationSettings applicationSettings;

        /// <summary>
        /// The palette helper.
        /// </summary>
        private readonly PaletteHelper paletteHelper = new ();

        /// <summary>
        /// The me property provides static access to the App class throughout the application.
        /// </summary>
        public static App Me => (App)System.Windows.Application.Current;

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public static IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        public static ViewModels.ApplicationSettings ApplicationSettings { get; private set; }

        /// <summary>
        /// Gets the user settings.
        /// </summary>
        public static ViewModels.UserSettings UserSettings { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether is dark theme.
        /// </summary>
        public bool IsDarkTheme { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.applicationSettingsManager = new SettingsManager<ViewModels.ApplicationSettings>($"{AppDomain.CurrentDomain.BaseDirectory}AppSettings.json");
            this.userSettingsManager = new SettingsManager<ViewModels.UserSettings>($"{AppDomain.CurrentDomain.BaseDirectory}UserSettings.json");

            this.applicationSettings = this.applicationSettingsManager.LoadSettings();


            ApplicationSettings = this.applicationSettings;
            UserSettings = this.userSettingsManager.LoadSettings();
        }

        #endregion

        #region Events

        /// <summary>
        /// The exit application method.
        /// </summary>
        internal static void ExitApplication()
        {
            isExiting = true;

            notifyIcon?.Dispose();
            notifyIcon = null;

            App.Current.Shutdown();
        }

        /// <summary>
        /// The startup event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="EventArgs">event arguments</see>.
        /// </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("AppSettings.json", false, false)
                .AddJsonFile("UserSettings.json", false, false)
                .AddUserSecrets<ViewModels.DeveloperSecrets>(false, true)
                .Build();

            App.Configuration = config;

            base.OnStartup(e);

            this.MainWindow = new MainWindow();

            this.MainWindow.Closing += this.Windows_PreventClosing;

            notifyIcon = new NotifyIcon();
            notifyIcon.DoubleClick += (s, args) => this.ShowMainWindow();
            notifyIcon.Icon = Wpf.Properties.Resources.CoderProIcon;
            
            notifyIcon.Visible = true;

            this.CreateContextMenu();

            App.Me.IsDarkTheme = UserSettings.IsDarkTheme;

            // Get the current theme used in the application
            var theme = this.paletteHelper.GetTheme();

            // If condition true, then set IsDarkTheme to false and, SetBaseTheme to light
            if (App.Me.IsDarkTheme)
            {
                App.Me.IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            else
            {
                // else set IsDarkTheme to true and SetBaseTheme to dark
                App.Me.IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }

            // Apply the changes.
            this.paletteHelper.SetTheme(theme);
        }

        /// <summary>
        /// The windows prevent closing event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Windows_PreventClosing(object? sender, CancelEventArgs e)
        {
            if (isExiting)
            {
                return;
            }

            e.Cancel = true;

            (sender as Window)?.Hide();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// The create context menu.
        /// </summary>
        private void CreateContextMenu()
        {
            Debug.Assert(notifyIcon != null, nameof(notifyIcon) + " != null");
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();

            notifyIcon.DoubleClick += (s, e) => this.ShowDefaultPage();

            notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        /// <summary>
        /// The show default page method.
        /// </summary>
        private void ShowDefaultPage()
        {
            var mainWindow = App.Current.Windows.OfType<MainWindow>().First();

            mainWindow.MainFrame.Source = new Uri("Pages/DefaultPage.xaml", UriKind.Relative);
            mainWindow.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// The show main window.
        /// </summary>
        private void ShowMainWindow()
        {
            Debug.Assert(this.MainWindow != null, nameof(notifyIcon) + " != null");

            if (this.MainWindow.IsVisible)
            {
                if (this.MainWindow.WindowState == WindowState.Minimized)
                {
                    this.MainWindow.WindowState = WindowState.Normal;
                }

                this.MainWindow.Activate();
            }
            else
            {
                this.MainWindow.Show();
            }
        }
        #endregion
    }
}
