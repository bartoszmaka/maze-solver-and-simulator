using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Zaj08
{
    class Cursor
    {
        public enum Directions { North, East, South, West };
        public const string validDirections = "▲►▼◄";
        public int direction { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public bool validMove { get; set; }
        public char cursorSign { get; set; }
        public ConsoleColor cursorColor { get; set; }
        public ConsoleColor alertColor { get; set; }
        public Cursor(int x = 0, int y = 1, char dir = '►')
        {
            cursorColor = ConsoleColor.Green;
            alertColor = ConsoleColor.Red;
            SetDirection(dir);
            validMove = true;
            posX = x;
            posY = y;
        }
        public Cursor(ConsoleColor cursor, ConsoleColor alert)
            :this()
        {
            cursorColor = cursor;
            alertColor = alert;
        }
        public void Display()
        {
            Console.SetCursorPosition(posX, posY);
            if (validMove) { Console.ForegroundColor = cursorColor; }
            else { Console.ForegroundColor = alertColor; }
            Console.Write(cursorSign);
            Console.ResetColor();
            validMove = true;
            Console.SetCursorPosition(0, 0);
            //CursorDebug();
        }
        public void SetDirection(char dir)
        {
            if (validDirections.Contains(dir))
            {
                direction = validDirections.IndexOf(dir);
                cursorSign = dir;
            }
        }
        public void TurnClockwise()
        {
            direction = (direction + 1) % 4;
            int cursorSignIndex = validDirections.IndexOf(cursorSign);
            cursorSign = validDirections[(cursorSignIndex + 1) % 4];
        }
        public void TurnAntiClockwise()
        {
            direction = (direction + 3) % 4;
            int cursorSignIndex = validDirections.IndexOf(cursorSign);
            cursorSign = validDirections[(cursorSignIndex + 3) % 4];
        }
        public void TurnAround()
        {
            direction = (direction + 2) % 4;
            int cursorSignIndex = validDirections.IndexOf(cursorSign);
            cursorSign = validDirections[(cursorSignIndex + 2) % 4];
        }
        public void MoveForward()
        {
            posX = NextX();
            posY = NextY();
        }
        public int LeftHandX()
        {
            TurnAntiClockwise();
            int x = NextX();
            TurnClockwise();
            return x;
        }
        public int LeftHandY()
        {
            TurnAntiClockwise();
            int y = NextY();
            TurnClockwise();
            return y;
        }
        public int RightHandX()
        {
            TurnClockwise();
            int x = NextX();
            TurnAntiClockwise();
            return x;
        }
        public int RightHandY()
        {
            TurnClockwise();
            int y = NextY();
            TurnAntiClockwise();
            return y;
        }
        public int NextX()
        {
            return Next(true);
        }
        public int NextY()
        {
            return Next(false);
        }
        private int Next(bool axisLetterIsX = true)
        {
            switch (direction)
            {
                case (int)Directions.North:
                    return (axisLetterIsX) ? posX : posY - 1;
                case (int)Directions.East:
                    return (axisLetterIsX) ? posX + 1 : posY;
                case (int)Directions.South:
                    return (axisLetterIsX) ? posX : posY + 1;
                case (int)Directions.West:
                    return (axisLetterIsX) ? posX - 1 : posY;
                default:
                    return -1;
            }
        }

        // Debug
        private void CursorDebug()
        {
            Console.SetCursorPosition(25, 0);
            Console.WriteLine("direction: {0}", direction);
            Console.SetCursorPosition(25, 1);
            Console.WriteLine("Current: [{0}, {1}]", posX, posY);
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Next: [{0}, {1}]", NextX(), NextY());
        }
    }
}
