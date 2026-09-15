using Raylib_cs;
using System.Numerics;

namespace crazy_s_hit_arena;

abstract class Enemy
{
    protected Vector2 position;
    protected Vector2 size;
    protected int health;
    protected Color color;

    public void Draw()
    {
        Raylib.DrawRectangleV(position, size, color);
    }
}

class Warrior : Enemy
{
    public Warrior()
    {
        size.X = 20;
        size.Y = 20;
        health = 100;
        color = Color.Green;
    }
}
