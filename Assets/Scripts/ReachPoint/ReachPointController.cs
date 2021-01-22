using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachPointController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space Clicked");
            if (!StageManager.Instance.IsCurrentStageLast())
            {
                Debug.Log("Ready to play next stage");
                StageManager.Instance.NextStage();
                SceneManager.LoadScene("Scenes/Levels/Level" + StageManager.Instance.GetCurrentStage());
            }

        }
    }
}
