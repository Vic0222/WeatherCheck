using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCheck.Application.SeedWork.Exceptions
{
    public class InvalidZipCodeException : ApplicationException
    {
        public string ZipCode { get; set; }

        public InvalidZipCodeException(string zipCode, string message) : base(message)
        {
            ZipCode = zipCode;
        }
    }
}
