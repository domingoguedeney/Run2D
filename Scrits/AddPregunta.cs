using UnityEngine;

public class AddPregunta : MonoBehaviour
{
    [SerializeField] public int numPreguntas;

    public static AddPregunta instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CantiPreguntas(int cantidad)
    {
        numPreguntas= cantidad;
    }
}
