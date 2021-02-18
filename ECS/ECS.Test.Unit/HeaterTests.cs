using System;
using System.IO;
using NUnit.Framework;

namespace ECS.Test.Unit
{
    public class ConsoleOutput : IDisposable
    {
        private readonly StringWriter _stringWriter;
        private readonly TextWriter _originalOutput;

        public ConsoleOutput()
        {
            _stringWriter = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_stringWriter);
        }

        public string GetOuput()
        {
            return _stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(_originalOutput);
            _stringWriter.Dispose();
        }
    }

    public class HeaterTests
    {
        private IHeater _heater;
        [SetUp]
        public void Setup()
        {
            _heater = new Heater();
        }
        
        [TestCase("Heater is on\r\n", TestName = "Expected string is printed to console")]
        public void HeaterTurnOnTestWritesToConsoleCorrect(string expected)
        {
            using var consoleOutput = new ConsoleOutput();

            _heater.TurnOn();
            Assert.AreEqual(expected, consoleOutput.GetOuput());
        }

        [TestCase("AnyOtherString")]
        [TestCase("Another string")]
        [TestCase("Will I fail?")]
        public void HeaterTurnOnTestWritesOtherStringsToConsoleFails(string expected)
        {
            using var consoleOutput = new ConsoleOutput();

            _heater.TurnOn();
            Assert.AreNotEqual(expected, consoleOutput.GetOuput());
        }

        [TestCase("Heater is off\r\n", TestName = "Expected string is printed to console")]
        public void HeaterTurnOffTestWritesToConsoleCorrect(string expected)
        {
            using var consoleOutput = new ConsoleOutput();

            _heater.TurnOff();
            Assert.AreEqual(expected, consoleOutput.GetOuput());
        }

        [TestCase("Maybe off will work")]
        [TestCase("Another string")]
        [TestCase("Will I fail?")]
        public void HeaterTurnOffTestWritesOtherStringsToConsoleFails(string expected)
        {
            using var consoleOutput = new ConsoleOutput();

            _heater.TurnOff();
            Assert.AreNotEqual(expected, consoleOutput.GetOuput());
        }

        [Test]
        public void HeaterRunSelfTestReturnsTrue()
        {
            Assert.IsTrue(_heater.RunSelfTest());
        }
        

        [TearDown]
        public void TearDown()
        {
        }
    }
}