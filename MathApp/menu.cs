namespace MathApp
{
    internal class menu
    {
        gameengine engine = new();
        internal void ShowMenu(string name,DateTime date)
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine($"Hello  {name}. It's {date}. This your math's game.That's great that you're working on improving yourself");
            Console.WriteLine("\n");
            var isGameOn = true;
            do
            {
                Console.Clear();
                Console.WriteLine($@"What game would you like to play today? choose from the options below:
                                    V - View Previous Games
                                    A - Addition
                                    S - Substraction
                                    M - Multiplication
                                    D - Division
                                    Q - Quit the program");
                Console.WriteLine("-------------------------------------------------------------------------");

                var gameSelected = Console.ReadLine();

                switch (gameSelected.Trim().ToLower())
                {
                    case "v":
                        Helpers.GetGames();
                        break;
                    case "a":
                        engine.AdditionGame("Addition game");
                        break;
                    case "s":
                        engine.SubstractionGame("Substraction game");
                        break;
                    case "m":
                        engine.MultiplicationGame("Multiplication game");
                        break;
                    case "d":
                        engine.DivisionGame("Division game");
                        break;
                    case "q":
                        Console.WriteLine("GoodBye");
                        isGameOn = false;
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        Environment.Exit(1);
                        break;
                }
            } while (isGameOn);
        }
    }
}
