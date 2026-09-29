using Unity.VisualScripting;
using UnityEngine;

public class BossMalito : MonoBehaviour
{
    [Header("Variable Disparo")]
    public Transform puntoA;
    public Transform puntoB;
    public float velBullet = 2f;
    public GameObject bullet;
    public bool isShooting;

    public Transform objetoPadre;

    public bool isEmpy = false;
    Vector3 objetivo;
    public Animator animator;
    Rigidbody2D rb2D;

    public static BossMalito instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        objetoPadre=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();



        bullet.transform.position = puntoA.position;
        objetivo = puntoB.position;

        transform.SetParent(objetoPadre);
    }

    void Update()
    {
        if (isShooting)
        {
            if (!isEmpy)
            { //shootingBullet();
                animator.SetTrigger("attack");
                isShooting = false;
            }
            else
            {
                Debug.Log("La lista esta vacia !");
            }
        }

    }

    public void shootingBullet()
    {
        Instantiate(bullet, puntoA.transform.position, Quaternion.identity);
        // Mueve el objetivo hacia el objetivo a una velocidad constante
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, objetivo, velBullet * Time.deltaTime);

        // Si llega al punto B, puedes hacer que se detenga o invertir la direccion
        if (Vector3.Distance(bullet.transform.position, objetivo) < 0.01f)
        {
            // Opcional: Invertir el movimiento
            objetivo = (objetivo == puntoA.position) ? puntoB.position : puntoA.position;
        }
    }

    public void Final_Ani()
    {
        animator.SetBool("idle", true);
        DialogoManager2.instance.Inicio();


    }

    public void Final_Ani_Attack()
    {
        shootingBullet();
    }

    public void Final_Ani_Dead()
    {
        gameObject.GetComponentInParent<Movimiento_Player>().panelOver.InicioPanel();
    }
}
