using UnityEngine;

public class ControladorInicio : MonoBehaviour
{

    [Header("Controles inicio")]
    public bool inicio;
    public GameObject enemy;
    public Transform enemySpawn;
    public introPlayerRun introRun;

    private void Start()
    {
        Instantiate(enemy, enemySpawn.position, Quaternion.identity);
    }


    
}
