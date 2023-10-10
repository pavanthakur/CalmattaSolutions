using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calmatta.DAL.Model
{
    public class Team
    {
        public List<Agent> Agents { get; }
        public List<OverflowAgent> OverflowAgents { get; }

        public Team(List<Agent> agents, List<OverflowAgent> overflowAgents)
        {
            Agents = agents;
            OverflowAgents = overflowAgents;
        }

        public int GetCapacity()
        {
            var teamCapacity = (int)(Agents.Sum(x => x.WorkCapacity) * 1.5);
            var overflowCapacity = OverflowAgents.Sum(x => x.WorkCapacity);

            return teamCapacity + overflowCapacity;
        }
    }
}