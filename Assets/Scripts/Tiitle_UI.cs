using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tiitle_UI : MonoBehaviour
{
    [SerializeField]
    GameObject matchingPanel;

    [SerializeField]
    GameObject characterPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void SetMatchingPanel(bool state)
    {
        matchingPanel.SetActive(state);
    }

    public void SetCharacterPanel(bool state)
    {
        characterPanel.SetActive(state);
    }

}
