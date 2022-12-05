using System;
using System.Net.WebSockets;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            //IMPORT ALLOWED WORDS
            string[] words = File.ReadAllLines("words.txt");

            //Choosing play word randomly
            string playWord = words[random.Next(words.Length)];

            List<char> guessedCorrectLetters = new List<char>();
            const int initialPlayerLives = 6;
            int playerLives = initialPlayerLives;
            char enteredLetter;
            bool playerWon = false;
            Console.WriteLine($"Lives left: {playerLives}");

            while (true)
            {
                Program.DrawHangman(playerLives);
                if (playerWon)
                {
                    Console.WriteLine("You WON!");
                    if(! Program.PlayAgain(ref playerLives, ref guessedCorrectLetters, ref playWord, words, initialPlayerLives, random))
                    {
                        break;
                    }
                }
                if (playerLives < 1)
                {
                    Console.WriteLine("You LOST!");
                    if (!Program.PlayAgain(ref playerLives, ref guessedCorrectLetters, ref playWord, words, initialPlayerLives, random))
                    {
                        break;
                    }
                }
                enteredLetter = Program.EnterLetter();
                Console.Clear();
                playerWon = Program.ProccessWord(playWord, enteredLetter, ref guessedCorrectLetters, ref playerLives);
                Console.WriteLine();
            }
        }

        static bool ProccessWord(string playWord, 
            char guessedLetter,
            ref List<char> guessedLetters,
            ref int playerLives
         )
        {
            bool playerWon = true;
            bool deductLive = true;
            for (int i = 0; i < playWord.Length; i++)
            {
                if (playWord[i] == guessedLetter)
                {
                    guessedLetters.Add(guessedLetter);
                    deductLive = false;
                }
                if (guessedLetters.Contains(playWord[i]))
                {
                    Console.Write($" {playWord[i]} ");
                    continue;
                }
                Console.Write(" _ ");
                playerWon = false;
            }
            if (deductLive)
            {
                playerLives--;
            }
            Console.WriteLine();
            Console.WriteLine($"Lives left: {playerLives}");
            return playerWon;
        }

        static char EnterLetter()
        {
            char enteredLetter;
            while (true)
            {
                Console.WriteLine("Enter letter:");
                enteredLetter = Char.ToLower(Console.ReadKey().KeyChar);
                if (Char.IsLetter(enteredLetter))
                {
                    break;
                }
                Console.WriteLine("Wrong input!");
            }

            return enteredLetter;
        }

        static bool PlayAgain(
            ref int playerLives,
            ref List<char> guessedLetters,
            ref string playWord,
            string[] words,
            int initialPlayerLives,
            Random random
            )
        {
            Console.WriteLine("Play again? y/n");
            char enteredLetter = Char.ToLower(Console.ReadKey().KeyChar);
            while (true)
            {
                if (enteredLetter == 'y')
                {
                    Console.Clear();
                    playerLives = initialPlayerLives;
                    guessedLetters = new List<char>();
                    playWord = words[random.Next(words.Length)];
                    return true;
                }
                if (enteredLetter == 'n')
                {
                    return false;
                }
                enteredLetter = Char.ToLower(Console.ReadKey().KeyChar);
            }
        }

        static void DrawHangman(int livesLeft)
        {
            Console.WriteLine("H A N G M A N");
            switch (livesLeft)
            {
                case 5:
                    Console.WriteLine(@"
                        +---+
                        O     |
                              |
                              |
                             ===
                    ");
                    break;
                case 4:
                    Console.WriteLine(@"
                        +---+
                        O     |
                        |     |
                              |
                             ===
                    ");
                    break;
                case 3:
                    Console.WriteLine(@"
                        +---+
                        O     |
                       /|     |
                              |
                             ===
                    ");
                    break;
                case 2:
                    Console.WriteLine(@"
                        +---+
                        O     |
                       /|\    |
                              |
                             ===
                    ");
                    break;
                case 1:
                    Console.WriteLine(@"
                        +---+
                        O     |
                       /|\    |
                       /      |
                             ===
                    ");
                    break;
                case 0:
                    Console.WriteLine(@"
                        +---+
                        O     |
                       /|\    |
                       / \    |
                             ===
                    ");
                    break;
                default:
                    Console.WriteLine(@"
                        +---+
                              |
                              |
                              |
                             ===
                    ");
                    break;
            }
        }
    }
}