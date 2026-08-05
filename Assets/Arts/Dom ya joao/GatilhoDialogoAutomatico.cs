using UnityEngine;

public class GatilhoDialogoAutomatico : MonoBehaviour
{
    [Header("Configurações do Diálogo")]
    [TextArea(3, 5)]
    public string[] dialogoAutomatico;

    private GerenciadorDialogo gerenciador;
    private bool jaDisparou = false;

    void Start()
    {
        gerenciador = FindFirstObjectByType<GerenciadorDialogo>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !jaDisparou)
        {
            jaDisparou = true;
            gerenciador.IniciarDialogo(dialogoAutomatico);
        }
    }
}
