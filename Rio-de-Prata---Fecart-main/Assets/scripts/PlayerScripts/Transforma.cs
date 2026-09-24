using UnityEngine;
using UnityEngine.InputSystem;

public class Transforma : MonoBehaviour
{
    [Header("Transformações")]
    [SerializeField] private GameObject humano;
    [SerializeField] private GameObject arara;
    [SerializeField] private GameObject macaco;

    // Imagens de transformação
    [SerializeField] private GameObject imagemArara;
    [SerializeField] private GameObject imagemMacaco;
    [SerializeField] private GameObject imagemHumano;

    [Header("Efeitos Visuais")]
    [SerializeField] private ParticleSystem fumacaParticula;

    [Header("Referência de Códigos")]
    [SerializeField] private FormaColisao Colisao;
    [SerializeField] private MonkeyClimb escalada;
    [SerializeField] private DebuffArara voar;
    [SerializeField] private SliderVoo slider;
    private Rigidbody2D rb;

    [Header("Estado Atual")]
    // 1 = Humano
    // 2 = Arara
    // 3 = Macaco
    public int transformacaoAtual = 1;

    // NOVO NOME DO INPUT SYSTEM
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Arara.performed += VirarArara;
        inputActions.Player.Macaco.performed += VirarMacaco;
        inputActions.Player.Humano.performed += VirarHumano;
    }

    private void OnDisable()
    {
        inputActions.Player.Arara.performed -= VirarArara;
        inputActions.Player.Macaco.performed -= VirarMacaco;
        inputActions.Player.Humano.performed -= VirarHumano;

        inputActions.Disable();
    }

    // =========================
    // EFEITO DE FUMAÇA
    // =========================
    private void TocarFumaca()
    {
        if (fumacaParticula != null)
        {
            fumacaParticula.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            fumacaParticula.Play();
        }
        else
        {
            Debug.LogWarning("Atenção: O campo 'Fumaca Particula' não foi atribuído no Inspector!", this);
        }
    }

    // =========================
    // ARARA
    // =========================

    public void VirarArara(InputAction.CallbackContext context)
    {
        if (transformacaoAtual == 2) return;

        TocarFumaca();

        escalada.DesagtivarEscalda();
        slider.AlternarSlider(true);

        imagemArara.SetActive(true);
        imagemMacaco.SetActive(false);
        imagemHumano.SetActive(false);

        humano.SetActive(false);
        macaco.SetActive(false);
        arara.SetActive(true);

        transformacaoAtual = 2;

        Colisao.FormaArara();

        Debug.Log("Transformação Atual = ARARA");
    }

    // =========================
    // MACACO
    // =========================

    private void VirarMacaco(InputAction.CallbackContext context)
    {
        if (transformacaoAtual == 3) return;

        TocarFumaca();

        slider.AlternarSlider(false);
        rb.gravityScale = 1.5f;

        imagemArara.SetActive(false);
        imagemMacaco.SetActive(true);
        imagemHumano.SetActive(false);

        humano.SetActive(false);
        arara.SetActive(false);
        macaco.SetActive(true);

        transformacaoAtual = 3;

        Colisao.FormaMacaco();

        Debug.Log("Transformação Atual = MACACO");
    }

    // =========================
    // HUMANO
    // =========================

    private void VirarHumano(InputAction.CallbackContext context)
    {
        if (transformacaoAtual == 1) return;

        TocarFumaca();

        slider.AlternarSlider(false);
        escalada.DesagtivarEscalda();

        rb.gravityScale = 1.5f;

        imagemArara.SetActive(false);
        imagemMacaco.SetActive(false);
        imagemHumano.SetActive(true);

        humano.SetActive(true);
        arara.SetActive(false);
        macaco.SetActive(false);

        transformacaoAtual = 1;

        Colisao.FormaHumano();

        Debug.Log("Transformação Atual = HUMANO");
    }
}