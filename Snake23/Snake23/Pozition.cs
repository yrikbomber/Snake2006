using System;

namespace Snake2._0
{
    public readonly struct Pozition
    {
        private const char PozChar = '▒';
        public Pozition(int x, int y, ConsoleColor color, int posSize = 1)
        {
            X = x;
            Y = y;
            Color = color;
            PosSize = posSize;
        }
        public int X { get; }
        public int Y { get; }

        public ConsoleColor Color { get; }
        public int PosSize { get; }
        public void Draw()
        {
            Console.ForegroundColor = Color;
            for (int x = 0; x < PosSize; x++)
            {
                for (int y = 0; y < PosSize; y++)
                {
                    Console.SetCursorPosition(X * PosSize + x, Y * PosSize + y);
                    Console.Write(PozChar);
                }
            }
        }
        public void Clear()
        {
            for (int x = 0; x < PosSize; x++)
            {
                for (int y = 0; y < PosSize; y++)
                {
                    Console.SetCursorPosition(X * PosSize + x, Y * PosSize + y);
                    Console.Write(' ');
                }
            }

        }

    }
}