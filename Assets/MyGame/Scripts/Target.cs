using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager gameManager;

    //points for hitting this target, set in the Inspector of each prefab (can be negative)
    public int points = 10;

    //makes sure that a target is only counted once
    private bool alreadyHit = false;

    //movement: speed in units per second, direction 1 = right, -1 = left
    public float speed = 150f;
    private float direction = 1f;

    //how long the target stays before it disappears (in seconds)
    public float lifeTime = 3f;

    private void Start()
    {
        //create a reference to the GameManager script
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        //move the target sideways. Time.deltaTime makes the speed independent of the frame rate
        transform.localPosition += new Vector3(speed * direction * Time.deltaTime, 0f, 0f);

        //at the edge of the playing area (same range as in Spawn()) change the direction
        if (transform.localPosition.x > 365f)
        {
            direction = -1f;
        }
        else if (transform.localPosition.x < -365f)
        {
            direction = 1f;
        }
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
