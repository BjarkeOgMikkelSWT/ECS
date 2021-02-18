using NUnit.Framework;
using NSubstitute;
namespace ECS.Test.Unit
{
    public class StubHeater : IHeater
    {
        private bool _result;
        public StubHeater(bool SelfResult =true)
        {
            _result = SelfResult;
        }
        public bool RunSelfTest()
        {
            return _result;
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
        private bool _result;

        public StubTempSensor(int temp=0,bool SelfResult =true)
        {
            _temp = temp;
            _result = SelfResult;
        }

        public int GetTemp()
        {
            return _temp;
        }

        public bool RunSelfTest()
        {
            return _result;
        }
    }

    public class Tests
    {
        private EnCoSy _myECS;
        private IHeater _myHeater;
        private ITempSensor _myTempSensor;
        [SetUp]
        public void Setup()
        {
            _myHeater = new StubHeater();
            _myTempSensor = new StubTempSensor();
            _myECS = new EnCoSy(25, _myTempSensor, _myHeater);

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

        [TestCase(false,false,Description = "Both returns false")]
        [TestCase(true,false,Description = "Sensor is true and Heater is false")]
        [TestCase(false,true,Description = "Sensor is false and Heater is true")]
        [TestCase(true,true,Description = "Both returns true")]
        public void RunSelfTestReturnsCorrect(bool SensorRes, bool heatTest)
        {
            //Arange
            _myHeater = new StubHeater(heatTest);
            _myTempSensor = new StubTempSensor(0,SensorRes);
            _myECS = new EnCoSy(1, _myTempSensor, _myHeater);

            //Action
            //Assert
            Assert.That(_myECS.RunSelfTest(), Is.EqualTo(SensorRes && heatTest));
        }

        [TestCase(1,1)]
        [TestCase(1,0)]
        [TestCase(1,-30)]
        public void GetCurTempReturnsCurTemp(int thr, int temp)
        {
            _myTempSensor = new StubTempSensor(temp);
            _myECS = new EnCoSy(thr, _myTempSensor, _myHeater);
            Assert.That(_myECS.GetCurTemp(), Is.EqualTo(temp));
        }

        [TestCase(5,6,true,Description = "Positive Thr higher then positive temp")]
        [TestCase(-5, -10, false, Description = "Negative numbers thr bellow temp")]
        [TestCase(5, 5, false, Description = "Equel temp and thr")]
        [TestCase(11, 10, false, Description = "Temp higher then thr")]
        [TestCase(-5, 10, true,Description = "Negative temp and positive thr")]
        public void RegulateRegulaesCorrect(int temp,int thr,bool On_off)
        {
            //Arrange
            var _dummyHeater = Substitute.For<IHeater>();

            _myTempSensor = new StubTempSensor(temp);

            _myECS = new EnCoSy(thr, _myTempSensor, _dummyHeater);
            _myECS.SetThreshold(thr);

            //Action
            _myECS.Regulate();

            //Assert
            if(On_off)
            {
                _dummyHeater.Received().TurnOn();
            }
            else
            {
                _dummyHeater.Received().TurnOff();
            }
            
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