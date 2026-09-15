using Raylib_cs;

namespace crazy_s_hit_arena;

public static class Program
{
    public static void Main()
    {
        int sFont = 20;
        
        Raylib.InitWindow(800, 450, "Crazy Hit Arena");
        int xCenter = Raylib.GetScreenWidth() / 2;
        int yCenter = Raylib.GetScreenHeight() / 2;
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            Raylib.DrawText("Crazy (S)hit Arena", (xCenter-sFont-50), (yCenter-sFont), sFont, Color.Red);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}