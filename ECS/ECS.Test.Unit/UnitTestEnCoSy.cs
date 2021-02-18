using NUnit.Framework;

namespace ECS.Test.Unit
{
    public class StubHeater : IHeater
    {
        public bool RunSelfTest()
        {
            return true;
        }

        public void TurnOff()
        {
        }

        public void TurnOn()
        {
        }
    }

    public class StubTempSensor : ITempSensor
    {
        private int _temp;
        public StubTempSensor(int temp)
        {
            _temp = temp;
        }

        public int GetTemp()
        {
            return _temp;
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
            _myHeater = new StubHeater();
        }

        [TestCase(10)]
        [TestCase(0)]
        [TestCase(-100)]
        public void ECSSetsThressHoldToInput(int thr)
        {
            //Action
            _myECS = new EnCoSy(thr, _myTempSensor, _myHeater);

            //Assert
            Assert.That(_myECS.GetThreshold(),Is.EqualTo(thr));
        }

        [TestCase(10)]
        [TestCase(0)]
        [TestCase(-10)]
        public void RunSelfTestReturnsCorrect(int temp)
        {
            //Arange
            _myTempSensor = new StubTempSensor(temp);

            _myECS = new EnCoSy(1, _myTempSensor, _myHeater);

            //Action
            bool ok = _myECS.RunSelfTest();

            bool sensOk = _myTempSensor.RunSelfTest();
            bool heatOK = _myHeater.RunSelfTest();
            bool depOK = sensOk && heatOK;

            Assert.That(ok, Is.EqualTo(depOK));
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-30)]
        public void GetCurTempReturnsCurTemp(int temp)
        {
            _myTempSensor = new StubTempSensor(temp);
            Assert.That(_myECS.GetCurTemp(), Is.EqualTo(temp));
        }

        [TestCase(5,10,true)]
        public void RegulateRegulaesCorrect(int temp,int thr,bool On_off)
        {
            //Arrange
            _myTempSensor = new StubTempSensor(temp);
            _myECS.SetThreshold(thr);

            //Action
            _myECS.Regulate();

            //Assert
            Assert.That(_myECS._Heater.Received().TurnOn(), Is.EqualTo(On_off));
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