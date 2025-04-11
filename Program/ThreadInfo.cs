using System.Diagnostics;

namespace Program
{
    public class ThreadInfo
    {
        public int Id { get; set; }
        public int TimeInSeconds { get; set; }
        public Thread Thread { get; set; }
        public Stopwatch Stopwatch { get; set; }
        
        public volatile bool ShouldStop;

        public ThreadInfo(int id, int timeToRun)
        {
            Id = id;
            TimeInSeconds = timeToRun;
        }
    }
}

