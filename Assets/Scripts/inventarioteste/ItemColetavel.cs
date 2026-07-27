using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    [Header("Configurações do Item")]
    public ItemData dadosDoItem; // Arraste aqui a ficha do item (ex: Item_Moeda)

    private bool jogadorPerto = false;
    private Inventario inventarioDoJogador;

    void Update()
    {
        // Se o jogador estiver dentro da área e apertar a tecla W
        if (jogadorPerto && Input.GetKeyDown(KeyCode.W))
        {
            Coletar();
        }
    }

    void Coletar()
    {
        if (inventarioDoJogador != null)
        {
            // Adiciona o item na lista do jogador
            inventarioDoJogador.AdicionarItem(dadosDoItem);

            // Remove o item físico do cenário
            Destroy(gameObject);
        }
    }

    // Detecta quando o jogador entra na área do item
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            // Pega o componente de inventário diretamente do jogador que se aproximou
            inventarioDoJogador = other.GetComponent<Inventario>();

            Debug.Log($"Perto de: {dadosDoItem.nomeDoItem}. Aperte 'W' para pegar!");
        }
    }

    // Detecta quando o jogador sai da área do item e se afasta
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            inventarioDoJogador = null; // Limpa a referência por segurança
            Debug.Log("Se afastou do item.");
        }
    }
}
