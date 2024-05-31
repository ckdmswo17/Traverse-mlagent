using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 1;
    public GameObject observerUI;

    public GameObject playerUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;

        playerUI.transform.LookAt(head.position);

        playerUI.transform.Rotate(0, 180, 0);

        // 관전 턴이면
        if(GameController.instance.player1_turn == 0) {
            observerUI.transform.position = head.position + new Vector3(head.forward.x, -0.5f, head.forward.z).normalized * spawnDistance;

            observerUI.transform.LookAt(head.position);

            observerUI.transform.Rotate(0, 180, 0);
        }
    }
}
