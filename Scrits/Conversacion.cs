using UnityEngine;
using UnityExtensions;

[CreateAssetMenu(fileName = "Conversacion", menuName = "Sistema de Dialogo/Nueva Conversacion")]
public class Conversacion : ScriptableObject
{
    //[System.Serializable]
    //public struct Linea
    //{
    //    public Personaje personaje;

    //    public AudioClip sonido;

    //    [TextArea(3, 5)]
    //    public string dialogo;
    //}

    //public bool desbloqueada;
    //public bool finalizado;
    //public bool reUsar;
    public bool isCorrecta;
    public bool isBoss;
    //[ReorderableList]
    //public Linea[] dialogos;

    /////////
    public Preguntas pregunta;
    //public Opciones opciones;
    ////////
}
