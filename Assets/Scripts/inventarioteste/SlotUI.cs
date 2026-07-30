using UnityEngine;
using UnityEngine.UI; // Obrigatório para mexer com o componente Image da UI
using TMPro; // Obrigatório para mexer com o texto do TextMeshPro

public class SlotUI : MonoBehaviour
{
    public Image iconeExibido; // Arraste o componente de imagem do slot aqui
    public TextMeshProUGUI textoQuantidade; // Arraste o componente de texto da quantidade aqui

    // Atualizamos a função para aceitar a quantidade (padrão é 1 se não for enviado nada)
    public void ConfigurarSlot(ItemData item, int quantidade = 1)
    {
        if (item != null)
        {
            iconeExibido.sprite = item.icone; // Coloca a foto do item no quadradinho
            iconeExibido.enabled = true;      // Mostra a imagem

            // Lógica do texto de empilhamento
            if (textoQuantidade != null)
            {
                if (quantidade > 1)
                {
                    textoQuantidade.text = quantidade.ToString(); // Define o número (ex: 2, 3...)
                    textoQuantidade.gameObject.SetActive(true);   // Mostra o texto na tela
                }
                else
                {
                    textoQuantidade.gameObject.SetActive(false);  // Esconde o número se for apenas 1 item
                }
            }
        }
        else
        {
            iconeExibido.enabled = false;     // Esconde a imagem se o slot estiver vazio
            if (textoQuantidade != null) textoQuantidade.gameObject.SetActive(false);
        }
    }
}
