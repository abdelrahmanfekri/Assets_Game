using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadScene(string sceneName, string data){
        PlayerPrefs.SetString("data", data);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }
    public void QuitTheGame(){
        Application.Quit();
    }
}