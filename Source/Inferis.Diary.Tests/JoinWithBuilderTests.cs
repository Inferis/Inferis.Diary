using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Inferis.Diary.Tests {
    [TestFixture]
    public class JoinWithBuilderTests {
        [Test]
        public void Test()
        {
            const int size = 100000;
            var rnd = new Random();
            var source2 = new string[size];
            for (var i = 0; i < size; ++i) {
                source2[i] = Guid.NewGuid().ToString("N");
            }

            var source = source2.ToList();

            string result1 = null, result2 = null, result3 = null;
            Test(100, new Dictionary<string, Action> { 
                { "string.Join", () => result2 = string.Join("\n", source)},
                {"JoinWithBuilder2", () => result3 = source.JoinWithBuilder2("\n") },
                {"JoinWithBuilder", () => result1 = source.JoinWithBuilder("\n")}
            });

            Assert.AreEqual(result1, result2);
            Assert.AreEqual(result1, result3);
        }

        private void Test(int count, Dictionary<string, Action> tests)
        {
            var elapsed = tests.ToDictionary(k => k.Key, v => new List<long>());
            var mem = tests.ToDictionary(k => k.Key, v => new List<long>());

            for (var i=0; i<count; ++i) {
                var ordered = tests.Keys.Randomize().ToArray();
                Console.WriteLine("interation " + i + ": " + string.Join(", ", ordered));

                foreach (string t in ordered) {
                    var timer = new Stopwatch();
                    GC.Collect();
                    GC.WaitForFullGCComplete();

                    var proc = Process.GetCurrentProcess();
                    Thread.MemoryBarrier();
                    var memoryStart = GC.GetTotalMemory(true);
                    timer.Start();
                    tests[t]();
                    timer.Stop();

                    Thread.MemoryBarrier();
                    elapsed[t].Add(timer.ElapsedTicks);
                    mem[t].Add(GC.GetTotalMemory(true) - memoryStart);
                }
            }

            foreach (var test in tests.Keys) {
                Console.WriteLine(test + ": " + elapsed[test].Average() + ", " + mem[test].Average());
            }
        }

        private void Test(string name, Action call)
        {
            var timer = new Stopwatch();
            GC.Collect();
            GC.WaitForFullGCComplete();

            var proc = Process.GetCurrentProcess();
            Thread.MemoryBarrier();
            var pmem = proc.VirtualMemorySize64;
            var memoryStart = System.GC.GetTotalMemory(true);
            timer.Start();
            call();
            timer.Stop();

            Thread.MemoryBarrier();
            Console.WriteLine(name + ": " + timer.ElapsedTicks + ", " + (System.GC.GetTotalMemory(true) - memoryStart) + ", " + (proc.VirtualMemorySize64 - pmem));
        }
    }

    public static class RandomExtensions
    {
        private static readonly Random rnd;

        static RandomExtensions()
        {
            rnd = new Random();
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> sequence)
        {
            var values = sequence.ToArray();
            var indexes = Enumerable.Range(0, values.Length).ToList();

            while (indexes.Any()) {
                var metaindex = rnd.Next(indexes.Count);
                var index = indexes[metaindex];
                indexes.RemoveAt(metaindex);
                yield return values[index];
            }

            yield break;
        }
    }
}
