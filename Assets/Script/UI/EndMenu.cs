using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EndMenuObject;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void ShowMenu()
    {
        EndMenuObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void OnRetry()
    {
        Debug.Log("retry");
        GameObject.Find("GameManagement").GetComponent<GameManager>().Reset();
        SceneManager.LoadSceneAsync("MainScene");
    }
    public void OnMenuBtn()
    {
        Debug.Log("menu");
        SceneManager.LoadScene("StartScene");
    }

    public void OnQuitBtn()
    {
        Debug.Log("quit");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                            Application.Quit();
        #endif
    }
}
