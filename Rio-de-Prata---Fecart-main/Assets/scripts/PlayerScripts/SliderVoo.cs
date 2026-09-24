using UnityEngine;
using UnityEngine.UI;

public class SliderVoo : MonoBehaviour
{
    [Header("Referências")]
    public Slider slider;
    public GameObject sliderPanel;
    public DebuffArara arara;

    void Start()
    {
        // Configura o Slider
        slider.minValue = 0;
        slider.maxValue = arara.cooldownMax;

        // Começa cheio
        slider.value = arara.cooldownMax;
    }

    void Update()
    {
        // O Slider acompanha o cooldownAtual
        slider.value = arara.cooldownAtual;
    }

    public void AlternarSlider(bool alternar)
    {
        sliderPanel.SetActive(alternar);
    }
}
