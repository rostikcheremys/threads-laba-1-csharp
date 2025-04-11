using System.Diagnostics;

namespace Program
{
    class Program
    {
        private const int NumberOfThreads = 10;
        private readonly Random _random = new ();

        static void Main()
        {
            new Program().Start();
        }

        private void Start()
        {
            ThreadInfo[] threads = new ThreadInfo[NumberOfThreads];

            for (int i = 0; i < NumberOfThreads; i++)
            {
                int threadId = i + 1;
                int timeInSeconds = _random.Next(10000, 30000);
                
                threads[i] = new ThreadInfo(threadId, timeInSeconds);
                
                Console.WriteLine($"Потiк №{threadId} почав виконання на {timeInSeconds / 1000.0:F1} сек.");
            }

            foreach (var info in threads)
            {
                Thread thread = new Thread(() => Counter(info));
                
                info.Thread = thread;
                info.Stopwatch = Stopwatch.StartNew();
                thread.Start();
            }

            Thread managerThread = new Thread(() => ManageThreads(threads));
            managerThread.Start();
        }

        private void Counter(ThreadInfo info)
        {
            long sum = 0;
            long count = 0;

            info.Stopwatch = Stopwatch.StartNew();

            while (!info.ShouldStop) 
            {
                sum += count;
                count++;
            }

            Console.WriteLine($"\n[Потiк #{info.Id}] зупинено через {info.TimeInSeconds / 1000.0:F1} сек.");
            Console.WriteLine($"Кiлькiсть елементiв: {count}");
            Console.WriteLine($"Сума: {sum}");
        }

        private static void ManageThreads(ThreadInfo[] infos)
        {
            bool isFinished  = false;

            while (!isFinished)
            {
                isFinished = true;

                foreach (var info in infos)
                {
                    if (!info.ShouldStop && info.Stopwatch.ElapsedMilliseconds >= info.TimeInSeconds)
                    {
                        info.ShouldStop = true;
                    }

                    if (!info.ShouldStop) isFinished = false;
                }
            }
        }
    }
}
