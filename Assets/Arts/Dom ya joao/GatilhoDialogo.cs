using UnityEngine;

public class GatilhoDialogo : MonoBehaviour
{
    [TextArea(3, 5)]
    public string[] dialogoDoNPC;
    [SerializeField] private float tempoPerca = 15f;
    private GerenciadorDialogo gerenciador;
    private TimerUI timer;
    private bool jogadorPerto = false;

    void Start()
    {
        gerenciador = FindFirstObjectByType<GerenciadorDialogo>();
        timer = FindFirstObjectByType<TimerUI>();
    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            if (timer != null)
            {
                timer.SubtrairTempo(tempoPerca);
                timer.PauseTimer();
            }

            gerenciador.IniciarDialogo(dialogoDoNPC);
        }
    }

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