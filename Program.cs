using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace crazy_s_hit_arena;

public enum State
{
    Start,
    Gameplay,
    End
}

public static class Program
{
    public static void Main()
    {
        Raylib.InitWindow(800, 450, "Crazy Hit Arena");
        int sFont = 20;
        int xCenter = Raylib.GetScreenWidth() / 2;
        int yCenter = Raylib.GetScreenHeight() / 2;
        int numOfEnemies = 5;
        Player player = new Player(new Vector2(xCenter, yCenter));
        List<Bullet> bullets = new List<Bullet>();
        List<Enemy> enemies = new List<Enemy>();
        State state = State.Start;

        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            float dt = Raylib.GetFrameTime();

            if (state == State.Start)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RayWhite);
                Raylib.DrawText("Crazy (S)hit Arena", (xCenter - sFont - 80), (yCenter - sFont), sFont, Color.LightGray);
                Rectangle button = new Rectangle(xCenter - 100, yCenter + 20, 200, 100);
                Raylib.DrawRectangleRec(button, Color.White);
                Raylib.DrawRectangleLines(xCenter - 100, yCenter + 20, 200, 100, Color.LightGray);
                Raylib.DrawText("Play", xCenter - 20, yCenter + 60, sFont, Color.Orange);

                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    Vector2 pos = Raylib.GetMousePosition();
                    if (Raylib.CheckCollisionPointRec(pos, button)) state = State.Gameplay;
                }
            }
            else if (state == State.Gameplay)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RayWhite);

                for (int i = numOfEnemies - 1; i >= 0; i--)
                {
                    
                }
                
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    bullets.Add(new Bullet(player.Position, Raylib.GetMousePosition()));
                }

                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    bullets[i].Update(dt);
                    bullets[i].Draw();

                    if (bullets[i].Position.X < 0 || bullets[i].Position.X > 800 || bullets[i].Position.Y < 0 || bullets[i].Position.Y > 450)
                    {
                        bullets.RemoveAt(i);
                    }
                }

                player.Update(dt);
                player.Draw();
            }
            else if (state == State.End)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RayWhite);
                Raylib.DrawText("end", xCenter, yCenter, sFont, Color.Orange);
            }
            else
            {
                Raylib.CloseWindow();
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}