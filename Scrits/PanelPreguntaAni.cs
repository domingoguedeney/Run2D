using UnityEngine;

public class PanelPreguntaAni : MonoBehaviour
{
    [Header("Parametros")]
    public Animator ani;

    [Header("Open Close")]
    public bool openMenu;
    public bool closeMenu;

    public static PanelPreguntaAni instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (openMenu)
        {
            OpenMenu();
            openMenu = false;
        }
        else if (closeMenu)
        {
            CloseMenu();
            closeMenu = false;
        }
    }




    public void OpenMenu()
    {
        // PONER EL TIEMPO EN CERO
        
        ani.SetTrigger("in");
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        ani.SetTrigger("out");
    }

}
