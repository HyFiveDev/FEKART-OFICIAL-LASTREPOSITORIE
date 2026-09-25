using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InimigoEspecial : MonoBehaviour
{
    public float velocidade = 3f;

    [Header("Referências")]
    public Transform player;
    public PlayerEsconderijo playerScript;
    [SerializeField] TrocaDeCena morte;
    [Header("Posto de Controle")]
    // 0 = não destruído
    // 1 = destruído
    public int postoDestruido = 0;

    [Header("Imagens")]
    public GameObject inimigoTras;
    public GameObject imagemFrente;
    public GameObject imagemAtirando;

    [Header("Colliders")] private Collider2D collider;

    [Header("Timer")]
    public float tempoTroca = 15f;
    private bool jaIniciou;
    private bool perseguindo = false;

    // Controla qual imagem/collider está ativo
    private bool usandoImagem1 = true;

    private float timer;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        timer = tempoTroca;
        AtualizarEstado();
        imagemAtirando.SetActive(false);
    }

    void Update()
    {
        // TIMER
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            usandoImagem1 = !usandoImagem1;
            AtualizarEstado();
            timer = tempoTroca;
        }
    }

    void AtualizarEstado()
    {
        // Alterna imagens
        inimigoTras.SetActive(usandoImagem1);
        imagemFrente.SetActive(!usandoImagem1);

        // Alterna colliders
        collider.enabled = !usandoImagem1;
    }

    // Chamado pelo posto de controle quando ele for destruído
    public void AtivarPerseguicao()
    {
        postoDestruido = 1;
        perseguindo = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!playerScript.protegido && other.CompareTag("Player"))
        {
            if (jaIniciou) return;
            Debug.Log("GAME OVER");
            StartCoroutine(Morte());
            StopCoroutine(Morte());
        }
    }

    private IEnumerator Morte()
    {
        jaIniciou = true;

        imagemAtirando.SetActive(true);
        imagemFrente.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        morte.Morte();
        imagemAtirando.SetActive(false);
        imagemFrente.SetActive (true);
        jaIniciou = false;

    }
}