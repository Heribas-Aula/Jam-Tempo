using UnityEngine;
using UnityEngine.SceneManagement; // Obrigatório para carregar novas cenas

public class MudarFaseComItem2D : MonoBehaviour
{
    [Header("Configurações de Destino")]
    [SerializeField] private string nomeDaNovaCena; // Nome exato da próxima fase no Build Settings
    [SerializeField] private string tagDoJogador = "Player";

    [Header("Configuração de Item")]
    [SerializeField] private ItemData itemObrigatorio; // O item necessário para liberar a viagem
    [SerializeField] private bool consumirItemAoMudar = true; // Se marcado, o item some do inventário ao viajar

    private void Start()
    {
        // Força o colisor a ser um gatilho invisível no chão
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagDoJogador))
        {
            // Busca o SEU script de inventário no jogador que encostou
            Inventario inventario = other.GetComponent<Inventario>();

            if (inventario != null)
            {
                // Verifica se a lista do inventário possui o item obrigatório
                if (inventario.itens.Contains(itemObrigatorio))
                {
                    // Se estiver marcado para gastar o item, remove antes de mudar de tela
                    if (consumirItemAoMudar)
                    {
                        inventario.itens.Remove(itemObrigatorio);
                        inventario.OnItemAlterado?.Invoke(); // Atualiza a UI para o item sumir
                    }

                    ViajarParaNovaCena();
                }
                else
                {
                    Debug.Log($"Acesso negado. Você precisa do item {itemObrigatorio.nomeDoItem} para avançar.");
                }
            }
        }
    }

    private void ViajarParaNovaCena()
    {
        if (!string.IsNullOrEmpty(nomeDaNovaCena))
        {
            Debug.Log("Item validado! Mudando para a cena: " + nomeDaNovaCena);
            SceneManager.LoadScene(nomeDaNovaCena);
        }
        else
        {
            Debug.LogError("Erro: Digite o nome da nova cena no Inspector do portal!");
        }
    }
}
