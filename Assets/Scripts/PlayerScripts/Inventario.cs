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
}
