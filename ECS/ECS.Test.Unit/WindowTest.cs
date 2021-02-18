using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ECS.Test.Unit
{
    public class WindowTest
    {
        private IWindow _window;

        [SetUp]
        public void SetUp()
        {
            _window = new Window();
        }

        [TestCase("Window is closed\r\n")]
        public void WindowCloseIsString(string expected)
        {
            using var consoleOutput = new ConsoleOutput();
            _window.close();

            Assert.AreEqual(expected, consoleOutput.GetOuput() );
        }

        [TestCase("Window is not closed\r\n")]
        [TestCase("Window is   closed\r\n")]
        [TestCase("Window is very closed\r\n")]
        public void WindowCloseIsNotOtherString(string expected)
        {
            using var consoleOutput = new ConsoleOutput();
            _window.close();

            Assert.AreNotEqual(expected, consoleOutput.GetOuput());
        }

        [TestCase("Window is open\r\n")]
        public void WindowOpenIsString(string expected)
        {
            using var consoleOutput = new ConsoleOutput();
            _window.open();

            Assert.AreEqual(expected, consoleOutput.GetOuput());
        }

        [TestCase("Window is not Open\r\n")]
        [TestCase("Window is   Open\r\n")]
        [TestCase("Window is very Open\r\n")]
        public void WindowOpenIsNotOtherString(string expected)
        {
            using var consoleOutput = new ConsoleOutput();
            _window.open();

            Assert.AreNotEqual(expected, consoleOutput.GetOuput());
        }

        [Test]
        public void WindowSelfTest()
        {
            Assert.IsTrue(_window.self_test());
        }

        [TearDown]
        public void TearDown()
        {

        }


    }
}
