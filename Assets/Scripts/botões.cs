using UnityEngine;
using UnityEngine.SceneManagement; 
public class botões : MonoBehaviour
{
  public void TrocarCena(string nomeDaCena)
    {
        SceneManager.LoadScene("FaseIdoso");
    }
    public void voltarMenu(string nomeCena)
    {
        SceneManager.LoadScene("Menu");
    }
}
