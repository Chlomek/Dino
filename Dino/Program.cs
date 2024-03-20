using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int speedMultiplier = 5;
            int startSpeed = 200;
            int maxSpeed = 40;
            int score = 0;

            Random rnd = new Random();
            string[,] map = new string[3, 30];
            int dinoX = 0;
            int dinoY = 0;
            int jump = 0;
            int cactusX = 40;
            int nextCactus = rnd.Next(25, 40) + cactusX;
            FillMap(map);
            while (true)
            {
                if (maxSpeed > score/speedMultiplier)
                    Thread.Sleep(startSpeed - score / speedMultiplier);
                else
                    Thread.Sleep(maxSpeed);


                if (cactusX == dinoX && dinoY == 0)
                {
                    Console.WriteLine("Game Over");
                    Console.ReadKey();
                }
                else if (nextCactus == dinoX && dinoY == 0)
                {
                    Console.WriteLine("Game Over");
                    Console.ReadKey();
                }

                if (cactusX > 0)
                {
                    cactusX--;
                    nextCactus--;
                }
                else
                {
                    cactusX = nextCactus;
                    nextCactus = rnd.Next(15, 30) + cactusX;
                }

                if (jump > 2)
                {
                    dinoY++;
                    jump--;
                }
                else if (jump > 0 && dinoY > 0)
                {
                    dinoY--;
                    jump--;
                }

                DrawMap(map, dinoX, dinoY, cactusX, nextCactus, score);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        if(dinoX < map.GetLength(1) - 1)
                            dinoX++;
                    }
                    else if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        if (dinoX > 0)
                            dinoX--;
                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (jump == 0)
                        {
                            jump = 4;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        dinoY--;
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                }

                score++;
            }

        }
        static void FillMap(string[,] map)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    map[y, x] = " ";
                }
            }
        }
        static void DrawMap(string[,] map, int dinoX, int dinoY, int CactusX, int nextCactus, int score)
        {
            Console.Clear();
            for (int y = map.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (x == dinoX && y == dinoY)
                    {
                        Console.Write("D");
                    }
                    else if (x == CactusX && y == 0)
                    {
                        Console.Write("C");
                    }
                    else if (x == nextCactus && y == 0)
                    {
                        Console.Write("K");
                    }
                    else
                    {
                        Console.Write(map[y, x]);
                    }
                }
                Console.WriteLine();
            }
            Testing(dinoX, dinoY, CactusX, nextCactus, score);
        }
        static void Testing(int dinoX, int dinoY, int CactusX, int nextCactus, int score)
        {
            Console.WriteLine("X: " + dinoX);
            Console.WriteLine("Y: " + dinoY);
            Console.WriteLine("C: " + CactusX);
            Console.WriteLine("K: " + nextCactus);
            Console.WriteLine("Score: " + score);
        }
    }
}
