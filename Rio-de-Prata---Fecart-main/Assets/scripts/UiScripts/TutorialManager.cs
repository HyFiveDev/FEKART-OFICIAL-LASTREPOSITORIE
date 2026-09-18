using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    [TextArea(2, 4)]
    public List<string> mensagens;

    [Tooltip("Tempo em segundos que cada frase vai ficar visível na tela")]
    [SerializeField] private float tempoPorFrase = 4f;

    private int passoAtual = -1;

    void Awake()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void IniciarTutorial()
    {
        // Se o tutorial já estiver rodando, evita iniciar outro por cima
        if (tutorialPanel.activeSelf) return;

        if (mensagens == null || mensagens.Count == 0)
        {
            Debug.LogWarning("Nenhuma mensagem cadastrada na lista do TutorialManager!");
            return;
        }

        tutorialPanel.SetActive(true);
        ProximoPasso();
    }

    private void ProximoPasso()
    {
        passoAtual++;
        tutorialText.text = mensagens[passoAtual];
    }
    

    public void FinalizarTutorial()
    {


        if (tutorialText != null)
        {
            tutorialText.text = "";
        }

        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }

        Debug.Log("Sequência de tutorial concluída e painel fechado.");
    }
}
