using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
            ReloadScene(1); // CurrentGameScene
    }
    public void GameOver()
    {
        _isGameOver = true;
    }

    private void ReloadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
