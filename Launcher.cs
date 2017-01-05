using System;

namespace Zaj08
{
    public sealed class Launcher
    {
        private static readonly object Padlock = new object();
        private static Launcher _instance = null;
        /// <summary>
        /// Gets launcher singleton instance
        /// </summary>
        public static Launcher Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Launcher();
                    }
                    return _instance;
                }
            }
        }
        private string[] _menuOptions;
        private string[][] _optionsExplanation;
        /// <summary>
        /// Tells which menu entry is currently selected
        /// </summary>
        private int _choice;
        /// <summary>
        /// Tells if user has chosen game mode already
        /// </summary>
        private bool _chosen;

        private Launcher()
        {
            _chosen = false;
            _choice = 0;
            FillMenuOptions();
        }

        /// <summary>
        /// Launches game menu
        /// </summary>
        public void Launch()
        {

            while (!_chosen)
            {
                Display();
                KeysManager();
            }
            ApplyChoice();
        }
        /// <summary>
        /// Binds keys to menu actions
        /// </summary>
        private void KeysManager()
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey();
            switch (pressedKey.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    IncrementChoice();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    DecrementChoice();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.Enter:
                    _chosen = true;
                    break;
            }
        }
        /// <summary>
        /// Launches game in choosen mode
        /// </summary>
        private void ApplyChoice()
        {
            if (Enum.IsDefined(typeof(GameOption), _choice))
            {
                Game.Instance.Play((GameOption)_choice);
            }
        }
        /// <summary>
        /// Selects previous menu entry
        /// </summary>
        private void DecrementChoice()
        {
            _choice = (_choice + 1) % _menuOptions.Length;
        }
        /// <summary>
        /// Selects next menu entry
        /// </summary>
        private void IncrementChoice()
        {
            _choice = (_choice + _menuOptions.Length - 1) % _menuOptions.Length;
        }
        /// <summary>
        /// Prints menu on console
        /// </summary>
        private void Display()
        {
            Console.Clear();
            for (int i = 0; i < _menuOptions.Length; i++)
            {
                Console.SetCursorPosition(5, 5 + i);
                if (_choice == i) { Console.ForegroundColor = ConsoleColor.Green; }
                Console.Write(_menuOptions[i]);
                Console.ResetColor();
                for (int j = 0; j < _optionsExplanation[_choice].Length; j++)
                {
                    Console.SetCursorPosition(45, 5 + j);
                    Console.Write(_optionsExplanation[_choice][j]);
                }
            }
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        /// Fills all menu entries
        /// </summary>
        /// <remarks>Separated to method for readability</remarks>
        private void FillMenuOptions()
        {
            _menuOptions = new string[]
            {
                "Sterowanie Manualne",
                "Sterowanie Manualne, alternatywne",
                "Autopilot (metoda lewej ręki)",
                "Autopilot (metoda prawej ręki)",
                "Porównanie obu metod",
                "Wyjście"
            };
            _optionsExplanation = new string[][]
            {
                new string[]
                {
                    "W - góra",
                    "S - dół",
                    "A - lewo",
                    "D - prawo"
                },
                new string[]
                {
                    "W - przód",
                    "S - obrót o 270 stopni",
                    "A - obrót o 180 stopni",
                    "D - obrót o 90 stopni"
                },
                new string[]
                {
                    "Kursor trzyma się lewej ściany"
                },
                new string[]
                {
                    "Kursor trzyma się prawej ściany"
                },
                new string[]
                {
                    "Tworzy 2 wymienione wcześniej kursory",
                    "i porównuje szybkość przejścia labiryntu"
                },
                new string[]
                {
                    "Kończy program"
                }
            };
        }
    }
}
