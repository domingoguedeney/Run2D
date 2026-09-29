using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ConectorWordPress : MonoBehaviour
{
    private string urlServidor = "https://http://192.168.100.106:8195/wp-admin/registro_unity.php";
    private string claveSecreta = "mauricio1982";

    public void EnviarDatosAlServidor(string nombre, string fecha)
    {
        StartCoroutine(SubirDatosCoroutine(nombre, fecha));
    }

    private IEnumerator SubirDatosCoroutine(string nombre, string fecha)
    {
        WWWForm formulario = new WWWForm();
        formulario.AddField("clave", claveSecreta);
        formulario.AddField("nombre_jugador", nombre);
        formulario.AddField("fecha_registro", fecha);

        using (UnityWebRequest www = UnityWebRequest.Post(urlServidor, formulario))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error de conexión: " + www.error);
            }
            else
            {
                string respuesta = www.downloadHandler.text;
                Debug.Log("Respuesta del servidor: " + respuesta);

                if (respuesta == "Exito")
                {
                    // Aquí podrías avisar al jugador que su nombre está reservado globalmente
                }
            }
        }
    }
}

