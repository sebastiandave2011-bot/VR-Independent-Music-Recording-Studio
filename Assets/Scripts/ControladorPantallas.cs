using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Controls dual studio monitor video playback, screen emission materials, and interaction scoring.
/// </summary>
public class ControladorPantallas : MonoBehaviour
{
    [Header("Componentes")]
    public VideoPlayer videoPantallaSuperior;
    public VideoPlayer videoPantallaInferior;
    public AudioSource audioClick;

    [Header("Materiales (Emisión)")]
    public Material materialPantallaSuperior;
    public Material materialPantallaInferior;
    public Color colorEncendido = Color.white;

    // Modified: Reference to the game state manager
    [Header("Conexión UI Fase 3")]
    public GameStateManager gameManager;

    private bool encendidas = false;
    // Modified: Flag to count the interaction only once
    private bool yaContabilizado = false;

    /// <summary>
    /// Disables screen emission keywords at the start of the scene.
    /// </summary>
    void Start()
    {
        // Turn off emission at startup
        materialPantallaSuperior.DisableKeyword("_EMISSION");
        materialPantallaInferior.DisableKeyword("_EMISSION");
    }

    /// <summary>
    /// Toggles monitor videos and emissive shaders on or off and registers the first activation.
    /// </summary>
    public void AlternarPantallas()
    {
        encendidas = !encendidas;
        audioClick.Play();

        // Modified: Interaction counting logic
        if (encendidas && !yaContabilizado && gameManager != null)
        {
            gameManager.RegistrarInteraccion();
            yaContabilizado = true;
        }

        if (encendidas)
        {
            videoPantallaSuperior.Play();
            videoPantallaInferior.Play();
            materialPantallaSuperior.EnableKeyword("_EMISSION");
            materialPantallaSuperior.SetColor("_EmissionColor", colorEncendido);
            materialPantallaInferior.EnableKeyword("_EMISSION");
            materialPantallaInferior.SetColor("_EmissionColor", colorEncendido);
        }
        else
        {
            videoPantallaSuperior.Stop();
            videoPantallaInferior.Stop();
            materialPantallaSuperior.DisableKeyword("_EMISSION");
            materialPantallaInferior.DisableKeyword("_EMISSION");
        }
    }
}