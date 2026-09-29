using UnityEngine;

public class Esquivar : MonoBehaviour
{
    public Movimiento_Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bala"))
        {
            if(player.isInmune)
            {
                player.animator.SetTrigger("esquivar");
            }
        }
    }
}
