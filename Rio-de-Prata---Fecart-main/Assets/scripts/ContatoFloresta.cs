using UnityEngine;

public class ContatoFloresta : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio")
            .GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Totem"))
        {
            audioManager.SFXSource.volume = 1f;
            audioManager.PlaySFX(audioManager.b);
            print("saiu som");
        }
    }
    private void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.name == "Floresta")
        {
            audioManager.SFXSource.volume = 0.2f;
            audioManager.PlaySFX(audioManager.a);
            Debug.Log("Tocando musica");
        }
    }
    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.name == "Floresta")
            audioManager.StopSFX();
    }
}