using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    public float backgroundSpeed;
    private float _startingHorizontalPosition;
    
    private BoxCollider _boxCollider;
    void Start()
    {
        LoadComponents();
        _startingHorizontalPosition = transform.position.x;
    }
    void Update()
    {
        if (GameManager.Instance.IsGameRunning())
        {
            MoveBackground();
            HandleRepeatingBackground();
        }
    }
    

    private void LoadComponents()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void MoveBackground()
    {
        transform.Translate(Vector3.left * backgroundSpeed * Time.deltaTime);
    }

    private void HandleRepeatingBackground()
    {
        if (transform.position.x < (_boxCollider.size.x / 2) * -1)
        {
            transform.position = new Vector3(_startingHorizontalPosition, transform.position.y, transform.position.z);
        }
    }
}
