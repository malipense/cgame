using System;

namespace cgame
{
    internal class Main
    {
        private readonly TextWriter _stdout;
        private readonly TextReader _stdin;
        public Main(TextWriter stdout, TextReader stdin)
        {
            _stdout = stdout;
            _stdin = stdin;
        }
        internal void Start()
        {
            BootSystem();
        }
        internal void Update()
        {
            
        }

        private void BootSystem()
        {
            Console.Beep();
            PrintBootMessage("Booting up system...");
            PrintBootMessage(".");
            PrintBootMessage(".");
            PrintBootMessage(".");

            PrintBootMessage("OS Info:");
            PrintBootMessage(Environment.OSVersion.ToString());
            PrintBootMessage("Memory Page Size: " + Environment.SystemPageSize.ToString() + "MBytes");
            PrintBootMessage("Logical CPU Cores: " + Environment.ProcessorCount.ToString());
            PrintBootMessage("Executing file from: " + Environment.ProcessPath);
            PrintBootMessage("Time Elapsed: " + Environment.TickCount64);
        }

        private void PrintBootMessage(string message)
        {
            _stdout.WriteLine(message);
            Thread.Sleep(250);
        }
    }
}
