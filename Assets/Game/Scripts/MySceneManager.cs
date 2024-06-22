using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    
    public void LoadSceneByIndex(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
    
    public void OpenLevelByNumber(int levelNumber)
    {
        string levelName = $"level-{levelNumber}";
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
