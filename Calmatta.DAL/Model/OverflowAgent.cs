using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Calmatta.DAL.Helper.CalmattaConstant;

namespace Calmatta.DAL.Model
{
    public class OverflowAgent : Agent
    {
        public OverflowAgent(string name) : base(name, SeniorityLevel.Junior)
        {
        }
    }
}