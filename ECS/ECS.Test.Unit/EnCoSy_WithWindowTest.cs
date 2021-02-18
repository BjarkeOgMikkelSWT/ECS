using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace ECS.Test.Unit
{
    public class StubWindow : IWindow
    {
        private readonly bool _selfTestRes;
        public StubWindow(bool selfTestRes = true)
        {
            _selfTestRes = selfTestRes;
        }
        public void close()
        { }

        public void open()
        {
        }

        public bool self_test()
        {
            return _selfTestRes;
        }
    }
    public class EnCoSy_WithWindowTest
    {
        private ITempSensor _tempSensor;
        private IHeater _heater;
        private IWindow _window;
        private EnCoSy_with_window _myEnCoSy_with_window;

        [SetUp]
        public void SetUp()
        {
            _tempSensor = new StubTempSensor();
            _heater = new StubHeater();
            _window = new StubWindow();
            _myEnCoSy_with_window = new EnCoSy_with_window(15, 25, _tempSensor, _heater, _window);
        }

        [TestCase(-50, -49, Description = "Negative and negative, thrHeat lowest")]
        [TestCase(-50, 0, Description = "Negative and zero, thrHeat lowest")]
        [TestCase(-50, 49, Description = "Negative and positive, thrHeat lowest")]
        [TestCase(0, 1, Description = "Zero and close positive, thrHeat lowest")]
        [TestCase(0, 49, Description = "Zero and positive, thrHeat lowest")]
        [TestCase(50, 149, Description = "positive and positive, thrHeat lowest")]
        public void EnCoSy_with_windowConstructorTestNoException(int thrHeat, int thrWindow)
        {
            Assert.DoesNotThrow(() => new EnCoSy_with_window(thrHeat, thrWindow, _tempSensor, _heater, _window));
        }

        [TestCase(-50, -49, Description = "Negative and negative, thrWindow lowest")]
        [TestCase(-50, 0, Description = "Negative and zero, thrWindow lowest")]
        [TestCase(-50, 49, Description = "Negative and positive, thrWindow lowest")]
        [TestCase(0, 1, Description = "Zero and close positive, thrWindow lowest")]
        [TestCase(0, 49, Description = "Zero and positive, thrWindow lowest")]
        [TestCase(50, 149, Description = "Positive and positive, thrWindow lowest")]
        [TestCase(-3, -3, Description = "Negative and Negative, thr is equal")]
        [TestCase(0, 0, Description = "Zero and Zero, thr is equal")]
        [TestCase(50, 50, Description = "Positive and Positive, thr is equal")]
        public void EnCoSy_with_windowConstructorTestThrowsArguemtException(int thrWindow, int thrHeat)
        {
            Assert.Throws<ArgumentException>(() => new EnCoSy_with_window(thrHeat, thrWindow, _tempSensor, _heater, _window), "Window threshold must be greater than heater threshold!");
        }

        [TestCase(-50, -49, Description = "Negative and negative, thrHeat lowest")]
        [TestCase(-50, 0, Description = "Negative and zero, thrHeat lowest")]
        [TestCase(-50, 49, Description = "Negative and positive, thrHeat lowest")]
        [TestCase(0, 1, Description = "Zero and close positive, thrHeat lowest")]
        [TestCase(0, 49, Description = "Zero and positive, thrHeat lowest")]
        [TestCase(50, 149, Description = "positive and positive, thrHeat lowest")]
        public void EnCoSy_with_windowSetThresholdTestNoException(int thrHeat, int thrWindow)
        {
            Assert.DoesNotThrow(() => _myEnCoSy_with_window.SetThreshold(thrHeat, thrWindow), "Window threshold must be greater than heater threshold!");
        }

        [TestCase(-50, -49, Description = "Negative and negative, thrWindow lowest")]
        [TestCase(-50, 0, Description = "Negative and zero, thrWindow lowest")]
        [TestCase(-50, 49, Description = "Negative and positive, thrWindow lowest")]
        [TestCase(0, 1, Description = "Zero and close positive, thrWindow lowest")]
        [TestCase(0, 49, Description = "Zero and positive, thrWindow lowest")]
        [TestCase(50, 149, Description = "Positive and positive, thrWindow lowest")]
        [TestCase(-3, -3, Description = "Negative and Negative, thr is equal")]
        [TestCase(0, 0, Description = "Zero and Zero, thr is equal")]
        [TestCase(50, 50, Description = "Positive and Positive, thr is equal")]
        public void EnCoSy_with_windowSetThresholdTestThrowsArgumentException(int thrWindow, int thrHeat)
        {
            Assert.Throws<ArgumentException>(() => _myEnCoSy_with_window.SetThreshold(thrHeat, thrWindow), "Window threshold must be greater than heater threshold!");
        }

        [TestCase(-50, -49, Description = "Negative and negative, thrHeat lowest")]
        [TestCase(-50, 0, Description = "Negative and zero, thrHeat lowest")]
        [TestCase(-50, 49, Description = "Negative and positive, thrHeat lowest")]
        [TestCase(0, 1, Description = "Zero and close positive, thrHeat lowest")]
        [TestCase(0, 49, Description = "Zero and positive, thrHeat lowest")]
        [TestCase(50, 149, Description = "positive and positive, thrHeat lowest")]
        public void EnCoSy_with_windowGetThresholdTestReturnsArguments(int thrHeat, int thrWindow)
        {
            //Arrange
            _myEnCoSy_with_window.SetThreshold(thrHeat, thrWindow);

            Assert.That(_myEnCoSy_with_window.GetThreshold(), Is.EqualTo(new [] {thrHeat, thrWindow}));
        }


        [TestCase(5, 6, false, Description = "Positive Thr higher than positive temp")]
        [TestCase(-5, -10, true, Description = "Negative numbers thr bellow temp")]
        [TestCase(5, 5, false, Description = "Positive, Equal temp and thr")]
        [TestCase(11, 10, true, Description = "Temp higher then thr")]
        [TestCase(-5, 10, false, Description = "Negative temp and positive thr")]
        [TestCase(-5, -5, false, Description = "Negative, Equal temp and thr")]
        [TestCase(0, 0, false, Description = "Zero, Equal temp and thr")]

        [TestCase(11, 0, true, Description = "Temp higher then thr, thr zero, temp pos")]
        [TestCase(-5, 0, false, Description = "Temp higher then thr, thr zero, temp neg")]
        public void EnCoSy_with_windowRegulateWindowCorrectly(int temp, int thrWindow, bool openClose)
        {
            //Arrange
            var dummyWindow = Substitute.For<IWindow>();
            
            _tempSensor = new StubTempSensor(temp);

            _myEnCoSy_with_window = new EnCoSy_with_window(int.MinValue, thrWindow, _tempSensor, _heater, dummyWindow);

            //Action
            _myEnCoSy_with_window.Regulate();

            //Assert
            if (openClose)
            {
                dummyWindow.Received().open();
            }
            else
            {
                dummyWindow.Received().close();
            }
        }

        [TestCase(false, false, false, Description = "All false")]
        [TestCase(true, false, false, Description = "Sensor is true")]
        [TestCase(false, true, false, Description = "Heater is true")]
        [TestCase(true, true, false, Description = "Heater and Sensor is true")]

        [TestCase(false, false, true, Description = "Window true")]
        [TestCase(true, false, true, Description = "Sensor and Window true")]
        [TestCase(false, true, true, Description = "Heater and Window true")]
        [TestCase(true, true, true, Description = "All true")]
        public void RunSelfTestReturnsCorrect(bool sensorRes, bool heatTest, bool windowTest)
        {
            //Arange
            _heater = new StubHeater(heatTest);
            _tempSensor = new StubTempSensor(0, sensorRes);
            _window = new StubWindow(windowTest);
            _myEnCoSy_with_window = new EnCoSy_with_window(0, 10, _tempSensor, _heater, _window);

            //Action
            //Assert
            Assert.That(_myEnCoSy_with_window.RunSelfTest(), Is.EqualTo(sensorRes && heatTest && windowTest));
        }



        [TearDown]
        public void TearDown()
        {

        }
    }
}
