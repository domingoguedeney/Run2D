using UnityEngine;

public class DestroyQuestions : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.CompareTag("Pregunta"))
        {
            collision.GetComponent<Enemy>().destruir();
        }*/
        if (collision.CompareTag("Ground"))
        {
            Debug.Log("Estoy en Ground !");
            collision.GetComponent<Enemy>().destruir();
        }
    }
}
