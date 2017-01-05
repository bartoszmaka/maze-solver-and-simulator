namespace Zaj08
{
    static class MovesValidator
    {
        /// <summary>
        /// Pushes given cursor 1 step forward in current direction on given maze
        /// </summary>
        /// <param name="currentMaze">maze on which cursor is moving</param>
        /// <param name="currenCursor">cursor to move</param>
        public static void PushInCurrentDirection(Maze currentMaze, Cursor currenCursor)
        {
            var currentDir = currenCursor.Direction;
            Push(currentMaze, currenCursor, currentDir);
        }
        /// <summary>
        /// Pushes given cursor 1 step forward on given maze in specified direction
        /// </summary>
        /// <param name="currentMaze">maze on which cursor is moving</param>
        /// <param name="currentCursor">cursor to move</param>
        /// <param name="dir">direction</param>
        public static void Push(Maze currentMaze, Cursor currentCursor, Direction dir)
        {
            currentCursor.SetDirection(dir);
            if (currentMaze.FieldAvailable(currentCursor.NextX(), currentCursor.NextY())) currentCursor.MoveForward();
            else currentCursor.MoveValid = false;
        }
        /// <summary>
        /// Move cursor automatically sticking to left wall
        /// </summary>
        /// <param name="currentMaze">maze on which cursor is moving</param>
        /// <param name="currentCursor">cursor to move</param>
        public static void LeftHandMethod(Maze currentMaze, Cursor currentCursor)
        {
            if (currentMaze.FieldAvailable(currentCursor.LeftHandX(), currentCursor.LeftHandY())) currentCursor.TurnAntiClockwise();
            if (NextFieldUnavaible(currentMaze, currentCursor)) currentCursor.TurnClockwise();
            PushInCurrentDirection(currentMaze, currentCursor);
        }
        /// <summary>
        /// Move cursor automatically sticking to right wall
        /// </summary>
        /// <param name="currentMaze">maze on which cursor is moving</param>
        /// <param name="currenCursor">cursor to move</param>
        public static void RightHandMethod(Maze currentMaze, Cursor currenCursor)
        {
            if (currentMaze.FieldAvailable(currenCursor.RightHandX(), currenCursor.RightHandY())) currenCursor.TurnClockwise();
            if (NextFieldUnavaible(currentMaze, currenCursor)) currenCursor.TurnAntiClockwise();
            PushInCurrentDirection(currentMaze, currenCursor);
        }

        /// <summary>
        /// Tells if cursor is able to take 1 step forward
        /// </summary>
        /// <param name="currentMaze">maze on which cursor is moving</param>
        /// <param name="currentCursor">cursor to move</param>
        private static bool NextFieldUnavaible(Maze currentMaze, Cursor currentCursor)
        {
            return currentCursor.NextY() >= 0 && currentCursor.NextX() >= 0 &&
                   !currentMaze.FieldAvailable(currentCursor.NextX(), currentCursor.NextY());
        }
    }
}
