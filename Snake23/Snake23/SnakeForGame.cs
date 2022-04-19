using System;
using System.Collections.Generic;

namespace Snake2._0
{
    class SnakeForGame
    {
        private readonly ConsoleColor HeadColor;
        private readonly ConsoleColor BodyColor;
        public SnakeForGame(int sX, int sY, ConsoleColor headColor, ConsoleColor bodyColor, int bodyLength = 2)
        {
            HeadColor = headColor;
            BodyColor = bodyColor;
            HeadSnake = new Pozition(sX, sY, HeadColor);
            for (int i = bodyLength; i >= 0; i--)
            {
                Body.Enqueue(new Pozition(HeadSnake.X - i - 1, sY, BodyColor));
            }
            Draw();
        }
        public Pozition HeadSnake { get; private set; }
        public Queue<Pozition> Body { get; } = new Queue<Pozition>();

        public void MoveSnake(Motion motion, bool eat = false)
        {
            Clear();
            Body.Enqueue(new Pozition(HeadSnake.X, HeadSnake.Y, BodyColor));
            if (!eat)
                Body.Dequeue();
            HeadSnake = motion switch
            {
                Motion.Left => new Pozition(HeadSnake.X - 1, HeadSnake.Y, HeadColor),
                Motion.Right => new Pozition(HeadSnake.X + 1, HeadSnake.Y, HeadColor),
                Motion.Up => new Pozition(HeadSnake.X, HeadSnake.Y - 1, HeadColor),
                Motion.Down => new Pozition(HeadSnake.X, HeadSnake.Y + 1, HeadColor),
                _ => HeadSnake
            };
            Draw();
        }
        public void Draw()
        {
            HeadSnake.Draw();
            foreach (Pozition poz in Body)
            {
                poz.Draw();
            }
        }
        public void Clear()
        {
            HeadSnake.Clear();
            foreach (Pozition poz in Body)
            {
                poz.Clear();
            }
        }

    }
}
