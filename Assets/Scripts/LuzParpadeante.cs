using UnityEngine;

/// <summary>
/// Controls a smooth oscillating intensity effect for an indicator light.
/// </summary>
public class LuzParpadeante : MonoBehaviour
{
    public Light luzIndicadora;
    public float velocidad = 3f;
    public float intensidadMinima = 0f;
    public float intensidadMaxima = 1.5f;

    /// <summary>
    /// Updates the indicator light intensity smoothly over time using a sine wave.
    /// </summary>
    void Update()
    {
        if (luzIndicadora != null)
        {
            // Creates a smooth fade-in and fade-out light cycle
            float t = (Mathf.Sin(Time.time * velocidad) + 1f) / 2f;
            luzIndicadora.intensity = Mathf.Lerp(intensidadMinima, intensidadMaxima, t);
        }
    }
}