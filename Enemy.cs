using Raylib_cs;
using System.Numerics;

namespace crazy_s_hit_arena;

abstract class Enemy
{
    public Vector2 Position;
    public Vector2 Size;
    public int Health;
    public Color Color;
    
    public Rectangle GetRect()
    {
        return new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
    }

    public void Draw()
    {
        Raylib.DrawRectangleV(Position, Size, Color);
    }

    public void takeDamage(int takeDamage)
    {
        Health = Health - takeDamage;
    }
}

class Warrior : Enemy
{
    public Warrior(Vector2 startPosition)
    {
        Position = startPosition;
        Size = new Vector2(20, 20);
        Health = 100;
        Color = Color.Red;
    }
}