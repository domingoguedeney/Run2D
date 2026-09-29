using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator ani;
    public void destruir()
    {
        //Animacion de muerte enemigo, si es que la hay
        //ani.SetTrigger("death");
        
        Destroy(this.gameObject);
    }
}
