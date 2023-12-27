using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RobotService.Models
{
    public class LaserRadar : Supplement
    {
        public LaserRadar() : base(20082, 5000)
        {
        }
    }
}
