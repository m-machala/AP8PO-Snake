using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Tile
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
}
