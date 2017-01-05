using System;
using System.Collections.Generic;
using System.Threading;

namespace Zaj08
{
    public sealed class Game
    {
        /// <summary>
        /// Gets game singleton instance
        /// </summary>
        public static Game Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Game();
                    }
                    return _instance;
                }
            }
        }
        private static readonly object Padlock = new object();
        private static Game _instance = null;
        private GameOption _launchOption;
        /// <summary>
        /// Stores cursors that take part in game
        /// </summary>
        /// <remarks>depending on launchOption, there might be 1 or 2 cursors</remarks>
        private Cursor[] _cursors;
        private Maze _currentMaze = new Maze();
        private Dictionary<ConsoleKey, Direction> _directionKeys;
        /// <summary>
        /// Tells if any cursor won already
        /// </summary>
        private bool _win = false;

        private Game()
        {
            FillDirectionKeys();
        }

        /// <summary>
        /// Starts game in given mode
        /// </summary>
        /// <param name="launchOption">game mode</param>
        public void Play(GameOption launchOption = GameOption.ManualNormal)
        {
            _launchOption = launchOption;
            PrepareCursors();
            while (!_win)
            {
                Console.Clear();
                _currentMaze.Display();
                DrawCursors();
                MoveCursors();
                CheckIfWon();
            }
            DisplayWinMessage();
        }

        private static void DisplayWinMessage()
        {
            Console.Clear();
            Console.WriteLine("You won!");
            Thread.Sleep(2000);
        }
        /// <summary>
        /// Move cursors with method dependent on game mode
        /// </summary>
        private void MoveCursors()
        {
            switch (_launchOption)
            {
                case GameOption.ManualNormal:
                    KeysManager(_cursors[0]);
                    break;
                case GameOption.ManualAlternative:
                    AlternativeKeys(_cursors[0]);
                    break;
                case GameOption.AutoLeft:
                    MovesValidator.LeftHandMethod(_currentMaze, _cursors[0]);
                    Thread.Sleep(100);
                    break;
                case GameOption.AutoRight:
                    MovesValidator.RightHandMethod(_currentMaze, _cursors[0]);
                    Thread.Sleep(100);
                    break;
                case GameOption.AutoComparsion:
                    MovesValidator.LeftHandMethod(_currentMaze, _cursors[0]);
                    MovesValidator.RightHandMethod(_currentMaze, _cursors[1]);
                    Thread.Sleep(100);
                    break;
            }
        }
        /// <summary>
        /// Draws all cursors
        /// </summary>
        private void DrawCursors()
        {
            foreach (Cursor currentCursor in _cursors)
            {
                currentCursor.Display();
            }
        }
        /// <summary>
        /// Fills key - direction pairs
        /// </summary>
        /// <remarks>Separated to method for readability</remarks>
        private void FillDirectionKeys()
        {
            _directionKeys = new Dictionary<ConsoleKey, Direction> {
                { ConsoleKey.W, Direction.North },
                { ConsoleKey.UpArrow, Direction.North },
                { ConsoleKey.S, Direction.South },
                { ConsoleKey.DownArrow, Direction.South },
                { ConsoleKey.A, Direction.West },
                { ConsoleKey.LeftArrow, Direction.West },
                { ConsoleKey.D, Direction.East },
                { ConsoleKey.RightArrow, Direction.East },
            };
        }
        /// <summary>
        /// Checks if any cursor reached dot field
        /// </summary>
        private void CheckIfWon()
        {
            foreach (Cursor currentCursor in _cursors)
            {
                if (_currentMaze.FieldIsDot(currentCursor.PosX, currentCursor.PosY)) { _win = true; }
            }
        }
        /// <summary>
        /// Basic controls - wsad moves cursor to corresponding direction
        /// </summary>
        /// <param name="currentCursor">cursor to move</param>
        private void KeysManager(Cursor currentCursor)
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey();
            if (_directionKeys.ContainsKey(pressedKey.Key))
            {
                MovesValidator.Push(_currentMaze, currentCursor, _directionKeys[pressedKey.Key]);
            }
        }
        /// <summary>
        /// Alternative controls -> w to push, asd to turn
        /// </summary>
        /// <param name="currentCursor">cursor to move</param>
        private void AlternativeKeys(Cursor currentCursor)
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey();
            switch (pressedKey.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    MovesValidator.PushInCurrentDirection(_currentMaze, currentCursor);
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    currentCursor.TurnAround();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    currentCursor.TurnAntiClockwise();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    currentCursor.TurnClockwise();
                    break;
            }
        }
        /// <summary>
        /// Initialize cursors depending on launch mode
        /// </summary>
        private void PrepareCursors()
        {
            if (_launchOption == GameOption.AutoComparsion)
            {
                _cursors = new Cursor[]
                {
                    new Cursor(),
                    new Cursor(ConsoleColor.DarkCyan, ConsoleColor.DarkYellow)
                };
            }
            else
            {
                _cursors = new Cursor[]
                {
                    new Cursor()
                };
            }
        }
    }
}
