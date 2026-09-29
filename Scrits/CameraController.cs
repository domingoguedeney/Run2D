using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform activeRoom;
    public float dampSpeed;
    public Vector3 localPos;
    public float speed;
    public BOSS boss;

    [Header("Configuracion de Boss")]
    public Vector3 bossOffset = Vector3.zero; // Modifica esto manualmente o desde el script

    public static CameraController instance;



    [Range(-50, 250)]
    public float minModX, maxModX, minModY, maxModY;

    private void Awake()
    {
        if (instance == null) { instance = this; }

        dampSpeed = 500;
    }

    private void Start()
    {
        activeRoom = player;
        transform.position = new Vector3(player.position.x, player.position.y, -1);
        //dampSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        localPos = Camera.main.transform.InverseTransformPoint(player.transform.position);

        if (transform.position.y >= 8.45f)
        {
            dampSpeed = 3;

        }

        
        var minPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.min.y + minModY;
        var maxPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.max.y + maxModY;
        var minPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.min.x + minModX;
        var maxPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.max.x + maxModX;

        Vector3 clampedPos = new Vector3(Mathf.Clamp(player.position.x, minPosX, maxPosX), Mathf.Clamp(player.position.y, minPosY, maxPosY), Mathf.Clamp(player.position.z, -10f, -10f));

        Vector3 smoothPos = Vector3.Lerp(transform.position, clampedPos, dampSpeed * Time.deltaTime);


        transform.position = smoothPos;


        // Aplicamos el offset al clampedPos
        Vector3 targetPos = new Vector3(Mathf.Clamp(player.position.x, minPosX, maxPosX), Mathf.Clamp(player.position.y, minPosY, maxPosY), -10) + bossOffset; // <--- Aqui aplicamos el offset
        Vector3 smoothPos2 = Vector3.Lerp(transform.position, targetPos, dampSpeed * Time.deltaTime);
        transform.position = smoothPos2;
    }

    public IEnumerator MoverCamaraToBossView(Vector3 newOffset, float duration)
    {
        Vector3 startOffset = bossOffset;
        float elapsed = 0;

        while (elapsed < duration)
        {
            bossOffset = Vector3.Lerp(startOffset, newOffset, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        boss.IntroBoss();
        bossOffset = newOffset;
    }

    public System.Collections.IEnumerator MoverCameraToBossView(Vector3 newOffset, float duration)
    {
        // Asumiendo que añadiste la variuable 'bossOffset' a tu clase CameraController
        Vector3 startOffset = bossOffset;
        float elapsed = 0;

        while(elapsed < duration)
        {
            bossOffset = Vector3.Lerp(startOffset, newOffset, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        bossOffset = newOffset;
    }
}
