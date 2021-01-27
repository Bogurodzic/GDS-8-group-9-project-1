using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CautionController : MonoBehaviour
{
    public float dangerActiveTime;
    
    public GameObject textPrefab;
    public GameObject airPrefab;
    public GameObject landPrefab;
    public GameObject obstaclePrefab;

    public Sprite textNormal;
    public Sprite textDanger;

    public Sprite airNormal;
    public Sprite airDanger;

    public Sprite landNormal;
    public Sprite landDanger;

    public Sprite obstacleNormal;
    public Sprite obstacleDanger;


    private Image _textImage;
    private Image _airImage;
    private Image _landImage;
    private Image _obstacleImage;
    void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _textImage = textPrefab.GetComponent<Image>();
        _airImage = airPrefab.GetComponent<Image>();
        _landImage = landPrefab.GetComponent<Image>();
        _obstacleImage = obstaclePrefab.GetComponent<Image>();

    }
    void Update()
    {
    }

    public void ActiveAirFlag()
    {
        AlertText();
        _airImage.sprite = airDanger;
        Invoke("ResetFlags", dangerActiveTime);
    }
    
    public void ActiveLandFlag()
    {
        AlertText();
        _landImage.sprite = landDanger;
        Invoke("ResetFlags", dangerActiveTime);
    }
    
    public void ActiveObstacleFlag()
    {
        AlertText();
        _obstacleImage.sprite = obstacleDanger;
        Invoke("ResetFlags", dangerActiveTime);
    }

    public void AlertText()
    {
        ActivateDangerText();
        Invoke("DectivateDangerText", 0.5f);
        Invoke("ActivateDangerText", 1f);
        Invoke("DectivateDangerText", 1.5f);
        Invoke("ActivateDangerText", 2f);
        Invoke("DectivateDangerText", 2.5f);

    }

    public void ActivateDangerText()
    {
        _textImage.sprite = textDanger;
    }
    
    public void DectivateDangerText()
    {
        _textImage.sprite = textNormal;
    }
    


    public void ResetFlags()
    {
        _textImage.sprite = textNormal;
        _airImage.sprite = airNormal;
        _landImage.sprite = landNormal;
        _obstacleImage.sprite = obstacleNormal;

    }
}
