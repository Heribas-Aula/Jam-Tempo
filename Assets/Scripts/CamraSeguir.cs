using UnityEngine;

public class CamraSeguir : MonoBehaviour
{
    [Header("Alvo")]
    public Transform alvo;               // Arraste o jogador aqui

    [Header("Configurações de Movimento")]
    public float suavidadeX = 3f;        // Velocidade de arrasto horizontal
    public float offsetZ = -10f;         // Distância de profundidade da câmara
    public float alturaFixaY = 0f;       // Altura (Y) onde a câmara vai ficar travada

    [Header("Antecipação (Look-Ahead)")]
    public float distanciaAntecipacao = 2f; // Distância à frente do jogador
    public float velocidadeAntecipacao = 2f;

    [Header("Limites Laterais")]
    public bool usarLimites = true;
    public float minX = -10f;            // Limite máximo à esquerda
    public float maxX = 50f;             // Limite máximo à direita

    private float _posicaoAntecipadaX;
    private float _ultimaPosicaoXJogador;

    void Start()
    {
        // Se a altura fixa for 0, assume a altura inicial da câmara no cenário
        if (alturaFixaY == 0f)
        {
            alturaFixaY = transform.position.y;
        }

        // Inicializa o rastreio da posição do jogador
        if (alvo != null)
        {
            _ultimaPosicaoXJogador = alvo.position.x;
        }
    }

    void LateUpdate()
    {
        if (alvo == null) return;

        // 1. Calcular a velocidade e direção com base no movimento do Transform
        float movimentoX = alvo.position.x - _ultimaPosicaoXJogador;

        // Se o movimento for relevante, define a direção (1 para direita, -1 para esquerda)
        if (Mathf.Abs(movimentoX) > 0.001f)
        {
            float direcao = Mathf.Sign(movimentoX);
            _posicaoAntecipadaX = Mathf.Lerp(_posicaoAntecipadaX, direcao * distanciaAntecipacao, velocidadeAntecipacao * Time.deltaTime);
        }
        else
        {
            // Se o jogador parar, a câmara centraliza suavemente
            _posicaoAntecipadaX = Mathf.Lerp(_posicaoAntecipadaX, 0f, velocidadeAntecipacao * Time.deltaTime);
        }

        // Salva a posição atual do jogador para o cálculo do próximo frame
        _ultimaPosicaoXJogador = alvo.position.x;

        // 2. Definir a posição alvo horizontal desejada
        float posXDesejada = alvo.position.x + _posicaoAntecipadaX;

        // 3. Aplicar a interpolação suave apenas no eixo X
        float posXSuave = Mathf.Lerp(transform.position.x, posXDesejada, suavidadeX * Time.deltaTime);

        // 4. Aplicar os limites horizontais (esquerda e direita)
        if (usarLimites)
        {
            posXSuave = Mathf.Clamp(posXSuave, minX, maxX);
        }

        // 5. Atualizar a posição mantendo o Y fixo e o Z configurado
        transform.position = new Vector3(posXSuave, alturaFixaY, offsetZ);
    }

    // Desenha as linhas de limite no editor do Unity
    private void OnDrawGizmosSelected()
    {
        if (!usarLimites) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minX, -100f, 0), new Vector3(minX, 100f, 0));
        Gizmos.DrawLine(new Vector3(maxX, -100f, 0), new Vector3(maxX, 100f, 0));
    }

}
