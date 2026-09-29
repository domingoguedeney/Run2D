using UnityEngine;

public class BOSS : MonoBehaviour
{
    [Header("Variabes salud")]
    [SerializeField] private int vida = 5;

    [Header("Punto de Aparicion")]
    public GameObject check;

    [Header("Boss Prefab")]
    public GameObject bossPrefab;

    [Header("Variables de Ataque")]
    public int numPreguntas;
    public bool isAttack;
    public bool isHurt;
    public GameObject bullet;
    public Transform shootingPoint;


    [Header("Variables UI")]
    public GameObject panelPreguntas;

    bool isDead;
    Animator ani;
    BossMovimiento bossMove;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void IntroBoss()
    {

        Instantiate(bossPrefab,check.transform.position, Quaternion.identity);
        //ani = GameObject.FindWithTag("Boss").GetComponent<Animator>();

    }

    public void Disparo()
    {
        Instantiate(bullet,shootingPoint.transform.position, shootingPoint.rotation);
        //Instantiate(bullet,shootingPoint.transform.position, Quaternion.identity);
    }
}
