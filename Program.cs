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
    public static (int X, int Y) GetPos(int divider = 1)
    {
        return (Raylib.GetScreenWidth() / divider, Raylib.GetScreenHeight() / divider);
    }
    
    public static void Main()
    {
        Raylib.InitWindow(1024, 768, "Crazy Hit Arena");
        int press_counter = 1;
        int sFont = 20;
        int numOfEnemies = 5;
        int Score = 0;
        Random rnd =  new Random();
        Player player = new Player(new Vector2(GetPos(2).X, GetPos(2).Y));
        List<Bullet> bullets = new List<Bullet>();
        State state = State.Start;
        
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < numOfEnemies; i++)
        {
            Vector2 spawnPos = new Vector2(
                rnd.Next(0, GetPos().X - 20),
                rnd.Next(0, GetPos().Y - 20)
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
                Raylib.DrawText("Crazy (S)hit Arena", (GetPos(2).X - sFont - 80), (GetPos(2).Y - sFont), sFont, Color.LightGray);
                Rectangle button = new Rectangle(GetPos(2).X - 100, GetPos(2).Y + 20, 200, 100);
                Raylib.DrawRectangleRec(button, Color.White);
                Raylib.DrawRectangleLinesEx(button, 2, Color.DarkBlue);
                Raylib.DrawText("Play", GetPos(2).X - 20, GetPos(2).Y + 60, sFont, Color.Orange);
                Raylib.DrawText("Press \'f\' fullscreen",  GetPos(2).X - 100, Raylib.GetScreenHeight() - 50, sFont, Color.Orange);
                
                if (Raylib.IsKeyPressed(KeyboardKey.F))
                {
                    if (press_counter % 2 == 1)
                    {
                        Raylib.ToggleFullscreen();
                        press_counter++;
                    }
                    else
                    {
                        Raylib.ToggleFullscreen();
                        press_counter++;
                    }
                }
                
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
                Raylib.DrawText($"Score : {Score}",  20, 20, sFont, Color.Orange);
                for (int i = 0; i < enemies.Count; i++) enemies[i].Draw();
                
                
                if (Raylib.IsMouseButtonPressed(MouseButton.Left)) bullets.Add(new Bullet(player.Position, Raylib.GetMousePosition()));
                

                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    bullets[i].Update(dt);
                    bullets[i].Draw();

                    if (bullets[i].Position.X < 0 || bullets[i].Position.X > Raylib.GetScreenWidth() || bullets[i].Position.Y < 0 || bullets[i].Position.Y > Raylib.GetScreenHeight())
                    {
                        bullets.RemoveAt(i);
                        continue;
                    }

                    for (int j = enemies.Count - 1; j >= 0; j--)
                    {
                        if (Raylib.CheckCollisionCircleRec(bullets[i].Position, 5, enemies[j].GetRect()))
                        {
                            enemies[j].takeDamage(20);
                            if (enemies[j].Health <= 0)
                            {
                                enemies.RemoveAt(j);
                                Score++;
                            }
                            bullets.RemoveAt(i);
                            break;
                        }
                    }
                }

                player.Update(dt);
                player.Draw();
            }
            else if (state == State.End)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RayWhite);
                Raylib.DrawText("end", GetPos(2).X, GetPos(2).Y, sFont, Color.Orange);
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