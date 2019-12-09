using System;
using System.Collections.Generic;
using System.Linq;


namespace Snake
{
    class Program
    {
        //Alap egység definiálása
        class sprite
        {
            public int xpos { get; set; }
            public int ypos { get; set; }
            public ConsoleColor mycolor { get; set; }
        }
        static void Main(string[] args)
        {
            //Méret állítása (szükség esetén)
            //Console.WindowHeight = 16;
            //Console.WindowWidth = 32;

            int screenwidth = Console.WindowWidth;
            int screenheight = Console.WindowHeight;

            Random rnd = new Random();

            //Base
            int score = 1;
            int gameover = 0;

            //Kígyó feje, kezdő "koordináta" + színe + értelme(iránya)
            sprite shead = new sprite();
            shead.xpos = screenwidth / 2;
            shead.ypos = screenheight / 2;
            shead.mycolor = ConsoleColor.Red;
            string movement = "RIGHT";

            //Kígyó teste
            List<int> xbody = new List<int>();
            List<int> ybody = new List<int>();

            //Kaja random helyre
            int foodx = rnd.Next(0, screenwidth);
            int foody = rnd.Next(0, screenheight);

            //Idő az update-hez (ne villogjon a kép)
            DateTime time1;
            DateTime time2;

            //Keystate check változó
            string buttonpressed;

            //Gameloop
            while (true)
            {
                Console.Clear();

                //State check
                if (shead.xpos == screenwidth - 1 || shead.xpos == 0 || shead.ypos == screenheight - 1 || shead.ypos == 0)
                {
                    gameover = 1;
                }

                //Keret
                for (int i = 0; i < screenwidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("#");
                }
                for (int i = 0; i < screenwidth; i++)
                {
                    Console.SetCursorPosition(i, screenheight - 1);
                    Console.Write("#");
                }
                for (int i = 0; i < screenheight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("#");
                }
                for (int i = 0; i < screenheight; i++)
                {
                    Console.SetCursorPosition(screenwidth - 1, i);
                    Console.Write("#");
                }

                //Console.ForegroundColor = ConsoleColor.Green;

                //Score system
                if (foodx == shead.xpos && foody == shead.ypos)
                {
                    score++;
                    foodx = rnd.Next(1, screenwidth - 3);
                    foody = rnd.Next(1, screenheight - 3);
                }

                //Test logika
                for (int i = 0; i < xbody.Count(); i++)
                {
                    Console.SetCursorPosition(xbody[i], ybody[i]);
                    Console.Write("o");
                    if (xbody[i] == shead.xpos && ybody[i] == shead.ypos)
                    {
                        gameover = 1;
                    }
                }

                //Ha gameover, akkor kilépünk a gameloopból
                if (gameover == 1)
                {
                    break;
                }

                //Start setup
                Console.SetCursorPosition(shead.xpos, shead.ypos);
                Console.ForegroundColor = shead.mycolor;
                //Kígyó feje
                Console.Write("O");
                Console.SetCursorPosition(foodx, foody);
                Console.ForegroundColor = ConsoleColor.Green;
                //Kaja kinézete
                Console.Write("X");
                time1 = DateTime.Now;
                buttonpressed = "no";
                while (true)
                {
                    time2 = DateTime.Now;
                    //Update intervallum (két update között eltelt idő definiálása)
                    //A játék dinamikusságának növelés/csökkentése
                    //A 150 msec ideális, mivel így még normálisan lehet irányítani a kigyót
                    //Kisebb értéknél nyomkodni kell a gombokat, hogy érzékelje az irányváltoztatást
                    if (time2.Subtract(time1).TotalMilliseconds > 150) { break; }

                    //User Input definiálása
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo mkeys = Console.ReadKey(true);
                        if (mkeys.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN" && buttonpressed == "no")
                        {
                            movement = "UP";
                            buttonpressed = "yes";
                        }
                        if (mkeys.Key.Equals(ConsoleKey.DownArrow) && movement != "UP" && buttonpressed == "no")
                        {
                            movement = "DOWN";
                            buttonpressed = "yes";
                        }
                        if (mkeys.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT" && buttonpressed == "no")
                        {
                            movement = "LEFT";
                            buttonpressed = "yes";
                        }
                        if (mkeys.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT" && buttonpressed == "no")
                        {
                            movement = "RIGHT";
                            buttonpressed = "yes";
                        }
                    }
                }

                //Test mozgás
                xbody.Add(shead.xpos);
                ybody.Add(shead.ypos);

                //Mozgás
                switch (movement)
                {
                    case "UP":
                        shead.ypos--;
                        break;
                    case "DOWN":
                        shead.ypos++;
                        break;
                    case "LEFT":
                        shead.xpos--;
                        break;
                    case "RIGHT":
                        shead.xpos++;
                        break;
                }

                //A kígyó test utolsó elemének törlése minden update után (mozgás)
                if (xbody.Count() > score)
                {
                    xbody.RemoveAt(0);
                    ybody.RemoveAt(0);
                }
            }

            //Eredmény kiírása
            string mszoveg = "Game over! Pontszám: ";
            Console.SetCursorPosition((screenwidth / 2) - (mszoveg.Length / 2), screenheight / 2);
            Console.WriteLine(mszoveg + score);
            Console.SetCursorPosition(screenwidth / 2, screenheight / 2 + 1);
            Console.ReadKey();
        }
    }
}