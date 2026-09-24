using UnityEngine;
using UnityEngine.InputSystem;

public class PegarItens : MonoBehaviour
{
    private EstadoPedra pedra;
    [SerializeField] Transform pontoSegurar;
    [SerializeField] float forcaArremesso = 10f;
    [SerializeField] private Andar andar;
    InputSystem_Actions input;
    private SpriteRenderer flip;
    GameObject itemAlcance;
    GameObject itemCarregado;
    Rigidbody2D itemRb;
    private int direcao;

    Vector3 posicaoOriginal;

    void Awake()
    {
        input = new InputSystem_Actions();
        flip = GetComponent<SpriteRenderer>();

        if (pontoSegurar)
            posicaoOriginal = pontoSegurar.localPosition;
    }

    void OnEnable()
    {
        input.Player.Interact.Enable();
        input.Player.Attack.Enable();
    }

    void OnDisable()
    {
        input.Player.Interact.Disable();
        input.Player.Attack.Disable();
    }

    void Update()
    {
        if (input.Player.Interact.triggered)
        {
            if (itemCarregado)
                Soltar();
            else if (itemAlcance)
                Pegar();
        }

        if (input.Player.Attack.triggered && itemCarregado)
            Arremessar();
    }

    void Pegar()
    {
        itemCarregado = itemAlcance;
        itemRb = itemCarregado.GetComponent<Rigidbody2D>();
        pedra = itemAlcance.GetComponent<EstadoPedra>();

        if (!itemRb) return;

        itemRb.simulated = false;
        itemRb.linearVelocity = Vector2.zero;
        itemRb.angularVelocity = 0;

        itemCarregado.transform.SetParent(pontoSegurar);
        itemCarregado.transform.localPosition = Vector3.zero;
    }

    void Soltar()
    {
        itemCarregado.transform.SetParent(null);
        itemRb.simulated = true;
        itemCarregado = null;
        itemRb = null;
        pedra = null;
    }

    void Arremessar()
    {
        itemCarregado.transform.SetParent(null);
        itemRb.simulated = true;
        pedra.pedraArremessada = true;

        if (!flip.flipX)
        {
            direcao = 1;
            itemRb.linearVelocity = Vector2.right * direcao * forcaArremesso;
        }
        else if (flip.flipX)
        {
            direcao = -1;
            itemRb.linearVelocity = Vector2.right * direcao * forcaArremesso;
        }

        itemCarregado = null;
        itemRb = null;
        pedra = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
            itemAlcance = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == itemAlcance)
            itemAlcance = null;
    }
}