using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public GameObject snowman;
    public GameObject gift;
    public GameObject iceBlock;
    public GameObject parentOfTargets;
    public GameObject winSprite;
    public Text scoreText;
    public GameObject randomTarget;
    public Text timerText;
    public float timeLeft = 60f;
    public bool gameRunning = true;
    private int score = 0;
    public Text scorefinished;
    

    private void Start()
    {
        InvokeRepeating("Spawn",1f,1f);
    }

    private void Update()
    {
        //countdown: only while the round is running
        if (gameRunning)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Zeit: " + (int)timeLeft;

            if (timeLeft <= 0)
            {
                EndGame();
            }
        }

        //after the round: press R to start a new round (reloading the scene resets time and score)
        if (!gameRunning && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //every shoot (clicking left mouse button) produce a shooting sound.
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    //Spawn a target at a random position within a specified x and y range.
    //Instantiate (make a concrete GameObject, i.e., a clone from the given prefeb target) the target as a child
    //of the GameObject ParentTargets. In this case transform.localPosition instead of transform.position is important!!
    private void Spawn()
    {
        float randomX = Random.Range(-365f, 365f);
        float randomY = Random.Range(-202f,202f);

        Vector2 random2DPosition = new Vector2(randomX, randomY);

        //choose the target type with a random number from 0 to 99
        //  0 - 59 -> Snowman  (60 %)
        // 60 - 89 -> IceBlock (30 %)
        // 90 - 99 -> Gift     (10 %)
        int chance = Random.Range(0, 100);
        

        if (chance < 60)
        {
            randomTarget = snowman;
        }
        else if (chance < 90)
        {
            randomTarget = iceBlock;
        }
        else
        {
            randomTarget = gift;
        }

        GameObject newTarget = Instantiate(randomTarget, parentOfTargets.transform);
        newTarget.transform.localPosition = random2DPosition;
    }

    //is called in the class Target, points can also be negative
    //the old win condition (10 hits / winScore) was replaced by the 60 second time limit
    public void AddScore(int points)
    {
        //after the end of the round no more points are counted
        if (!gameRunning)
        {
            return;
        }

        score += points;
        print(score);

        scoreText.text = score.ToString();
    }

    //is called when the time is over
    private void EndGame()
    {
        gameRunning = false;
        timeLeft = 0;
        CancelInvoke("Spawn");

        //remove all targets 
        foreach (Transform child in parentOfTargets.transform)
        {
            Destroy(child.gameObject);
        }
        winSprite.SetActive(true);
        scorefinished.text = "Dein Score ist: " + score;
        
    }
}
