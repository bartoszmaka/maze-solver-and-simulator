using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zaj08
{
    static class Game
    {
        static Cursor[] cursors;
        static Maze M1 = new Maze();
        private static Dictionary<char, char> directionKeys;
        private static bool win = false;
        private static string options;
        private static void PrepareCursors()
        {
            if (options == "Both")
            {
                cursors = new Cursor[] {
                    new Cursor(),
                    new Cursor(ConsoleColor.DarkCyan, ConsoleColor.DarkYellow)
                };
            }
            else { cursors = new Cursor[] { new Cursor() }; }
        }
        static Game()
        {
            FillDirectionKeys();
        }
        public static void Play(string launchOptions = "Manual")
        {
            options = launchOptions;
            PrepareCursors();
            while (!win)
            {
                Console.Clear();
                DrawMaze(M1);
                DrawCursors();
                MoveCursors(M1);
                CheckIfWon(M1);
            }
            DisplayWinMessage();
        }

        private static void DisplayWinMessage()
        {
            Console.Clear();
            Console.WriteLine("You won!");
            Thread.Sleep(2000);
        }
        private static void MoveCursors(Maze M)
        {
            if (options != "Manual")
            {
                switch (options)
                {
                    case "Left":
                        MovesValidator.LeftHandMethod(M, cursors[0]);
                        break;
                    case "Right":
                        MovesValidator.RightHandMethod(M, cursors[0]);
                        break;
                    case "Both":
                        MovesValidator.LeftHandMethod(M, cursors[0]);
                        MovesValidator.RightHandMethod(M, cursors[1]);
                        break;
                }
                Thread.Sleep(100);
            }
            else { KeysManager(M1, cursors[0]); }
        }
        private static void DrawMaze(Maze M)
        {
            M.Display();
        }
        private static void DrawCursors()
        {
            foreach (Cursor C in cursors)
            {
                C.Display();
            }
        }
        private static void FillDirectionKeys()
        {
            directionKeys = new Dictionary<char, char> {
                { 'w', '▲'},
                { 's', '▼'},
                { 'a', '◄'},
                { 'd', '►'},
            };
        }
        private static void CheckIfWon(Maze M)
        {
            foreach (Cursor C in cursors)
            {
                if (M.FieldIsDot(C.posX, C.posY)) { win = true; }
            }
        }
        private static void KeysManager(Maze M, Cursor C)
        {
            char pressedKey = Console.ReadKey().KeyChar;
            if (directionKeys.ContainsKey(pressedKey))
            {
                MovesValidator.Push(M, C, directionKeys[pressedKey]);
            }
        }

        private static void AlternativeKeys(Maze M, Cursor C)
        {
            char pressedKey = Console.ReadKey().KeyChar;
            switch (pressedKey)
            {
                case 'w':
                    MovesValidator.Push(M, C);
                    break;
                case 's':
                    C.TurnAround();
                    break;
                case 'a':
                    C.TurnAntiClockwise();
                    break;
                case 'd':
                    C.TurnClockwise();
                    break;
                default:
                    break;
            }
        }
    }
}
