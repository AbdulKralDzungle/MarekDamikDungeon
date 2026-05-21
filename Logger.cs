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
        private StreamWriter streamWriter;

        public Logger()
        {
            streamWriter = new StreamWriter("./Log.txt");
            streamWriter.AutoFlush = true;
        }
        public void Log(string message)
        {
            Console.WriteLine(message);
            string time = $"[{DateTime.Now} {DateTime.Now.Microsecond }]";
            Write(time + message);
        }

        public void Write(string message)
        {
            streamWriter.WriteLine(message);
        }

    }
}
