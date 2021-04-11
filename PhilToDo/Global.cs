using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilToDo
{
    public static class Global
    {
        public const string fileDirectory = "C:\\PhilToDo\\";
        public const string fileLocation = fileDirectory + "taskList.xml";
        public const string errorText = "Error";
        public static int TopNumber { get; set; }
    }
}
