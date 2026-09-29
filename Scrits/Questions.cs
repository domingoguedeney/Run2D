using UnityEngine;
using UnityExtensions;


[System.Serializable]
public struct OpcionesPregunta
{
    [TextArea(2, 4)]
    public string opcion;
    public bool iscorrecta;
}

[CreateAssetMenu(fileName = "Preguntas", menuName = "Nueva Pregunta")]
public class Questions : ScriptableObject
{
    //public bool isCorrecta;
    public bool isBoss;

    [TextArea(3, 5)]
    public string pregunta;
    [ReorderableList]
    public OpcionesPregunta[] opciones;


}
