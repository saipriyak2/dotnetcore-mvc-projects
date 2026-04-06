using MathApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp
{
    internal class Helpers
    {
        internal static List<Game> games = new List<Game>
        {
            new Game {Date = DateTime.Now.AddDays(1), Type = GameType.Addititon, Score = 5},
            new Game {Date = DateTime.Now.AddDays(2), Type = GameType.Multiplication, Score = 4},
            new Game {Date = DateTime.Now.AddDays(3), Type = GameType.Division, Score = 4},
            new Game {Date = DateTime.Now.AddDays(4), Type = GameType.Substraction, Score = 3},
            new Game {Date = DateTime.Now.AddDays(5), Type = GameType.Addititon, Score = 5},
            new Game {Date = DateTime.Now.AddDays(6), Type = GameType.Multiplication, Score = 4},
            new Game {Date = DateTime.Now.AddDays(7), Type = GameType.Division, Score = 4},
            new Game {Date = DateTime.Now.AddDays(8), Type = GameType.Substraction, Score = 3},
            new Game {Date = DateTime.Now.AddDays(9), Type = GameType.Addititon, Score = 5},
            new Game {Date = DateTime.Now.AddDays(10), Type = GameType.Multiplication, Score = 4},
            new Game {Date = DateTime.Now.AddDays(11), Type = GameType.Division, Score = 4},
            new Game {Date = DateTime.Now.AddDays(12), Type = GameType.Substraction, Score = 3},
            new Game {Date = DateTime.Now.AddDays(13), Type = GameType.Substraction, Score = 3},
        };
        internal static void GetGames()
        {
            var gamesToPrint = games.Where(x => x.Type == GameType.Division);
            Console.Clear();
            Console.WriteLine("Game History");
            Console.WriteLine("-----------------------------------------");
            foreach (var game in games)
            {
                Console.WriteLine($"{game.Date} - {game.Type}: {game.Score} pts ");
            }
            Console.WriteLine("-----------------------------------------\n");
            Console.WriteLine("Press any key to return into main menu");
            Console.ReadLine();

        }

        internal static int[] GetDivisionNumbers()
        {
            var random = new Random();
            var firstNumber = random.Next(0, 99);
            var secondNumber = random.Next(0, 99);

            var result = new int[2];
            result[0] = firstNumber;
            result[1] = secondNumber;

            while (firstNumber % secondNumber != 0)
            {
                firstNumber = random.Next(1, 99);
                secondNumber = random.Next(1, 99);
            }
            result[0] = firstNumber;
            result[1] = secondNumber;



            return result;
        }

        internal static void AddToHistory(int gameScore, GameType gameType)
        {
            games.Add(new Game
            {
                Date = DateTime.Now,
                Score = gameScore,
                Type = gameType

            });
               

                


        }

    }
}
