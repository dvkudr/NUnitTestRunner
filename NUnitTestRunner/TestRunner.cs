using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace NUnitTestRunner
{
    public class TestRunner
    {
        private readonly Assembly _testAssembly;

        public TestRunner(Assembly testAssembly)
        {
            _testAssembly = testAssembly;
        }

        public void RunTests()
        {
            var testTypes = GetTestFixtureTypes();

            foreach (var testType in testTypes)
            {
                RunTestType(testType);
            }
        }

        private ICollection<Type> GetTestFixtureTypes()
        {
            return _testAssembly.ExportedTypes
                .Where(x => x.GetCustomAttributes<TestFixtureAttribute>().Any())
                .ToList();
        }


        private ICollection<MethodInfo> GetTestMethods(Type testType)
        {
            return testType.GetRuntimeMethods()
                .Where(x => x.GetCustomAttributes<TestAttribute>().Any() || x.GetCustomAttributes<TestCaseAttribute>().Any())
                .ToList();
        }

        private ICollection<TestCaseAttribute> GetTestCaseAttributes(MethodInfo testMethod)
        {
            return testMethod.GetCustomAttributes<TestCaseAttribute>().ToList();
        }

        private void RunTestType(Type testType)
        {
            var testMethods = GetTestMethods(testType);

            if (!testMethods.Any())
                return;

            var instance = Activator.CreateInstance(testType);

            foreach (var testMethod in testMethods)
            {
                var testCaseAttributes = GetTestCaseAttributes(testMethod);

                var arguments = testCaseAttributes.Any()
                    ? testCaseAttributes.Select(x => x.Arguments)
                    : Enumerable.Repeat(default(object[]), 1);

                foreach (var args in arguments)
                {
                    var argsString = args != null ? string.Join(", ", args) : string.Empty;
                    Console.WriteLine($"Run test method {testType.Name}.{testMethod.Name}({argsString})");

                    try
                    {
                        testMethod.Invoke(instance, args);
                        Console.WriteLine($"   success!!!");
                    }
                    catch (TargetInvocationException exception)
                    {
                        if (exception.InnerException is AssertionException)
                        {
                            Console.WriteLine(exception.InnerException.Message);
                        }
                        else
                        {
                            Console.WriteLine($"Unexpected exception: {exception.Message}");
                        }
                    }
                }
            }
        }
    }
}
