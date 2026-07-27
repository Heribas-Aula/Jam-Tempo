using UnityEngine;

public class InventarioUI : MonoBehaviour
{
    public Transform painelDosSlots; // Arraste o 'PainelInventario' aqui
    public GameObject slotPrefab;    // Arraste o Prefab do 'Slot' aqui

    private Inventario inventarioJogador;

    void Start()
    {
        // Encontra o inventário do jogador automaticamente na cena
        inventarioJogador = FindFirstObjectByType<Inventario>();

        if (inventarioJogador != null)
        {
            // Se inscreve no evento para atualizar a tela sempre que a lista mudar
            inventarioJogador.OnItemAlterado += AtualizarTela;
        }

        AtualizarTela(); // Roda uma vez no início para começar limpo
    }

    void AtualizarTela()
    {
        if (painelDosSlots == null) return;

        // Limpa a tela de trás para frente (garante que absolutamente tudo suma antes de redesenhar)
        for (int i = painelDosSlots.childCount - 1; i >= 0; i--)
        {
            Destroy(painelDosSlots.GetChild(i).gameObject);
        }

        if (inventarioJogador == null) return;

        // Cria um novo slot apenas para os itens reais da lista do jogador
        foreach (ItemData item in inventarioJogador.itens)
        {
            GameObject novoSlot = Instantiate(slotPrefab, painelDosSlots);
            novoSlot.GetComponent<SlotUI>().ConfigurarSlot(item);
        }
    }

}
