using System.Numerics;
using Raylib_cs;

namespace crazy_s_hit_arena;

public class Bullet
{
    public Vector2 Position;
    public Vector2 Direction;
    public float Speed = 500f;

    public Bullet(Vector2 startPosition, Vector2 targetPosition)
    {
        Position = startPosition;
        Vector2 diff = targetPosition - startPosition;
        if (diff.LengthSquared() > 0)
        {
            Direction = Vector2.Normalize(diff);
        }
    }

    public void Update(float dt)
    {
        Position += Direction * Speed * dt;
    }

    public void Draw()
    {
        Raylib.DrawCircleV(Position, 5, Color.Red);
    }
}