using Medusa.Analyze1553B.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VM
{
    public class Rule
    {
        public int Address { get; set; }
        public DataDirection Direction { get; set; }
        public int Subaddress { get; set; }
        public int Length { get; set; }

        public string Name { get; set; }

        public Rule(int Address, DataDirection Direction, int Subaddress, int Length)
        {
            new Rule(Address, Direction, Subaddress, Length, "");
        }

        public Rule(int Address, DataDirection Direction, int Subaddress, int Length, string Name)
        {
            this.Address = Address;
            this.Direction = Direction;
            this.Subaddress = Subaddress;
            this.Length = Length;

            this.Name = Name;
        }

    }
}
