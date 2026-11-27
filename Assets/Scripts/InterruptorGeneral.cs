using UnityEngine;

/// <summary>
/// Controls the main studio lighting state, starting in soft dim light and toggling full illumination.
/// </summary>
public class InterruptorGeneral : MonoBehaviour
{
    public Light luzDireccional;
    public AudioSource sonidoClick;

    [Header("Conexión UI Fase 3")]
    public GameStateManager gameManager;

    private bool estaEncendida = false;
    private Color ambientLightOriginal;
    private bool yaContabilizado = false;

    /// <summary>
    /// Saves initial ambient lighting settings and initializes the room in a dimmed state.
    /// </summary>
    private void Start()
    {
        // Save the original ambient color before dimming
        ambientLightOriginal = RenderSettings.ambientLight;

        // Starts turned off (soft dim lighting) upon entering the scene
        estaEncendida = false;
        if (luzDireccional != null) luzDireccional.enabled = false;

        RenderSettings.ambientIntensity = 0.2f;
        RenderSettings.ambientLight = Color.gray;
    }

    /// <summary>
    /// Toggles the main directional light and ambient intensity while logging the interaction once.
    /// </summary>
    public void AlternarLucesEstudio()
    {
        estaEncendida = !estaEncendida;

        // Counts the interaction when turning ON the studio lights
        if (estaEncendida && !yaContabilizado && gameManager != null)
        {
            gameManager.RegistrarInteraccion();
            yaContabilizado = true;
        }

        // Turns the main studio light on or off
        if (luzDireccional != null)
        {
            luzDireccional.enabled = estaEncendida;
        }

        // Ambient lighting control without altering the exterior skybox
        if (estaEncendida)
        {
            RenderSettings.ambientIntensity = 1f;
            RenderSettings.ambientLight = ambientLightOriginal;
        }
        else
        {
            RenderSettings.ambientIntensity = 0.2f;
            RenderSettings.ambientLight = Color.gray;
        }

        // Plays the switch click sound
        if (sonidoClick != null)
        {
            sonidoClick.Play();
        }
    }
}