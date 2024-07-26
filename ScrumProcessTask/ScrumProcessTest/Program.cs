using ScrumProcessTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumProcessTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ScrumProcess scrumProcess = new ScrumProcess();
            scrumProcess.Run(args);
        }
    }
}
