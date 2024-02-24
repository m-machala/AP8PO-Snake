using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Snake
    {
        public Vector2D movementVector { get; set; }
        private ConsoleColor bodyColor;

        private char bodyCharacter;

        public Tile head { get; }

        public List<Tile> body { get; }


        public Snake(ConsoleColor headColor, ConsoleColor bodyColor, char headCharacter, char bodyCharacter, Vector2D startingHeadPosition, int startingSegmentCount, Vector2D startingDirection)
        {
            head = new Tile(startingHeadPosition, headCharacter, headColor);
            this.bodyColor = bodyColor;
            this.bodyCharacter = bodyCharacter;
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
                newTilePosition.addToVector(head.position);
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
                body[i].position = body[i - 1].position;
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
