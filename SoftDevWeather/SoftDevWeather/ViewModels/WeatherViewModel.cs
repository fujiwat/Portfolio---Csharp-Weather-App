using SoftDevWeather.Models;
using SoftDevWeather.ServicesHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SoftDevWeather.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        WeatherServices _weatherServices = new WeatherServices();

        private WeatherMainModel _weatherMainModel;  // for xaml binding
        private string getWeatherLargeImageString(string iconImageString)
        {
            switch (iconImageString)
            {
                case "01d": return "https://media.istockphoto.com/id/475526792/hu/fot%C3%B3/k%C3%A9k-%C3%A9g-felh%C5%91-k%C3%B6zeli.jpg?s=612x612&w=0&k=20&c=pyvzkBVyTX51w2Tw80O4BmwQ1ySe7NGNUcjQ2QOgqWY=";
                case "02d": return "https://media.istockphoto.com/id/492866927/hu/fot%C3%B3/n%C3%A9h%C3%A1ny-kis-bolyhos-feh%C3%A9r-felh%C5%91k-k%C3%A9k-%C3%A9gen.jpg?s=612x612&w=0&k=20&c=BrkWrhi3R1X9sU4CNzzS3rb7oY-mZIQYZXg2epKv7b8=";
                case "03d": return "https://media.istockphoto.com/id/1218424963/hu/fot%C3%B3/cirrus-felh%C5%91k-k%C3%A9k-naps%C3%BCt%C3%A9ses-tavaszi-%C3%A9gen-term%C3%A9szet-h%C3%A1tt%C3%A9r-f%C3%B3kusz-n%C3%A9lk%C3%BCl.jpg?s=612x612&w=0&k=20&c=RHPbSk7_6qIdjNQTQzIZB5pgLcbc45JFXVsNmzbkd5U=";
                case "04d": return "https://media.istockphoto.com/id/827799448/hu/fot%C3%B3/makr%C3%A9la-vagy-%C3%ADr%C3%B3-%C3%A9g.jpg?s=612x612&w=0&k=20&c=uip89T8dsl3jlSxZIwhVl5nn4wD0CWjdcfHk7oE-A7Y=";
                case "09d": return "https://media.istockphoto.com/id/453684353/hu/fot%C3%B3/es%C5%91-a-mez%C5%91k%C3%B6n.jpg?s=612x612&w=0&k=20&c=Kc99QPQ_utqZ-XNUOaEtWi5Tq0ULGbII67fSYEskUbY=";
                case "10d": return "https://media.istockphoto.com/id/1257951336/hu/fot%C3%B3/%C3%A1tl%C3%A1tsz%C3%B3-eserny%C5%91-es%C5%91-alatt-a-v%C3%ADzcseppek-fr%C3%B6ccsen%C3%A9si-h%C3%A1tter%C3%A9ben-es%C5%91s-id%C5%91j%C3%A1r%C3%A1s-koncepci%C3%B3.jpg?s=612x612&w=0&k=20&c=YzYSiaGaKurzC_WY5q67-zDhAgxSfd6JBXSZehOfc8A=";
                case "11d": return "https://media.istockphoto.com/id/520167608/hu/fot%C3%B3/%C3%BAt-a-pokolba.jpg?s=612x612&w=0&k=20&c=2wHSDL6dHO9qt0vLDF6N8QgDcxAw32m9Jz8Atbk1CbE=";
                case "13d": return "https://media.istockphoto.com/id/521998431/hu/fot%C3%B3/feh%C3%A9red%C3%A9s-felt%C3%A9telei.jpg?s=612x612&w=0&k=20&c=q3JoZbvFWORckoGeyg0juluwIPcmlV9SwU-Vtt54QVw=";
                case "50d": return "https://media.istockphoto.com/id/1176634793/hu/fot%C3%B3/a-meleg-t%C3%A9li-k%C3%B6d-k%C3%ADs%C3%A9rti-a-t%C3%A1j-jenksville-egy-kis-vid%C3%A9ki-v%C3%A1ros-new-york-%C3%A1llamban-amely-a.jpg?s=612x612&w=0&k=20&c=LJyeRsAgRGBhPY9AXiAViVrwqtj1UlhK0VZLD-u3Itk=";
            }
            return "https://media.istockphoto.com/id/1205270037/hu/fot%C3%B3/k%C3%A9rd%C5%91jel-a-besz%C3%A9dbubor%C3%A9kon.jpg?s=612x612&w=0&k=20&c=C2CVyhIPxLQ9glt9kYXGGi0vgsQUwJHSIQyXxZHXdNw=";
        }
        public ICommand ShowCity { get; set; }

        public WeatherMainModel WeatherMainModel
        {
            get { return _weatherMainModel; }
            set
            {
                _weatherMainModel = value;
                IconImageString = "https://openweathermap.org/img/w/" + _weatherMainModel.weather[0].icon + ".png"; // fetch weather icon image
                LargeImageString = getWeatherLargeImageString((_weatherMainModel.weather[0].icon).Replace('n', 'd') );
                OnPropertyChanged();
            }
        }

        private string _city;   // for entry binding and for method parameter value
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                Task.Run(async () => {
                    await InitializeGetWeatherAsync();
                });
                OnPropertyChanged();
            }
        }

        private string _iconImageString; // for weather icon image string binding
        public string IconImageString
        {
            get { return _iconImageString; }
            set
            {
                _iconImageString = value;
                OnPropertyChanged();
            }
        }

        private string _largeImageString; // for weather icon image string binding
        public string LargeImageString
        {
            get { return _largeImageString; }
            set
            {
                _largeImageString = value;
                OnPropertyChanged();
            }
        }


        private bool _isBusy;   // for showing loader when the task is initializing
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private async Task InitializeGetWeatherAsync()
        {
            try
            {
                IsBusy = true; // set the ui property "IsRunning" to true(loading) in Xaml ActivityIndicator Control
                WeatherMainModel = await _weatherServices.GetWeatherDetails(_city);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
