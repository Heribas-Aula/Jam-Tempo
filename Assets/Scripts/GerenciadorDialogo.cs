using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Necessário para controlar o TextMeshPro

public class GerenciadorDialogo : MonoBehaviour
{
    [Header("Componentes de UI")]
    public GameObject painelDialogo;
    public TextMeshProUGUI campoTexto;

    [Header("Configurações")]
    public float velocidadeDigitacao = 0.03f; // Tempo de espera entre cada letra

    private Queue<string> frases;
    private Coroutine coroutineDigitando;   // Guarda a corrotina atual para podermos pará-la se necessário
    private string fraseAtualCompleta;      // Guarda o texto inteiro da frase que está sendo digitada
    private bool estaDigitando = false;

    void Start()
    {
        frases = new Queue<string>();
        painelDialogo.SetActive(false); // Garante que começa escondido
    }

    void Update()
    {
        // Se o painel estiver aberto e o jogador clicar com o botão esquerdo do mouse (ou Espaço)
        if (painelDialogo.activeSelf && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            // Se ainda estiver digitando, completa a frase instantaneamente
            if (estaDigitando)
            {
                CompletarFraseInstantaneamente();
            }
            else
            {
                // Se já terminou de digitar, avança para a próxima frase
                ExibirProximaFrase();
            }
        }
    }

    public void IniciarDialogo(string[] novoDialogo)
    {
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

        // Se já houver um texto sendo digitado, para ele antes de começar o próximo
        if (coroutineDigitando != null)
        {
            StopCoroutine(coroutineDigitando);
        }

        // Inicia o efeito de máquina de escrever
        coroutineDigitando = StartCoroutine(DigitarFrase(fraseAtualCompleta));
    }

    // A CORROTINA: Faz o efeito letra por letra
    IEnumerator DigitarFrase(string frase)
    {
        campoTexto.text = ""; // Limpa o texto anterior
        estaDigitando = true;

        // Transforma o texto em um array de letras e passa por cada uma
        foreach (char letra in frase.ToCharArray())
        {
            campoTexto.text += letra; // Adiciona uma letra na tela
            yield return new WaitForSeconds(velocidadeDigitacao); // Espera um curto tempo
        }

        estaDigitando = false;
    }

    void CompletarFraseInstantaneamente()
    {
        StopCoroutine(coroutineDigitando); // Para o efeito de digitação
        campoTexto.text = fraseAtualCompleta; // Mostra o texto todo de uma vez
        estaDigitando = false;
    }

    void EncerrarDialogo()
    {
        painelDialogo.SetActive(false);
        campoTexto.text = "";
        Debug.Log("Fim do diálogo.");
    }
}
