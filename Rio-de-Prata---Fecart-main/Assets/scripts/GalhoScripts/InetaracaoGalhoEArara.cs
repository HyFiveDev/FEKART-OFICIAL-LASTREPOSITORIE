using UnityEngine;

public class InteracaoGalhoEArara : MonoBehaviour
{
    private void Start()
    {
        int layerGalho = LayerMask.NameToLayer("Galho");
        int layerArara = LayerMask.NameToLayer("Player_Arara");

        // Ignora a colisão entre Player_Humano e Galho
        Physics2D.IgnoreLayerCollision(layerArara, layerGalho, true);
    }
}
