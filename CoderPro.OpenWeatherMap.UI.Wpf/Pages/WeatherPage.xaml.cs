// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeatherPage.xaml.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Interaction logic for WeatherPage.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.UI.Wpf.Pages
{
    #region Usings

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    using CoderPro.OpenWeatherMap.Wrapper;
    using CoderPro.OpenWeatherMap.Wrapper.Models.Common;
    using CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather;

    using MaterialDesignThemes.Wpf;

    #endregion

    /// <summary>
    /// Interaction logic for WeatherPage.
    /// </summary>
    public partial class WeatherPage
    {
        #region Properties

        #region API Clients

        /// <summary>
        /// The current weather client.
        /// </summary>
        private readonly CurrentWeatherClient currentWeatherClient = new CurrentWeatherClient(App.Configuration["OpenWeatherMapApiKey"]);

        /// <summary>
        /// The five day forecast (every 3 hours) client.
        /// </summary>
        private readonly FiveDayForecastClient fiveDayForecastClient = new FiveDayForecastClient(App.Configuration["OpenWeatherMapApiKey"]);

        /// <summary>
        /// The geo coding client.
        /// </summary>
        private readonly GeoCodingClient geoCodingClient = new GeoCodingClient(App.Configuration["OpenWeatherMapApiKey"]);

        /// <summary>
        /// The air pollution client.
        /// </summary>
        private readonly AirPollutionClient airPollutionClient = new AirPollutionClient(App.Configuration["OpenWeatherMapApiKey"]);

        #endregion

        /// <summary>
        /// The coordinate to be searched.
        /// </summary>
        private NetTopologySuite.Geometries.Point? coordinate;

        /// <summary>
        /// The complete weather view model.
        /// </summary>
        private ViewModels.CompleteWeather completeWeatherViewModel;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherPage"/> class.
        /// </summary>
        public WeatherPage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// The on initialized event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="EventArgs">event args</see>.
        /// </param>
        protected override async void OnInitialized(EventArgs e)
        {
            this.LocationTextBox.Text = $"{App.ApplicationSettings.Weather.City}, {App.ApplicationSettings.Weather.Province}, {App.ApplicationSettings.Weather.Country}";

            await this.SetCoordinate(App.ApplicationSettings.Weather.City, App.ApplicationSettings.Weather.Country, App.ApplicationSettings.Weather.Province);
            await this.GetWeather();

            this.DataContext = this.completeWeatherViewModel;
            this.LastUpdated.Text = DateTime.Now.ToShortTimeString();

            switch (App.ApplicationSettings.Weather.DefaultUOM)
            {
                case "Imperial":
                    this.FahrenheitRadioButton.IsChecked = true;
                    break;
                case "Metric":
                    this.CelsiusRadioButton.IsChecked = true;
                    break;
                case "Standard":
                    this.KelvinRadioButton.IsChecked = true;
                    break;
            }

            base.OnInitialized(e);
        }

        /// <summary>
        /// The page loaded event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs">event args</see>.
        /// </param>
        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();

            // Get the current theme used in the application
            var theme = paletteHelper.GetTheme();

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
            paletteHelper.SetTheme(theme);

            var timer = new DispatcherTimer();

            timer.Tick += new EventHandler(this.Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 5, 0);

            timer.Start();
        }

        /// <summary>
        /// The timer tick event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs">event args</see>.
        /// </param>
        private async void Timer_Tick(object? sender, EventArgs e)
        {
            if (!this.AutoRefreshButton.IsChecked.GetValueOrDefault())
            {
                return;
            }

            var searchText = this.LocationTextBox.Text.Split(",");

            await this.SetCoordinate(searchText[0], searchText[2], searchText[1]);
            await this.GetWeather();

            this.LastUpdated.Text = DateTime.Now.ToShortTimeString();
        }

        /// <summary>
        /// The temperature unit check change event is responsible for changing the <see cref="Unit">unit of measure</see> for the page.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs">event args</see>.
        /// </param>
        private void TemperatureUnit_CheckChange(object sender, System.Windows.RoutedEventArgs e)
        {
            var temperatureUnit = (RadioButton)sender;
            var highLowMetroBinding = new MultiBinding { StringFormat = " {0:N0}° | {1:N0}°" };

            // Convert temperatures to appropriate unit of measure.
            switch (temperatureUnit.Tag)
            {
                case "Celsius":
                    highLowMetroBinding.Bindings.Add(new Binding("CurrentWeather.Main.Temperature.CelsiusMaximum"));
                    highLowMetroBinding.Bindings.Add(new Binding("CurrentWeather.Main.Temperature.CelsiusMinimum"));

                    this.CurrentTemperatureTextBlock.SetBinding(TextBlock.TextProperty, new Binding("CurrentWeather.Main.Temperature.CelsiusCurrent") { StringFormat = " {0:N0} °C" });
                    this.CurrentFeelsLikeTemperatureTextBlock.SetBinding(TextBlock.TextProperty, new Binding("CurrentWeather.Main.Temperature.CelsiusFeelsLike") { StringFormat = " {0:N0} °C" });
                    this.HighLowTextBlock.SetBinding(TextBlock.TextProperty, highLowMetroBinding);

                    this.CurrentWindSpeed.SetBinding(TextBlock.TextProperty, new Binding("CurrentWeather.Wind.SpeedMetersPerSecond") { StringFormat = " {0:F2} m/s" });

                    break;
                case "Kelvin":
                    highLowMetroBinding.Bindings.Add(new Binding("CurrentWeather.Main.Temperature.KelvinMaximum"));
                    highLowMetroBinding.Bindings.Add(new Binding("CurrentWeather.Main.Temperature.KelvinMinimum"));

                    this.CurrentTemperatureTextBlock.SetBinding(TextBlock.TextProperty, new Binding("CurrentWeather.Main.Temperature.KelvinCurrent") { StringFormat = " {0:N0} K" });
                    this.CurrentFeelsLikeTemperatureTextBlock.SetBinding(TextBlock.TextProperty, new Binding("CurrentWeather.Main.Temperature.KelvinFeelsLike") { StringFormat = " {0:N0} K" });
                    this.HighLowTextBlock.SetBinding(TextBlock.TextProperty, highLowMetroBinding);

                    switch (App.ApplicationSettings.Weather.DefaultUOM)
                    {
                        case "Imperial":
                            this.CurrentWindSpeed.SetBinding(TextBlock.TextProperty, new Binding("CurrentWeather.Wind.SpeedFeetPerSecond") { StringFormat = " {0:F2} ft/s" });
                            break;
                        default:
                            this.CurrentWindSpeed.SetBinding(TextBlock.TextProperty, new Binding("CurrentWeather.Wind.SpeedMetersPerSecond") { StringFormat = " {0:F2} m/s" });
                            break;
                    }

                    break;
                case "Fahrenheit":
                    highLowMetroBinding.Bindings.Add(new Binding("CurrentWeather.Main.Temperature.FahrenheitMaximum"));
                    highLowMetroBinding.Bindings.Add(new Binding("CurrentWeather.Main.Temperature.FahrenheitMinimum"));

                    this.CurrentTemperatureTextBlock.SetBinding(TextBlock.TextProperty, new Binding("CurrentWeather.Main.Temperature.FahrenheitCurrent") { StringFormat = " {0:N0} °F" });
                    this.CurrentFeelsLikeTemperatureTextBlock.SetBinding(TextBlock.TextProperty, new Binding("CurrentWeather.Main.Temperature.FahrenheitFeelsLike") { StringFormat = " {0:N0} °F" });
                    this.HighLowTextBlock.SetBinding(TextBlock.TextProperty, highLowMetroBinding);

                    this.CurrentWindSpeed.SetBinding(TextBlock.TextProperty, new Binding("CurrentWeather.Wind.SpeedFeetPerSecond") { StringFormat = " {0:F2} ft/s" });

                    break;
            }
        }

        /// <summary>
        /// The search button click event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs" />.
        /// </param>
        private async void SearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.GetWeather(this.LocationTextBox.Text);
        }

        /// <summary>
        /// The auto refresh button on mouse right button up event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs" />.
        /// </param>
        private async void AutoRefreshButton_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var toggleButton = (ToggleButton)sender;

            if (!toggleButton.IsChecked.GetValueOrDefault())
            {
                return;
            }

            var searchText = this.LocationTextBox.Text.Split(",");

            await this.SetCoordinate(searchText[0], searchText[2], searchText[1]);
            await this.GetWeather();
        }
        
        #endregion

        #region Helpers

        /// <summary>
        /// The get weather function gets the weather conditions for a location and updates the data context.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task GetWeather()
        {
            if (this.coordinate != null)
            {
                try
                {
                    var currentWeather = await this.currentWeatherClient.QueryByCoordinatesAsync(SearchType.Coordinate, this.coordinate) ?? throw new InvalidOperationException();

                    this.completeWeatherViewModel = new ViewModels.CompleteWeather(
                        currentWeather,
                        await this.airPollutionClient.CurrentPollutionAsync(this.coordinate) ?? throw new InvalidOperationException(),
                        await this.airPollutionClient.HistoryPollutionAsync(this.coordinate, DateTime.Now, DateTime.Now.AddDays(5), currentWeather.TimeZone) ?? throw new InvalidOperationException(),
                        await this.fiveDayForecastClient.QueryAsync(this.coordinate) ?? throw new InvalidOperationException());

                    foreach (var forecast in this.completeWeatherViewModel.Forecast5.ForecastList)
                    {
                        forecast.Pollution =
                            this.completeWeatherViewModel.ForecastAirPollution.PollutionList.FirstOrDefault(
                                p => p.Date == forecast.Date);
                    }

                    this.DataContext = this.completeWeatherViewModel;
                }
                catch (Exception e)
                {
                    MessageBox.Show(
                        "Could not get search results. Please check your criteria or try again later.",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// This get weather function queries the OpenWeather API by location string.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task GetWeather(string location)
        {
            try
            {
                var currentWeatherResponse =
                    await this.currentWeatherClient.QueryAsync(location, SearchType.LocationName)
                    ?? throw new InvalidOperationException();

                this.coordinate = currentWeatherResponse.Coordinate;

                this.completeWeatherViewModel = new ViewModels.CompleteWeather(
                    currentWeatherResponse,
                    await this.airPollutionClient.CurrentPollutionAsync(this.coordinate) ?? throw new InvalidOperationException(),
                    await this.airPollutionClient.HistoryPollutionAsync(this.coordinate, DateTime.Now, DateTime.Now.AddDays(5), currentWeatherResponse.TimeZone) ?? throw new InvalidOperationException(),
                    await this.fiveDayForecastClient.QueryAsync(this.coordinate) ?? throw new InvalidOperationException());

                foreach (var forecast in this.completeWeatherViewModel.Forecast5.ForecastList)
                {
                    forecast.Pollution =
                        this.completeWeatherViewModel.ForecastAirPollution.PollutionList.FirstOrDefault(
                            p => p.Date == forecast.Date);
                }

                this.DataContext = this.completeWeatherViewModel;
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Could not get search results. Please check your criteria or try again later.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// The set coordinate routine updates the coordinate that the page uses for queries.
        /// </summary>
        /// <param name="city">
        /// The city.
        /// </param>
        /// <param name="country">
        /// The country.
        /// </param>
        /// <param name="province">
        /// The province.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task SetCoordinate(string city, string country, string province = "")
        {
            var location = await this.geoCodingClient.QueryCoordinatesAsync($"{city}, {province}, {country}");

            if (location.LocationList.Any())
            {
                this.coordinate = location.LocationList[0].Coordinates;
            }
        }
        #endregion
    }
}
