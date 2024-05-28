using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Text text_score;
    public Text text_highscore;
    int score = 0;
    int highscore = 0;

    int remained_arrows = 10;

    int player_turn = 0; // 0 = 1P, 1 = 2P

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (remained_arrows == 0) 
        {
            // 플레이어 턴 전환
            player_turn ^= 1;

            // 남은 화살 수 초기화
            remained_arrows = 10;
        }
    }

    public void throwArrow()
    {
        // 남은 화살 수 -1
        remained_arrows -= 1;
    }

    public void GetScore()
    {
        score += 1;
        SetScore();
    }

    public void SetScore()
    {
        SetText(text_score, score);
    }

    void SetText(Text txt, int score)
    {
        txt.text = score.ToString();
    }
}
