using UnityEngine;
using UnityExtensions;

[System.Serializable]
public struct Opciones
{
    [TextArea(2, 4)]
    public string opcion;
    public Conversacion convResultante;
}

[CreateAssetMenu(fileName = "Pregunta", menuName ="Sistema de Dialogo/Nueva Pregunta")]
public class Preguntas : ScriptableObject
{
    [TextArea(3, 5)]
    public string pregunta;
    [ReorderableList]
    public Opciones[] opciones;
    
}
