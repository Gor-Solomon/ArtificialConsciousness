using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Environment.DataLayer.Nodes.Connections.ConnectionsTypeEnum
{
    //the negative values represents negative connections,
    //each negative connection have an opposite positive connection
   public enum ConnectionType
    {
        Divide = -11,
        Plus,
        Unequal,
        Or,
        Smaller = -7,
        //Operations
        CanNotDo ,
        AreNot,
        DoseNotHaveA,
        IsNotA,
        Out,
        Hates ,
        //BAIS -0
        Connected = 0,
        //BAIS +0
        Loves,
        In,
        IsA,
        HaveA,
        Are,
        CanDo,
        //Operations
        Greater = +7 ,
        And,
        Equal,
        Minus,
        Multiply = +11,
        //+5
        Adverb = +12, // Decribes a verb
        Adjective, //describes a noun

    }
}
