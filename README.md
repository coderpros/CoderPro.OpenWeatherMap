<a href="coderpro.net" target="_blank"><img src="https://coderpro.net/media/g0qlgmoq/coderpro_jump_blue_300w.gif" align="right" width="125" /></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]
[![Twitter](https://img.shields.io/twitter/url/https/twitter.com/cloudposse.svg?style=social&label=Follow%20%40coderProNet)](https://twitter.com/coderProNet)
[![GitHub](https://img.shields.io/github/followers/coderpros?label=Follow&style=social)](https://github.com/coderpros)

[contributors-shield]: https://img.shields.io/github/contributors/coderpros/CoderPro.OpenWeatherMap.svg?style=flat-square
[contributors-url]: https://github.com/coderpros/CoderPro.OpenWeatherMap/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/coderpros/CoderPro.OpenWeatherMap?style=flat-square
[forks-url]: https://github.com/coderpros/CoderPro.OpenWeatherMap/network/members
[stars-shield]: https://img.shields.io/github/stars/coderpros/CoderPro.OpenWeatherMap.svg?style=flat-square
[stars-url]: https://github.com/coderpros/CoderPro.OpenWeatherMap/stargazers
[issues-shield]: https://img.shields.io/github/issues/coderpros/CoderPro.OpenWeatherMap?style=flat-square
[issues-url]: https://github.com/coderpros/CoderPro.OpenWeatherMap/issues
[license-shield]: https://img.shields.io/github/license/coderpros/CoderPro.OpenWeatherMap?style=flat-square
[license-url]: https://github.com/coderpros/CoderPro.OpenWeatherMap/master/CoderPro.OpenWeatherMap/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/company/coderpros
[twitter-shield]: https://img.shields.io/twitter/follow/coderpronet?style=social
[twitter-follow-url]: https://img.shields.io/twitter/follow/coderpronet?style=social
[github-shield]: https://img.shields.io/github/followers/coderpros?label=Follow&style=social
[github-follow-url]: https://img.shields.io/twitter/follow/coderpronet?style=social

# OpenWeatherMap API Wrapper Library (C#)
## Overview

The most complete and modern library written for the OpenWeather API in .Net. This library retrieves the JSON from the OpenWeatherMap API and serializes them into objects. All of the
free textual APIs are supported. We have planned support for the paid and mapping APIs also. See the roadmap for more information.

## Clients
- AirPollutionClient - Current and historical pollution conditions. See OpenWeather [API Doc](https://openweathermap.org/api/air-pollution) for more information.
- CurrentWeatherClient - Current weather conditions. See OpenWeather [API Doc](https://openweathermap.org/current) for more information.
- FiveDayForecastClient - Up to five days of three hour forecasts. See OpenWeather [API Doc](https://openweathermap.org/forecast5) for more information.
- GeocodingClient - Full support for geocoding by location name, post/zip code, and reverse geocoding. See OpenWeather [API Doc](https://openweathermap.org/api/geocoding-api) for more information.

## Installing
Install the NuGet package https://www.nuget.org/packages/CoderPro.OpenWeatherMap.Wrapper/

### .NET CLI

```powershell
dotnet add package CoderPro.OpenWeatherMap.Wrapper
```
### Package Manager

```powershell
Install-Package CoderPro.OpenWeatherMap.Wrapper
```

## Example Usage

```csharp
var openWeatherClient = new CoderPro.OpenWeatherMap.Wrapper.CurrentWeatherClient("my open weather api key");

// Use async version wherever possible.
var query = await openWeatherAPI.QueryAsync("city/location");

// or non-async version if needed for legacy code
var query = openWeatherAPI.Query("city/location");

Console.WriteLine($"The temperature in {query.Name}, {query.Sys.Country} is currently {query.Main.Temperature.FahrenheitCurrent} °F");
```

# Demo Applications
A demo .Net 6 Windows Presentation Framework (WPF) application is bundled in this repo.

## Roadmap
| Feature | Version | Release Date |
| ------- | ------- | ------------ | 
| All free textual APIs | 0.5.0 | 2023/06/08 |
| Demo WPF App | 1.0.0 | 2023/06/17 |
| Weather Triggers API | 1.1.0 | 2023/07/08 |
| One Call API 3 | 2.0.0 | 2023/08/08 |
| Weather Maps 1.0 API | 2.1.0 | 2023/09/08 |
| Weather station API | 2.2.0 | 2023/10/08 |

## Change Log
- 2023/06/08
  - Initial checkin