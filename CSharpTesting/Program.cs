using Medusa.Analyze1553B.Common;
using Olympus.Checkers;
using Olympus.Translation;
using System;
using System.IO;
using System.Linq;

namespace CSharpTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var Hex = "0F82";
            Hex = "FBF1";
            Hex = "0F82";
            Hex = "0B82";
            Hex = "0FC1";
            ushort Value = Convert.ToUInt16(Hex, 16);
            Console.WriteLine("{0} = {1}",Hex, Value);

            var Address = Value >> 11;
            Console.WriteLine("Address = {0}",Address);

            var Direction = (DataDirection)((Value >> 10) & 1);
            Console.WriteLine("Direction = {0}", Direction);

            var Subaddress = (Value >> 5) & 0x1F;
            Console.WriteLine("Subaddress = {0}", Subaddress);

            var Length = (Value & 0x1F).Replace(0, 32);
            Console.WriteLine("Length = {0}", Length);

            Console.WriteLine("----------------------");
            var cw1 = new ControlWord(Value);
            Console.WriteLine("Address = {0}", cw1.Address);
            Console.WriteLine("Direction = {0}", cw1.Direction);
            Console.WriteLine("Subaddress = {0}", cw1.Subaddress);
            Console.WriteLine("Length = {0}", cw1.Length);

            Console.WriteLine("----------------------");
            var cw2 = new ControlWord(Address,Direction,Subaddress,Length);
            Console.WriteLine("Address = {0}", cw2.Address);
            Console.WriteLine("Direction = {0}", cw2.Direction);
            Console.WriteLine("Subaddress = {0}", cw2.Subaddress);
            Console.WriteLine("Length = {0}", cw2.Length);

        }
    }
}
