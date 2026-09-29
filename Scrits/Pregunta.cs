using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Pregunta : MonoBehaviour
{
    //public Button bueno;
    //public Button malo1,malo2;

    public GameObject pregunta;
    public GameObject enemy;

    public Movimiento_Player player;


    /////////////////////
    public GameObject boss;
    public bool isBoss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movimiento_Player>();
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
        if (isBoss)
        {
            player.isInmune = true;
            //player.animator.SetTrigger("esquivar");
            boss.GetComponent<DisparoEnemigo>().Disparar();
            
        }
        else
        {
            pregunta.SetActive(false);
            player.velocidadMovimiento = 4;
            player.saltar = true;
            Debug.Log("Repuesta Buena  !");
            StartCoroutine(SaltoTime());
        }
        



    }

    public void BotonMalo()
    {
        Debug.Log("Respuesta Equivocada  !");

        if (isBoss)
        {
            boss.GetComponent<DisparoEnemigo>().Disparar();
        }
        else
        {
            player.Hurt();
            player.animator.SetTrigger("daño");
            player.health--;
           // pregunta.SetActive(false);
        }


    }

    IEnumerator SaltoTime()
    {
        yield return new WaitForSeconds(.5f);
        player.BotonSaltoArriba();
        player.velocidadMovimiento = 3;
    }
}
