using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MultuTread
{
    record class Couple(int Start, int End);

    class Lab_1_1
    {
        static void PrintRange(object? data)
        {
            if (data is Couple couple)
            {
                for (int i = couple.Start; i < couple.End+1; i++)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();
            }
        }

        public static void Run()
        {
            Console.WriteLine("Lab01 started");
            Thread thread1 = new Thread(new ParameterizedThreadStart(PrintRange));
            Thread thread2 = new Thread(new ParameterizedThreadStart(PrintRange));

            thread1.Start(new Couple(1, 5));
            //thread1.Join();
            thread2.Start(new Couple(15, 30));
            //thread2.Join();

            Console.WriteLine("Lab01 is over");
        }
    }
}
