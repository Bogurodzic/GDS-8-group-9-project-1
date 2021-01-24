using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CautionController : MonoBehaviour
{
    // Start is called before the first frame update
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
        _textImage.sprite = textDanger;
        _airImage.sprite = airDanger;
        _landImage.sprite = landDanger;
        _obstacleImage.sprite = obstacleDanger;

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
}
