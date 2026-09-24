using UnityEngine;

public class KnockoutOnCapsuleHit : MonoBehaviour
{
    [Header("Configuração")]
    public bool nocauteado = false;

    [Header("Componentes")]
    private Rigidbody2D rb;

    private EstadoPedra pedra;
    [SerializeField] private EnemyAnimScripts anim;
    private Collider2D coll;
  
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto que bateu possui CapsuleCollider2D
        CapsuleCollider2D capsule = collision.collider.GetComponent<CapsuleCollider2D>();

        if (capsule != null)
        {
            print("Colisão funcionou");
            EntrarEmNocaute();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            pedra = collision.gameObject.GetComponent<EstadoPedra>();
    }

    private void EntrarEmNocaute()
    {
        print("nocaute chamado " + pedra.pedraArremessada );
        if (nocauteado || pedra.pedraArremessada == false) return;
        print("passou do if");

        nocauteado = true;

         
        if (rb != null)
        {
            print("collider desligado");
            rb.bodyType = RigidbodyType2D.Static;
            coll.enabled = false;
        }

        // Ativa a animação de nocaute
            anim.Atordoar(nocauteado);
        print("inimigo nocauteado");
        

        // Desativa os scripts que controlam o personagem
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }

        Debug.Log("Personagem entrou em estado de NOCAUTEADO!");
    }
}