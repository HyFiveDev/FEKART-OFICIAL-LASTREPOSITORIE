using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    [TextArea(2, 4)]
    public List<string> mensagens; // Lista dinâmica (funciona para as 4 frases perfeitamente)

    [Tooltip("Tempo em segundos que cada frase vai ficar visível na tela")]
    [SerializeField] private float tempoPorFrase = 4f;

    private int passoAtual = -1;
    private Coroutine tutorialCoroutine;

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
        passoAtual = -1; // Reseta o contador para começar do início da lista

        // Inicia a sequência controlada por tempo
        tutorialCoroutine = StartCoroutine(SequenciaDoTutorial());
    }

    private IEnumerator SequenciaDoTutorial()
    {
        // Roda o loop até passar por todas as frases da lista (inclusive as 4 atuais)
        while (passoAtual < mensagens.Count - 1)
        {
            passoAtual++;
            Debug.Log("Mostrando a frase do passo: " + passoAtual);
            tutorialText.text = mensagens[passoAtual];

            // Espera o tempo definido antes de passar para a próxima frase
            yield return new WaitForSeconds(tempoPorFrase);
        }

        // Após passar por todas as frases da lista, fecha o painel
        FinalizarTutorial();
    }

    public void FinalizarTutorial()
    {
        // Interrompe o temporizador se ele ainda estiver ativo
        if (tutorialCoroutine != null)
        {
            StopCoroutine(tutorialCoroutine);
        }

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
