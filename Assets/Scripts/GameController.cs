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

    private Transform Player1;
    private Transform Player2;
    private Transform Observer;
    Vector3 playPos = new Vector3(-2, 0, 0);
    Vector3 observeFirstPos = new Vector3(0, 0.0f, 4);
    Vector3 observeSecondPos = new Vector3(2, 0.0f, 0);

    public GameObject arrow;

    public Text text_remainingArrows;
    int remaining_arrows = 10;

    public Text text_turnTime;
    float turn_time = 30f;

    
    public GameObject SingleScoreBoard;
    public GameObject MultiScoreBoard;
    public Text text_score;
    public Text text_highscore;
    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;

        Player1 = GameObject.Find("OVRCameraRigInteraction").transform;
        Player2 = GameObject.Find("Player2").transform;
        // Player2 = GameObject.Find("OVRCameraRigInteraction_2").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 점수 세팅
        SetScore();
        SetHighScore();

        if (gameType == 0) 
        {
            // 싱글 점수판 표시
            SingleScoreBoard.SetActive(true);
            MultiScoreBoard.SetActive(false);
        }
        else if(gameType == 1) 
        {
            // 멀티 점수판 표시
            SingleScoreBoard.SetActive(false);
            MultiScoreBoard.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (turn_time > 0)
        {   // 턴 시간을 1초마다 감소
            turn_time -= Time.deltaTime;
            SetText(text_turnTime, (int)turn_time);
        }

        else
        {   // 턴 시간을 다시 초기화하고 턴을 변경
            turn_time = 30f;
            ChangeTurn();
        }

        if (remaining_arrows == 0) 
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

    void SetText(Text txt, int val)
    {
        txt.text = val.ToString();
    }

    public void ThrowArrow()
    {
        // 남은 화살 수 -1
        remaining_arrows -= 1;
        SetText(text_remainingArrows, remaining_arrows);

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
            ChangeTurn();
        }
    }

    public void ChangeTurn() 
    {  
        // 턴 바꾸기
        player1_turn ^= 1;
        player2_turn ^= 1;

        // 디버그 로그 추가
        Debug.Log("Changing turn: Player1_turn = " + player1_turn + ", Player2_turn = " + player2_turn);

        // P1: 관전 , P2: 플레이
        if (player1_turn == 0 && player2_turn == 1)
        {
            // 플레이어2 위치 이동
            Debug.Log("Moving Player2 to play position: " + playPos);
            Player2.position = playPos;

            // 관전자(플레이어1) 위치 이동
            Debug.Log("Moving Player1 to observe position: " + observeFirstPos);
            Player1.position = observeFirstPos;
        }
        // P1: 플레이 , P2: 관전
        else if (player1_turn == 1 && player2_turn == 0)
        {
            // 플레이어1 위치 이동
            Debug.Log("Moving Player1 to play position: " + playPos);
            Player1.position = playPos;

            // 관전자(플레이어2) 위치 이동
            Debug.Log("Moving Player2 to observe position: " + observeFirstPos);
            Player2.position = observeFirstPos;
        }
    }

    private void ManageAudioListeners()
    {
        AudioListener player1AudioListener = Player1.GetComponent<AudioListener>();
        AudioListener player2AudioListener = Player2.GetComponent<AudioListener>();

        // 플레이어1이 현재 플레이어인 경우
        if (player1_turn == 1)
        {
            if (player1AudioListener == null)
            {
                player1AudioListener = Player1.gameObject.AddComponent<AudioListener>();
            }
            player1AudioListener.enabled = true;

            if (player2AudioListener != null)
            {
                player2AudioListener.enabled = false;
            }
        }
        // 플레이어2가 현재 플레이어인 경우
        else if (player2_turn == 1)
        {
            if (player2AudioListener == null)
            {
                player2AudioListener = Player2.gameObject.AddComponent<AudioListener>();
            }
            player2AudioListener.enabled = true;

            if (player1AudioListener != null)
            {
                player1AudioListener.enabled = false;
            }
        }
    }


    public void MoveObservePos()
    {
        if(player1_turn == 0) {
            Observer = Player1;
        }
        else if(player2_turn == 0) {
            Observer = Player2;
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
        // else 
        // { 
        //     Observer.position = observeFirstPos;
        // }
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
