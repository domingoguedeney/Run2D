using UnityEngine;
using UnityEngine.UI;

public class Cambio_Cara : MonoBehaviour
{
    public GameObject[] caras;

    public int cara;
    public bool isChange=false;

    public static Cambio_Cara instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance=this;
        }
        
    }
    private void Start()
    {
        CambioCaras(0);
    }

    private void Update()
    {
        if (isChange)
        {
            for (int i = 0; i < caras.Length; i++)
            {
                caras[i].SetActive(i == cara);
                
            }
            isChange = false;
        }
    }


    public void CambioCaras(int indiceActivo)
    {
        cara = indiceActivo;
        isChange = true;
       /* for (int i = 0; i < caras.Length; i++) 
        {
            caras[i].SetActive(i == indiceActivo);
        }*/

    }
}
