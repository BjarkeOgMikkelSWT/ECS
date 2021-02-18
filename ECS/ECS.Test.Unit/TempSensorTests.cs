using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ECS.Test.Unit
{
    public class TempSensorTests
    {
        private TempSensor _tempSensor;

        [SetUp]
        public void SetUp()
        {
            _tempSensor = new TempSensor();
        }

        /*
         *  There is no test for the int GetTemp() method.
         *  This is because it returns a value from the standard implemented Random class
         *  and has no logic. The standard library is tested by microsoft and we assume that
         *  it is tested thoroughly. Therefore there is no need for at test of a function
         *  that just returns the value from Random, with no logic.
         */

        [Test]
        public void TempSensorSelfTestReturnsTrue()
        {
            Assert.IsTrue(_tempSensor.RunSelfTest());
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
