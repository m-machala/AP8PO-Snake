﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class ConsoleRenderer : Renderer
    {
        private static char snakeHeadCharacter = '■';
        private static ConsoleColor snakeHeadColor = ConsoleColor.Red;

        private static char snakeBodyCharacter = '■';
        private static ConsoleColor snakeBodyColor = ConsoleColor.Green;

        private static char foodCharacter = '■';
        private static ConsoleColor foodColor = ConsoleColor.Cyan;

        private static char borderCharacter = '■';
        private static ConsoleColor borderColor = ConsoleColor.Cyan;

        public void renderSnake(Snake snake)
        {
            Console.ForegroundColor = snakeBodyColor;
            for (int i = 0; i < snake.body.Count; i++)
            {
                Console.SetCursorPosition(snake.body[i].position.xPosition, snake.body[i].position.yPosition);
                Console.Write(snakeBodyCharacter);
            }

            Console.ForegroundColor = snakeHeadColor;
            Console.SetCursorPosition(snake.head.position.xPosition, snake.head.position.yPosition);
            Console.Write(snakeHeadCharacter);
        }
        public void renderFood(Food food)
        {
            Console.ForegroundColor = foodColor;
            Console.SetCursorPosition(food.position.xPosition, food.position.yPosition);
            Console.Write(foodCharacter);

        }
        public void renderBorders(Vector2D borderSize)
        {
            Console.ForegroundColor = borderColor;
            for (int y = 0; y < borderSize.yPosition; y++)
            {
                for (int x = 0; x < borderSize.xPosition; x++)
                {
                    if((x == 0 || x == borderSize.xPosition - 1) || (y == 0 || y == borderSize.yPosition - 1)) {
                        Console.SetCursorPosition(x, y);
                        Console.Write(borderCharacter);
                    }
                }
            }
        }
    }
}
