using UnityEngine;
using UnityEngine.UI;

public class SliderVoo : MonoBehaviour
{
    [Header("Referências")]
    public Slider slider;
    public DebuffArara arara;
    [Header("Tempo de voo")]
    public float tempoMaximo = 5f;

    private float tempoAtual;

    void Start()
    {
        tempoAtual = tempoMaximo;

        slider.maxValue = tempoMaximo;
        slider.value = tempoAtual;
    }

    void Update()
    {
        // Enquanto estiver voando
        if (arara.voando)
        {
            tempoAtual -= Time.deltaTime;

            if (tempoAtual < 0)
                tempoAtual = 0;
        }

        // Quando estiver no chão
        if (!arara.voando)
        {
            tempoAtual = tempoMaximo;
        }

        // Atualiza o Slider
        slider.value = tempoAtual;
    }
}