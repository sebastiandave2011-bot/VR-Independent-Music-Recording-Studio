using UnityEngine;

/// <summary>
/// Animates a smooth pulsing scale effect for LED indicator spheres.
/// </summary>
public class PulsoLED : MonoBehaviour
{
    public float velocidad = 4f;
    public float tamañoMinimo = 0.01f;
    public float tamañoMaximo = 0.035f;

    /// <summary>
    /// Continuously interpolates the object's local scale between minimum and maximum values.
    /// </summary>
    void Update()
    {
        // Smoothly expands and contracts the sphere
        float t = (Mathf.Sin(Time.time * velocidad) + 1f) / 2f;
        float escalaActual = Mathf.Lerp(tamañoMinimo, tamañoMaximo, t);
        transform.localScale = new Vector3(escalaActual, escalaActual, escalaActual);
    }
}