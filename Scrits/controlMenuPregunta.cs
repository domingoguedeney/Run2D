using UnityEngine;

public class controlMenuPregunta : MonoBehaviour
{
    public void InicioPreguntas()
    {
        gameObject.GetComponentInParent<PregutaManager>().ScanPreguntas(1);
    }
}
