using UnityEngine;
using TMPro;

public class PiaInteracao2D : MonoBehaviour
{
    [Header("Configurações dos Itens")]
    [SerializeField] private ItemData regadorVazio;
    [SerializeField] private ItemData regadorCheio;

    [Header("Interface")]
    [SerializeField] private TextMeshProUGUI textoAvisoUI;
    [SerializeField] private string mensagemComRegador = "Aperte W para encher o regador";
    [SerializeField] private string mensagemSemRegador = "Uma pia comum. Preciso de algo para coletar água.";

    private bool jogadorNaArea = false;
    private Inventario inventarioJogador;

    private void Update()
    {
        if (jogadorNaArea && Input.GetKeyDown(KeyCode.W) && inventarioJogador != null)
        {
            if (inventarioJogador.itens.Contains(regadorVazio))
            {
                // Troca o vazio pelo cheio
                inventarioJogador.itens.Remove(regadorVazio);
                inventarioJogador.itens.Add(regadorCheio);

                // Atualiza a UI do inventário
                inventarioJogador.OnItemAlterado?.Invoke();

                if (textoAvisoUI != null) textoAvisoUI.text = "Regador cheio de água!";
                Debug.Log("Regador foi abastecido com água.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorNaArea = true;
            inventarioJogador = other.GetComponent<Inventario>();

            if (textoAvisoUI != null)
            {
                // Mostra mensagem dinâmica se ele tem ou não o regador vazio
                textoAvisoUI.text = inventarioJogador.itens.Contains(regadorVazio) ? mensagemComRegador : mensagemSemRegador;
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
}
