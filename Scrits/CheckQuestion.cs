using UnityEngine;

public class CheckQuestion : MonoBehaviour
{
    Movimiento_Player player;

    [Header("Preguntas")]
    public GameObject pregunta;

    private void Start()
    {
        player=GetComponent<Movimiento_Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Pregunta"))
        {
            Debug.Log("Hacer Pregunta! ");
            player.velocidadMovimiento = 0;
            pregunta.SetActive(true);
        }
    }
}
