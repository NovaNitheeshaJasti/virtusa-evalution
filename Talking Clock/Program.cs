using System;
namespace TalkingClock 
{
    class Program
    {
        static void Main(string[] args)
        {
            string errMsg = "Please use format h:mm or hh:mm, where h is hour and m is minute.";
            Console.WriteLine("Talking Clock");
            Console.WriteLine($"{errMsg}");
            while (true)
            {
                try
                {
                    string? t1 = Console.ReadLine();
                    Console.WriteLine(TalkingClassRef.Time(t1));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{errMsg}");
                }
            }
        }

    }
}
