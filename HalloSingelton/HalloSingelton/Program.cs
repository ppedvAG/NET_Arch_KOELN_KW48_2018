using System;
using System.Threading;
using System.Threading.Tasks;

namespace HalloSingelton
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() => Logger.Instance.Log($"Was wollen wir trinken? {i} Tage lang..."));
            }

            Logger.Instance.Log("Hello World!");
            Logger.Instance.Log("Was geht ab?");
            Logger.Instance.Log("Heute trinken wir Kölsch!");

            Console.ReadLine();
        }
    }

    public class Logger
    {
        static int instCounter = 0;
        int instId = 0;
        private static Logger instance = null;
        private static object syncObj = new Object();
        public static Logger Instance
        {
            get
            {
                lock (syncObj)
                {
                    if (instance == null)
                        instance = new Logger();
                }

                return instance;
            }
        }

        private Logger()
        {
            instCounter++;
            instId = instCounter;
        }


        public void Log(string txt)
        {
            Console.WriteLine($"({instId}/{instCounter}) [{DateTime.Now}] {txt}");
        }
    }
}
