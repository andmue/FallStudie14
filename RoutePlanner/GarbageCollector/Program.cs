using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarbageCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            var watcher = new Stopwatch();
            long sum = 0;
            int rounds = 20;

            var writer = new StreamWriter("output.txt");

            for (int j = 0; j < rounds; j++)
            {
                watcher.Start();
                var bytes = new List<byte[]>();
                for (int i = 0; i < 50; i++)
                {
                    bytes.Add(new byte[1]);
                }
                bytes = null;

                GC.Collect();
                watcher.Stop();
                writer.WriteLine("Round {0}: Elapsed ticks = {1}", j, watcher.ElapsedTicks);
                Console.WriteLine("Round {0}: Elapsed ticks = {1}", j, watcher.ElapsedTicks);
                sum += watcher.ElapsedTicks;
                watcher.Reset();
            }
            writer.WriteLine("---------------------------------------");
            writer.WriteLine("Average: {0}", sum / rounds);
            Console.WriteLine("Average: {0}", sum / rounds);

            writer.Close();

            Console.ReadKey();
        }

        /*
         * Wir haben zwei Arten von Garbage Collectoren:
         *  1) Concurrent Collector enabled
         *  2) Concurrent Collector disabled
         *  
         * Entsprechende Konfiguration sind im App-Config getätigt worden.
         * 
         * Auswertung 1):
            Round 0: Elapsed ticks = 20266
            Round 1: Elapsed ticks = 13672
            Round 2: Elapsed ticks = 7618
            Round 3: Elapsed ticks = 63447
            Round 4: Elapsed ticks = 62942
            Round 5: Elapsed ticks = 56569
            Round 6: Elapsed ticks = 58629
            Round 7: Elapsed ticks = 57777
            Round 8: Elapsed ticks = 77237
            Round 9: Elapsed ticks = 80800
            Round 10: Elapsed ticks = 78579
            Round 11: Elapsed ticks = 75512
            Round 12: Elapsed ticks = 79210
            Round 13: Elapsed ticks = 80506
            Round 14: Elapsed ticks = 75974
            Round 15: Elapsed ticks = 110981
            Round 16: Elapsed ticks = 139751
            Round 17: Elapsed ticks = 140646
            Round 18: Elapsed ticks = 135473
            Round 19: Elapsed ticks = 129316
            ---------------------------------------
            Average: 77245
         *
         * Auswertung 2):
            Round 0: Elapsed ticks = 14829
            Round 1: Elapsed ticks = 15223
            Round 2: Elapsed ticks = 8926
            Round 3: Elapsed ticks = 9570
            Round 4: Elapsed ticks = 9657
            Round 5: Elapsed ticks = 6050
            Round 6: Elapsed ticks = 9606
            Round 7: Elapsed ticks = 6632
            Round 8: Elapsed ticks = 7942
            Round 9: Elapsed ticks = 9483
            Round 10: Elapsed ticks = 6430
            Round 11: Elapsed ticks = 6292
            Round 12: Elapsed ticks = 6022
            Round 13: Elapsed ticks = 6033
            Round 14: Elapsed ticks = 8331
            Round 15: Elapsed ticks = 8874
            Round 16: Elapsed ticks = 6891
            Round 17: Elapsed ticks = 6395
            Round 18: Elapsed ticks = 6228
            Round 19: Elapsed ticks = 8243
            ---------------------------------------
            Average: 8382
         *
         * Man sieht, dass wenn der Concurrent Collector deaktiviert ist, der Durchlauf des Programmes
         * um etwa den Faktor 10 schneller ist.
         * 
         * Bei grossen Objekten ist es also nicht umgedingt sinnvoll Concurrent Collector zu verwenden.
         */
    }
}
