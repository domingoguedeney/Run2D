using UnityEngine;

public class introPlayerRun : MonoBehaviour
{
    [Header("Variables")]
    public bool isStart=false;
    public GameObject playerRendered;

    Animator ani;
    SpriteRenderer apriteR;

    public static introPlayerRun instance;
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
        apriteR = GetComponent<SpriteRenderer>();

        playerRendered.GetComponent<Movimiento_Player>().isEnableIntro = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            ani.SetBool("isRun", true);
            
            //Final_Intro();
           // return;
        }
    }

    public void Final_Intro()
    {
        apriteR.enabled = false;
        playerRendered.GetComponent<SpriteRenderer>().enabled = true;
        playerRendered.GetComponent<Movimiento_Player>().isIntro = true;
        apriteR.enabled = false;
        isStart = false;
        //Destroy(this);


    }

}
