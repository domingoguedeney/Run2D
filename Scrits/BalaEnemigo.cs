using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{

    public float velocidad;
    //public int daño;
    public Transform player;

    private void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        //transform.Translate(Time.deltaTime * velocidad * Vector2.right);
        transform.Translate(Time.deltaTime*velocidad*-player.position);
        //transform.Translate(Time.deltaTime * velocidad * new Vector2(-23,-15));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.LogWarning("Pierdes un Corazon !!");
        }
    }
}
