using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultuTread
{
    internal class Lab03
    {
        static double value = 0.3;
        static readonly object locker = new();
        const int MaxIterations = 6;
        static int iteration = 1;
        static bool isCos = true;

        public static void Run()
        {
            Console.WriteLine("Lab03 started");
            Thread thread1 = new Thread(CalcCos);
            thread1.Name = "Thread 1";
            Thread thread2 = new Thread(CalcArccos);
            thread2.Name = "Thread 2";

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Lab03 ended");
        }

        static void CalcCos()
        {
            while (iteration <= MaxIterations)
            {
                lock (locker)
                {
                    if (iteration >= MaxIterations)
                    {
                        Monitor.Pulse(locker);
                        break;
                    }

                    while (!isCos)
                    {
                        Monitor.Wait(locker);
                        if (iteration > MaxIterations) break;
                    }

                    var result = Math.Cos(value);
                    Console.WriteLine($"[{iteration}/{MaxIterations}] cos({value}) = {result}");
                    value = result;
                    iteration++;
                    isCos = false;
                    Monitor.Pulse(locker);
                }
            }
        }

        static void CalcArccos()
        {
            while (iteration <= MaxIterations)
            {
                lock (locker)
                {
                    if (iteration >= MaxIterations)
                    {
                        Monitor.Pulse(locker);
                        break;
                    }

                    while (isCos)
                    {
                        Monitor.Wait(locker);
                        if (iteration > MaxIterations) break;
                    }

                    var result = Math.Acos(value);
                    Console.WriteLine($"[{iteration}/{MaxIterations}] acos({value}) = {result}");
                    value = result;
                    iteration++;
                    isCos = true;
                    Monitor.Pulse(locker);
                }
            }
        }
    }
}
