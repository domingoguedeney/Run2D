using UnityEngine;
using UnityEngine.UI;

public class DeathEffectHandler : MonoBehaviour
{
    [Header("Referencias")]
    public Material irisMaterial;
    public Transform playerTransform;
    public Transform cameraTransform; //Arrastra la camara principal aqui

    [Header("Ajustes de Animacion")]
    public float transitionSpeed = 1.5f;
    public float slowMoTarget = 0.2f; // que tan lento queremos el tiempo

    [Header("Ajustes de Shake")]
    public float shakeIntensity = 0.1f;

    private float currentRadius = 1.5f;
    private bool isDying = false;
    private Vector3 originalCamPos;

    private void Start()
    {
        if (cameraTransform != null) 
        {
            originalCamPos = cameraTransform.localPosition;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // 1. Convertimos la posición del jugador a coordenadas de pantalla (0 a 1)
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(playerTransform.position);
        irisMaterial.SetVector("_Center", new Vector4(screenPoint.x, screenPoint.y, 0, 0));

        // 2. Lógica de animación
        if (isDying)
        {
            // Cerramos el circulo gradualmente
            currentRadius = Mathf.MoveTowards(currentRadius, 0f, Time.deltaTime * transitionSpeed);

            // 3. Efecto: Camara lenta (SlowMotion)
            // El tiempo se ralentiza a medida que el circulo se cierra
            float t = 1 - (currentRadius / 1.5f);
            Time.timeScale = Mathf.Lerp(1f, slowMoTarget, t);

            // 4. Efecto: Shake de Camara
            if (currentRadius > 0f)
            {
                cameraTransform.localPosition = originalCamPos + Random.insideUnitSphere * shakeIntensity;
            }
            else 
            {
                cameraTransform.localPosition = originalCamPos; // Reset al final
            }

            if (currentRadius <= 0) 
            {
                Movimiento_Player.instance.panelOver.InicioPanel();
                isDying = false;
                ResetEffect();
            }

            
        }
        irisMaterial.SetFloat("_Radius", currentRadius);
        //Movimiento_Player.instance.panelOver.InicioPanel();
        /*else
        {
            // Opcional: Para que empiece abierto al iniciar nivel
            currentRadius = 1.5f;
        }
        irisMaterial.SetFloat("_Radius", currentRadius);
        irisMaterial.SetFloat("_Radius", currentRadius);*/
    }

    [ContextMenu("Trigger Death")] // Para probarlo desde el Inspector
    public void StartDeathEffect()
    {
        isDying = true;
    }

    // Metodo para resetear todo (llamalo al reaparecer)
    [ContextMenu("Reset Pantalla")]
    public void ResetEffect()
    {
        Debug.Log("Estoy en reset");
        isDying = false;
        currentRadius = 1.5f;
        Time.timeScale = 1f;
        if (cameraTransform != null) cameraTransform.localPosition = originalCamPos;
    }
}