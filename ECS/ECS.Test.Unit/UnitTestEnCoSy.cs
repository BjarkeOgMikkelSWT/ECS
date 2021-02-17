using NUnit.Framework;

namespace ECS.Test.Unit
{
    internal class StubHeater : IHeater
    {
        public bool RunSelfTest()
        {
            return true;
        }

        public void TurnOff()
        {
            throw new System.NotImplementedException();
        }

        public void TurnOn()
        {
            throw new System.NotImplementedException();
        }
    }

    internal class StubTempSensor : ITempSensor
    {
        public int GetTemp()
        {
            return 5;
        }

        public bool RunSelfTest()
        {
            return true;
        }
    }

    public class Tests
    {
        private IEnCoSy _myECS;
        private IHeater _myHeater;
        private ITempSensor _myTempSensor;
        [SetUp]
        public void Setup()
        {
            _myHeater = new Heater();
            _myTempSensor = new TempSensor();
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-100)]
        public void ECSSetsThressHoldToInput(int thr)
        {
            //Arange
            //Action
            _myECS = new EnCoSy(thr, _myTempSensor, _myHeater);

            //Assert
            Assert.That(_myECS.GetThreshold(),Is.EqualTo(thr));
        }

        [Test]
        public void RunSelfTestReturnsCorrect()
        {
            //Arange
            _myECS = new EnCoSy(1, _myTempSensor, _myHeater);

            //Action
            bool ok = _myECS.RunSelfTest();

            bool sensOk = _myTempSensor.RunSelfTest();
            bool heatOK = _myHeater.RunSelfTest();
            bool depOK = sensOk && heatOK;

            Assert.That(ok, Is.EqualTo(depOK));
        }

        [Test]
        public void GetCurTempReturnsCurTemp()
        {

        }

        [TearDown]
        public void TearDown()
        {
            _myECS = null;
            _myHeater = null;
            _myTempSensor = null;
        }
    }
}