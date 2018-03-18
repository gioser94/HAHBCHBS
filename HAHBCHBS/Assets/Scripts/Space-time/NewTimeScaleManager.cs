using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTimeScaleManager : MonoBehaviour {

    float _customDTime;
    float _customFixedDTime;
    float customTimeScale = 1;

    public float customDTime
    {
        get
        {
            return _customDTime;
        }
    }
    public float customFixedDTime
    {
        get
        {
            return _customFixedDTime;
        }
    }

    public static NewTimeScaleManager instance;

	void Awake () {
        instance = this;
	}

	void FixedUpdate () {
        _customFixedDTime = Time.fixedDeltaTime * customTimeScale;
	}

    void Update()
    {
        _customDTime = Time.deltaTime * customTimeScale;
    }

    public void SetCustomTimeScale(float newTimeScale)
    {
        customTimeScale = newTimeScale;
    }

    public float GetCustomTimeScale()
    {
        return customTimeScale;
    }



}
