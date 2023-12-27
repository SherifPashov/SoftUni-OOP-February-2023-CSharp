using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Telephony
{
    public class StationaryPhone : ICallable
    {

        public string Call(string phoneNumber)
        {
            if (!phoneNumber.All(c=>Char.IsDigit(c))&& phoneNumber.Length!=10)
            {
                throw new ArgumentException("Invalid number!");
            }
            return $"Dialing... {phoneNumber}";
        }
    }
}
