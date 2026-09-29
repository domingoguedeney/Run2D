using System;
using System.Collections;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambiodemusica : MonoBehaviour
{
    public AudioClip[] musicaFondo;
    UnityEngine.SceneManagement.Scene escena;

    bool isStartScene, isStage1;
    //public SceneManager escena;

    [SerializeField] private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        escena = SceneManager.GetActiveScene();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        escena = SceneManager.GetActiveScene();
        Debug.Log("Escena actual: " + escena.name);

        if (escena.name == "StartScene" && !isStartScene)
        {
            CambioMusicaFondo(0);
            isStartScene = true;

        }
        if (escena.name == "Nivel_1" && !isStage1)
        {
            CambioMusicaFondo(1);
            isStage1 = true;

        }
    }


    public void CambioMusicaFondo(int audio)
    {
        switch (audio)
        {
            case 0:
                audioSource.clip = (musicaFondo[0]);
                audioSource.Play();
                Debug.Log("Musica 0 Tocando!. ");
                break;
            case 1:
                audioSource.clip = (musicaFondo[1]);
                audioSource.Play();
                Debug.Log("Musica 1 Tocando!. ");
                break;
            case 2:
                Debug.Log("Musica 2 Tocando!. ");
                break;
            case 3:
                Debug.Log("Musica 3 Tocando!. ");
                break;
            case 4:
                Debug.Log("Musica 4 Tocando!. ");
                break;
            case 5:
                Debug.Log("Musica 5 Tocando!. ");
                break;
        }


    }


}
