using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

using static System.Console;
namespace Snake2._0
{
    class Program
    {
        private const int MapWidth = 30;
        private const int MapHeight = 20;
        //
        private const int SWight = MapWidth * 2;
        private const int SHeight = MapHeight * 2;
        //
        private const int Frame = 200;
        //
        private const ConsoleColor BorderColor = ConsoleColor.Gray;
        private const ConsoleColor FoodColor = ConsoleColor.Blue;
        //
        private const ConsoleColor _HeadColor = ConsoleColor.Green;
        private const ConsoleColor _BodyColor = ConsoleColor.Red;
        //
        private static readonly Random random = new Random();
        static void Main(string[] args)
        {
            SetWindowSize(SWight, SHeight);
            SetBufferSize(SWight, SHeight);
            CursorVisible = false;
            while (true)
            {
                StartOurGame();
                Thread.Sleep(1000);
                ReadKey();
            }
        }
        static void StartOurGame()
        {
            Clear();
            DrawBorder();
            Motion currentMov = Motion.Right;

            var snake = new SnakeForGame(10, 5, _HeadColor, _BodyColor);

            Pozition food = Food(snake);
            food.Draw();
            int f = 0;
            Stopwatch sw = new Stopwatch();
            while (true)
            {
                sw.Restart();
                Motion oldMov = currentMov;
                while (sw.ElapsedMilliseconds <= Frame)
                {
                    if (currentMov == oldMov)
                    {
                        currentMov = MotionKeyboard(currentMov);
                    }

                }
                if (snake.HeadSnake.X == food.X && snake.HeadSnake.Y == food.Y) // Head + Eat 
                {
                    snake.MoveSnake(currentMov, true);
                    food = Food(snake);
                    f++;
                }
                else
                {
                    snake.MoveSnake(currentMov);
                    food.Draw();
                }
                if (snake.HeadSnake.X == MapWidth - 1 || snake.HeadSnake.X == 0
                    || snake.HeadSnake.Y == MapHeight - 1
                    || snake.HeadSnake.Y == 0
                    || snake.Body.Any(a => a.X == snake.HeadSnake.X
                    && a.Y == snake.HeadSnake.Y))
                    break;
            }
            snake.Clear();

            WriteLine($"OMG,HOWWWW YOU CAN? Your score: {f}");
        }
        static Pozition Food(SnakeForGame snake)
        {
            Pozition food;
            do
            {
                food = new Pozition(random.Next(1, MapWidth - 2), random.Next(1, MapHeight - 2), FoodColor);
            } while (snake.HeadSnake.X == food.X && snake.HeadSnake.Y == food.Y
            || snake.Body.Any(b => b.X == food.X && b.Y == food.Y));
            return food;
        }

        static Motion MotionKeyboard(Motion currentMotion)
        {
            if (!KeyAvailable)
                return currentMotion;
            ConsoleKey key = ReadKey(true).Key;
            currentMotion = key switch // действия запрещают змейке двигаться в противоположную сторону
            {
                ConsoleKey.UpArrow when currentMotion != Motion.Down => Motion.Up,
                ConsoleKey.DownArrow when currentMotion != Motion.Up => Motion.Down,
                ConsoleKey.LeftArrow when currentMotion != Motion.Right => Motion.Left,
                ConsoleKey.RightArrow when currentMotion != Motion.Left => Motion.Right,
                _ => currentMotion
            };
            return currentMotion;
        }
        static void DrawBorder()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pozition(i, 0, BorderColor).Draw();
                new Pozition(i, MapHeight - 1, BorderColor).Draw();
            }
            for (int i = 0; i < MapHeight; i++)
            {
                new Pozition(0, i, BorderColor).Draw();
                new Pozition(MapWidth - 1, i, BorderColor).Draw();
            }
        }

    }
}
