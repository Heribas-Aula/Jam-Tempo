using UnityEngine;


public class GatilhoBarreira2D : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private string tagDoJogador = "Player";
    [SerializeField] private ItemData itemRequerido; // O item craftado que será consumido

    [Header("Referências da Cena")]
    [SerializeField] private GameObject objetoBarreira;


    private void Start()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagDoJogador))
        {
            Inventario inventario = other.GetComponent<Inventario>();

            if (inventario != null)
            {
                // Verifica se o jogador tem o item
                if (inventario.itens.Contains(itemRequerido))
                {
                    // 1. O ITEM É CONSUMIDO AQUI: Tira da lista do inventário
                    inventario.itens.Remove(itemRequerido);

                    // 2. Notifica a UI para atualizar e fazer o item sumir da tela
                    inventario.OnItemAlterado?.Invoke();

                    // 3. Libera o caminho
                    DestruirBarreira();
                }
            }
        }
    }

    private void DestruirBarreira()
    {
        if (objetoBarreira != null)
        {
            Debug.Log("Item consumido e barreira destruída!");
            Destroy(objetoBarreira);
            Destroy(gameObject);
        }
    }
}
