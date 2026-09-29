using UnityEngine;

public class DieScript : MonoBehaviour
{
    public float velocidad = 3;
    Rigidbody2D rb2D;


    private void Update()
    {
        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x,velocidad);
    }


    public void Final_ani()
    {
        //Movimiento_Player.instance.panelOver.InicioPanel();

        Movimiento_Player.instance.deathEffect.StartDeathEffect();
        //PanelGameOver.instance.InicioPanel();
    }

    public void Final_Ani_Destroy()
    {
        Destroy(this.gameObject);
    }
}
