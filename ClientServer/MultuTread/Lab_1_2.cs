using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultuTread
{
    class Lab_1_2
    {
        static int limit = 10;
        static Thread? thread1;

        static void ThreadWork1()
        {
            for (int i = 1; i <= limit; i++)
            {
                Console.WriteLine($"thread1: {i}");
            }
        }

        static void ThreadWork2()
        {
            thread1?.Join();
            for (int i = 1; i <= limit; i++)
            {
                Console.WriteLine($"thread2: {i}");
            }
        }

        public static void Run()
        {
            Console.WriteLine("Lab02 started");
            thread1 = new Thread(ThreadWork1);
            Thread thread2 = new Thread(ThreadWork2);

            thread1.Start();
            Thread.Sleep(1000);
            thread2.Start();
        }
    }
}
