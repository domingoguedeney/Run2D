using UnityEngine;

public class ClockTime : MonoBehaviour
{
    //Mover el minutero del Reloj
    public Transform clockHandTransform;

    //  Temporizador
    [SerializeField] private float tiempoMaximo;
    private float tiempoActual;
    private bool tiempoActivado = false;


    private void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ActivarTemporizador();
    }

    // Update is called once per frame
    void Update()
    {
        clockHandTransform.eulerAngles=new Vector3 (0,0,tiempoActual *90);

        if (tiempoActivado)
        {
            CambiarContador();
        }
    }

    private void CambiarContador()
    {
        tiempoActual -= Time.deltaTime;

        if(tiempoActual<=0)
        {
            Debug.Log("Derrota");
            CambiarTemporizador(false);
        }
    }

    private void CambiarTemporizador(bool estado)
    {
        tiempoActivado = estado;
    }

    private void ActivarTemporizador()
    {
        tiempoActual = tiempoMaximo;
        CambiarTemporizador(true);
    }

    private void DesactivarTemporizador()
    {
        CambiarTemporizador(false);
    }
}
