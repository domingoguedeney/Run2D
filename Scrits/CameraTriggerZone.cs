using UnityEngine;

public class CameraTriggerZone : MonoBehaviour
{
    [Header("Configuracion  del Offset")]
    public Vector3 bossOffset = new Vector3(11.5f, 0f, 0f); // Ajusta este X segun necesites
    public float transitionDuration = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Asegurate de que el jugador tenga el tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Accedemos a la instancia de tu CameraController
            if (CameraController.instance != null)
            {
                StartCoroutine(CameraController.instance.MoverCamaraToBossView(bossOffset, transitionDuration));
            }
        }
    }
}
