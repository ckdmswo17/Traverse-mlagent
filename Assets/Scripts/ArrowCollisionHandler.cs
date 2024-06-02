using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollisionHandler : MonoBehaviour
{

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name); // 디버그 로그 추가
        
        if (other.gameObject.name == "TargetPoint")
        {
            GameController.instance.ThrowArrow();
            GameController.instance.GetScore();
        }

        else if (other.gameObject.name == "Plane")
        {
            GameController.instance.ThrowArrow();
        }
    }
}