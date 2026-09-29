using Unity.VisualScripting;
using UnityEngine;

public class MoverBullet : MonoBehaviour
{
    [Header("Variables Transform")]
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad=2f;
    private Vector3 objetivo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
        puntoA= GameObject.FindWithTag("puntoA").transform;
        puntoB= GameObject.FindWithTag("puntoB").transform;

        transform.position=puntoA.position;
        objetivo = puntoB.position;

        
        
                
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve el objeto hacia el objetivo a una velocidad constante
        transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);




        // Si llega al punto B, puedes hacer que se detenga o invertir la dirección
        if (Vector3.Distance(transform.position, objetivo) < 0.01f)
        {
            // Opcional: Invertir el movimiento
            //objetivo = (objetivo == puntoA.position) ? puntoB.position : puntoA.position;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Esquivar"))
        {
            Debug.Log("Toque Esquivar !");
            velocidad = 0;
        }
        if (collision.CompareTag("ColiderFinal"))
        {
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        velocidad = 16;
    }
}
