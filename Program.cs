using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using System.Xml.Linq;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    class Program
    {
        static int verticalTileCount = 16;
        static int horizontalTileCount = 32;
        static char snakeHeadCharacter = '■';
        static char snakeBodyCharacter = '■';
        static ConsoleColor snakeHeadColor = ConsoleColor.Red;
        static ConsoleColor snakeBodyColor = ConsoleColor.Green;
        static int initialBodyLength = 5;
        static void Main(string[] args)
        {
            Console.WindowHeight = verticalTileCount;
            Console.WindowWidth = horizontalTileCount;

            Random randomNumberGenerator = new Random();
            int score = 5;
            bool gameover = false;
            /*Tile snakeHead = new Tile(calculateScreenCenterPoint(), snakeHeadCharacter, snakeHeadColor);
            string movement = "RIGHT";
            List<int> snakeBodySegmentsX = new List<int>();
            List<int> snakeBodySegmentsY = new List<int>();*/
            Snake snake = new Snake(snakeHeadColor, snakeBodyColor, snakeHeadCharacter, snakeBodyCharacter, calculateScreenCenterPoint(), initialBodyLength, Directions.Right);
            int foodX = randomNumberGenerator.Next(0, horizontalTileCount);
            int foodY = randomNumberGenerator.Next(0, verticalTileCount);
            DateTime time = DateTime.Now;
            DateTime time2 = DateTime.Now;
            bool buttonPressed = false;
            while (true)
            {
                Console.Clear();
                int headX = snake.headPosition().xPosition;
                int headY = snake.headPosition().yPosition;

                /*if (!(0 <= headX && headX < horizontalTileCount)) gameover = true;
                if (!(0 <= headY && headY < verticalTileCount)) gameover = true;
                gameover = snake.headCollidesWithBody();*/

                for (int i = 0;i< horizontalTileCount; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }
                for (int i = 0; i < horizontalTileCount; i++)
                {
                    Console.SetCursorPosition(i, verticalTileCount -1);
                    Console.Write("■");
                }
                for (int i = 0; i < verticalTileCount; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }
                for (int i = 0; i < verticalTileCount; i++)
                {
                    Console.SetCursorPosition(horizontalTileCount - 1, i);
                    Console.Write("■");
                }

                if (foodX == headX && foodY == headY)
                {
                    score++;
                    foodX = randomNumberGenerator.Next(1, horizontalTileCount-2);
                    foodY = randomNumberGenerator.Next(1, verticalTileCount-2);
                    snake.eat();
                }

               
                /*for (int i = 0; i < snakeBodySegmentsX.Count(); i++)
                {
                    Console.SetCursorPosition(snakeBodySegmentsX[i], snakeBodySegmentsY[i]);
                    Console.Write("■");
                    if (snakeBodySegmentsX[i] == snakeHead.xPosition() && snakeBodySegmentsY[i] == snakeHead.yPosition())
                    {
                        gameover = true;
                    }
                }*/
                for (int i = 0; i < snake.body.Count; i++)
                {
                    renderTile(snake.body[i]);
                }
                renderTile(snake.head);

                if (gameover)
                {
                    break;
                }
                renderTile(snake.head);
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
                /*switch (movement)
                {
                    case "UP":
                        snakeHead.position.yPosition--;
                        break;
                    case "DOWN":
                        snakeHead.position.yPosition++;
                        break;
                    case "LEFT":
                        snakeHead.position.xPosition--;
                        break;
                    case "RIGHT":
                        snakeHead.position.xPosition++;
                        break;
                }*/
                /*if (snakeBodySegmentsX.Count() > score)
                {
                    snakeBodySegmentsX.RemoveAt(0);
                    snakeBodySegmentsY.RemoveAt(0);
                }*/
            }
            Console.SetCursorPosition(horizontalTileCount / 5, verticalTileCount / 2);
            Console.WriteLine("Game over, Score: "+ score);
            Console.SetCursorPosition(horizontalTileCount / 5, verticalTileCount / 2 +1);
        }

        private static Vector2D calculateScreenCenterPoint()
        {
            return new Vector2D(horizontalTileCount/2, verticalTileCount/2);
        }

        private static void renderTile(Tile tile)
        {
            Console.ForegroundColor = tile.characterColor;
            Console.SetCursorPosition(tile.xPosition(), tile.yPosition());
            Console.Write(tile.character);
        }
    }

    class Vector2D
    {
        public int xPosition { get; set;}
        public int yPosition { get; set;}

        public Vector2D(int xPosition, int yPosition)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
        }

        public Vector2D(Vector2D toCopy)
        {
            xPosition = toCopy.xPosition;
            yPosition = toCopy.yPosition;
        }

        public void addToVector(Vector2D toAdd)
        {
            xPosition += toAdd.xPosition;
            yPosition += toAdd.yPosition;
        }

        public bool compareToVector(Vector2D toCompare)
        {
            return xPosition == toCompare.xPosition && yPosition == toCompare.yPosition;
        }
    }

    class Tile
    {
        public Vector2D position { get; set; }
        public char character { get; set; }
        public ConsoleColor characterColor { get; set; }

        public Tile(int xPosition, int yPosition, char character, ConsoleColor characterColor)
        {
            this.position = new Vector2D(xPosition, yPosition);
            this.character = character;
            this.characterColor = characterColor;
        }

        public Tile(Vector2D position, char character, ConsoleColor characterColor)
        {
            this.position = position;
            this.character = character;
            this.characterColor = characterColor;
        }

        public int xPosition()
        {
            return position.xPosition;
        }

        public int yPosition()
        {
            return position.yPosition;
        }
    }

    static class Directions
    {
        public static Vector2D Up = new Vector2D(0, -1);
        public static Vector2D Down = new Vector2D(0, 1);
        public static Vector2D Left = new Vector2D(-1, 0);
        public static Vector2D Right = new Vector2D(1, 0);
    }

    class Snake
    {
        public Vector2D movementVector { get; set; }
        public ConsoleColor headColor { get; set; }
        public ConsoleColor bodyColor {  get; set; }

        public char headCharacter { get; set; }
        public char bodyCharacter {  get; set; }

        public Tile head { get; }

        public List<Tile> body { get; }


        public Snake(ConsoleColor headColor, ConsoleColor bodyColor,  char headCharacter, char bodyCharacter, Vector2D startingHeadPosition, int startingSegmentCount, Vector2D startingDirection)
        {
            head = new Tile(startingHeadPosition, headCharacter, headColor);
            this.headColor = headColor;
            this.bodyColor = bodyColor;
            this.bodyCharacter = bodyCharacter;
            this.headCharacter = headCharacter;
            this.movementVector = startingDirection;
            body = new List<Tile>();

            for (int i = 0; i < startingSegmentCount; i++)
            {
                eat();
            }
        }

        public void eat()
        {
            Vector2D newTilePosition = new Vector2D(0, 0);
            if (body.Count <= 0)
            {
                newTilePosition = head.position;
            }
            else
            {
                newTilePosition = body[body.Count - 1].position;
            }

            body.Add(new Tile(newTilePosition, bodyCharacter, bodyColor));
        }

        public void move()
        {
            for (int i = body.Count - 1; i >= 1; i--)
            {
                body[i].position = body[i-1].position;
            }
            if (body.Count > 0)
            {
                body[0].position = new Vector2D(head.position);
            }
            head.position.addToVector(this.movementVector);
        }

        public bool headCollidesWithBody()
        {
            for (int i = 0; i < body.Count; i++)
            {
                if (head.position.compareToVector(body[i].position)) return true;
            }
            return false;
        }

        public bool collidesWithHead(Vector2D position)
        {
            return head.position.compareToVector(position);
        }

        public Vector2D headPosition()
        {
            return head.position;
        }
    }
}
