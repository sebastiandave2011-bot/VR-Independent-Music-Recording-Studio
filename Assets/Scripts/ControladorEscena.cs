using UnityEngine;
using UnityEngine.SceneManagement; // Required to load scenes

/// <summary>
/// Manages scene reloading and level reset actions from the VR user interface.
/// </summary>
public class ControladorEscena : MonoBehaviour
{
    /// <summary>
    /// Reloads the currently active scene from scratch when called by a UI button.
    /// </summary>
    // Function called by the button
    public void ReiniciarNivel()
    {
        // Reloads the current scene from scratch
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}