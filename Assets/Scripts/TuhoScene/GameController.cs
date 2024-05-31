using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private int gameType = 1; // 0 = Single, 1 = Multi
    public int player1_turn = 1; // 1 = true = player
    public int player2_turn = 0; // 0 = false = observer

    private Transform Player1Pos;
    private Transform Player2Pos;
    private Transform Observer;
    Vector3 playPos = new Vector3(-2, 0, 0);
    Vector3 observeFirstPos = new Vector3(0, 0.0f, 4);
    Vector3 observeSecondPos = new Vector3(3, 0.0f, 0);

    public GameObject arrow;

    int remained_arrows = 10;

    float turn_time = 30f;

    public Text text_score;
    public Text text_highscore;
    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;

        Player1Pos = GameObject.Find("OVRCameraRigInteraction").transform;
        Player2Pos = GameObject.Find("OVRCameraRigInteraction").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 점수 세팅
        SetScore();
        SetHighScore();
        
        if(gameType == 1) 
        {
            // 턴 시간 
            Invoke("ChangeTurn", turn_time);
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (remained_arrows == 0) 
        {
            EndGame();
        }
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

    public void GetHighScore()
    {

    }

    public void SetHighScore()
    {
        SetText(text_highscore, highscore);
    }

    void SetText(Text txt, int score)
    {
        txt.text = score.ToString();
    }

    public void ThrowArrow()
    {
        // 남은 화살 수 -1
        remained_arrows -= 1;

        if(gameType == 0) 
        {
            // 화살 위치 초기화
            arrow.transform.position = new Vector3(-1.5f, 1.2f, 0);
        }

        else if(gameType == 1) 
        {
            // 화살 위치 초기화
            arrow.transform.position = new Vector3(-1.5f, 1.2f, 0);

            // 멀티라면 플레이어 턴 전환
            changeTurn();
        }
    }

    public void changeTurn() 
    {  
        player1_turn ^= 1;
        player2_turn ^= 1;

        // P1: 관전 , P2: 플레이
        if(player1_turn == 0 && player2_turn == 1) {
            // 플레이어 위치 이동
            Player2Pos.position = playPos;

            // 관전자 위치 이동
            MoveObservePos();
        }

        // P1: 플레이 , P2: 관전
        else if(player1_turn == 1 && player2_turn == 0) {
            // 플레이어 위치 이동
            Player1Pos.position = playPos;
            
            // 관전자 위치 이동;
            MoveObservePos();
        }

        Invoke("ChangeTurn", turn_time);
    }

    public void MoveObservePos()
    {
        if(player1_turn == 0 && player2_turn == 1) {
            Observer = Player1Pos;
        }
        else if(player1_turn == 1 && player2_turn == 0) {
            Observer = Player2Pos;
        }

        // 관전 위치1 이면 2로 이동
        if (Observer.position == observeFirstPos)
        {   
            Observer.position = observeSecondPos;
        } 

        // 관전 위치2 이면 1로 이동
        else if (Observer.position == observeSecondPos)
        { 
            Observer.position = observeFirstPos;
        }

        // 관전 위치가 아니라면 관전 위치1로 이동
        else 
        { 
            Observer.position = observeFirstPos;
        }
    }

    public void EndGame()
    {
        if(gameType == 0) 
        {
            highscore = score;
            SetHighScore();
        }
    }
}
