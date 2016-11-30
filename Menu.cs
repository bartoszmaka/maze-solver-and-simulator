using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zaj08
{
    class Menu
    {
        private string[] MenuOptions;
        private int choice;
        private bool choosen;
        public Menu()
        {
            choosen = false;
            choice = 0;
            FillMenuOptions();
        }
        public void Play()
        {
            while (!choosen)
            {
                Display();
                KeysManager();
            }
            ApplyChoice();
        }

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
                    choosen = true;
                    break;
                default:
                    break;
            }
        }
        private void ApplyChoice()
        {
            Game G1;
            switch (choice)
            {
                case 0:
                    G1 = new Game();
                    break;
                case 1:
                    G1 = new Game("Left");
                    break;
                case 2:
                    G1 = new Game("Right");
                    break;
                case 3:
                    G1 = new Game("Both");
                    break;
                default:
                    break;
            }
        }
        private void DecrementChoice()
        {
            choice = (choice + 1) % MenuOptions.Length;
        }
        private void IncrementChoice()
        {
            choice = (choice + MenuOptions.Length - 1) % MenuOptions.Length;
        }
        private void Display()
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
        private void FillMenuOptions()
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
