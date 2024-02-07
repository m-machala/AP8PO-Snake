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
        static void Main(string[] args)
        {
            Console.WindowHeight = verticalTileCount;
            Console.WindowWidth = horizontalTileCount;

            Random randomNumberGenerator = new Random();
            int score = 5;
            bool gameover = false;
            tile snakeHead = new tile();
            snakeHead.xPosition = horizontalTileCount/2;
            snakeHead.yPosition = verticalTileCount/2;
            snakeHead.screenColor = ConsoleColor.Red;
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
                if (snakeHead.xPosition == horizontalTileCount-1 || snakeHead.xPosition == 0 ||snakeHead.yPosition == verticalTileCount-1 || snakeHead.yPosition == 0)
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
                if (foodX == snakeHead.xPosition && foodY == snakeHead.yPosition)
                {
                    score++;
                    foodX = randomNumberGenerator.Next(1, horizontalTileCount-2);
                    foodY = randomNumberGenerator.Next(1, verticalTileCount-2);
                } 
                for (int i = 0; i < snakeBodySegmentsX.Count(); i++)
                {
                    Console.SetCursorPosition(snakeBodySegmentsX[i], snakeBodySegmentsY[i]);
                    Console.Write("■");
                    if (snakeBodySegmentsX[i] == snakeHead.xPosition && snakeBodySegmentsY[i] == snakeHead.yPosition)
                    {
                        gameover = true;
                    }
                }
                if (gameover)
                {
                    break;
                }
                Console.SetCursorPosition(snakeHead.xPosition, snakeHead.yPosition);
                Console.ForegroundColor = snakeHead.screenColor;
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
                snakeBodySegmentsX.Add(snakeHead.xPosition);
                snakeBodySegmentsY.Add(snakeHead.yPosition);
                switch (movement)
                {
                    case "UP":
                        snakeHead.yPosition--;
                        break;
                    case "DOWN":
                        snakeHead.yPosition++;
                        break;
                    case "LEFT":
                        snakeHead.xPosition--;
                        break;
                    case "RIGHT":
                        snakeHead.xPosition++;
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

        private point2D calculateScreenCenterPoint()
        {

        }
    }
    /*class pixel
    {
        public int xPosition { get; set; }
        public int yPosition { get; set; }
        public ConsoleColor screenColor { get; set; }
    }*/

    class point2D
    {
        public int xPosition { get; set;}
        public int yPosition { get; set;}

        public point2D(int xPosition, int yPosition)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
        }
    }

    class tile
    {
        public point2D position { get; set; }
        public char character { get; set; }
        public ConsoleColor characterColor { get; set; }

        public tile(int xPosition, int yPosition, char character, ConsoleColor characterColor)
        {
            this.position = new point2D(xPosition, yPosition);
            this.character = character;
            this.characterColor = characterColor;
        }
    }
}
