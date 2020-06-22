using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text _score;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restart;

    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprite;

    // Start is called before the first frame update
    void Start()
    {

        _score.text = "Score: " + 0; //+ score from player
        _gameOver.gameObject.SetActive(false);
        _restart.gameObject.SetActive(false);
        
    }


    public void UpdateScore(int playerScore)
    {
        _score.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives (int currentLives)
    {

        _LivesImg.sprite = _livesSprite[currentLives];
        if (currentLives == 0)
        {
            _gameOver.gameObject.SetActive(true);
            _restart.gameObject.SetActive(true);
            StartCoroutine(TextFlicker());
        }
    }

    IEnumerator TextFlicker()
    {
        while (true)
        {
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
