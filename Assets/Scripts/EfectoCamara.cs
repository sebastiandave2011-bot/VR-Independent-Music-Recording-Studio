using System.Collections;
using UnityEngine;

/// <summary>
/// Controls camera shutter audio, particle bursts, and point light flash behavior.
/// </summary>
public class EfectoCamara : MonoBehaviour
{
    public Light luzFlash;
    public AudioSource sonidoObturador;
    public ParticleSystem particulasFlash;
    public float duracionFlash = 0.1f;
    public float intensidadMaxima = 5f;

    /// <summary>
    /// Ensures the camera flash light starts disabled.
    /// </summary>
    void Start()
    {
        luzFlash.enabled = false;
    }

    /// <summary>
    /// Triggers the camera flash sequence coroutine.
    /// </summary>
    public void TomarFoto()
    {
        StartCoroutine(SecuenciaFlash());
    }

    /// <summary>
    /// Plays shutter sound, triggers particles, and briefly illuminates the flash light.
    /// </summary>
    private IEnumerator SecuenciaFlash()
    {
        sonidoObturador.Play();
        if (particulasFlash != null) particulasFlash.Play();

        luzFlash.enabled = true;
        luzFlash.intensity = intensidadMaxima;

        yield return new WaitForSeconds(duracionFlash);

        luzFlash.enabled = false;
    }
}