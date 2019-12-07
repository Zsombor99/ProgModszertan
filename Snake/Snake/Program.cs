using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        const int WIDTH = 40;
        const int HEIGTH = 20;
        ConsoleKeyInfo consoleKey;
        Coordinate snakeHead = new Coordinate();
        List<Coordinate> snakeBody = new List<Coordinate>();
        Direction direction;
        Coordinate food = new Coordinate();

        static void Main(string[] args)
        {
            Program p = new Program();

            p.snakeHead.X = WIDTH / 2;
            p.snakeHead.Y = HEIGTH / 2;
            p.direction = Direction.NEUTRAL;
            p.food = p.getFoodCoordinate();

            while (true)
            {
                p.drawContainer();
                p.movementInput();
                p.calculation();
            }
        }

        public void drawContainer()
        {
            Console.Clear();
            //felső szegély
            Console.Write("+");
            for (int i = 0; i < WIDTH; i++)
            {
                Console.Write("-");
            }
            Console.Write("+\n");

            //Köztes rész
            for (int y = 0; y < HEIGTH; y++)
            {
                Console.Write("|");
                for (int x = 0; x < WIDTH; x++)
                {
                    if (x == snakeHead.X && y == snakeHead.Y)
                        Console.Write("O");
                    else if (x == food.X && y == food.Y)
                        Console.Write("#");
                    else
                    {
                        bool isSnakeBody = false;
                        for (int i = 0; i < snakeBody.Count; i++)
                        {
                            if (x == snakeBody[i].X && y == snakeBody[i].Y)
                            {
                                Console.Write("O");
                                isSnakeBody = true;
                                break;
                            }
                        }
                        if (!isSnakeBody)
                        {
                            Console.Write(" ");
                        }
                    }

                }
                Console.Write("|\n");
            }

            //Alsó szegély
            Console.Write("+");
            for (int i = 0; i < WIDTH; i++)
            {
                Console.Write("-");
            }
            Console.Write("+\n");

            Console.WriteLine("Kigyó hossza:{0} ", snakeBody.Count);
        }

        public void movementInput()
        {
            if (Console.KeyAvailable)
            {
                consoleKey = Console.ReadKey(true);
                switch (consoleKey.Key)
                {
                    case ConsoleKey.W:
                       if(direction != Direction.DOWN) direction = Direction.UP;
                        break;
                    case ConsoleKey.D:
                        if (direction != Direction.LEFT) direction = Direction.RIGHT;
                        break;
                    case ConsoleKey.S:
                        if (direction != Direction.UP) direction = Direction.DOWN;
                        break;
                    case ConsoleKey.A:
                        if (direction != Direction.RIGHT) direction = Direction.LEFT;
                        break;
                }
            }

        }

        public void calculation()
        {
            snakeBody.Add(snakeHead);
            snakeBody.RemoveAt(snakeBody.Count()-1);

            switch (direction)
            {
                case Direction.UP:
                    snakeHead.Y--;
                    break;
                case Direction.RIGHT:
                    snakeHead.X++;
                    break;
                case Direction.DOWN:
                    snakeHead.Y++;
                    break;
                case Direction.LEFT:
                    snakeHead.X--;
                    break;
            }

            if (snakeHead.X == -1)
                snakeHead.X = WIDTH - 1;
            else if (snakeHead.X == WIDTH)
                snakeHead.X = 0;

            if (snakeHead.Y == -1)
                snakeHead.Y = HEIGTH - 1;
            else if (snakeHead.Y == HEIGTH)
                snakeHead.Y = 0;

            if (snakeHead.X == food.X && snakeHead.Y == food.Y)
            {
                food = getFoodCoordinate();

                snakeBody.Add(snakeHead);
            }

        }

        public Coordinate getFoodCoordinate()
        {
            Coordinate c = new Coordinate();
            Random rnd = new Random();

            while (true)
            {
                c.X = rnd.Next() % WIDTH;
                c.Y = rnd.Next() % HEIGTH;
                int i;

                for (i = 1; i < snakeBody.Count; i += 2)
                {
                    if (snakeBody[i].X == c.X && snakeBody[i].Y == c.Y)
                    {
                        break;
                    }
                }

                if (!(c.X == snakeHead.X && c.Y == snakeHead.Y) && snakeBody.Count < i)
                {
                    return c;
                }
            }
        }
    }
}
