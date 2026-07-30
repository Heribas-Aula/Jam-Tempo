using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

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

    // Garante a remoção do evento se o objeto for destruído para evitar erros de memória
    private void OnDestroy()
    {
        if (inventarioJogador != null)
        {
            inventarioJogador.OnItemAlterado -= AtualizarTela;
        }
    }

    public void MyGridUpdate() { } // Função auxiliar de segurança

    public void AtualizarTela()
    {
        if (painelDosSlots == null) return;

        // 1. Limpa a tela completamente antes de desenhar
        for (int i = painelDosSlots.childCount - 1; i >= 0; i--)
        {
            Destroy(painelDosSlots.GetChild(i).gameObject);
        }

        if (inventarioJogador == null || inventarioJogador.itens == null) return;

        // 2. DICIONÁRIO BLINDADO: Agrupa os itens e conta as quantidades de forma infalível
        Dictionary<ItemData, int> itensAgrupados = new Dictionary<ItemData, int>();

        foreach (ItemData item in inventarioJogador.itens)
        {
            if (item == null) continue;

            // Procuramos se já existe um item com o mesmo nome no nosso dicionário
            ItemData itemCorrespondente = null;
            foreach (ItemData key in itensAgrupados.Keys)
            {
                // ATENÇÃO: Se no seu ItemData a variável for 'idNome' ou 'nomeDoItem', ajuste aqui abaixo!
                if (key.nomeDoItem == item.nomeDoItem)
                {
                    itemCorrespondente = key;
                    break;
                }
            }

            if (itemCorrespondente != null)
            {
                itensAgrupados[itemCorrespondente]++; // Já existe, aumenta a quantidade
            }
            else
            {
                itensAgrupados[item] = 1; // É o primeiro desse tipo, começa com 1
            }
        }

        // 3. Renderiza apenas UM slot por tipo de item com sua respectiva quantidade
        foreach (KeyValuePair<ItemData, int> par in itensAgrupados)
        {
            ItemData itemUnico = par.Key;
            int quantidadeDoItem = par.Value;

            GameObject novoSlot = Instantiate(slotPrefab, painelDosSlots);

            SlotUI scriptSlot = novoSlot.GetComponent<SlotUI>();
            if (scriptSlot != null)
            {
                scriptSlot.ConfigurarSlot(itemUnico, quantidadeDoItem);
            }
        }
    }
}
