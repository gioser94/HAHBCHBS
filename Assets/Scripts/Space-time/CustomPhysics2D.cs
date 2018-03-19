using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics2D : MonoBehaviour {

    public Vector2 velocity = Vector2.zero;
    float slope;
    public float targetMagnitude;

    // Update is called once per frame
    void FixedUpdate()
    {
        float timeNormalizedMagnitude = targetMagnitude * NewTimeScaleManager.instance.GetCustomTimeScale();
        

        velocity = CurrentDirections(timeNormalizedMagnitude, slope);
        MoveUpdate();
    }


    private Vector2 CurrentDirections(float magn, float slope)
    {
        Vector2 dir = new Vector2(magn * Mathf.Cos(slope), magn * Mathf.Sin(slope));

        return dir;
    }

    private void MoveUpdate()
    {
        
        transform.position += new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0f);
    }

    public void SetDirection(float radAngle)
    {
        slope = radAngle;
    }

    public float GetDirection()
    {
        return slope;
    }

    public void ChangeMagnitude(float newMagnitude)
    {
        targetMagnitude = newMagnitude;
    }

    public static float ClampAngleZeroToPi(float angle)
    {

        angle %= Mathf.PI * 2;


        if (angle < 0)
            angle += Mathf.PI*2f;

        if (angle > Mathf.PI && angle <= Mathf.PI * 1.5f)
            angle = Mathf.PI;
        else if (angle > Mathf.PI * 1.5f && angle < Mathf.PI * 2f)
            angle = 0f;

        //Debug.Log("ClampedAngle = " + angle * Mathf.Rad2Deg);

        return angle; 
    }
}
