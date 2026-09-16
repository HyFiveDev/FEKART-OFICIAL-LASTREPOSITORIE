using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class DebuffArara : MonoBehaviour
{
    [SerializeField] private Andar andar;
    [SerializeField] private Rigidbody2D rb;

    private InputSystem_Actions inputSystem;
    private InputAction move;
    private Vector2 movimento;
    private int velocidade = 5;

    public bool podeVoar = true;
    public bool voando;
    public bool isGrounded;

    private float cooldownAtual;
    public float cooldownMax = 5f;
    private float intervaloDeVerificacao = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async Task Awake()
    {
        isGrounded = andar.isGrounded;
        inputSystem = new InputSystem_Actions();
        move = inputSystem.Player.Move;
    }


    void OnEnable()
    {
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Contador();
        movimento = move.ReadValue<Vector2>();
        StartCoroutine(VerificarPossibilidadeDeVoo());
        if (!isGrounded && podeVoar) Voar();
        else if (!podeVoar) Cair();

        
    }

    private void Voar()
    {
        rb.linearVelocity = movimento * velocidade;
    }

    private void Cair()
    {
        rb.linearVelocity = new Vector2(movimento.x * velocidade, transform.position.y * -velocidade);
    }

    private IEnumerator VerificarPossibilidadeDeVoo()
    {
        if (!isGrounded && cooldownAtual >= cooldownMax)
        {
            podeVoar = false;
        }
        else if(isGrounded && !podeVoar && cooldownAtual == 0)
        {
            podeVoar = true;
        }
        yield return new WaitForSeconds(intervaloDeVerificacao);
    }

    private void Contador()
    {
        if(!isGrounded)
        {
            cooldownAtual += Time.deltaTime;
        }
        if(isGrounded && cooldownAtual == cooldownMax)
        {
            cooldownAtual -= Time.deltaTime;
        }
    }
}
