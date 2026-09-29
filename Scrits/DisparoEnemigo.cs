using UnityEngine;
using UnityEngine.Rendering;

public class DisparoEnemigo : MonoBehaviour
{
    public Transform controladordisparo;
    //public float distanciaLinea;
    //public LayerMask capaJugador;
    //public bool jugadorEnRango;


    public GameObject balaEnemigo;


    private void Update()
    {
        
    }

    public void Disparar()
    {
        Instantiate(balaEnemigo, controladordisparo.position, controladordisparo.rotation);
    }

}
