using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para mudar de cena

public class PoltronaCena2D : MonoBehaviour
{
    [TextArea(3, 5)] // Cria uma caixa de texto maior e confortável no Inspector da Unity
    public string[] dialogoDoNPC;

    private GerenciadorDialogo gerenciador;
    private bool jogadorPerto = false;

    [Header("Configurações de Cena")]
    [SerializeField] private string nomeDaNovaCena; // Digite o nome exato da próxima cena aqui

    [Header("Interface e Detecção")]
    [SerializeField] private string tagDoJogador = "Player";
    private bool jogadorNaArea = false;
    private bool dialogoIniciado = false;

    void Start()
    {
        // Busca o gerenciador na cena automaticamente
        gerenciador = FindFirstObjectByType<GerenciadorDialogo>();
    }

    private void Update()
    {
        // Se o jogador estiver perto e apertar a tecla E, começa a conversa
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            gerenciador.IniciarDialogo(dialogoDoNPC);
        }
        // 1. Jogador aperta W para interagir e começar o diálogo
        if (jogadorNaArea && Input.GetKeyDown(KeyCode.E) && !dialogoIniciado)
        {
            IniciarDialogoDaPoltrona();
        }

        // 2. Monitora se o diálogo acabou (Simulação)
        if (dialogoIniciado)
        {
            ChecarFimDoDialogo();
        }
    }

    private void IniciarDialogoDaPoltrona()
    {
        dialogoIniciado = true;
        Debug.Log("Diálogo da poltrona iniciado...");

        // CHAME O SEU SCRIPT DE DIÁLOGO AQUI
        // Exemplo: SeuGerenciadorDialogo.Instance.ComecarDialogo(meuTexto);
    }

    private void ChecarFimDoDialogo()
    {
        // Você deve substituir a condição abaixo pela verificação real do seu sistema de diálogo.
        // Exemplo: if (SeuGerenciadorDialogo.Instance.dialogoTerminou)
        bool seuSistemaDeDialogoTerminou = true; // Altere isso!

        if (seuSistemaDeDialogoTerminou)
        {
            MudarDeCena();
        }
    }

    private void MudarDeCena()
    {
        if (!string.IsNullOrEmpty(nomeDaNovaCena))
        {
            Debug.Log("Carregando nova cena: " + nomeDaNovaCena);
            SceneManager.LoadScene(nomeDaNovaCena);
        }
        else
        {
            Debug.LogError("O nome da nova cena não foi configurado no Inspector da Poltrona!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagDoJogador))
        {
            jogadorNaArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tagDoJogador))
        {
            jogadorNaArea = false;
        }
    }
}
