using Unity.VisualScripting;
using UnityEngine;

public class SpawnPregunta : MonoBehaviour
{
    public GameObject spawn;

    public bool isEmpy=false;
    public static SpawnPregunta instance;
    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
    }

    public void SpawnQuestions()
    {
        if (!isEmpy)
        {
            Instantiate(spawn, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No se Espawnea mas.!");
        }
        
    }
}
