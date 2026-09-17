using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Raylib_cs;

namespace crazy_s_hit_arena;

public class Player
{
    public Vector2 Position;
    public float Speed = 250f;
    public float Radius = 20f;

    public Player(Vector2 startPosition)
    {
        Position = startPosition;
    }

    public void Update(float dt)
    {
        Vector2 input = Vector2.Zero;

        if (Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D)) input.X += 1;
        if (Raylib.IsKeyDown(KeyboardKey.Left)  || Raylib.IsKeyDown(KeyboardKey.A)) input.X -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.Down)  || Raylib.IsKeyDown(KeyboardKey.S)) input.Y += 1;
        if (Raylib.IsKeyDown(KeyboardKey.Up)    || Raylib.IsKeyDown(KeyboardKey.W)) input.Y -= 1;
        
        if (input.LengthSquared() > 0)
        {
            if (!((Position.X + 20) > Raylib.GetScreenWidth()) &&  !(Position.X < 20))
            Position += Vector2.Normalize(input) * Speed * dt;
        }
    }

    public void Draw()
    {
        Raylib.DrawCircleV(Position, Radius, Color.Green);
    }
}