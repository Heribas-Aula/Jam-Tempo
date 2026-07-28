using UnityEngine;

[CreateAssetMenu(fileName = "NovaReceita", menuName = "Inventario/Receita")]
public class CraftingRecipe : ScriptableObject
{
    [Header("Configurações da Receita")]
    public ItemData[] ingredientes = new ItemData[3]; // Força o array a ter espaço para 3 itens distintos
    public ItemData resultado;                         // O item que será gerado
}
