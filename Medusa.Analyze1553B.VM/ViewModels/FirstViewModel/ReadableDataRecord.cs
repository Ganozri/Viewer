
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VM.ViewModels
{
    public class ReadableMainModel : BaseMainModel
    {
        public int FirstNumber => dataRecord.FirstNumber;

        public int SecondNumber => dataRecord.SecondNumber;
        public int ThirdNumber => dataRecord.ThirdNumber;
        public C c { get; set; }

        private readonly MainModel dataRecord;
        public ReadableMainModel(MainModel dataRecord) : base(dataRecord)
        {
            this.dataRecord = dataRecord;
            //
            c = new C();
            //
        }
    }
    //
    public class A
    {
        public int x { get; set; }
        public B b { get;set;}

        public A()
        {
            x = 1;
            b = new B();
        }
    }
    public class B
    {
        public int x { get; set; }
        public C c { get; set; }

        public B()
        {
            x = 2;
            c = new C();
        }
    }
    public class C
    {
        public int x { get; set; }
        public C()
        {
            x = 3;
        }
    }
    //
}
