using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the opening and closing interaction of the main studio door.
/// Manages smooth rotation, spatial audio playback for both states, and VR comfort.
/// </summary>
public class ControladorPuerta : MonoBehaviour
{
    [Header("Components")]
    [Tooltip("The pivot point (Hinge) of the door.")]
    public Transform doorHinge;

    [Tooltip("AudioSource for the opening creak sound.")]
    public AudioSource creakSound;

    // Modified: Added a second audio component for the closing sound
    [Tooltip("AudioSource for the closing slam sound.")]
    public AudioSource closeSound;

    [Header("Settings")]
    [Tooltip("Target rotation angle in the Y axis.")]
    public float openAngle = 90f;

    // Modified: Reduced default value for a slower and more comfortable transition in VR
    [Tooltip("Speed of the animation. Lower this value to make it slower.")]
    public float openSpeed = 1f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    // Modified: Stores the current coroutine so it can be stopped if the user interacts mid-animation
    private Coroutine animationCoroutine;

    /// <summary>
    /// Stores the initial closed and target open rotations of the door hinge.
    /// </summary>
    void Start()
    {
        if (doorHinge != null)
        {
            closedRotation = doorHinge.rotation;
            openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        }
    }

    /// <summary>
    /// Toggles the door state between open and closed.
    /// Designed to be called via UnityEvents (e.g., XR Simple Interactable -> Select Entered).
    /// </summary>
    public void InteractuarPuerta()
    {
        // Modified: Toggles the boolean state
        isOpen = !isOpen;

        // Modified: Conditional logic to play the audio matching the current state
        if (isOpen && creakSound != null)
        {
            creakSound.Play();
        }
        else if (!isOpen && closeSound != null)
        {
            closeSound.Play();
        }

        // Modified: Stops any previous animation to prevent visual snapping conflicts
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        // Starts interpolation toward the new target angle
        animationCoroutine = StartCoroutine(AnimateDoor(isOpen ? openRotation : closedRotation));
    }

    /// <summary>
    /// Coroutine that smoothly interpolates the door's rotation from its CURRENT rotation to the TARGET rotation.
    /// </summary>
    private IEnumerator AnimateDoor(Quaternion targetRotation)
    {
        // Modified: Starts from the current rotation rather than zero to allow smooth direction changes
        Quaternion startRotation = doorHinge.rotation;
        float timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * openSpeed;
            doorHinge.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed);
            yield return null;
        }

        doorHinge.rotation = targetRotation;
    }
}