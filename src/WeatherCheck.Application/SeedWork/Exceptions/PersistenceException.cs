using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCheck.Application.SeedWork.Exceptions
{
    public class PersistenceException : ApplicationException
    {
        public string ErrorCode { get; set; }

        public PersistenceException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
