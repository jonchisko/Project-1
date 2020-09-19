using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    private bool _isGameOver;
    [SerializeField]
    private bool _isCoop = false;
    [SerializeField]
    private GameObject _pausePanel;
    private Animator _pauseAnimator;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        _isGameOver = false;
        if(_pausePanel == null)
        {
            Debug.LogError("GameManagerScript::Start() -> _pausePanel is null.");
        }
        _pauseAnimator = _pausePanel.GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            if (!_isCoop)
            {
                SceneManager.LoadScene("Game");
            }
            else
            {
                SceneManager.LoadScene("CoopGame");
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseAnimator.SetBool("PauseEnabled", true);
            //_pausePanel.SetActive(true);
            Time.timeScale = 0;
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
        _isGameOver = life == 0;
    }

    public bool IsCoop()
    {
        return _isCoop;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void ContinueGame()
    {
        _pauseAnimator.SetBool("PauseEnabled", false);
        Time.timeScale = 1;
    }

}
