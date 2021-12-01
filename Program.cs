using System;
using System.Collections.Generic;

namespace Snake_and_Ladder
{
    class Program
    {
        public Dictionary<int, int> Snake = new Dictionary<int, int>()
        {
            {17, 7},{54, 34},{62, 19},{64, 60},{87, 24},{93, 73},{95, 75},{99, 78}
        };
        public Dictionary<int, int> ladder = new Dictionary<int, int>()
        {
            {4, 14},{9, 31},{20, 38},{28, 84},{40, 59},{51, 67},{63, 81},{71, 91}
        };
        public int rendice()
        {
            var rand = new Random();
            int dice = rand.Next(1, 7);
            return dice;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("============= Snake And Ladder =============\n");
            Console.WriteLine("Lets Start ???\n(Reply y For Yes)");
            string UserRespo = Console.ReadLine();
            int position = 0;

            while(position <= 100 && UserRespo == "y")
            {
                Program a = new Program();
                int rndval = a.rendice();
                Console.WriteLine("Dice Value : " + rndval);
                if (rndval == 6)
                {
                    position += rndval;
                    Console.WriteLine("You Got 6 Roll Again");
                    rndval = a.rendice();
                    Console.WriteLine("Dice Value : " + rndval);
                }
                position += rndval;
                if (a.ladder.ContainsKey(position))
                {
                    Console.WriteLine("You Got A ladder");
                    position = a.ladder[position];
                }
                else if (a.Snake.ContainsKey(position))
                {
                    Console.WriteLine("You Got Eaten By The Snake");
                    position = a.Snake[position];
                }
                Console.WriteLine("You Are On " + position + " Position.\n");

                if(position<100)
                {
                    Console.WriteLine("Do You Want To Roll Again ??\n(If Yes Enter y & If No Enter n");
                    UserRespo = Console.ReadLine();
                }
                else Console.WriteLine("Congratulations You Won !!!!!!");
            }
            Console.WriteLine("Bye!!!!!");
        }
    }
}
