using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onPlayClicked()
    {
        SceneManager.LoadScene("Start");
    }

    public void onSettingsClicked()
    {
        //SceneManager.LoadScene("Settings");
    }

    public void onCreditsClicked()
    {
        //SceneManager.LoadScene("Credits");
    }

    public void onQuitClicked()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
