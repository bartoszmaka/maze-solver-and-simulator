using System;

namespace Zaj08
{
    class Cursor
    {
        public static readonly string DirectionSigns = "▲►▼◄";
        public int PosX;
        public int PosY;
        public bool MoveValid;
        /// <summary>
        /// Cursor direction
        /// </summary>
        public Direction Direction;
        /// <summary>
        /// Default cursor color
        /// </summary>
        private readonly ConsoleColor _cursorColor;
        /// <summary>
        /// Color indicating invalid move
        /// </summary>
        private readonly ConsoleColor _alertColor;
        /// <summary>
        /// Sign used to represent cursor on console
        /// </summary>
        private char _cursorSign;

        public Cursor(int x = 0, int y = 1, Direction dir = Direction.East)
        {
            _cursorColor = ConsoleColor.Green;
            _alertColor = ConsoleColor.Red;
            SetDirection(dir);
            MoveValid = true;
            PosX = x;
            PosY = y;
        }
        public Cursor(ConsoleColor cursor, ConsoleColor alert)
            : this()
        {
            _cursorColor = cursor;
            _alertColor = alert;
        }

        /// <summary>
        /// Prints cursor on console
        /// </summary>
        public void Display()
        {
            Console.SetCursorPosition(PosX, PosY);
            if (MoveValid) { Console.ForegroundColor = _cursorColor; }
            else { Console.ForegroundColor = _alertColor; }
            Console.Write(_cursorSign);
            Console.ResetColor();
            MoveValid = true;
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        /// Sets cursor direction and proper sign
        /// </summary>
        /// <param name="dir">one of four directions from Enums::Directions </param>
        public void SetDirection(Direction dir)
        {
            Direction = dir;
            _cursorSign = DirectionSigns[(int)dir];
        }
        /// <summary>
        /// Turns cursor 90 degrees clockwise
        /// </summary>
        public void TurnClockwise()
        {
            Direction = (Direction)((int)(Direction + 1) % 4);
            int cursorSignIndex = DirectionSigns.IndexOf(_cursorSign);
            _cursorSign = DirectionSigns[(cursorSignIndex + 1) % 4];
        }
        /// <summary>
        /// Turns cursor 90 degrees anticlockwise
        /// </summary>
        public void TurnAntiClockwise()
        {
            Direction = (Direction)((int)(Direction + 3) % 4);
            int cursorSignIndex = DirectionSigns.IndexOf(_cursorSign);
            _cursorSign = DirectionSigns[(cursorSignIndex + 3) % 4];
        }
        /// <summary>
        /// Turns cursor 180 degrees
        /// </summary>
        public void TurnAround()
        {
            Direction = (Direction)((int)(Direction + 2) % 4);
            int cursorSignIndex = DirectionSigns.IndexOf(_cursorSign);
            _cursorSign = DirectionSigns[(cursorSignIndex + 2) % 4];
        }
        /// <summary>
        /// Moves cursor 1 field forward in current direction
        /// </summary>
        public void MoveForward()
        {
            PosX = NextX();
            PosY = NextY();
        }
        /// <summary>
        /// Returns X coordinate of field on left side of cursor
        /// </summary>
        public int LeftHandX()
        {
            TurnAntiClockwise();
            int x = NextX();
            TurnClockwise();
            return x;
        }
        /// <summary>
        /// Returns Y coordinate of field on left side of cursor
        /// </summary>
        public int LeftHandY()
        {
            TurnAntiClockwise();
            int y = NextY();
            TurnClockwise();
            return y;
        }
        /// <summary>
        /// Returns X coordinate of field on right side of cursor
        /// </summary>
        public int RightHandX()
        {
            TurnClockwise();
            int x = NextX();
            TurnAntiClockwise();
            return x;
        }
        /// <summary>
        /// Returns Y coordinate of field on right side of cursor
        /// </summary>
        public int RightHandY()
        {
            TurnClockwise();
            int y = NextY();
            TurnAntiClockwise();
            return y;
        }
        /// <summary>
        /// Returns X coordinate of cursor after 1 step forward
        /// </summary>
        public int NextX()
        {
            return Next();
        }
        /// <summary>
        /// Returns Y coordinate of cursor after 1 step forward
        /// </summary>
        public int NextY()
        {
            return Next(false);
        }
        /// <summary>
        /// Returns cursor coordinate on given axis
        /// </summary>
        private int Next(bool axisLetterIsX = true)
        {
            switch (Direction)
            {
                case Direction.North:
                    return axisLetterIsX ? PosX : PosY - 1;
                case Direction.East:
                    return axisLetterIsX ? PosX + 1 : PosY;
                case Direction.South:
                    return axisLetterIsX ? PosX : PosY + 1;
                case Direction.West:
                    return axisLetterIsX ? PosX - 1 : PosY;
                default:
                    return -1;
            }
        }
        /// <summary>
        /// Prints debug information at given position
        /// </summary>
        /// <param name="x">Console column</param>
        /// <param name="y">Console row</param>
        private void CursorDebug(int x = 25, int y = 0)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("direction: {0}", Direction);
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("Current: [{0}, {1}]", PosX, PosY);
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("Next: [{0}, {1}]", NextX(), NextY());
        }
    }
}
