using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Agnets.States
{
    public enum LogicAgentStatesTypeEnum
    {
        Idle,
        DefiningGraph = 2,
        DefiningNode = 4,
        DefiningConnection = 8
    }
}
