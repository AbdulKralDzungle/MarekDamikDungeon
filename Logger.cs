using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarekDamikDungeon
{
    /**
     * writes server logs to a file
     */
    internal class Logger
    {
        public Logger()
        {
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
            string time = $"[{DateTime.Now} {DateTime.Now.Microsecond }]";
            Write(time + message);
        }

        public void Write(string message)
        {
            File.AppendAllText("./Log.txt", message + Environment.NewLine);
        }

    }
}
