using System;

namespace Model
{
    public class MainModel
    {
        public int FirstNumber { get;set;}
        public int SecondNumber { get; set; }
        public int ThirdNumber { get; set; }

        public MainModel()
        {
            //Создание объекта для генерации чисел
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                FirstNumber  = rnd.Next(0, 10);
                SecondNumber = rnd.Next(0, 10);
                ThirdNumber  = rnd.Next(0, 10);
            }
        }

    }
}
