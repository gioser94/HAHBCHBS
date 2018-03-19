using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogics : MonoBehaviour {

    Collider2D coll;
    CustomPhysics2D customRB2D;
    [Range(0, 90)] public float maxAngleAdjustment;
    [Range(10f, 20f)] public float playerSpeed;
    [Range(1f, 5f)] public float lerpRaySpeed;

    private LineRenderer lineRenderer;



    public static int lives = 3;

	void Awake () {
        coll = GetComponent<Collider2D>();
        customRB2D = GetComponent<CustomPhysics2D>();
        customRB2D.ChangeMagnitude(playerSpeed);
        lineRenderer = GetComponentInChildren<LineRenderer>();
	}
	

	void Update () {
		
	}

    public void SetMoving(bool b, int dir)
    {
        if (!b)
        {
            customRB2D.ChangeMagnitude(0f);
        }
        else
        {
            customRB2D.ChangeMagnitude(playerSpeed);
        }
        customRB2D.SetDirection(Mathf.PI*dir);

    }

    public float ReturnAdjustmentAngle(float pointXPos, float bounceAngle)
    {
        //Debug.Log((transform.position.x - pointXPos) / coll.bounds.size.x);

        float angle = (Mathf.PI/2) *((transform.position.x - pointXPos)/coll.bounds.extents.x);
        return angle;
    }

    public float AdjustedNormalAngle(float posX)
    {

        float angle = ((transform.position.x - posX) / coll.bounds.extents.x) * maxAngleAdjustment*Mathf.Deg2Rad;
        Debug.Log(transform.position.x + " - " + posX + " / " + coll.bounds.extents.x + " = " + (transform.position.x - posX) / coll.bounds.extents.x);
        return angle;
    }

    public void ResetGame()
    {
        transform.position = new Vector3(0f, transform.position.y, 0f);
    }

    public void DrawLineTowardsAngle(float angle, Vector2 applicationPoint)
    {
        if (angle == 0 && applicationPoint == Vector2.zero)
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);

            return;
        }

        lineRenderer.transform.position = new Vector3(applicationPoint.x, applicationPoint.y, -3f);

        Vector3 lerpedPointPos = Vector3.Lerp(lineRenderer.GetPosition(1), new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f), Time.deltaTime * lerpRaySpeed);

        lineRenderer.SetPosition(1, lerpedPointPos);


        
    }

}
