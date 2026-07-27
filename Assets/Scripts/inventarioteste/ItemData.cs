using UnityEngine;

[CreateAssetMenu(fileName = "Novo Item", menuName = "Inventario/Item")]
public class ItemData : ScriptableObject
{
    public string nomeDoItem;
    public Sprite icone; // A foto que vai aparecer no inventário
}
