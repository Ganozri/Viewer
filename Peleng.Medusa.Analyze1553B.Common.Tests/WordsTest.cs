using System;
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
            var cw1 = new ControlWord(0b11111, DataDirection.RtReceives, CommandCode.Raw.SynchronizeWithDataWord, CommandSubaddress.Ones);
            Assert.AreEqual(0b11111_0_11111_10001, cw1.Value);
            Assert.AreEqual(CommandCode.Raw.SynchronizeWithDataWord, cw1.CommandCode);

            var cw2 = new ControlWord(0b01010, CommandCode.WithDirection.TransmitBuiltInTestWord, CommandSubaddress.Zero);
            Assert.AreEqual(0b01010_1_00000_10011, cw2.Value);
            Assert.AreEqual(CommandCode.Raw.TransmitBuiltInTestWord, cw2.CommandCode);
        }

        [TestMethod]
        public void ResponseWordRaw()
        {
            var rsp = new ResponseWord(0b10011_00000000001);
            Assert.AreEqual(0b10011, rsp.Address);
            Assert.AreEqual(1, rsp.Flags);

            var rsp2 = new ResponseWord(0b10011_10000000000);
            Assert.AreEqual(0b10011, rsp2.Address);
            Assert.AreNotEqual(0, rsp2.Flags);
        }

        [TestMethod]
        public void CheckNotACommand()
        {
            var cw = new ControlWord(1, DataDirection.RtReceives, 1, 5);
            Assert.ThrowsException<InvalidOperationException>(() => cw.CommandCode);
        }
    }
}
