using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
               
    }

    public void PlayGameButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void PlayGameCoopButton()
    {
        SceneManager.LoadScene("CoopGame");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

}
