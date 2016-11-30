using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zaj08
{
    class Game
    {
        Cursor[] cursors;
        Cursor C1 = new Cursor();
        Cursor C2 = new Cursor(ConsoleColor.DarkCyan, ConsoleColor.DarkYellow);
        Maze M1 = new Maze();
        private Dictionary<char, char> directionKeys;
        private bool win = false;
        private string options;
        public Game(string launchOptions = "Manual")
        {
            options = launchOptions;
            if (options == "Both")
            {
                cursors = new Cursor[] {
                    new Cursor(),
                    new Cursor(ConsoleColor.DarkCyan, ConsoleColor.DarkYellow)
                };
            }
            else { cursors = new Cursor[] { new Cursor() }; }
            FillDirectionKeys();
            Play();
        }
        public void Play()
        {
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

        private void DisplayWinMessage()
        {
            Console.Clear();
            Console.WriteLine("You won!");
            Thread.Sleep(2000);
        }
        private void MoveCursors(Maze M)
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
        private void DrawMaze(Maze M)
        {
            M.Display();
        }
        private void DrawCursors()
        {
            foreach (Cursor C in cursors)
            {
                C.Display();
            }
        }
        private void FillDirectionKeys()
        {
            directionKeys = new Dictionary<char, char> {
                { 'w', '▲'},
                { 's', '▼'},
                { 'a', '◄'},
                { 'd', '►'},
            };
        }
        private void CheckIfWon(Maze M)
        {
            foreach (Cursor C in cursors)
            {
                if (M.FieldIsDot(C.posX, C.posY)) { win = true; }
            }
        }
        private void KeysManager(Maze M, Cursor C)
        {
            char pressedKey = Console.ReadKey().KeyChar;
            if (directionKeys.ContainsKey(pressedKey))
            {
                MovesValidator.Push(M, C, directionKeys[pressedKey]);
            }
        }

        // Debug
        private void AlternativeKeys(Maze M, Cursor C)
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
