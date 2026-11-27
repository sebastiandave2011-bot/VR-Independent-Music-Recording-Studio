using UnityEngine;

/// <summary>
/// Controls a local light source, its emissive lamp material, and switch audio feedback.
/// </summary>
public class ControladorLuz : MonoBehaviour
{
    [Header("Componentes")]
    public Light luzReal;
    public AudioSource audioSwitch;
    public MeshRenderer modeloLampara;

    [Header("Configuración")]
    public int indiceMaterialEmision = 0; // Which material glows
    public Color colorEmision = Color.white;
    public float intensidadEmision = 2f;

    private bool encendida = false;
    private Material matEmision;

    /// <summary>
    /// Initializes the emissive material reference and turns off the light on startup.
    /// </summary>
    void Start()
    {
        matEmision = modeloLampara.materials[indiceMaterialEmision];
        matEmision.DisableKeyword("_EMISSION");
        luzReal.enabled = false;
    }

    /// <summary>
    /// Toggles the light source and emissive shader state while playing switch sound feedback.
    /// </summary>
    public void AlternarLuz()
    {
        encendida = !encendida;
        if (audioSwitch != null) audioSwitch.Play();

        luzReal.enabled = encendida;

        if (encendida)
        {
            matEmision.EnableKeyword("_EMISSION");
            matEmision.SetColor("_EmissionColor", colorEmision * intensidadEmision);
        }
        else
        {
            matEmision.DisableKeyword("_EMISSION");
        }
    }
}