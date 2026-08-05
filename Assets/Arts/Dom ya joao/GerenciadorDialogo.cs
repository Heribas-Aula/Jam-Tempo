using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GerenciadorDialogo : MonoBehaviour
{
    public GameObject painelDialogo;
    public TextMeshProUGUI campoTexto;
    public float velocidadeDigitacao = 0.03f;
    [SerializeField] private TimerUI timer;
    private Queue<string> frases;
    private Coroutine coroutineDigitando;
    private string fraseAtualCompleta;
    private bool estaDigitando = false;
    void Start()
    {
        frases = new Queue<string>();
        painelDialogo.SetActive(false);
        if (timer == null)
        {
            timer = FindFirstObjectByType<TimerUI>();
        }
    }

    void Update()
    {
        if (painelDialogo.activeSelf && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (estaDigitando)
            {
                CompletarFraseInstantaneamente();
            }
            else
            {
                ExibirProximaFrase();
            }
        }
    }

    public void IniciarDialogo(string[] novoDialogo)
    {
        if (timer == null)
        {
            timer = FindFirstObjectByType<TimerUI>();
        }
        painelDialogo.SetActive(true);
        frases.Clear();

        foreach (string frase in novoDialogo)
        {
            frases.Enqueue(frase);
        }

        ExibirProximaFrase();
    }

    public void ExibirProximaFrase()
    {
        if (frases.Count == 0)
        {
            EncerrarDialogo();
            return;
        }

        fraseAtualCompleta = frases.Dequeue();

        if (coroutineDigitando != null)
        {
            StopCoroutine(coroutineDigitando);
        }

        coroutineDigitando = StartCoroutine(DigitarFrase(fraseAtualCompleta));
    }

    IEnumerator DigitarFrase(string frase)
    {
        campoTexto.text = "";
        estaDigitando = true;

        foreach (char letra in frase.ToCharArray())
        {
            campoTexto.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }

        estaDigitando = false;
    }

    void CompletarFraseInstantaneamente()
    {
        if (coroutineDigitando != null)
        {
            StopCoroutine(coroutineDigitando);
        }
        campoTexto.text = fraseAtualCompleta;
        estaDigitando = false;
    }

    void EncerrarDialogo()
    {
        painelDialogo.SetActive(false);
        campoTexto.text = "";
        if (timer != null)
        {
            timer.ResumeTimer();
        }
    }
}