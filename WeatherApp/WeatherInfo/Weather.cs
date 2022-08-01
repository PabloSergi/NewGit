using System.Drawing;

namespace WeatherApp.WeatherInfo
{
    public class Weather
    {
        public int Id { get; set; }

        public string? Main { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public Bitmap Bitmap
        {
            get
            {
                return new Bitmap(Image.FromFile($"icons/{Icon}.png"));
            }
        }
    }
}


