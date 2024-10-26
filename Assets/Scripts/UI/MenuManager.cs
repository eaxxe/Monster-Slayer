using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }
    
}
