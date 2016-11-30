using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//"########################",
//"     #                 #",
//"#### ## #############  #",
//"#    #     #           #",
//"#    #     #     #     #",
//"## #### ##### ##########",
//"#    #     #           #",
//"#          #     #     #",
//"##################.#####"
namespace Zaj08
{
    class Maze
    {
        private string[] fields;
        public Maze()
        {
            fields = new string[]
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
        public void Display()
        {
            foreach (string line in fields)
            {
                foreach (char sign in line)
                {
                    Console.Write(sign);
                }
                Console.WriteLine();
            }
        }
        public bool FieldAvailable(int x, int y)
        {
            if (y >= 0 && x >= 0 && fields[y][x] != '#') { return true; }
            return false;
        }
        public bool FieldIsDot(int x, int y)
        {
            if (fields[y][x] == '.') { return true; }
            else { return false; }
        }
    }
}
