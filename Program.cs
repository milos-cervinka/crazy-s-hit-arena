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
        Raylib.InitWindow(1024, 768, "Crazy Hit Arena");
        int sFont = 20;
        int xCenter = Raylib.GetScreenWidth() / 2;
        int yCenter = Raylib.GetScreenHeight() / 2;
        int numOfEnemies = 5;
        Random rnd =  new Random();
        Player player = new Player(new Vector2(xCenter, yCenter));
        List<Bullet> bullets = new List<Bullet>();
        State state = State.Start;
        
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < numOfEnemies; i++)
        {
            Vector2 spawnPos = new Vector2(
                rnd.Next(0, Raylib.GetScreenWidth() - 20),
                rnd.Next(0, Raylib.GetScreenHeight() - 20)
            );
            enemies.Add(new Warrior(spawnPos));
        }

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
                
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw();
                }
                
                
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    bullets.Add(new Bullet(player.Position, Raylib.GetMousePosition()));
                }

                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    bullets[i].Update(dt);
                    bullets[i].Draw();

                    if (bullets[i].Position.X < 0 || bullets[i].Position.X > Raylib.GetScreenWidth() || bullets[i].Position.Y < 0 || bullets[i].Position.Y > Raylib.GetScreenHeight())
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