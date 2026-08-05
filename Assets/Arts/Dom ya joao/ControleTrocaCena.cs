using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public enum LoadMode { ByIndex, ByName }
    [SerializeField] private LoadMode loadMode = LoadMode.ByIndex;
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private string sceneName;
    public void LoadNextScene()
    {
        if (loadMode == LoadMode.ByIndex)
        {
            LoadSceneByIndex(sceneIndex);
        }
        else
        {
            LoadSceneByName(sceneName);
        }
    }

    public void LoadSceneByIndex(int index)
    {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
    }
    public void LoadSceneByName(string name){
        if (!string.IsNullOrEmpty(name))
        {
            SceneManager.LoadScene(name);
        }
        else{
            Debug.LogWarning("sem cena");
        }
    }
    public void LoadNextBuildIndex()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}