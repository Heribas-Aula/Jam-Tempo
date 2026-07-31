using TMPro;
using UnityEngine;


public class GatilhoBarreiraQuantidade2D : MonoBehaviour
{
    [Header("Configurações do Jogador")]
    [SerializeField] private string tagDoJogador = "Player";

    [Header("Configurações do Item e Meta")]
    [SerializeField] private ItemData itemRequerido;
    [SerializeField] private int quantidadeNecessaria = 3; // Defina quantas unidades precisa
    [SerializeField] private bool consumirItensAoPassar = true; // Se marcado, os itens somem do inventário

    [Header("Referências da Cena")]
    [SerializeField] private GameObject objetoBarreira;
    [SerializeField] private TextMeshProUGUI textoAvisoUI;

    private void Start()
    {
        // Garante que o colisor funcione como gatilho invisível no chão
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagDoJogador))
        {
            Inventario inventario = other.GetComponent<Inventario>();

            if (inventario != null && inventario.itens != null)
            {
                // 1. CONTAGEM: Conta quantas cópias deste item existem no inventário
                int quantidadeAtual = 0;
                foreach (ItemData item in inventario.itens)
                {
                    if (item == itemRequerido)
                    {
                        quantidadeAtual++;
                    }
                }

                // 2. VERIFICAÇÃO: Se o jogador atingiu ou passou a meta necessária
                if (quantidadeAtual >= quantidadeNecessaria)
                {
                    if (consumirItensAoPassar)
                    {
                        // Remove a quantidade exata exigida da lista oficial do inventário
                        for (int i = 0; i < quantidadeNecessaria; i++)
                        {
                            inventario.itens.Remove(itemRequerido);
                        }

                        // Alerta o Canvas para atualizar os slots e os números na tela na hora
                        inventario.OnItemAlterado?.Invoke();
                    }

                    LiberarCaminho();
                }
                else
                {
                    // Mostra quantos itens ele tem contra quantos ele precisa (Ex: 1 / 3)
                    AvisarJogadorFaltaItem(quantidadeAtual);
                }
            }
        }
    }

    private void LiberarCaminho()
    {
        if (objetoBarreira != null)
        {
            Debug.Log("Quantidade atingida! Barreira destruída.");

            if (textoAvisoUI != null)
            {
                textoAvisoUI.text = "Caminho Liberado!";
                textoAvisoUI.gameObject.SetActive(true);
                Invoke(nameof(DesativarTexto), 2f);
            }

            Destroy(objetoBarreira); // Destrói a barreira física do cenário
            Destroy(gameObject);      // Destrói este gatilho do chão para não rodar novamente
        }
    }

    private void AvisarJogadorFaltaItem(int quantidadeAtual)
    {
        if (textoAvisoUI != null)
        {
            // Ajuste aqui se a variável no seu ItemData for 'idNome' ou 'nomeDoItem'
            string nomeItem = itemRequerido != null ? itemRequerido.nomeDoItem : "Item";

            textoAvisoUI.text = $"Precisa de: {nomeItem} ({quantidadeAtual}/{quantidadeNecessaria})";
            textoAvisoUI.gameObject.SetActive(true);

            CancelInvoke(nameof(DesativarTexto));
            Invoke(nameof(DesativarTexto), 3f);
        }
    }

    private void DesativarTexto()
    {
        if (textoAvisoUI != null) textoAvisoUI.gameObject.SetActive(false);
    }
}
