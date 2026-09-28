using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int highScore = 0;
    private static int score = 0;

    public int getScore()
    {
        return score;
    }

    // Used to add scores from coins
    public static void addScore(int points)
    {
        score += points;
    }

    // When game restarts for whever reason
    public static void resetScore()
    {
        score = 0;
    }

    private void OnGUI()
    {
        bool gameOver = SceneManager.GetActiveScene().name == "End";

        // If game ends display you win
        if (gameOver)
        {
            highScore = Mathf.Max(highScore, score);

            GUIStyle gameOverStyle = new GUIStyle();
            gameOverStyle.fontSize = 52;
            gameOverStyle.normal.textColor = Color.white;

            GUI.Label(
                new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 50),
                "You Win!",
                gameOverStyle
            );


            gameOverStyle.fontSize = 24;
            GUI.Label(
                new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 200, 50),
                "Enter the door to play again!",
                gameOverStyle
            );
        }

        // Always display the score
        GUIStyle scoreStyle = new GUIStyle();
        scoreStyle.fontSize = 32;
        scoreStyle.normal.textColor = Color.white;

        float padding = Screen.width * 0.02f;

        GUI.Label(new Rect(padding + 20, padding + 30, 200, 30),
                  "Score: " + score,
                  scoreStyle);
        GUI.Label(new Rect(padding + 20, padding, 200, 30),
                  "High Score: " + highScore,
                  scoreStyle);
    }
}
