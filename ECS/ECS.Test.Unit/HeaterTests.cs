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

        [Test]
        public void HeaterTurnOnTestWritesToConsole()
        {
            using var consoleOutput = new ConsoleOutput();
            _heater.TurnOn();

            Assert.AreEqual("Heater is on\r\n", consoleOutput.GetOuput());
        }

        [Test]
        public void HeaterTurnOffTestWritesToConsole()
        {
            using var consoleOutput = new ConsoleOutput();
            _heater.TurnOff();

            Assert.AreEqual("Heater is off\r\n", consoleOutput.GetOuput());
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}