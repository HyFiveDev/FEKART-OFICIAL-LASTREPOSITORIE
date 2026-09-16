using UnityEngine;

public class TutorialAtivator : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorial;
    private bool jaAtivou = false; // Trava para impedir múltiplas chamadas seguidas

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se já foi ativado, sai imediatamente e ignora as próximas colisões
        if (jaAtivou) return;

        // Verifica se o objeto que entrou tem a tag "Tutorial"
        if (other.CompareTag("Tutorial"))
        {
            jaAtivou = true; // Ativa a trava
            Debug.Log("Gatilho detectado no objeto: " + this.gameObject.name);

            // Inicia o processo no gerenciador
            tutorial.IniciarTutorial();
        }
    }
}
