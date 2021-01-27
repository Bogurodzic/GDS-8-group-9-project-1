using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingInHoleController : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    void Start()
    {
        LoadComponents();
    }

    void Update()
    {
        if (!GameManager.Instance.GetPlayerFallenInHole())
        {
            TurnOnBoxCollider();
        } else if (GameManager.Instance.GetPlayerFallenInHole())
        {
            TurnOffBoxCollider();
        }
    }

    private void LoadComponents()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void TurnOffBoxCollider()
    {
        _boxCollider2D.enabled = false;
    }

    private void TurnOnBoxCollider()
    {
        _boxCollider2D.enabled = true;
    }
}
