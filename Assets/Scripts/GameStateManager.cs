// Modified: Created to manage the countdown timer and interaction counter required in Phase 3
using UnityEngine;
using UnityEngine.Events;
using TMPro; // Required for TextMeshPro

/// <summary>
/// Centralizes game state rules, countdown timer, interaction counter, and World Space UI screens.
/// </summary>
public class GameStateManager : MonoBehaviour
{
    [Header("Configuración del Juego")]
    public int totalInteraccionesRequeridas = 6;
    public float tiempoLimiteSegundos = 120f; // 2 minutes

    [Header("Referencias a los Canvas (World Space)")]
    public GameObject canvasHUD;
    public GameObject canvasGameOver;
    public GameObject canvasYouWin;

    [Header("Referencias de Texto UI")]
    public TextMeshProUGUI textoTemporizador;
    public TextMeshProUGUI textoContador;

    private int interaccionesActuales = 0;
    private float tiempoRestante;
    private bool juegoActivo = false;

    // Optional events to trigger win/lose audio from the Inspector
    public UnityEvent AlGanar;
    public UnityEvent AlPerder;

    /// <summary>
    /// Starts the game session when the scene loads.
    /// </summary>
    void Start()
    {
        IniciarJuego();
    }

    /// <summary>
    /// Resets counters, activates the main HUD, and hides end-state canvases.
    /// </summary>
    public void IniciarJuego()
    {
        interaccionesActuales = 0;
        tiempoRestante = tiempoLimiteSegundos;
        juegoActivo = true;

        // Ensure only the HUD is visible at startup
        if (canvasHUD != null) canvasHUD.SetActive(true);
        if (canvasGameOver != null) canvasGameOver.SetActive(false);
        if (canvasYouWin != null) canvasYouWin.SetActive(false);

        ActualizarUI();
    }

    /// <summary>
    /// Updates the countdown timer every frame and triggers Game Over when time expires.
    /// </summary>
    void Update()
    {
        if (!juegoActivo) return;

        tiempoRestante -= Time.deltaTime;
        ActualizarUI();

        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            ActivarGameOver();
        }
    }

    /// <summary>
    /// Increments the completed interaction counter and triggers victory once the target is reached.
    /// </summary>
    // Key function called by interactive object scripts
    public void RegistrarInteraccion()
    {
        if (!juegoActivo) return;

        interaccionesActuales++;
        ActualizarUI();

        if (interaccionesActuales >= totalInteraccionesRequeridas)
        {
            ActivarYouWin();
        }
    }

    /// <summary>
    /// Formats and refreshes the timer and counter labels on the World Space HUD.
    /// </summary>
    private void ActualizarUI()
    {
        if (textoTemporizador != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            textoTemporizador.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }

        if (textoContador != null)
        {
            textoContador.text = $"{interaccionesActuales} / {totalInteraccionesRequeridas}";
        }
    }

    /// <summary>
    /// Stops gameplay, switches to the Game Over World Space canvas, and invokes defeat events.
    /// </summary>
    private void ActivarGameOver()
    {
        juegoActivo = false;
        if (canvasHUD != null) canvasHUD.SetActive(false);
        if (canvasGameOver != null) canvasGameOver.SetActive(true);
        AlPerder?.Invoke();
    }

    /// <summary>
    /// Stops gameplay, switches to the You Win World Space canvas, and invokes victory events.
    /// </summary>
    private void ActivarYouWin()
    {
        juegoActivo = false;
        if (canvasHUD != null) canvasHUD.SetActive(false);
        if (canvasYouWin != null) canvasYouWin.SetActive(true);
        AlGanar?.Invoke();
    }
}