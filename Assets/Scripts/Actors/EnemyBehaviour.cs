using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehaviour : MonoBehaviour {

    public float rotationSpeed;
    public PlayerLogics player;

    bool canShoot = true;
    float toShootCountdown;


    // Use this for initialization
    void Start () {
        canShoot = true;
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 targetDir = player.transform.position - transform.position;

        float targetAngle = Mathf.Atan(targetDir.y / targetDir.x) * Mathf.Rad2Deg + 180f;

        if(targetDir.x >= 0f)
            targetAngle += 180;
        
        transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.localEulerAngles, new Vector3(0f, 0f, targetAngle), rotationSpeed*Time.deltaTime));

        if (canShoot)
        {
            toShootCountdown = Random.Range(2f, 3f);
            canShoot = false;
        }

        if (toShootCountdown > 0f)
        {
            toShootCountdown -= Time.deltaTime * NewTimeScaleManager.instance.GetCustomTimeScale();
        }
        else
        {
            Debug.Log("Shoot");
            Shoot();
            canShoot = true;
        }
    }

    public void Spawn(Vector2 pos)
    {

    }

    private IEnumerator FadeIn()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = f;
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void Shoot()
    {

    }

}
