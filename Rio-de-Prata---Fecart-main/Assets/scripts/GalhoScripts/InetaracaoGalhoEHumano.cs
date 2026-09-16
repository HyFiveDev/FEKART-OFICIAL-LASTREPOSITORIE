using UnityEngine;

public class InteracaoGalhoEHumano : MonoBehaviour
{
    private void Start()
    {
        int layerPlayer = LayerMask.NameToLayer("Player_Humano");
        int layerGalho = LayerMask.NameToLayer("Galho");

        // Ignora a colisão entre Player_Humano e Galho
        Physics2D.IgnoreLayerCollision(layerPlayer, layerGalho, true);
    }
}
