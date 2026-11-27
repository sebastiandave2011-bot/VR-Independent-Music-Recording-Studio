using UnityEngine;
using System.Collections;

/// <summary>
/// Simulates a physical camera flash burst by scaling a flash mesh, playing shutter audio, and logging interaction progress.
/// </summary>
public class EfectoFlash : MonoBehaviour
{
    public Transform flashFisico; // The white flash sphere
    public AudioSource sonidoObturador;

    public float tamañoDelFlash = 0.2f;
    public float velocidadApagado = 25f; // The higher the value, the faster it fades out

    // Modified: Reference to the game state manager
    [Header("Conexión UI Fase 3")]
    public GameStateManager gameManager;

    // Modified: Flag to count the interaction only once
    private bool yaContabilizado = false;

    /// <summary>
    /// Hides the physical flash sphere at startup by setting its scale to zero.
    /// </summary>
    private void Start()
    {
        // Ensure the flash starts invisible
        if (flashFisico != null) flashFisico.localScale = Vector3.zero;
    }

    /// <summary>
    /// Registers the camera interaction once, plays shutter audio, and triggers the flash scale animation.
    /// </summary>
    public void DispararCamara()
    {
        // Modified: Interaction counting logic
        if (!yaContabilizado && gameManager != null)
        {
            gameManager.RegistrarInteraccion();
            yaContabilizado = true;
        }

        // 1. Play shutter sound
        if (sonidoObturador != null) sonidoObturador.Play();

        // 2. Trigger visual flash burst
        if (flashFisico != null)
        {
            StopAllCoroutines(); // Allows burst photography
            StartCoroutine(AnimarFlash());
        }
    }

    /// <summary>
    /// Coroutine that expands the flash mesh instantaneously and shrinks it smoothly back to zero.
    /// </summary>
    IEnumerator AnimarFlash()
    {
        // Instant burst to maximum size
        flashFisico.localScale = new Vector3(tamañoDelFlash, tamañoDelFlash, tamañoDelFlash);

        // Shrinks rapidly until disappearing (realistic flash effect)
        while (flashFisico.localScale.x > 0.005f)
        {
            flashFisico.localScale = Vector3.Lerp(flashFisico.localScale, Vector3.zero, Time.deltaTime * velocidadApagado);
            yield return null;
        }

        flashFisico.localScale = Vector3.zero;
    }
}