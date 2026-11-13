using System.Collections;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private float leftBound = -15;
    private PlayerController playerControllerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            if (playerControllerScript.isDashing)
            {
                transform.Translate(Vector3.left * Time.deltaTime * (speed * playerControllerScript.dashSpeedModifier));
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            
        }
        

        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

   
}
