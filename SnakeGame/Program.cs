using SnakeGame;

Random random = new Random();
Coord gridDimensions = new Coord(50,20);

Coord SnakePos = new Coord(10, 1);
Direction movementDirection = Direction.Down;
List<Coord> SnakePosHistory = new List<Coord>();
int tailLength = 1;

Coord applePos = new Coord(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));
int frameDelayMilli = 100;
int score = 0;

while(true)
{
    Console.SetCursorPosition(0, 0);
    Console.WriteLine("Score: " + score + "      ");
    SnakePos.ApplyMovementDirection(movementDirection);

    //Render the game to console
    for(int y=0;y< gridDimensions.Y;y++)
    {
        for(int x =0;x<gridDimensions.X;x++)
        {
            Coord curentCoord = new Coord(x, y);

            if (SnakePos.Equals(curentCoord) || SnakePosHistory.Contains(curentCoord))
                Console.Write("■");
            else if (applePos.Equals(curentCoord))
                Console.Write("a");
            else if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1)
                Console.Write("#");
            else
                Console.Write(" ");
        }
        Console.WriteLine();
    }

    if (SnakePos.Equals(applePos)) 
    {
        tailLength++;
        score++;
        applePos = new Coord(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));
    }
    // Check for game over conditions - snake has hit wall or snake has hit tail
    else if (SnakePos.X == 0 || SnakePos.Y == 0 || SnakePos.X == gridDimensions.X - 1 ||
        SnakePos.Y == gridDimensions.Y - 1 || SnakePosHistory.Contains(SnakePos))
        {
        // Reset Game

         score = 0;
        tailLength = 1;
        SnakePos = new Coord(10, 1);
        SnakePosHistory.Clear();
        movementDirection = Direction.Down;
        continue;
        }

    // Add the snake's current position to the snakePosHistory list

    SnakePosHistory.Add(new Coord(SnakePos.X, SnakePos.Y));

    if (SnakePosHistory.Count > tailLength)
        SnakePosHistory.RemoveAt(0);

    // Delay the rendering of next frame for frameDelayMilli milliseconds whilst checking for player input

    DateTime time = DateTime.Now;

    while((DateTime.Now - time).TotalMilliseconds < frameDelayMilli)
    {
        if(Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey().Key;

            // Assign snake new direction to move in based on what key was pressed

            switch(key)
            {
                case ConsoleKey.LeftArrow:
                    movementDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    movementDirection = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    movementDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    movementDirection = Direction.Down;
                    break;
            }
        }
    }
    Thread.Sleep(frameDelayMilli);
}


