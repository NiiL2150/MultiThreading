using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
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

            ChooseTask();
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

        private static void ChooseTask()
        {
            Console.WriteLine("1 for first task, 2 for second, 3 for loading last values");
            int z = int.Parse(Console.ReadLine());
            if (z == 1)
            {
                Task1();
            }
            else if (z == 2)
            {
                Task2();
            }
            else if (z == 3)
            {
                Task3();
            }
        }

        private static void Task1()
        {
            ParameterizedThreadStart start = new ParameterizedThreadStart(ShowNabor);
            Console.WriteLine("Enter the number of threads");
            int threadCount = int.Parse(Console.ReadLine());

            int[] StartEnd = new int[3];

            Console.WriteLine("Enter starting number");
            StartEnd[0] = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter ending number");
            StartEnd[1] = int.Parse(Console.ReadLine());

            for (int i = 0; i < threadCount; i++)
            {
                StartEnd[2] = i;
                Thread thread1 = new Thread(start);
                thread1.Start(StartEnd);
            }
        }

        private static void Task2()
        {
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

            ParameterizedThreadStart start4 = new ParameterizedThreadStart(ShowConsole);
            Thread thread4 = new Thread(start4);
            thread4.Start(collect);

            ParameterizedThreadStart start5 = new ParameterizedThreadStart(CopyToFile);
            Thread thread5 = new Thread(start5);
            thread5.Start(collect);

            Thread.Sleep(10000);

            Console.WriteLine($"\nMax = {results[0]}");
            Console.WriteLine($"Min = {results[1]}");
            Console.WriteLine($"Avg = {results[2]}");
        }

        private static void Task3()
        {
            collect = new int[10000];
            ParameterizedThreadStart start = new ParameterizedThreadStart(ReadFromFile);
            Thread thread = new Thread(start);
            thread.Priority = ThreadPriority.AboveNormal;
            thread.Start(collect);

            int[] results = new int[3];

            Thread.Sleep(1000);

            ParameterizedThreadStart start1 = new ParameterizedThreadStart(Max);
            Thread thread1 = new Thread(start1);
            thread1.Start(results);

            ParameterizedThreadStart start2 = new ParameterizedThreadStart(Min);
            Thread thread2 = new Thread(start2);
            thread2.Start(results);

            ParameterizedThreadStart start3 = new ParameterizedThreadStart(Avg);
            Thread thread3 = new Thread(start3);
            thread3.Start(results);

            ParameterizedThreadStart start4 = new ParameterizedThreadStart(ShowConsole);
            Thread thread4 = new Thread(start4);
            thread4.Start(collect);

            ParameterizedThreadStart start5 = new ParameterizedThreadStart(CopyToFile);
            Thread thread5 = new Thread(start5);
            thread5.Start(collect);

            Thread.Sleep(10000);

            Console.WriteLine($"\nMax = {results[0]}");
            Console.WriteLine($"Min = {results[1]}");
            Console.WriteLine($"Avg = {results[2]}");
        }

        private static void ShowNabor(object startEnd)
        {
            int[] StartEnd = startEnd as int[];

            //Copy of the variable
            int thread = StartEnd[2];

            for (int i = StartEnd[0]; i < StartEnd[1]; i++)
            {
                Console.WriteLine($"Thread {thread} - {i}");
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

        private static void ShowConsole(object coll)
        {
            int[] colle = coll as int[];
            foreach (var item in colle)
            {
                Console.WriteLine(item);
            }
        }

        private static void CopyToFile(object coll)
        {
            int[] colle = coll as int[];
            string path = Environment.CurrentDirectory + @"\savefile.txt";

            using (var writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                foreach (var item in colle)
                {
                    writer.Write(item);
                }
            }
        }

        private static void ReadFromFile(object coll)
        {
            string path = Environment.CurrentDirectory + @"\savefile.txt";

            if (!File.Exists(path))
            {
                return;
            }

            int[] colle = coll as int[];

            using (var reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                for (int i = 0; i < 10000; i++)
                {
                    colle[i] = reader.ReadInt32();
                }
            }
        }
    }
}
