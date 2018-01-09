using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceModule.Fuzzy.Tests
{
    [TestClass]
    public class FuzzyLogicTests
    {
        FuzzyLogic fuzzy;

        [TestInitialize]
        public void SetUp()
        {
            fuzzy = new FuzzyLogic();
        }
        [TestMethod]
        public void FuzzyLogic_FuzzificationInput_ShouldSetSlow()
        {
            fuzzy.FuzzificationInput(101);
            Assert.AreEqual(fuzzy.fuzzificationInput[0], 1);
            Assert.AreEqual(fuzzy.fuzzificationInput[1], 0);
            Assert.AreEqual(fuzzy.fuzzificationInput[2], 0);
        }

        [TestMethod]
        public void FuzzyLogic_FuzzificationInput_ShouldSetMiddle()
        {
            fuzzy.FuzzificationInput(551);
            Assert.AreEqual(fuzzy.fuzzificationInput[0], 0);
            Assert.AreEqual(fuzzy.fuzzificationInput[1], 1);
            Assert.AreEqual(fuzzy.fuzzificationInput[2], 0);
        }

        [TestMethod]
        public void FuzzyLogic_FuzzificationInput_ShouldSetFast()
        {
            fuzzy.FuzzificationInput(1001);
            Assert.AreEqual(fuzzy.fuzzificationInput[0], 0);
            Assert.AreEqual(fuzzy.fuzzificationInput[1], 0);
            Assert.AreEqual(fuzzy.fuzzificationInput[2], 1);
        }

        [TestMethod]
        public void FuzzyLogic_FuzzificationOutput_ShouldSetBig()
        {
            fuzzy.FuzzificationOutput(1500);
            Assert.AreEqual(fuzzy.fuzzificationOutput[0], 1);
            Assert.AreEqual(fuzzy.fuzzificationOutput[1], 0);
            Assert.AreEqual(fuzzy.fuzzificationOutput[2], 0);
        }

        [TestMethod]
        public void FuzzyLogic_FuzzificationOutput_ShouldSetMiddle()
        {
            fuzzy.FuzzificationOutput(2500);
            Assert.AreEqual(fuzzy.fuzzificationOutput[0], 0);
            Assert.AreEqual(fuzzy.fuzzificationOutput[1], 1);
            Assert.AreEqual(fuzzy.fuzzificationOutput[2], 0);
        }

        [TestMethod]
        public void FuzzyLogic_FuzzificationOutput_ShouldSetSmal()
        {
            fuzzy.FuzzificationOutput(3500);
            Assert.AreEqual(fuzzy.fuzzificationOutput[0], 0);
            Assert.AreEqual(fuzzy.fuzzificationOutput[1], 0);
            Assert.AreEqual(fuzzy.fuzzificationOutput[2], 1);
        }
    }
}