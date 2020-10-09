using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSAlignmentClassLibrary.Util;

namespace MSAlignmentUnitTest
{
    [TestClass]
    public class UtilTest
    {
        [TestMethod]
        public void MZCalcTestMethod()
        {
            List<double> mzList = IonMassCalc.Instance.ComputeMZ(470.2727);
            Assert.IsTrue(mzList.Count > 0);

            Assert.IsTrue(mzList.Any(x => Math.Abs(x - 253.17062) < 0.001));




        }
    }
}
