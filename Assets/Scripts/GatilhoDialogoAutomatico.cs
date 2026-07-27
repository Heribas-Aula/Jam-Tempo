using UnityEngine;

public class GatilhoDialogoAutomatico : MonoBehaviour
{
    [Header("Configurações do Diálogo")]
    [TextArea(3, 5)]
    public string[] dialogoAutomatico;

    private GerenciadorDialogo gerenciador;
    private bool jaDisparou = false; // Impede que o diálogo se repita infinitamente

    void Start()
    {
        // Busca o gerenciador de diálogos na cena automaticamente
        gerenciador = FindFirstObjectByType<GerenciadorDialogo>();
    }

    // Detecta quando algo encosta no colisor invisível
    void OnTriggerEnter2D(Collider2D other)
    {
        // IMPORTANTE: Verifica se foi o Player que pisou E se ainda não disparou
        if (other.CompareTag("Player") && !jaDisparou)
        {
            jaDisparou = true; // Ativa a trava de segurança

            // Dispara o diálogo letra por letra imediatamente
            gerenciador.IniciarDialogo(dialogoAutomatico);

            // Opcional: Se você quiser que o gatilho suma para sempre após o uso,
            // desinale as duas barras da linha abaixo:
            // Destroy(gameObject);
        }
    }
}
