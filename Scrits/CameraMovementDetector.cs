using UnityEngine;

public class CameraMovementDetector : MonoBehaviour
{
    private Vector2 previousPosition;
    public bool isMoving;
    [SerializeField] private float threshold;
    public static CameraMovementDetector instance;

    private void Awake()
    {
        if(instance != null)
        {
            instance = this;
        }    
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousPosition = transform.position;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position,previousPosition) > threshold) 
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

    }
}
