using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using JetBrains.Annotations;

public class DebuffArara : MonoBehaviour
{
    [SerializeField] private Andar andar;
    [SerializeField] private Rigidbody2D rb;

    private InputSystem_Actions inputSystem;
    private InputAction move;
    private Vector2 movimento;
    public int velocidade = 5;

    public bool podeVoar = true;
    public bool voando;

    private float cooldownAtual;
    public float cooldownMax = 5f;
    private float intervaloDeVerificacao = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async Task Awake()
    {
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
        movimento = move.ReadValue<Vector2>();
        Contador();
        StartCoroutine(VerificarPossibilidadeDeVoo());   
    }

    private void FixedUpdate()
    {
        if (!andar.isGrounded && podeVoar) Voar();
        else if (!podeVoar && voando) Cair();
        print(cooldownAtual);
    }

    private void Voar()
    {
        print("voando");
        voando = true;
        rb.linearVelocity = movimento * velocidade;
    }

    public void Cair()
    {
        if (!voando) return;
        print("Caindo");
        rb.linearVelocity = new Vector2(movimento.x * velocidade, -1f * velocidade);
    }

    private IEnumerator VerificarPossibilidadeDeVoo()
    {
        if (!andar.isGrounded && cooldownAtual >= cooldownMax)
        {
            print("você não pode mais voar");
            print(cooldownAtual);
            podeVoar = false;
            cooldownAtual = cooldownMax;

        }
        else if(andar.isGrounded && !podeVoar && cooldownAtual <= 0)
        {
            print("agora você pode voar");
            podeVoar = true;
            cooldownAtual = 0;
        }
        yield return new WaitForSeconds(intervaloDeVerificacao);
    }

    private void Contador()
    {
        if (andar.isGrounded && !podeVoar)
        {
            cooldownAtual -= Time.deltaTime;
        }
        if (!andar.isGrounded && podeVoar)
        {
            cooldownAtual += Time.deltaTime;
        }
    }

    public void AraraNoChao()
    {
        voando = false;
        if (podeVoar) cooldownAtual = 0;
    }
}
