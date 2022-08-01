using System;
namespace WeatherApp.Exceptions
{
    public class UnknownCity : Exception
    {
        public UnknownCity() : base ("Я не знаю такого города, попробуйте ввести название подругому.") { }
    }
}

