using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Telephony
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Browsing(string url)
        {
            if (url.Any(c=>Char.IsDigit(c)))
            {
                throw new ArgumentException("Invalid URL!");
            }
            return $"Browsing: {url}!";
        }

        public string Call(string phoneNumber)
        {
            if (!phoneNumber.All(c => Char.IsDigit(c)))
            {
                throw new ArgumentException("Invalid number!");
            }
                return $"Calling... {phoneNumber}";
            
            
           
        }
    }
}
