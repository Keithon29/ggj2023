using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text ScoreText;

    public void SetScore(int score, int PlayerIndex)
    {
        ScoreText.text = "プレイヤー" + PlayerIndex.ToString() + ": " + score.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}