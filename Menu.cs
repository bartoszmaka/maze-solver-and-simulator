using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zaj08
{
    static class Menu
    {
        private static string[] MenuOptions;
        private static int choice;
        private static bool choosen;
        static Menu()
        {
            choosen = false;
            choice = 0;
            FillMenuOptions();
        }
        public static void Play()
        {
            while (!choosen)
            {
                Display();
                KeysManager();
            }
            ApplyChoice();
        }

        private static void KeysManager()
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
                    choosen = true;
                    break;
                default:
                    break;
            }
        }
        private static void ApplyChoice()
        {
            switch (choice)
            {
                case 0: {
                    Game.Play("Manual");
                    break;
                }
                case 1:
                    {
                    Game.Play("Left");
                    break;
                }
                case 2: {
                    Game.Play("Right");
                    break;
                }
                case 3: {
                    Game.Play("Both");
                    break;
                }
                default:
                    break;
            }
        }
        private static void DecrementChoice()
        {
            choice = (choice + 1) % MenuOptions.Length;
        }
        private static void IncrementChoice()
        {
            choice = (choice + MenuOptions.Length - 1) % MenuOptions.Length;
        }
        private static void Display()
        {
            Console.Clear();
            for (int i = 0; i < MenuOptions.Length; i++)
            {
                Console.SetCursorPosition(5, 5 + i);
                if (choice == i) { Console.ForegroundColor = ConsoleColor.Green; }
                Console.WriteLine(MenuOptions[i]);
                Console.ResetColor();
            }
            Console.SetCursorPosition(0, 0);
        }
        private static void FillMenuOptions()
        {
            MenuOptions = new string[]
            {
                "Sterowanie Manualne",
                "Autopilot (metoda lewej ręki)",
                "Autopilot (metoda prawej ręki)",
                "Porównanie obu metod",
                "Wyjście"
            };
        }
    }
}
