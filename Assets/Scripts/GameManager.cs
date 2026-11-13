using TMPro;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private TextMeshProUGUI scoreText;
    [SerializeField] private float score;

    public Transform startingPoint;
    public float lerpSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        score= 0;

        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            if (playerControllerScript.isDashing)
            {
                score += 2;
            }
            else
            {
                score++;
            }
            scoreText.text = "Score: " + score;
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;

        }

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerControllerScript.gameOver = false;

    }
}
