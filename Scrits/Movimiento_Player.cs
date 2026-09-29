using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.GPUSort;

public class Movimiento_Player : MonoBehaviour
{
    [Header("IsClass")]
    public bool isClass;

    [Header("Parametros Generales")]
    public BOSS boss;
    public PregutaManager pManager;
    public GameObject panel;
    public BoxCollider2D colEsquivar;
    public int contadorPreguntas = 0;
    public PanelGameOver panelOver;
    public GameObject dieAnimation;
    private GameObject instanciaActual;
    public DeathEffectHandler deathEffect;

    [Header("Parametros MenuPregunta")]
    public GameObject menuPregunta;

    [Header("Configuracion del Offset Boss")]
    public Vector3 bossOffset = new Vector3(11.5f, 0f, 0f); // Ajusta este X segun necesites
    public float transitionDuration = 1.5f;


    [Header("Referencias")]
    private Rigidbody2D rb2D;
    public Animator animator;

    [Header("Movimiento")]
    public bool isMove = false;
    public float velocidadMovimiento;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private LayerMask queEsSuelo;
    public bool ensuelo;
    public bool saltar;

    [Header("SaltoRegulable")]
    [Range(0, 1)][SerializeField] private float multiplicadorCancelarSalto;
    [SerializeField] private float multiplicadorGravedad;
    private float escalaGravedad;
    private bool botonSaltoArriba = true;

    [Header("Vida")]
    public GameObject[] corazones;
    public int health = 3;
    public float fuerzaHurt = 2;
    public bool isDead;

    InputAction playerInput;

    [Header("Parametros")]
    public float gravedad;
    public bool aniJump;
    public bool isIntro = true;
    public bool isboss;
    public bool isEnableIntro;

    int menos;
    public BoxCollider2D boxBoss;

    public CircleCollider2D coliderP;
    bool isSave;
    private Enemy enemy;

    float positionPlayer;

    int respuestasMala = 0;


    public BoxCollider2D boxCollider;
    public bool isInmune = false;
    public bool isDie = false;

    [Header("Parametros Bala")]
    public GameObject fxBala;
    public Transform checkBala;

    [Header("Parametros FX Correr")]
    public Transform checkFxCorrer;
    public GameObject fxCorrer;

    [Header("FX CAER")]
    public GameObject fxCaer;

    [Header("Parametros Pre...")]
    public Questions preguntaH;
    public string nombrePregunta;

    public static Movimiento_Player instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        deathEffect = GetComponent<DeathEffectHandler>();
        escalaGravedad = rb2D.gravityScale;

        boxBoss.enabled = false;

        /* if (isIntro)
         {
             aniJump = true;
             animator.SetTrigger("intro");

         }*/
        //isIntro = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (isEnableIntro)
        {
            velocidadMovimiento = 0;
            if (isIntro)
            {
                aniJump = true;
                animator.SetTrigger("intro");
                isIntro = false;
                isEnableIntro = false;
            }
        }

        positionPlayer = CameraController.instance.localPos.x;
        panelOver.respuestasCorrectas = contadorPreguntas;
        panelOver.respuestasInCorrectas = respuestasMala;

        if (transform.position.x >= -5f)
        {
            isMove = true;
        }

       

        if (!isInmune)
        {
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled = true;
        }

        //animator.SetFloat("velY", rb2D.linearVelocity.y);
        animator.SetFloat("gravedad", gravedad);

        ensuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0, queEsSuelo);

        if (!isDead)
        {
             switch (health)
             {
                 case 0:
                     corazones[0].SetActive(false);
                     corazones[1].SetActive(false);
                     corazones[2].SetActive(false);
                     isDead = true;
                     break;
                 case 1:
                     corazones[0].SetActive(false);
                     corazones[1].SetActive(false);
                     corazones[2].SetActive(true);
                     break;
                 case 2:
                     corazones[0].SetActive(false);
                     corazones[1].SetActive(true);
                     corazones[2].SetActive(true);
                     break;

             }

        }
        else
        {
            Dead();
            Cambio_Cara.instance.CambioCaras(5);
            isDead = false;
        }

        if (!aniJump && !isDead)
        {

            rb2D.linearVelocity = new Vector2(velocidadMovimiento, rb2D.linearVelocity.y);

            if (rb2D.linearVelocity.x != 0 && ensuelo)
            {
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
            }

        }

        animator.SetBool("jump", saltar);
        animator.SetBool("enSuelo", ensuelo);
    }

    public void Dead()
    {
        // Aqui activamos animacion de muerte y activacion pantalla de GAME OVER!
        velocidadMovimiento = 0;
        animator.SetTrigger("die");
    }

    //Referencia Boton de Salto
    public void PruebaSalto(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            saltar = true;
        }
        if (callbackContext.canceled)
        {
            BotonSaltoArriba();
        }
    }
    private void FixedUpdate()
    {
        if (saltar && botonSaltoArriba && ensuelo)
        {
            Saltar();
        }

        if (rb2D.linearVelocity.y < 0 && !ensuelo)
        {
            rb2D.gravityScale = escalaGravedad * multiplicadorGravedad;
        }
        else
        {
            rb2D.gravityScale = escalaGravedad;
        }
        saltar = false;
    }

    private void Saltar()
    {
        aniJump = true;
        rb2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        gravedad = 1;
        ensuelo = false;
        saltar = false;
        botonSaltoArriba = false;
    }

    public void Hurt()
    {
        aniJump = true;
        rb2D.AddForce(Vector2.left * fuerzaHurt, ForceMode2D.Impulse);
    }
    public void BotonSaltoArriba()
    {
        if (rb2D.linearVelocity.y > 0)
        {
            rb2D.AddForce(Vector2.down * rb2D.linearVelocity.y * (1 - multiplicadorCancelarSalto), ForceMode2D.Impulse);
            gravedad = 0;
        }

        botonSaltoArriba = true;
        saltar = false;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }

    public void Inicio_Jump()
    {
        //pManager.ScanPreguntas(0);
    }

    public void Inicio_Hurt()
    {
        //pManager.ScanPreguntas(0);
    }
    public void Final_Jump()
    {
        aniJump = false;
        pManager.ScanPreguntas(0);
        enemy.destruir();
        isIntro = false;
        Cambio_Cara.instance.CambioCaras(0);
        //PanelPreguntaAni.instance.closeMenu = true;// BETA
        //panel.SetActive(false);// ORIGINAL 

        SpawnPregunta.instance.SpawnQuestions();
        if (pManager.listaVacia)
        {
            StartCoroutine(NoRun()); // Comando para que entre el boss a Escena
        }
    }

    public void Final_Hurt()
    {
        aniJump = false;
        pManager.ScanPreguntas(0);
        if (isboss)
        {
            // No deberia pasar nada aca!
        }
        else
        {
            enemy.destruir();
            velocidadMovimiento = 4;
        }


        Cambio_Cara.instance.CambioCaras(0);
        //PanelPreguntaAni.instance.closeMenu=true;// BETA
        //panel.SetActive(false);// ORIGINAL
        if (isboss)
        {
            DialogoManager2.instance.isAttack = true;
        }
        else
        {
            SpawnPregunta.instance.SpawnQuestions();
        }

        if (pManager.listaVacia)
        {

            if (!isboss && !isDead)
            {
                StartCoroutine(NoRun());
            }
            else
            {
                panelOver.InicioPanel();
            }
        }
    }

    public void Final_Intro()
    {
        aniJump = false;

        isIntro = false;
        Cambio_Cara.instance.CambioCaras(0);
        velocidadMovimiento = 4;
        panel.SetActive(false);
        //SpawnPregunta.instance.SpawnQuestions();
    }

    public void Final_Die()
    {
        if (instanciaActual==null)
        {
            instanciaActual = Instantiate(dieAnimation, transform.position, Quaternion.identity);
            
        }

    }
    public void Final_Dash()
    {
        pManager.ScanPreguntas(0);
        //enemy.destruir();
        Cambio_Cara.instance.CambioCaras(0);
        panel.SetActive(false);
        DialogoManager2.instance.isAttack = true;
        if (pManager.listaVacia)
        {
            Debug.LogWarning("You Win !!");
            BossMalito.instance.animator.SetTrigger("dead");
            //panelOver.InicioPanel(); // Activa el panel de GameOver
            //panelOver.enabled = true;
        }
    }

    public void Final_Ani_Intro()
    {
        Instantiate(fxCorrer, checkFxCorrer.position, Quaternion.identity);
    }

    IEnumerator NoRun()
    {
        yield return new WaitForSeconds(2);
        velocidadMovimiento = 0;
        StartCoroutine(CameraController.instance.MoverCamaraToBossView(bossOffset, transitionDuration));
        // isCambio = true;
        boxBoss.enabled = true;
        colEsquivar.enabled = true;
        isboss = true;
        pManager.isBossP = true;
        pManager.isBoss = true;
       // boss.IntroBoss();
    }

    public void isCorrecta(bool correcta)
    {
        if (isboss)
        {

            if (correcta)
            {
                Debug.Log("Aqui va el movimiento de esquivar si la respuesta es correcta !");
                //isSave = true;
                coliderP.enabled = false;
                animator.SetTrigger("esquivar");
                contadorPreguntas++;
                StartCoroutine(OcultarUI());
            }
            else
            {
                //DialogoManager.instance.MostrarUI(false);
                Cambio_Cara.instance.CambioCaras(2);
                Hurt();
                animator.SetTrigger("daño");
                enemy.destruir();
                Instantiate(fxBala,checkBala.position,Quaternion.identity);
                //respuestasMala++;
                health--;
                StartCoroutine(OcultarUI());

                Debug.Log("Resibi daño del Boss !");
            }
        }
        else
        {
            if (correcta)
            {
                Cambio_Cara.instance.CambioCaras(3);
                //DialogoManager.instance.MostrarUI(false);
                //Debug.LogWarning("PEPITO    !!!");
                velocidadMovimiento = 4;
                saltar = true;
                contadorPreguntas++;
                PanelPreguntaAni.instance.CloseMenu();
                StartCoroutine(SaltoTime());

            }
            else
            {
                //DialogoManager.instance.MostrarUI(false);
                Cambio_Cara.instance.CambioCaras(2);
                Hurt();
                animator.SetTrigger("daño");
                respuestasMala++;
                health--;
                PanelPreguntaAni.instance.CloseMenu(); // NUEVO
                //StartCoroutine(OcultarUI());// ORIGINAL
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pregunta"))
        {
            if (!isboss)
            {
                velocidadMovimiento = 0;

                Cambio_Cara.instance.CambioCaras(1);
                //StartCoroutine(CambioCara());
                enemy = collision.GetComponentInParent<Enemy>();
                // pManager.LimpiarPreguntas();
                RamdonPreguntas();

                if (isClass)
                {
                    //menuPregunta.GetComponent<Animator>().SetTrigger("in");
                    PanelPreguntaAni.instance.OpenMenu();
                    pManager.ScanPreguntas(1);
                }
                else
                {
                    pManager.ScanPreguntas(1);
                }
                    

                // Prueba de RandomPreguntas add Lista
                // pManager.AgregarAlAzar(pManager.cantidadAExtraer);
                //Debug.LogWarning("Colisiones con la pregunta !.");
            }
            else
            {
                coliderP = collision.GetComponent<CircleCollider2D>();
                velocidadMovimiento = 0;
                enemy = collision.GetComponent<Enemy>();
                Cambio_Cara.instance.CambioCaras(1);
                //StartCoroutine(CambioCara());
                enemy = collision.GetComponentInParent<Enemy>();
                // pManager.LimpiarPreguntas();
                RamdonPreguntas();

                pManager.ScanPreguntas(1);

                // Prueba de RandomPreguntas add Lista
                // pManager.AgregarAlAzar(pManager.cantidadAExtraer);
                //Debug.LogWarning("Colisiones con la pregunta !.");
            }
        }

       
    }

    IEnumerator OcultarUI()
    {
        yield return new WaitForSeconds(.2f);
        //DialogoManager.instance.MostrarUI(false);
    }
    IEnumerator SaltoTime()
    {
        yield return new WaitForSeconds(.5f);
        BotonSaltoArriba();
        velocidadMovimiento = 3;
        //Cambio_Cara.instance.CambioCaras(0);
        //DialogoManager.instance.MostrarUI(false);
    }

    IEnumerator CambioCara()
    {
        Cambio_Cara.instance.CambioCaras(4);
        yield return new WaitForSeconds(0.0000001f);
        Cambio_Cara.instance.CambioCaras(1);
    }


    public void RamdonPreguntas()
    {
        pManager.Prueba();
    }

    public void FX_Caer()
    {
        Instantiate(fxCaer, transform.position, Quaternion.identity);
    }

    public void FX_Land()
    {
        Instantiate(fxCorrer,transform.position, Quaternion.identity);
    }
}

