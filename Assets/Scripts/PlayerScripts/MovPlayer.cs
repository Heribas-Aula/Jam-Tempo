using UnityEngine;
using UnityEngine.InputSystem;

public class MovPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private float direcaoHorizontal;
    public float velocidade = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

    }
    public void AoMover(InputAction.CallbackContext context)
    {
        direcaoHorizontal = context.ReadValue<float>();
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direcaoHorizontal * velocidade, rb.linearVelocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("ObjetoAleatorio"))
        {
            Debug.Log("Passou em algo cupinxa");
        }
        if (collider.CompareTag("fogs"))
        {
            Debug.Log("ta quentinho ta quentinho");
        }
    }
}
