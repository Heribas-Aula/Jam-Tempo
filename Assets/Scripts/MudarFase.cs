using UnityEngine;
using UnityEngine.SceneManagement; // Obrigatório para gerenciar e carregar cenas

public class MudarFase : MonoBehaviour
{
    [Header("Configuração de Destino")]
    [SerializeField] private string nomeDaNovaCena; // Digite o nome exato da próxima fase/cena aqui
    [SerializeField] private string tagDoJogador = "Player";

    private void Start()
    {
        // Garante automaticamente que o colisor do objeto funcione como um gatilho invisível
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que tocou neste ponto possui a tag do jogador
        if (other.CompareTag(tagDoJogador))
        {
            if (!string.IsNullOrEmpty(nomeDaNovaCena))
            {
                Debug.Log("Jogador tocou no portal! Carregando: " + nomeDaNovaCena);
                SceneManager.LoadScene(nomeDaNovaCena);
            }
            else
            {
                Debug.LogError("Erro: Você esqueceu de digitar o nome da nova cena no Inspector deste objeto!");
            }
        }
    }
}
