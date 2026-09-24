using Unity.Cinemachine; // Se estiver usando uma versão mais antiga do Cinemachine, mude para "using Cinemachine;"
using UnityEngine;
using UnityEngine.InputSystem;

public class AreaZoomTrigger : MonoBehaviour
{
    [Header("Referências")]
    [Tooltip("Arraste a sua Cinemachine Virtual Camera para cá")]
    public CinemachineCamera virtualCamera;
    [SerializeField] private DebuffArara arara;
    [SerializeField] private Transforma transformar;

    [Header("Configurações de Zoom")]
    [Tooltip("Tamanho do zoom normal da câmera")]
    public float normalZoom = 5f;

    [Tooltip("Tamanho do zoom afastado (mostrando a área inteira)")]
    public float panoramicZoom = 12f;

    [Tooltip("Velocidade com que o zoom transiciona")]
    public float zoomSpeed = 2f;

    private float targetZoom;
    private bool playerInside = false;

    void Start()
    {
        // Define o zoom inicial como o zoom normal
        if (virtualCamera != null)
        {
            targetZoom = normalZoom;
            virtualCamera.Lens.OrthographicSize = normalZoom;
        }
    }

    void Update()
    {
        if (virtualCamera == null) return;

        // Faz a transição suave (Lerp) do tamanho da lente da câmera
        float currentZoom = virtualCamera.Lens.OrthographicSize;
        virtualCamera.Lens.OrthographicSize = Mathf.Lerp(currentZoom, targetZoom, zoomSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou é o Player
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            targetZoom = panoramicZoom; // Alvo passa a ser o zoom afastado
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        arara.cooldownAtual = 0;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o objeto que saiu é o Player
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            targetZoom = normalZoom; // Alvo volta a ser o zoom normal
        }
    }
}