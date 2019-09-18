using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Agnets.Categories.Interfaces
{
    interface ILogicAgent
    {
        string ProcessInput(string input);
        string AnswerQuestion(string input);
    }
}
