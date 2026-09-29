using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlMusica : MonoBehaviour
{
    [Header("Celda Volumen")]
    public GameObject[] VolumenCelda;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumenParam = "VolumenMusica";
    [SerializeField] private float volumenMax = 0f;
    [SerializeField] private float volumenMin = -80f;
    public float porcentaje;
    [SerializeField] private Toggle toggle;

    //int CorazonesActivos;

    //bool isMute;
    //int corazonesOriginales;

    private int corazonesActivos;


    //public void ControldeMusica(float sliderMusica)
    //{
    //    audioMixer.SetFloat("VolumenMusica", Mathf.Log10(sliderMusica) * 20);
    //}


    //public void SubirVolumen()
    //{
    //    slider.value += 0.05f;
    //    if (slider.value >= 1)
    //    {
    //        Debug.Log("Volumen al Maximo");
    //        slider.value = 1;
    //    }

    //}

    //public void BajarVolumen()
    //{
    //    slider.value -= 0.05f;
    //    if (slider.value <= 0.001)
    //    {
    //        slider.value = 0.01f;
    //        Debug.Log("Volumnen al Minimo");
    //    }
    //}



    ////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        corazonesActivos=VolumenCelda.Length;
        ActualizarCorazones();
        ActualizarVolumen();
    }
    private void Update()
    {
       
        OnMute();
    }

    public void UpVolumen()
    {
        //slider.value += 1;

        //if (corazonesActivos < VolumenCelda.Length)
        //{
        //    corazonesActivos++;
        //    ActualizarCorazones();
        //}

        if (corazonesActivos >= VolumenCelda.Length)
            return;

        corazonesActivos++;
        ActualizarCorazones();
        ActualizarVolumen();
    }

    public void DownVolume()
    {
       // slider.value -= 1;

        if (corazonesActivos <= 0)
            return;

        corazonesActivos--;
        ActualizarCorazones();
        ActualizarVolumen();
        //if (corazonesActivos > 0)
        //{
        //    corazonesActivos--;
        //    ActualizarCorazones();
        //}
    }
    private void ActualizarCorazones()
    {
        for (int i = 0; i < VolumenCelda.Length; i++)
        {
            //corazones[i].SetActive(i < corazonesActivos);
            VolumenCelda[i].GetComponent<Image>().enabled = (i < corazonesActivos);
        }
    }

    private void ActualizarVolumen()
    {
        //Calculamos la proporcion (0 a 1)
        porcentaje = (float)corazonesActivos / VolumenCelda.Length;

        //Interpolamos entre volumenMin y volumenMax
        float volumenDB=Mathf.Lerp(volumenMin,volumenMax,porcentaje);

        //Asignamos el valor al AudioMixer
        audioMixer.SetFloat(volumenParam, volumenDB);
    }

    public void OnMute()
    {

        if (toggle.isOn)
        {
            audioSource.mute=true;
        }
        else
        {
            audioSource.mute = false;
        }
        
    }
       
    
}