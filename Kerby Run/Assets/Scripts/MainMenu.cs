using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onPlayClicked()
    {
        SceneManager.LoadScene("Start");
    }
}
