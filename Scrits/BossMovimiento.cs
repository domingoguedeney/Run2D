using UnityEngine;

public class BossMovimiento : MonoBehaviour
{
    [Header("Variables Movimiento")]
    public float velocidad;
    public bool isMoving;

    [Header("Variable Disparo")]
    public Transform puntoA;
    public Transform puntoB;
    public float velBullet = 2f;
    public GameObject bullet;
    public bool isShooting;

    public bool isEmpy=false;
    Vector3 objetivo;
    Animator animator;
    Rigidbody2D rb2D;

    public static BossMovimiento instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //gameObject.GetComponent<SpriteRenderer>().flipX = true;

        bullet.transform.position=puntoA.position;
        objetivo=puntoB.position;
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

        if (!isMoving)
        {
            // Aqui no pasa nada !!
            //animator.SetBool("run", false);
            velocidad = 0;
        }
        rb2D.linearVelocity = new Vector2(velocidad, rb2D.linearVelocity.y);

        if (rb2D.linearVelocity.x != 0)
        {

            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("vol", true);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FinalBossWalk"))
        {

            isMoving = false;
            //animator.SetBool("vol", true);

        }



    }


    public void shootingBullet()
    {
        Instantiate(bullet,puntoA.transform.position,Quaternion.identity);
        // Mueve el objetivo hacia el objetivo a una velocidad constante
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, objetivo, velBullet * Time.deltaTime);

        // Si llega al punto B, puedes hacer que se detenga o invertir la direccion
        if(Vector3.Distance(bullet.transform.position, objetivo) < 0.01f)
        {
            // Opcional: Invertir el movimiento
            objetivo=(objetivo==puntoA.position) ? puntoB.position:puntoA.position;
        }
    }

    public void Final_Ani()
    {
        animator.SetBool("vol", false);
        animator.SetBool("idle", true);
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        DialogoManager2.instance.Inicio();

        
    }
    public void Final_Ani_Attack()
    {
        shootingBullet();
    }



}
