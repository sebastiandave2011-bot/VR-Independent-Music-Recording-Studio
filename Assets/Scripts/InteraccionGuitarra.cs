using UnityEngine;

/// <summary>
/// Handles electric guitar interaction by toggling audio playback, floating signage, and scoring.
/// </summary>
public class InteraccionGuitarra : MonoBehaviour
{
    public AudioSource audioBanda;     // Band riff or song track
    public GameObject letreroHelsreach; // Floating UI sign

    // Modified: Reference to the game state manager
    [Header("Conexión UI Fase 3")]
    public GameStateManager gameManager;

    private bool estaActivo = false;
    // Modified: Flag to count the interaction only once
    private bool yaContabilizado = false;

    /// <summary>
    /// Toggles the guitar audio track and floating label, registering the interaction on first activation.
    /// </summary>
    public void TocarGuitarra()
    {
        estaActivo = !estaActivo;

        // Modified: Interaction counting logic
        if (estaActivo && !yaContabilizado && gameManager != null)
        {
            gameManager.RegistrarInteraccion();
            yaContabilizado = true;
        }

        // Shows or hides the floating sign
        if (letreroHelsreach != null)
            letreroHelsreach.SetActive(estaActivo);

        // Plays or stops the audio track
        if (audioBanda != null)
        {
            if (estaActivo)
                audioBanda.Play();
            else
                audioBanda.Stop(); // Stops playback if clicked again
        }
    }
}