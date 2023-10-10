using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calmatta.DAL.Helper
{
    public static class CalmattaConstant
    {
        public enum SeniorityLevel
        {
            Junior = 0,
            Mid,
            Senior,
            TeamLead
        }

        public static Dictionary<SeniorityLevel, double> Multipliers = new Dictionary<SeniorityLevel, double>
        {
            {SeniorityLevel.Junior, 0.4},
            {SeniorityLevel.Mid, 0.6},
            {SeniorityLevel.Senior, 0.8},
            {SeniorityLevel.TeamLead, 0.5},
        };

        public static double GetSeniorityMultiplier(SeniorityLevel seniorityLevel)
        {
            return Multipliers[seniorityLevel];
        }
    }
}
