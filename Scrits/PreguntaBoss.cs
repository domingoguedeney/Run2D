using System.Collections;
using UnityEngine;

public class PreguntaBoss : MonoBehaviour
{
    public GameObject pregunta;
    public GameObject enemy;

    public Movimiento_Player player;

    DisparoEnemigo disparoBoss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movimiento_Player>();
        disparoBoss= GetComponent<DisparoEnemigo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //Debug.Log("Encontre el Scripts...!");
        }
    }

    public void BotonBueno()
    {
        //pregunta.SetActive(false);
        //player.velocidadMovimiento = 4;
        //player.saltar = true;
        Debug.Log("Repuesta Buena  !");
        StartCoroutine(SaltoTime());

    }

    public void BotonMalo()
    {
        Debug.Log("Respuesta Equivocada  !");

        disparoBoss.Disparar();
        //player.Hurt();
        //player.animator.SetTrigger("daño");
        //player.health--;
        //pregunta.SetActive(false);
    }

    IEnumerator SaltoTime()
    {
        yield return new WaitForSeconds(.5f);
        player.BotonSaltoArriba();
        player.velocidadMovimiento = 3;
    }
}

