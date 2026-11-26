using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager gameManager;

    //points for hitting this target, set in the Inspector of each prefab (can be negative)
    public int points = 10;

    //makes sure that a target is only counted once
    private bool alreadyHit = false;

    private void Start()
    {
        //create a reference to the GameManager script
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(gameObject, 2f);
    }

    private void OnMouseDown()
    {
        //if this target was already hit, do nothing
        if (alreadyHit)
        {
            return;
        }
        alreadyHit = true;

        //give the points of this target to the GameManager
        gameManager.AddScore(points);
        Destroy(gameObject);
    }
}
