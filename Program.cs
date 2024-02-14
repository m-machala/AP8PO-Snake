using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    class Program
    {
        static int verticalTileCount = 16;
        static int horizontalTileCount = 32;
        static char snakeHeadCharacter = '■';
        static ConsoleColor snakeHeadColor = ConsoleColor.Red;
        static void Main(string[] args)
        {
            Console.WindowHeight = verticalTileCount;
            Console.WindowWidth = horizontalTileCount;

            Random randomNumberGenerator = new Random();
            int score = 5;
            bool gameover = false;
            Tile snakeHead = new Tile(calculateScreenCenterPoint(), snakeHeadCharacter, snakeHeadColor);
            string movement = "RIGHT";
            List<int> snakeBodySegmentsX = new List<int>();
            List<int> snakeBodySegmentsY = new List<int>();
            int foodX = randomNumberGenerator.Next(0, horizontalTileCount);
            int foodY = randomNumberGenerator.Next(0, verticalTileCount);
            DateTime time = DateTime.Now;
            DateTime time2 = DateTime.Now;
            bool buttonPressed = false;
            while (true)
            {
                Console.Clear();
                if (snakeHead.xPosition() == horizontalTileCount-1 || snakeHead.xPosition() == 0 ||snakeHead.yPosition() == verticalTileCount-1 || snakeHead.yPosition() == 0)
                { 
                    gameover = true;
                }
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
                Console.ForegroundColor = ConsoleColor.Green;
                if (foodX == snakeHead.xPosition() && foodY == snakeHead.yPosition())
                {
                    score++;
                    foodX = randomNumberGenerator.Next(1, horizontalTileCount-2);
                    foodY = randomNumberGenerator.Next(1, verticalTileCount-2);
                } 
                for (int i = 0; i < snakeBodySegmentsX.Count(); i++)
                {
                    Console.SetCursorPosition(snakeBodySegmentsX[i], snakeBodySegmentsY[i]);
                    Console.Write("■");
                    if (snakeBodySegmentsX[i] == snakeHead.xPosition() && snakeBodySegmentsY[i] == snakeHead.yPosition())
                    {
                        gameover = true;
                    }
                }
                if (gameover)
                {
                    break;
                }
                Console.SetCursorPosition(snakeHead.xPosition(), snakeHead.yPosition());
                Console.ForegroundColor = snakeHead.characterColor;
                Console.Write("■");
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
                        ConsoleKeyInfo toets = Console.ReadKey(true);
                        //Console.WriteLine(toets.Key.ToString());
                        if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN" && !buttonPressed)
                        {
                            movement = "UP";
                            buttonPressed = true;
                        }
                        if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != "UP" && !buttonPressed)
                        {
                            movement = "DOWN";
                            buttonPressed = true;
                        }
                        if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT" && !buttonPressed)
                        {
                            movement = "LEFT";
                            buttonPressed = true;
                        }
                        if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT" && !buttonPressed)
                        {
                            movement = "RIGHT";
                            buttonPressed = true;
                        }
                    }
                }
                snakeBodySegmentsX.Add(snakeHead.xPosition());
                snakeBodySegmentsY.Add(snakeHead.yPosition());
                switch (movement)
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
                }
                if (snakeBodySegmentsX.Count() > score)
                {
                    snakeBodySegmentsX.RemoveAt(0);
                    snakeBodySegmentsY.RemoveAt(0);
                }
            }
            Console.SetCursorPosition(horizontalTileCount / 5, verticalTileCount / 2);
            Console.WriteLine("Game over, Score: "+ score);
            Console.SetCursorPosition(horizontalTileCount / 5, verticalTileCount / 2 +1);
        }

        private static Vector2D calculateScreenCenterPoint()
        {
            return new Vector2D(horizontalTileCount/2, verticalTileCount/2);
        }
    }
    /*class pixel
    {
        public int xPosition { get; set; }
        public int yPosition { get; set; }
        public ConsoleColor screenColor { get; set; }
    }*/

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

        private ConsoleColor bodyColor;

        private char bodyCharacter;

        private Tile head;

        private List<Tile> body;


        public Snake(ConsoleColor headColor, ConsoleColor bodyColor,  char headCharacter, char bodyCharacter, Vector2D startingHeadPosition, int startingSegmentCount, Vector2D startingDirection)
        {
            head = new Tile(startingHeadPosition, headCharacter, bodyColor);
            this.bodyColor = bodyColor;
            this.bodyCharacter = bodyCharacter;
            this.movementVector = startingDirection;
            body = new List<Tile>();

            for (int i = 0; i < startingSegmentCount; i++)
            {
                eat();
            }
        }

        private void eat()
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
    }
}
