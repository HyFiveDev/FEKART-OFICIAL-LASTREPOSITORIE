using UnityEngine;

public class EstadoPedra : MonoBehaviour
{
    public bool pedraArremessada= false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!pedraArremessada) return;
        if (collision.gameObject.CompareTag("Ground"))
        {
            pedraArremessada = false;
        }
    }

}
