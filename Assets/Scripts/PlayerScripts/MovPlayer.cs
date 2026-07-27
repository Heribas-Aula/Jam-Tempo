using UnityEngine;
using UnityEngine.InputSystem;

public class MovPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private float direcaoHorizontal;
    public float velocidade = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AoMover(InputAction.CallbackContext context)
    {
        direcaoHorizontal = context.ReadValue<float>();
    }
    void FixedUpdate()
    {
        // Move o personagem mantendo a velocidade vertical atual (gravidade)
        rb.linearVelocity = new Vector2(direcaoHorizontal * velocidade, rb.linearVelocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Verifica se passou por cima de uma moeda
        if (collider.CompareTag("ObjetoAleatorio"))
        {
            Debug.Log("Passou em algo cupinxa");
        }
    }
}
