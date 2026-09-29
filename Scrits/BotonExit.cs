using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonExit : MonoBehaviour
{
    public bool isExit;
    private void Start()
    {
    }
    private void Update()
    {
    }

    public void CambioScena()
    {
        if (isExit)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
        }
        else
        {
            SceneManager.LoadScene("StartScene");
        }

    }
}
