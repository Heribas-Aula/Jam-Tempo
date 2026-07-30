using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public List<ItemData> itens = new List<ItemData>();

    // EVENTO: Um mensageiro que avisa o Canvas que a lista de itens mudou
    public System.Action OnItemAlterado;

    public void AdicionarItem(ItemData novoItem)
    {
        itens.Add(novoItem);
        Debug.Log("Item guardado no inventário: " + novoItem.nomeDoItem);

        // Se o Canvas estiver ouvindo, ele atualiza a tela na hora
        OnItemAlterado?.Invoke();
    }

    // NOVA FUNÇÃO AUXILIAR: Permite que outros scripts removam um item perfeitamente
    public void RemoverItem(ItemData itemParaRemover)
    {
        if (itens.Contains(itemParaRemover))
        {
            itens.Remove(itemParaRemover);
            OnItemAlterado?.Invoke();
        }
    }

    // NOVA FUNÇÃO: Tenta transformar os itens com base em uma receita
    public bool TentarTransformar(CraftingRecipe receita)
    {
        bool temTodosOsIngredientes = true;

        // Criamos uma lista temporária para simular a checagem sem mexer no inventário real ainda
        List<ItemData> inventarioTemporario = new List<ItemData>(itens);

        // Verifica se cada um dos 3 ingredientes da receita está no inventário
        foreach (ItemData ingredienteRequerido in receita.ingredientes)
        {
            if (inventarioTemporario.Contains(ingredienteRequerido))
            {
                // Remove temporariamente para garantir que não vai validar o mesmo slot duas vezes
                inventarioTemporario.Remove(ingredienteRequerido);
            }
            else
            {
                temTodosOsIngredientes = false;
                break;
            }
        }

        // Se o jogador tiver os 3 itens corretos, executa a troca real
        if (temTodosOsIngredientes)
        {
            // 1. Destrói/Remove os 3 itens antigos da lista oficial
            foreach (ItemData ingredienteRequerido in receita.ingredientes)
            {
                itens.Remove(ingredienteRequerido);
            }

            // 2. Adiciona o novo item gerado
            itens.Add(receita.resultado);
            Debug.Log("Sucesso! Criou: " + receita.resultado.nomeDoItem);

            // 3. Alerta o Canvas para atualizar a UI (os antigos somem e o novo aparece)
            OnItemAlterado?.Invoke();

            return true;
        }

        Debug.Log("Você não tem os 3 ingredientes necessários.");
        return false;
    }
}
