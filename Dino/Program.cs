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
            int speedDivider = 5;
            int startSpeed = 200;
            int maxSpeed = 40;
            int score = 0;
            int minDistance = 10;
            int startDistance = 30;

            Random rnd = new Random();
            string[,] map = new string[3, 25];
            int dinoX = 0;
            int dinoY = 0;
            int jump = 0;
            int distance = startDistance;
            int bird = rnd.Next(100,200);
            int cactusX = 40;
            int nextCactus = rnd.Next(25, 40) + cactusX;
            FillMap(map);
            while (true)
            {
                if (maxSpeed < startSpeed - score / speedDivider)
                    Thread.Sleep(startSpeed - score / speedDivider);
                else
                    Thread.Sleep(maxSpeed);

                //Collision check
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
                else if (bird == dinoX && dinoY == 1)
                {
                    Console.WriteLine("Game Over");
                    Console.ReadKey();
                }

                cactusX--;
                nextCactus--;
                bird--;

                //Remove and swap cactuses
                if (cactusX == 0)
                {
                    cactusX = nextCactus;
                    if(minDistance < startDistance - score / speedDivider)
                        nextCactus = rnd.Next(startDistance - score/speedDivider, startDistance * 2 - score / speedDivider) + cactusX;
                    else
                        nextCactus = rnd.Next(minDistance, minDistance * 2) + cactusX;
                }

                //Generate bird
                if(bird == 0)
                {
                    bird = BirdMet(minDistance, startDistance, score, speedDivider, cactusX);
                }

                if (bird - nextCactus < 3 && bird - nextCactus >= 0 || nextCactus - bird < 3 && nextCactus - bird >= 0)
                {
                    bird = BirdMet(minDistance, startDistance, score, speedDivider, cactusX);
                }

                //Jumping
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

                DrawMap(map, dinoX, dinoY, cactusX, nextCactus, score, bird);

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
        static void DrawMap(string[,] map, int dinoX, int dinoY, int CactusX, int nextCactus, int score, int bird)
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
                    else if (x == bird && y == 1)
                    {
                        Console.Write("P");
                    }
                    else
                    {
                        Console.Write(map[y, x]);
                    }
                }
                Console.WriteLine();
            }
            Testing(dinoX, dinoY, CactusX, nextCactus, score, bird);
        }
        static void Testing(int dinoX, int dinoY, int CactusX, int nextCactus, int score, int bird)
        {
            Console.WriteLine();
            Console.WriteLine("X: " + dinoX);
            Console.WriteLine("Y: " + dinoY);
            Console.WriteLine("C: " + CactusX);
            Console.WriteLine("K: " + nextCactus);
            Console.WriteLine("P: " + bird);
            Console.WriteLine("Score: " + score);
        }
        static int BirdMet(int minDistance, int startDistance, int score, int speedDivider, int cactusX)
        {
            Random rnd = new Random();

            if (minDistance < startDistance - score / speedDivider)
                return rnd.Next(startDistance * 5 - score / speedDivider, startDistance * 10 - score / speedDivider) + cactusX;
            else
                return rnd.Next(minDistance * 5, minDistance * 10);
        }
    }
}
