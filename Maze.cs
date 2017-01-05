using System;

namespace Zaj08
{
    class Maze
    {
        /// <summary>
        /// Actual maze structure
        /// </summary>
        private readonly string[] _fields;

        public Maze()
        {
            _fields = new string[]
            {
                "########################",
                "     #                 #",
                "#### ## #############  #",
                "#    #     #           #",
                "#    #     #     #     #",
                "## #### ##### ##########",
                "#    #     #           #",
                "#          #     #     #",
                "##################.#####"
            };
        }

        /// <summary>
        /// Prints maze on console
        /// </summary>
        public void Display()
        {
            foreach (string line in _fields)
            {
                foreach (char sign in line)
                {
                    Console.Write(sign);
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Tells if given field is available
        /// </summary>
        /// <param name="x">row</param>
        /// <param name="y">column</param>
        /// <returns>true if field is available, false otherwise</returns>
        public bool FieldAvailable(int x, int y)
        {
            return y >= 0 && x >= 0 && _fields[y][x] != '#';
        }
        /// <summary>
        /// Tells if given field is dot
        /// </summary>
        /// <remarks>game is over, when cursor reaches dot</remarks>
        /// <param name="x">row</param>
        /// <param name="y">column</param>
        /// <returns>true if field is dot, false otherwise</returns>
        public bool FieldIsDot(int x, int y)
        {
            return _fields[y][x] == '.';
        }
    }
}
