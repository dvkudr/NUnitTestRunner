using System;
using System.Reflection;

namespace NUnitTestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var testAssembly = Assembly.LoadFile(args[0]);

            var testRunner = new TestRunner(testAssembly);

            testRunner.RunTests();

            Console.ReadLine();
        }
    }
}
