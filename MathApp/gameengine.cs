using MathApp.Models;

namespace MathApp
{
    internal class gameengine
    {
        internal void AdditionGame(string msg)
        {
            Console.WriteLine(msg);
            var random = new Random();
            var score = 0;
            int firstNumber;
            int secondNumber;

            for (int i = 0; i < 5; i++)
            {
                firstNumber = random.Next(1, 9);
                secondNumber = random.Next(1, 9);

                Console.WriteLine($"{firstNumber} + {secondNumber}");
                var result = Console.ReadLine();

                if (int.Parse(result) == firstNumber + secondNumber)
                {
                    Console.WriteLine("Your answer was correct.Type any key for the next question.");
                    score++;
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Your answer was incorrect.Type any key for the next question.");
                    Console.ReadLine();
                }
                if (i == 4) Console.WriteLine($"Game Over.Your final score is {score} .Press any key to go back to main menu.");
                Console.ReadLine();
            }
            Helpers.AddToHistory(score, GameType.Addititon);



        }
        internal void SubstractionGame(string msg)
        {
            Console.WriteLine(msg);
            var random = new Random();
            var score = 0;
            int firstNumber;
            int secondNumber;

            for (int i = 0; i < 5; i++)
            {
                firstNumber = random.Next(1, 9);
                secondNumber = random.Next(1, 9);

                Console.WriteLine($"{firstNumber} - {secondNumber}");
                var result = Console.ReadLine();

                if (int.Parse(result) == firstNumber - secondNumber)
                {
                    Console.WriteLine("Your answer was correct.Type any key for the next question.");
                    score++;
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Your answer was incorrect.Type any key for the next question.");
                    Console.ReadLine();
                }
                if (i == 4) Console.WriteLine($"Game Over.Your final score is {score}.Press any key to go back to main menu.");
                Console.ReadLine();
            }
            Helpers.AddToHistory(score, GameType.Substraction);


        }
        internal void MultiplicationGame(string msg)
        {
            Console.WriteLine(msg);
            var random = new Random();
            var score = 0;
            int firstNumber;
            int secondNumber;

            for (int i = 0; i < 5; i++)
            {
                firstNumber = random.Next(1, 9);
                secondNumber = random.Next(1, 9);

                Console.WriteLine($"{firstNumber} * {secondNumber}");
                var result = Console.ReadLine();

                if (int.Parse(result) == firstNumber * secondNumber)
                {
                    Console.WriteLine("Your answer was correct.Type any key for the next question.");
                    score++;
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Your answer was incorrect.Type any key for the next question.");
                    Console.ReadLine();
                }
                if (i == 4) Console.WriteLine($"Game Over.Your final score is {score}.Press any key to go back to main menu.");
                Console.ReadLine();
            }
            Helpers.AddToHistory(score, GameType.Multiplication);

        }
        internal void DivisionGame(string msg)
        {
            var score = 0;
            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Console.WriteLine(msg);
                var divisionNumbers = Helpers.GetDivisionNumbers();
                var firstNumber = divisionNumbers[0];
                var secondNumber = divisionNumbers[1];

                Console.WriteLine($"{firstNumber}/{secondNumber}");
                var result = Console.ReadLine();

                if (int.Parse(result) == firstNumber / secondNumber)
                {
                    Console.WriteLine("Your answer was correct.Type any key for the next question.");
                    score++;
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Your answer was incorrect.Type any key for the next question.");
                    Console.ReadLine();
                }
                if (i == 4) Console.WriteLine($"Game Over.Your final score is {score} .Press any key to go back to main menu.");
                Console.ReadLine();
            }
            Helpers.AddToHistory(score, GameType.Division);
        }

    }
}
