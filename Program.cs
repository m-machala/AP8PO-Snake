using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    class Program
    {
        static int verticalTileCount = 16;
        static int horizontalTileCount = 32;
        static Vector2D borderDimensions = new Vector2D(horizontalTileCount, verticalTileCount);

        static char snakeHeadCharacter = '■';
        static char snakeBodyCharacter = '■';

        static ConsoleColor snakeHeadColor = ConsoleColor.Red;
        static ConsoleColor snakeBodyColor = ConsoleColor.Green;

        static int initialBodyLength = 5;

        static void Main(string[] args)
        {
            Renderer renderer = new ConsoleRenderer();
            

            Console.WindowHeight = verticalTileCount;
            Console.WindowWidth = horizontalTileCount;

            Random randomNumberGenerator = new Random();
            int score = 5;
            bool gameover = false;

            Snake snake = new Snake(snakeHeadColor, snakeBodyColor, snakeHeadCharacter, snakeBodyCharacter, calculateScreenCenterPoint(), initialBodyLength, Directions.Right);
            Food food = new Food(randomPosition(borderDimensions));

            DateTime time = DateTime.Now;
            DateTime time2 = DateTime.Now;
            bool buttonPressed = false;
            int headX = snake.head.xPosition;
            int headY = snake.head.yPosition;
            int foodX = food.position.xPosition;
            int foodY = food.position.yPosition;
            while (!gameover)
            {
                Console.Clear();
                renderer.renderBorders(borderDimensions);

                if (foodX == headX && foodY == headY)
                {
                    score++;
                    foodX = randomNumberGenerator.Next(1, horizontalTileCount-2);
                    foodY = randomNumberGenerator.Next(1, verticalTileCount-2);
                    snake.eat();
                }
                renderer.renderSnake(snake);

                Console.SetCursorPosition(foodX, foodY);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                time = DateTime.Now;
                buttonPressed = false;
                while (true)
                {
                    time2 = DateTime.Now;
                    if (time2.Subtract(time).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                        if (!buttonPressed)
                        {
                            Vector2D newMovementVector = new Vector2D(0, 0);
                            if (pressedKey.Key.Equals(ConsoleKey.UpArrow))
                            {
                                newMovementVector = Directions.Up;
                                buttonPressed = true;
                            }
                            if (pressedKey.Key.Equals(ConsoleKey.DownArrow))
                            {
                                newMovementVector = Directions.Down;
                                buttonPressed = true;
                            }
                            if (pressedKey.Key.Equals(ConsoleKey.LeftArrow))
                            {
                                newMovementVector = Directions.Left;
                                buttonPressed = true;
                            }
                            if (pressedKey.Key.Equals(ConsoleKey.RightArrow))
                            {
                                newMovementVector = Directions.Right;
                                buttonPressed = true;
                            }

                            Vector2D compareVector = new Vector2D(snake.movementVector);
                            compareVector.addToVector(newMovementVector);
                            if(!compareVector.compareToVector(new Vector2D(0, 0)))
                            {
                                snake.movementVector = newMovementVector;
                            }
                        }
                        
                    }
                }
                snake.move();
                headX = snake.head.xPosition;
                headY = snake.head.yPosition;
                if (!(0 <= headX && headX < horizontalTileCount)) gameover = true;
                else if (!(0 <= headY && headY < verticalTileCount)) gameover = true;
                else gameover = snake.headCollidesWithBody();
            }
            Console.SetCursorPosition(horizontalTileCount / 5, verticalTileCount / 2);
            Console.WriteLine("Game over, Score: "+ score);
            Console.SetCursorPosition(horizontalTileCount / 5, verticalTileCount / 2 +1);
        }

        private static Vector2D calculateScreenCenterPoint()
        {
            return new Vector2D(horizontalTileCount/2, verticalTileCount/2);
        }

        private static Vector2D randomPosition(Vector2D screenDimensions)
        {
            Random random = new Random();
            return new Vector2D(random.Next(1, screenDimensions.xPosition - 1), random.Next(1, screenDimensions.yPosition - 1));
        }
    }  
}
