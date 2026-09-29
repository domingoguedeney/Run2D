using UnityEngine;

public class Ground : MonoBehaviour
{
    private LevelGenerator levelGenerator;
    private bool spawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();    
    }

    // Update is called once per frame
    void Update()
    {
        // Si el jugador se acerca al final de este bloque, genera otro
        if (!spawned && Camera.main.transform.position.x > transform.position.x) 
        {
            levelGenerator.SpawnGround();
            spawned = true;

            // Opcional: Destruye este suelo despues de un tiempo para no saturar el juego
            //Destroy(gameObject, 10f);
        }
    }
}
