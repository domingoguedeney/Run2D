using UnityEngine;

public class Persistente : MonoBehaviour
{
    private static Persistente instance;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
