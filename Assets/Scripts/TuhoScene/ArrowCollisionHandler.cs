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
        {
            if (other.gameObject.name == "TargetPoint")
            {
                GameController.instance.throwArrow();
                GameController.instance.GetScore();
            }

            else if (other.gameObject.name == "Plane")
            {
                GameController.instance.throwArrow();
                gameObject.SetActive(false);
            }

            else 
            {
                //
            }
        }
    }
}