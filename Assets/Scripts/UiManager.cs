using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text _score;

    // Start is called before the first frame update
    void Start()
    {

        _score.text = "Score: "; //+ score from player
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void UpdateScore(int playerScore)
    {
        _score.text = "Score: " + playerScore;
    }
}
