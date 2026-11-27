using UnityEngine;

public class VRWallPushback : MonoBehaviour
{
    [Tooltip("Arrastra aquí tu XR Origin Hands desde la jerarquía")]
    public Transform xrOrigin;

    [Tooltip("El tamaño de tu cabeza virtual. 0.15 es un buen promedio")]
    public float headRadius = 0.15f;

    [Tooltip("La capa de colisión exclusiva para los muros")]
    public LayerMask wallLayer;

    void Update()
    {
        // Tira una esfera imaginaria desde el centro de la cámara para detectar muros
        Collider[] hits = Physics.OverlapSphere(transform.position, headRadius, wallLayer);

        if (hits.Length > 0)
        {
            // Calculamos desde dónde nos está golpeando la pared
            Vector3 pushDirection = transform.position - hits[0].ClosestPoint(transform.position);

            // Anulamos el eje Y para que no te empuje hacia el techo o el piso
            pushDirection.y = 0;

            // Empujamos el XR Origin entero hacia atrás suavemente
            if (pushDirection != Vector3.zero)
            {
                xrOrigin.position += pushDirection.normalized * Time.deltaTime * 3f;
            }
        }
    }
}