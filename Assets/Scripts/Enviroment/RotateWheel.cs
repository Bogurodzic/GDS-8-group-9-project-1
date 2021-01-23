using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public float slowRotationSpeed;
    public float normalRotationSpeed;
    public float fastRotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0,   GetRotationSpeed() * Time.deltaTime));
    }

    private float GetRotationSpeed()
    {
        if (GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Fast)
        {
            return fastRotationSpeed * -1;
        } else if (GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Normal)
        {
            return normalRotationSpeed * -1;

        } else if (GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Slow)
        {
            return slowRotationSpeed * -1;
        }
        else
        {
            return normalRotationSpeed * -1;
        }
    }

}
