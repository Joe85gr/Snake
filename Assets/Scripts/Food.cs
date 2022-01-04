using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    public BoxCollider2D grid;

    private void Start()
    {
        GetRandomFoodPosition();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) GetRandomFoodPosition();
    }

    private void GetRandomFoodPosition()
    {
        var bounds = grid.bounds;

        var x = Random.Range(bounds.min.x, bounds.max.x);
        var y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
}
