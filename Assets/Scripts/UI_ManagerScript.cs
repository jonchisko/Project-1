using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_ManagerScript : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private GameObject _gameOverObject;
    [SerializeField]
    private GameObject _restartLevelObject;
    [SerializeField]
    private Sprite[] _lifeSprites;

    private Image _lifeImage;

    // Start is called before the first frame update
    void Start()
    {
        // turn off the gameover
        _gameOverObject.SetActive(false);
        _restartLevelObject.SetActive(false);
        // life sprite
        _lifeImage = GameObject.Find("LivesUI")?.GetComponent<Image>();
        _lifeImage.sprite = _lifeSprites[3];
        // score
        SetScoreText(0);
    }

    // Update is called once per frame
    void Update()
    {
    }


    void SetScoreText(int amount)
    {
        _scoreText.text = string.Format("Score:{0, 10}", amount);
    }

    void SetLifeSprite(int life)
    {
        _lifeImage.sprite = _lifeSprites[life];
        if(life == 0)
        {
            _gameOverObject.SetActive(true);
            _restartLevelObject.SetActive(true);
        }
    }
    private void OnEnable()
    {
        PlayerScript.playerScoreEvent += SetScoreText;
        PlayerScript.playerLifeEvent += SetLifeSprite;
    }

    private void OnDisable()
    {
        PlayerScript.playerScoreEvent -= SetScoreText;
        PlayerScript.playerLifeEvent -= SetLifeSprite;
    }

}
