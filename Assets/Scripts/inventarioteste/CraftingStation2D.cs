using UnityEngine;


public class CraftingStation2D : MonoBehaviour
{
    [Header("Configurações do Gatilho")]
    [SerializeField] private string tagDoJogador = "Player";

    [Header("Dados da Transformação")]
    [SerializeField] private CraftingRecipe receita; // O arquivo de receita criado anteriormente

    private bool jogadorNaArea = false;
    private Inventario inventarioJogador; // Referência ao seu script existente
    private void Update()
    {
        // Se o jogador estiver perto e apertar W, tenta fazer a fusão
        if (jogadorNaArea && Input.GetKeyDown(KeyCode.W))
        {
            ExecutarTransformacao();
        }
    }

    private void ExecutarTransformacao()
    {
        if (inventarioJogador == null) return;

        // Executa a função que adicionamos no seu script Inventario
        bool sucesso = inventarioJogador.TentarTransformar(receita);
    }

    // Quando o jogador entra na área da bancada
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagDoJogador))
        {
            jogadorNaArea = true;
            inventarioJogador = other.GetComponent<Inventario>();
        }
    }

    // Quando o jogador sai da área da bancada
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tagDoJogador))
        {
            jogadorNaArea = false;
            inventarioJogador = null;
        }
    }
}
