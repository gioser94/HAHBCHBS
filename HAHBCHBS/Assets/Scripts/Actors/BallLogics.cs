using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallLogics : MonoBehaviour {

    RaycastHit2D hit = new RaycastHit2D();
    CustomPhysics2D ballRB;
    public PlayerLogics player;

    public GameObject arrow;

    private bool canRaycast = true;


    private void Awake()
    {
        ballRB = GetComponent<CustomPhysics2D>();
    }


    private void FixedUpdate()
    {
        if (canRaycast)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(ballRB.GetDirection()), Mathf.Sin(ballRB.GetDirection())));

            if (hit.collider.CompareTag("Player") && hit.normal == Vector2.up)
            {
                player.DrawLineTowardsAngle(PredictBounceOnPlayer(), hit.point);
            }
            else
            {
                player.DrawLineTowardsAngle(0f, Vector2.zero);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("DeathWall"))
        {
            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            PlayerLogics.lives--;
            if (gameManager.CheckGameOver())
            {
                //TODO LOAD GAME OVER SCREEN
                Debug.Log("Game Over");
                SceneManager.LoadScene(2);
            }
            GameObject.FindObjectOfType<GameManager>().ResetGame();
        }

        if (!canRaycast)
            return;

        canRaycast = false;

        Debug.Log(collision.gameObject.name);


        if (collision.CompareTag("Player"))
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down);
            Debug.Log(hit.collider.name);
        }

        float normalAngle = 0f;


        normalAngle = Mathf.Atan(hit.normal.y / hit.normal.x);

        if (hit.normal == Vector2.up && collision.CompareTag("Player"))
        {
            
            //Debug.Log(hit.point.x);
            normalAngle += collision.GetComponent<PlayerLogics>().AdjustedNormalAngle(hit.point.x);

        }


        float newSlope = BounceSlope(normalAngle);

        if (hit.normal == Vector2.up && collision.CompareTag("Player"))
        {
            Debug.Log(CustomPhysics2D.ClampAngleZeroToPi(newSlope));
            if (CustomPhysics2D.ClampAngleZeroToPi(newSlope) > Mathf.PI * 0.8f)
                newSlope = Mathf.PI * 0.9f;
            else if (CustomPhysics2D.ClampAngleZeroToPi(newSlope) < Mathf.PI * 0.2f)
                newSlope = Mathf.PI * 0.1f;

            Debug.DrawRay(hit.point, new Vector3(Mathf.Cos(newSlope), Mathf.Sin(newSlope), 0f), Color.white, 2f);

        }

        newSlope %= Mathf.PI*2;

        Debug.Log(hit.collider.gameObject.name + " _ Normal: " + hit.normal);

        ballRB.SetDirection(newSlope);

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<GameplayObject>().ChekcDeath();
            if (GameManager.CheckWin())
            {
                //TODO LOAD WIN SCREEN
                Debug.Log("You Win");
                SceneManager.LoadScene(3);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canRaycast = true;
    }

    public float BounceSlope(float normal)
    {
        float angularDistance = Mathf.PI + ballRB.GetDirection() - normal;
        return normal - angularDistance;
    }

    public void ResetGame()
    {
        ballRB.ChangeMagnitude(0f);
        ballRB.SetDirection(Mathf.PI / 2f);
        transform.SetParent(GameObject.FindObjectOfType<PlayerLogics>().transform);
        transform.localPosition = new Vector3(0f, 1.5f, 0f);
    }

    public void BallInGame()
    {
        transform.SetParent(null);
        ballRB.ChangeMagnitude(10f);
    }

    private float PredictBounceOnPlayer()
    {
        float normalAngle = 0f;
        normalAngle = Mathf.Atan(hit.normal.y / hit.normal.x);
        normalAngle += player.AdjustedNormalAngle(hit.point.x);

        float newSlope = BounceSlope(normalAngle);

        if (CustomPhysics2D.ClampAngleZeroToPi(newSlope) > Mathf.PI * 0.8f)
            newSlope = Mathf.PI * 0.9f;
        else if (CustomPhysics2D.ClampAngleZeroToPi(newSlope) < Mathf.PI * 0.2f)
            newSlope = Mathf.PI * 0.1f;

        newSlope %= Mathf.PI * 2;

        return newSlope;
    }

}
