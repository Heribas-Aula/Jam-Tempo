using UnityEngine;
using TMPro;

public class PlantaInteracao2D : MonoBehaviour
{
    [Header("Configurações do Item")]
    [SerializeField] private ItemData regadorCheio;
    [SerializeField] private ItemData regadorVazio; // Para devolver o regador vazio ao jogador

    [Header("Alvo para Destruir")]
    [SerializeField] private GameObject barreiraParaDestruir;

    [Header("Interface")]
    [SerializeField] private TextMeshProUGUI textoAvisoUI;
    [SerializeField] private string mensagemComAgua = "Aperte W para regar a planta";
    [SerializeField] private string mensagemSemAgua = "A planta parece murcha. Preciso de água.";

    private bool jogadorNaArea = false;
    private Inventario inventarioJogador;

    private void Update()
    {
        if (jogadorNaArea && Input.GetKeyDown(KeyCode.W) && inventarioJogador != null)
        {
            if (inventarioJogador.itens.Contains(regadorCheio))
            {
                // 1. Remove apenas o regador cheio (gasta a água)
                inventarioJogador.itens.Remove(regadorCheio);

                // [A LINHA DE ADICIONAR O REGADOR VAZIO FOI REMOVIDA DAQUI]

                // 2. Atualiza a UI do inventário para o item sumir da tela
                inventarioJogador.OnItemAlterado?.Invoke();

                // 3. Executa a lógica de crescimento e destrói a barreira
                RegarPlanta();
            }
        }
    }
    private void RegarPlanta()
    {
        Debug.Log("Planta regada com sucesso!");

        if (textoAvisoUI != null)
        {
            textoAvisoUI.text = "A planta cresceu! Caminho liberado.";
            Invoke(nameof(DesativarTexto), 2f);
        }

        // Destrói a segunda barreira do mapa
        if (barreiraParaDestruir != null)
        {
            Destroy(barreiraParaDestruir);
        }

        // Destrói o gatilho da própria planta (ou desativa o script se quiser manter o visual dela)
        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorNaArea = true;
            inventarioJogador = other.GetComponent<Inventario>();

            if (textoAvisoUI != null)
            {
                textoAvisoUI.text = inventarioJogador.itens.Contains(regadorCheio) ? mensagemComAgua : mensagemSemAgua;
                textoAvisoUI.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorNaArea = false;
            inventarioJogador = null;
            if (textoAvisoUI != null) textoAvisoUI.gameObject.SetActive(false);
        }
    }

    private void DesativarTexto()
    {
        if (textoAvisoUI != null) textoAvisoUI.gameObject.SetActive(false);
    }
}
