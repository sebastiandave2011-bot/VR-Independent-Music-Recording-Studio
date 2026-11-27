using UnityEngine;

/// <summary>
/// Plays sequential VR footsteps differentiating between hardwood floor and rug surfaces
/// based on the horizontal displacement of the player's camera.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PasosEstudioVR : MonoBehaviour
{
    [Header("Seguimiento VR")]
    [Tooltip("Arrastra aquí la Main Camera dentro de XR Origin.")]
    public Transform cabezaJugador;

    [Tooltip("Metros recorridos antes de disparar el siguiente paso.")]
    public float distanciaPorPaso = 1.6f;

    [Header("Clips de Audio (Orden Secuencial)")]
    [Tooltip("Pasos por defecto sobre el piso de madera del estudio.")]
    public AudioClip[] pasosMadera;

    [Tooltip("Pasos amortiguados cuando el jugador pisa objetos con el Tag 'Tapete'.")]
    public AudioClip[] pasosTapete;

    [Range(0f, 1f)]
    public float volumenPasos = 0.6f;

    private AudioSource audioSource;
    private Vector3 ultimaPosicionHorizontal;
    private float distanciaAcumulada;

    private int indiceMadera = 0;
    private int indiceTapete = 0;

    /// <summary>
    /// Configures the AudioSource component for non-looping 2D VR audio playback.
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f; // Direct 2D audio to the user's ears in VR
    }

    /// <summary>
    /// Initializes the player head reference and records the initial horizontal position.
    /// </summary>
    private void Start()
    {
        if (cabezaJugador == null && Camera.main != null)
        {
            cabezaJugador = Camera.main.transform;
        }

        if (cabezaJugador != null)
        {
            ultimaPosicionHorizontal = ObtenerPosicionPlana();
        }
    }

    /// <summary>
    /// Tracks horizontal movement distance per frame and triggers a footstep sound once the step threshold is reached.
    /// </summary>
    private void Update()
    {
        if (cabezaJugador == null) return;

        Vector3 posicionActual = ObtenerPosicionPlana();
        float desplazamiento = Vector3.Distance(posicionActual, ultimaPosicionHorizontal);
        ultimaPosicionHorizontal = posicionActual;

        // Ignores sudden jumps when the user teleports (Blink)
        if (desplazamiento > 1.2f) return;

        distanciaAcumulada += desplazamiento;

        if (distanciaAcumulada >= distanciaPorPaso)
        {
            distanciaAcumulada = 0f;
            ReproducirPaso();
        }
    }

    /// <summary>
    /// Returns the player's horizontal position projected onto the XZ plane.
    /// </summary>
    private Vector3 ObtenerPosicionPlana()
    {
        return new Vector3(cabezaJugador.position.x, 0f, cabezaJugador.position.z);
    }

    /// <summary>
    /// Casts a downward ray to detect the floor tag and plays the next sequential footstep clip with subtle pitch variation.
    /// </summary>
    private void ReproducirPaso()
    {
        AudioClip clipSeleccionado = null;

        // Casts a downward ray to detect if the player is stepping on the rug
        if (Physics.Raycast(cabezaJugador.position, Vector3.down, out RaycastHit hit, 3.5f))
        {
            if (hit.collider.CompareTag("Tapete") && pasosTapete != null && pasosTapete.Length > 0)
            {
                clipSeleccionado = pasosTapete[indiceTapete];
                indiceTapete = (indiceTapete + 1) % pasosTapete.Length;
            }
        }

        // If not on the rug, plays the default hardwood floor footstep sound
        if (clipSeleccionado == null && pasosMadera != null && pasosMadera.Length > 0)
        {
            clipSeleccionado = pasosMadera[indiceMadera];
            indiceMadera = (indiceMadera + 1) % pasosMadera.Length;
        }

        if (clipSeleccionado == null) return;

        // Slight pitch variation to prevent a robotic audio effect
        audioSource.pitch = Random.Range(0.96f, 1.04f);
        audioSource.PlayOneShot(clipSeleccionado, volumenPasos);
    }
}