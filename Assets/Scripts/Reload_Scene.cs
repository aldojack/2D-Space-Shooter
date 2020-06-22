using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload_Scene : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;

    // Start is called before the first frame update

    void Update()
    {
        RestartLevel(_isGameOver);
    }

    public void RestartLevel(bool GameOver)
    {
        _isGameOver = GameOver;

        if (Input.GetKeyDown(KeyCode.R) && GameOver == true)
        {
            SceneManager.LoadScene("Game");
            _isGameOver = false;
        }

    }

}
