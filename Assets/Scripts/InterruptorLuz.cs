using UnityEngine;

/// <summary>
/// Controls a standalone LED tube fixture, toggling its emissive cylinder, point light, and switch audio.
/// </summary>
public class InterruptorLuz : MonoBehaviour
{
    public GameObject tuboBrillante; // Emissive cylinder mesh
    public Light luzAmbiente;        // Scene point light component
    public AudioSource sonidoClick;  // Switch click audio source

    // Modified: Reference to the game state manager for Phase 3
    [Header("Conexión UI Fase 3")]
    public GameStateManager gameManager;

    private bool estaEncendida = false;

    // Modified: Flag to count the interaction only once
    private bool yaContabilizado = false;

    /// <summary>
    /// Initializes the LED light tube and point light in an off state.
    /// </summary>
    private void Start()
    {
        estaEncendida = false;
        if (tuboBrillante != null) tuboBrillante.SetActive(false);
        if (luzAmbiente != null) luzAmbiente.enabled = false;
    }

    /// <summary>
    /// Toggles the LED tube visibility and light component, registering the interaction once.
    /// </summary>
    public void AlternarLuz()
    {
        estaEncendida = !estaEncendida;

        // Modified: Interaction counting logic
        if (estaEncendida && !yaContabilizado && gameManager != null)
        {
            gameManager.RegistrarInteraccion();
            yaContabilizado = true;
        }

        if (tuboBrillante != null) tuboBrillante.SetActive(estaEncendida);
        if (luzAmbiente != null) luzAmbiente.enabled = estaEncendida;

        // Plays the switch click sound when toggling on or off
        if (sonidoClick != null) sonidoClick.Play();
    }
}