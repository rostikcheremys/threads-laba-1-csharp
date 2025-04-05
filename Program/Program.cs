namespace Program;

class Program
{
    public static void Main()
    {
        new Program().Start();
    }

    private void Start()
    {
        Random random = new Random();

        for (int i = 1; i <= 10; i++)
        {
            int time = random.Next(15000, 30000);
            ThreadInfo info = new ThreadInfo(i, time);

            Thread thread = new Thread(Counter);
            thread.Start(info);
        }
    }

    private void Counter(object obj)
    {
        ThreadInfo info = (ThreadInfo)obj;

        long number = 0;
        long sum = 0;

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddMilliseconds(info.TimeToRun);

        while (DateTime.Now < endTime)
        {
            sum += number;
            number++;
        }

        Console.WriteLine($"\n[Потiк №{info.Id}] завершився через {info.TimeToRun / 1000.0:F1} сек.");
        Console.WriteLine($"  Сума: {sum}");
    }
}