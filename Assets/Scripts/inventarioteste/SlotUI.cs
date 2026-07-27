using UnityEngine;
using UnityEngine.UI; // Obrigatório para mexer com o componente Image da UI

public class SlotUI : MonoBehaviour
{
    public Image iconeExibido; // Arraste o componente de imagem do slot aqui

    public void ConfigurarSlot(ItemData item)
    {
        if (item != null)
        {
            iconeExibido.sprite = item.icone; // Coloca a foto do item no quadradinho
            iconeExibido.enabled = true;      // Mostra a imagem
        }
        else
        {
            iconeExibido.enabled = false;     // Esconde a imagem se o slot estiver vazio
        }
    }
}
