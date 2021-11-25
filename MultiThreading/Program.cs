using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MultiThreading
{
    internal class Program
    {
        static int[] collect;

        static void Main(string[] args)
        {
            #region Parameterless
            /*
            ThreadStart start = new ThreadStart(Hello);
            Thread thread = new Thread(start);

            thread.Start();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Hello from Main!");
            }
            */
            #endregion

            #region WithParameters

            //ParameterizedThreadStart start = new ParameterizedThreadStart(HelloWithParam);

            /*
            Thread thread1 = new Thread(start);
            thread1.IsBackground = true;
            thread1.Start("Nail");
            */

            //Console.WriteLine(thread1.GetHashCode());

            //Thread thread2 = new Thread(start);
            //thread2.Start("Step");

            //Console.WriteLine(thread2.GetHashCode());

            //Thread main = Thread.CurrentThread;

            //Console.WriteLine(main.GetHashCode());

            #endregion

            /*
            ParameterizedThreadStart start = new ParameterizedThreadStart(ShowNabor);
            int threadCount = 10;
            for (int i = 0; i < threadCount; i++)
            {
                int[] StartEnd = new int[3];
                StartEnd[0] = 0;
                StartEnd[1] = 50;
                StartEnd[2] = i;
                Thread thread1 = new Thread(start);
                thread1.Start(StartEnd);
            }
            */

            collect = new int[10000]; 
            ParameterizedThreadStart start = new ParameterizedThreadStart(RandomTenTh);
            Thread thread = new Thread(start);
            thread.Priority = ThreadPriority.AboveNormal;
            thread.Start(collect);

            int[] results = new int[3];

            Thread.Sleep(100);

            ParameterizedThreadStart start1 = new ParameterizedThreadStart(Max);
            Thread thread1 = new Thread(start1);
            thread1.Start(results);

            ParameterizedThreadStart start2 = new ParameterizedThreadStart(Min);
            Thread thread2 = new Thread(start2);
            thread2.Start(results);

            ParameterizedThreadStart start3 = new ParameterizedThreadStart(Avg);
            Thread thread3 = new Thread(start3);
            thread3.Start(results);

            Thread.Sleep(100);

            Console.WriteLine($"Max = {results[0]}");
            Console.WriteLine($"Min = {results[1]}");
            Console.WriteLine($"Avg = {results[2]}");
        }

        #region InClass
        private static void HelloWithParam(object obj)
        {
            string name = (string)obj;

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"\t\tHello, {name}!");
            }
        }

        private static void Hello()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("--Hello from Thread!");
            }
        }

        private static void ShowCurrentThreads()
        {
            Process process = Process.GetCurrentProcess();

            for (int i = 0; i < process.Threads.Count; i++)
            {
                ProcessThread thread = process.Threads[i];
                Console.WriteLine(thread.Id);
            }
        }
        #endregion

        private static void ShowNabor(object startEnd)
        {
            int[] StartEnd = startEnd as int[];
            for (int i = StartEnd[0]; i < StartEnd[1]; i++)
            {
                Console.WriteLine($"Thread {StartEnd[2]} - {i}");
            }
        }

        private static void RandomTenTh(object colle)
        {
            int[] nabor = colle as int[];
            Random random = new Random();
            for (int i = 0; i<10000; i++)
            {
                nabor[i] = random.Next();
            }
        }

        private static void Max(object results)
        {
            int[] max = results as int[];
            int maxi = Int32.MinValue;
            foreach (var item in collect)
            {
                if (item>maxi)
                {
                    maxi = item;
                }
            }
            max[0] = maxi;
        }

        private static void Min(object results)
        {
            int[] min = results as int[];
            int mini = Int32.MaxValue;
            foreach (var item in collect)
            {
                if (item < mini)
                {
                    mini = item;
                }
            }
            min[1] = mini;
        }

        private static void Avg(object results)
        {
            int[] avg = results as int[];
            long avgi = 0;
            foreach (var item in collect)
            {
                avgi += item;
            }
            avg[2] = (int)(avgi / 10000);
        }
    }
}
