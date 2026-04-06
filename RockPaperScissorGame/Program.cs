using System;
namespace RockPaperSciccorGame
{
    class program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int PlayerScore = 0;
            int EnemyScore = 0;

            Console.WriteLine("Welcome to rock paper scissors");

            while (PlayerScore != 3 && EnemyScore != 3)
            {
                Console.WriteLine();
                Console.WriteLine("PlayerScore:" + PlayerScore + ". EnemyScore" + EnemyScore);
                Console.WriteLine("please enter 'r' for rock 'p' for paper or anything else for scissors");
                string playerChoice = Console.ReadLine();

                int enemyChoice = random.Next(0, 3);

                if (enemyChoice == 0) // enemy chooses rock
                {
                    Console.WriteLine("Enemy chooses rock");

                    switch (playerChoice)
                    {
                        case "r":
                            Console.WriteLine("Tie!");
                            break;
                        case "p":
                            Console.WriteLine("Player wins!");
                            PlayerScore++;
                            break;
                        default:
                            Console.WriteLine("Enemy Wins!");
                            break;
                    }
                }
                else if (enemyChoice == 1)
                {
                    Console.WriteLine("Enemy chooses paper");

                    switch (playerChoice)
                    {
                        case "r":
                            Console.WriteLine("Enemy wins!");
                            EnemyScore++;
                            break;
                        case "p":
                            Console.WriteLine("Tie");
                            break;
                        default:
                            Console.WriteLine("PlayerScore Wins");
                            PlayerScore++;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Enemy chooses scissor");
                    switch (playerChoice)
                    {
                        case "r":
                            Console.WriteLine("Player wins");
                            PlayerScore++;
                            break;
                        case "p":
                            Console.WriteLine("Enemy wins");
                            EnemyScore++;
                            break;
                        default:
                            Console.WriteLine("Tie!");
                            break;
                    }

                    if (PlayerScore == 3)
                    {
                        Console.WriteLine("Player Wins");
                    }
                    else
                    {
                        Console.WriteLine("Enemy wins");
                    }
                }
            }
        }
    }
}






























