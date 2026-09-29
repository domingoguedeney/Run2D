using UnityEngine;

public class DialogoManager : MonoBehaviour
{
    public static DialogoManager instance { get; private set; }
    public static DialogueSpeaker speakerActual;
    [SerializeField] private DialogoUI dialUI;
    [SerializeField] private GameObject player;
    [SerializeField] private Movimiento_Player playerMovement;

    ///////////
    public ControladorPreguntas controladorPreguntas;
    //////////

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        dialUI = FindAnyObjectByType<DialogoUI>();

                
    }

    private void Start()
    {
        MostrarUI(false);
        //MostrarUI(true);

        
    }

    private void Update()
    {
        
    }

    public void MostrarUI(bool mostrar)
    {
        dialUI.gameObject.SetActive(mostrar);

        if(!mostrar)
        {
            ///playerMovement.velocidadMovimiento = 3;

        }
        else
        {
            playerMovement.velocidadMovimiento = 0;
            //Aqui puede ir detener al jugador mientras se muestra las preguntas
        }
    }
       
    
}
