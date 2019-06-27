using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Peleng.Medusa.Analyze1553B.Common.Tests
{
    [TestClass]
    public class WordsTest
    {
        [TestMethod]
        public void ControlWordBuilder()
        {
            var cw = new ControlWord(0b10101, DataDirection.RtTransmits, 0b11001, 1);
            Assert.AreEqual(0b10101_1_11001_00001, cw.Value);

            Assert.AreEqual(0b10101, cw.Address);
            Assert.AreEqual(DataDirection.RtTransmits, cw.Direction);
            Assert.AreEqual(0b11001, cw.Subaddress);
            Assert.AreEqual(1, cw.Length);
        }

        [TestMethod]
        public void ControlWordRaw()
        {
            var cw = new ControlWord(0b10101_1_11001_00001);
            Assert.AreEqual(0b10101_1_11001_00001, cw.Value);

            Assert.AreEqual(0b10101, cw.Address);
            Assert.AreEqual(DataDirection.RtTransmits, cw.Direction);
            Assert.AreEqual(0b11001, cw.Subaddress);
            Assert.AreEqual(1, cw.Length);
        }

        [TestMethod]
        public void ControlWordBuilder32()
        {
            var cw = new ControlWord(0b10101, DataDirection.RtReceives, 0b11001, 32);
            Assert.AreEqual(0b10101_0_11001_00000, cw.Value);

            Assert.AreEqual(0b10101, cw.Address);
            Assert.AreEqual(DataDirection.RtReceives, cw.Direction);
            Assert.AreEqual(0b11001, cw.Subaddress);
            Assert.AreEqual(32, cw.Length);
        }

        [TestMethod]
        public void ControlWordRaw32()
        {
            var cw = new ControlWord(0b10101_0_11001_00000);
            Assert.AreEqual(0b10101_0_11001_00000, cw.Value);

            Assert.AreEqual(0b10101, cw.Address);
            Assert.AreEqual(DataDirection.RtReceives, cw.Direction);
            Assert.AreEqual(0b11001, cw.Subaddress);
            Assert.AreEqual(32, cw.Length);
        }

        [TestMethod]
        public void Commands()
        {
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void ResponseWordRaw()
        {
            var rsp = new ResponseWord(0b10011_00000000001);
            Assert.AreEqual(0b10011, rsp.Address);
            Assert.AreEqual(1, rsp.Flags);

            var rsp2 = new ResponseWord(0b10011_10000000000);
            Assert.AreEqual(0b10011, rsp.Address);
            Assert.AreNotEqual(0, rsp.Flags);

        }
    }
}
