using UnityEngine;

public class GatilhoDialogo : MonoBehaviour
{
    [TextArea(3, 5)] // Cria uma caixa de texto maior e confortável no Inspector da Unity
    public string[] dialogoDoNPC;

    private GerenciadorDialogo gerenciador;
    private bool jogadorPerto = false;

    void Start()
    {
        // Busca o gerenciador na cena automaticamente
        gerenciador = FindFirstObjectByType<GerenciadorDialogo>();
    }

    void Update()
    {
        // Se o jogador estiver perto e apertar a tecla E, começa a conversa
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            gerenciador.IniciarDialogo(dialogoDoNPC);
        }
    }

    // IMPORTANTE: Seu Player precisa ter a Tag "Player" configurada na Unity
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
        }
    }
}
