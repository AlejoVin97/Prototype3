using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] obstaclePrefab;
    private Vector3[] spawnPos = { new Vector3(22, 0, 0), new Vector3(20, 0, 0), new Vector3(16, 0, 0) };
    private float startDelay = 2;
    private float[] repeatRate = {2.0f, 1.5f, 0.8f};
    private PlayerController playerControllerScript;
    private int itemInst;
    private int spawnInst;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate[0]);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        itemInst = Random.Range(0, obstaclePrefab.Length);
        spawnInst = Random.Range(0, spawnPos.Length);
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab[itemInst], spawnPos[spawnInst], obstaclePrefab[itemInst].transform.rotation);
        }
        
    }

  
}   

