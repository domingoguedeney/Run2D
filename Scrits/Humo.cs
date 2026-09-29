using UnityEngine;

public class Humo : MonoBehaviour
{
    public GameObject malito;


    public void Final_Ani()
    {
        Instantiate(malito,transform.position,Quaternion.identity);
        //malito.SetActive(true);
    }

    public void Final_Destroy()
    {
        Destroy(this.gameObject);
    }
}
