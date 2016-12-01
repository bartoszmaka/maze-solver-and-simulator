using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zaj08
{
    static class MovesValidator
    {
        private static bool MoveValid(Maze M, int x, int y)
        {
            if (M.FieldAvailable(x, y)) { return true; }
            else { return false; }
        }
        public static void Push(Maze M, Cursor C, char dir = '0')
        {
            if (dir != '0') { C.SetDirection(dir); }    // If not parsed direction -> push in current direction
            if (MoveValid(M, C.NextX(), C.NextY())) { C.MoveForward(); }
            else { C.validMove = false; }
        }
        private static bool NextFieldUnavaible(Maze M, Cursor C)
        {
            if (C.NextY() >= 0 && C.NextX() >= 0 &&
                !(M.FieldAvailable(C.NextX(), C.NextY()))) { return true; }
            return false;
        }
        public static void LeftHandMethod(Maze M, Cursor C)
        {
            if (MoveValid(M, C.LeftHandX(), C.LeftHandY())) { C.TurnAntiClockwise(); }
            if (NextFieldUnavaible(M, C)) { C.TurnClockwise(); }
            Push(M, C);
        }
        public static void RightHandMethod(Maze M, Cursor C)
        {
            if (MoveValid(M, C.RightHandX(), C.RightHandY())) { C.TurnClockwise(); }
            if (NextFieldUnavaible(M, C)) { C.TurnAntiClockwise(); }
            Push(M, C);
        }
    }
}
