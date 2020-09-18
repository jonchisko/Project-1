using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerScript : MonoBehaviour
{

    private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            SceneManager.LoadScene("Game");
        }
    }


    private void OnEnable()
    {
        PlayerScript.playerLifeEvent += SetGameOver;
    }

    private void OnDisable()
    {
        PlayerScript.playerLifeEvent -= SetGameOver;
    }

    void SetGameOver(int life)
    {
        isGameOver = life == 0;
    }
}
