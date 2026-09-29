using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GuardarYCargarDatos : MonoBehaviour
{
    public AudioSource audioSource;
    public ControlMusica musicControl;
    public float musicVolume;
    public Toggle toggle;
    public bool toggleIsOn;

    public static GuardarYCargarDatos instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggleIsOn = toggle.isOn;
        musicVolume = musicControl.porcentaje;
        
    }

    // Update is called once per frame
    void Update()
    {
        audioSource = FindAnyObjectByType<AudioSource>();

        if(audioSource == null)
        {
            toggleIsOn = toggle.isOn;
        }
        toggleIsOn = toggle.isOn;
        musicVolume = musicControl.porcentaje;
    }


    public void GuardarDatos()
    {
        // Guardar Datos
        PlayerPrefs.SetFloat("Volume", musicVolume);
        
    }

    public void CargarDatos()
    {
        // Cargamos los datos anteriormente guardados
    }


    public void OnMute()
    {

        if (toggle.isOn)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }

    }
}
