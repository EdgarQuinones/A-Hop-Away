using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int highScore = 0;
    private static int score = 0;

    private GUIStyle gameOverStyle;
    private GUIStyle scoreStyle;

    private void Awake()
    {
        // Game over text
        gameOverStyle = new GUIStyle();
        gameOverStyle.fontSize = 52;
        gameOverStyle.normal.textColor = Color.white;
        gameOverStyle.alignment = TextAnchor.MiddleCenter;

        // Score text
        scoreStyle = new GUIStyle();
        scoreStyle.fontSize = 32;
        scoreStyle.normal.textColor = Color.white;
    }

    public int getScore()
    {
        return score;
    }

    public static void addScore(int points)
    {
        score += points;
    }

    public static void resetScore()
    {
        score = 0;
    }

    private void OnGUI()
    {
        bool gameOver = SceneManager.GetActiveScene().name == "End";

        if (gameOver)
        {
            highScore = Mathf.Max(highScore, score);

            // You Win!
            DrawOutlinedText(
                new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 60),
                "You found the alien's ship!",
                gameOverStyle
            );

            // Play again instructions
            GUIStyle instructionsStyle = new GUIStyle(scoreStyle);
            instructionsStyle.fontSize = 24;
            instructionsStyle.alignment = TextAnchor.MiddleCenter;

            //DrawOutlinedText(
            //    new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 50),
            //    "Enter the door to play again!",
            //    instructionsStyle
            //);
        }

        // Score UI
        float padding = Screen.width * 0.02f;

        DrawOutlinedText(
            new Rect(padding + 20, padding + 30, 200, 40),
            "Score: " + score,
            scoreStyle
        );

        DrawOutlinedText(
            new Rect(padding + 20, padding, 250, 40),
            "High Score: " + highScore,
            scoreStyle
        );
    }

    private void DrawOutlinedText(Rect position, string text, GUIStyle style)
    {
        Color originalColor = style.normal.textColor;

        // Black outline
        style.normal.textColor = Color.black;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                Rect offset = new Rect(
                    position.x + 2,
                    position.y + 2,
                    position.width,
                    position.height
                );

                GUI.Label(offset, text, style);
            }
        }

        // Original white text
        style.normal.textColor = originalColor;

        GUI.Label(position, text, style);
    }
}