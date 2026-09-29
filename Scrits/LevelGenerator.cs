using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private int initialGrounds = 5;


    private Vector3 lastPosition;
    public float groundWidth;

    void Start()
    {
        lastPosition = startPoint.position;
        for (int i = 0; i < initialGrounds; i++)
        {
            SpawnGround();
        }

    }

    public void SpawnGround()
    {
        GameObject newGround = Instantiate(groundPrefab, lastPosition, Quaternion.identity);

        lastPosition = new Vector3(lastPosition.x + groundWidth, lastPosition.y, lastPosition.z);


    }

    public void Generador()
    {
        for (int i = 0; i < initialGrounds; i++)
        {
            SpawnGround();
        }
    }

}
