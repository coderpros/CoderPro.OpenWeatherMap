// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Interaction logic for the MainWindow.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.UI.Wpf
{
    #region Usings

    using System;
    using System.Windows;

    using MaterialDesignThemes.Wpf;

    #endregion

    /// <summary>
    /// Interaction logic for the MainWindow.
    /// </summary>
    public partial class MainWindow
    {
        #region Properties
        /// <summary>
        /// The palette helper.
        /// </summary>
        private readonly PaletteHelper paletteHelper = new ();

        /// <summary>
        /// The user settings manager.
        /// </summary>
        private readonly SettingsManager<ViewModels.UserSettings> userSettingsManager;

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.MouseLeftButtonDown += delegate { this.DragMove(); };
            this.userSettingsManager = new SettingsManager<ViewModels.UserSettings>($"{AppDomain.CurrentDomain.BaseDirectory}UserSettings.json");

            this.InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// The exit button click event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/>.
        /// </param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            App.ExitApplication();
        }

        /// <summary>
        /// The toggle theme button click event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="RoutedEventArgs" />.
        /// </param>
        private void ToggleThemeButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the current theme used in the application
            var theme = this.paletteHelper.GetTheme();

            // If condition true, then set IsDarkTheme to false and, SetBaseTheme to light
            // ReSharper disable once AssignmentInConditionalExpression
            if (App.Me.IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                App.Me.IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                // else set IsDarkTheme to true and SetBaseTheme to dark
                App.Me.IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            // to apply the changes use the SetTheme function
            this.paletteHelper.SetTheme(theme);
            
            // Persist the changes.
            App.UserSettings.IsDarkTheme = App.Me.IsDarkTheme;
            this.userSettingsManager.SaveSettings(App.UserSettings);
        }

        /// <summary>
        /// The window loaded event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="RoutedEventArgs" />.
        /// </param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
            this.ToggleThemeButton.IsChecked = App.Me.IsDarkTheme;
        }

        #endregion
    }
}
