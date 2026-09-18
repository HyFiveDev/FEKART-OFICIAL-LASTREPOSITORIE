using UnityEngine;
using System.Collections;

public class TutorialAtivator : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorial;
    private bool jaAtivou = false; // Trava para impedir múltiplas chamadas seguidas
    
    [Header("Variáveis de encerramento")]
    public float tempoEspera = 2f;
    private WaitForSeconds espera = new WaitForSeconds(2f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se já foi ativado, sai imediatamente e ignora as próximas colisões
        if (jaAtivou) return;

        // Verifica se o objeto que entrou tem a tag "Tutorial"
        if (other.CompareTag("Tutorial"))
        {
            jaAtivou = true; // Ativa a trava
            Debug.Log("Gatilho detectado no objeto: " + this.gameObject.name);

            tutorial.IniciarTutorial();
            StartCoroutine(esperarTempo());
            Destroy(other.gameObject);
        }
    }
    
    private IEnumerator esperarTempo()
    {
        espera = new WaitForSeconds(tempoEspera);
        yield return espera;
        tutorial.FinalizarTutorial();
        jaAtivou = false;
    }
}
