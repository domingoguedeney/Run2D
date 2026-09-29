using System.Collections;
using UnityEngine;

public class enemyIntro : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float velocidadMovimiento = 4;
    [SerializeField]private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb2D.linearVelocity = new Vector2(velocidadMovimiento, rb2D.linearVelocity.y);
        StartCoroutine(Destroy_Time());
    }


    public void Final_Ani()
    {
       // Movimiento_Player.instance.isIntro = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("intro"))
        {
            Debug.Log("Soy el Malo !");
            SpawnPregunta.instance.SpawnQuestions();
            introPlayerRun.instance.isStart = true;
        }
    }

    IEnumerator Destroy_Time()
    {
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);
    }

}
