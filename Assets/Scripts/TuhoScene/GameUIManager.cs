using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 2;
    public GameObject observerUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.player1_turn == 1) {
            observerUI.transform.position = head.position + new Vector3(head.forward.x, -0.5f, head.forward.z).normalized * spawnDistance;

            observerUI.transform.LookAt(head.position);

            observerUI.transform.Rotate(0, 180, 0);
        }
    }
}
