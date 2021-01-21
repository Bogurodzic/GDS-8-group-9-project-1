using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    private float _gameObjectWidth;
    private float _objectInitXPosition;
    void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _gameObjectWidth = GetGameObjectWidth();
        _objectInitXPosition = GetGameObjectXPosition();
    }

    void Update()
    {
    }
    
    private float GetGameObjectWidth()
    {
        return _boxCollider2D.bounds.max.x - _boxCollider2D.bounds.min.x;
    }


    private float GetGameObjectXPosition()
    {
        return transform.position.x;
    }

    public void SetGameObjectProgress(float progress)
    {
        Debug.Log("000SetGameObjectProgress: " + gameObject.name + " ::: " + _objectInitXPosition + " ::: " + _gameObjectWidth);

        Debug.Log("111SetGameObjectProgress: " + gameObject.name + " ::: " + progress);
        float newObjectPosition = _objectInitXPosition - (_gameObjectWidth * progress);
        Debug.Log("222SetGameObjectProgress: " + gameObject.name + " ::: " + newObjectPosition);
        transform.position = new Vector3(newObjectPosition, transform.position.y, transform.position.z);
    }

}
