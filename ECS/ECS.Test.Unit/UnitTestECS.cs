using NUnit.Framework;

namespace ECS.Test.Unit
{
    public class Tests
    {
        private IECS _myECS;
        private IHeater _myHeater;
        private ITempSensor _myTempSensor;
        [SetUp]
        public void Setup()
        {
            _myHeater = new IHeater();
            _myTempSensor = new ITempSensor();
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-100)]
        public void ECSSetsThressHoldToInput(int thr)
        {
            //Arange
            //Action
            _myECS = new ECS(thr, _myTempSensor, _myHeater);

            //Assert
            Assert.That(_myECS.GetThreshold(),Is.EqualTo(thr));
        }

        [Test]
        public void RunSelfTestReturnsCorrect()
        {
            //Arange
            _myECS = new ECS(1, _myTempSensor, _myHeater);

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