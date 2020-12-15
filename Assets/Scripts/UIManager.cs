using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _liveSprite;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Text _gameOverText;
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int lives)
    {
        _LivesImg.sprite = _liveSprite[lives];
    }

    public void ShowGameOver()
    {
        _gameOverText.gameObject.SetActive(true);
    }
}
