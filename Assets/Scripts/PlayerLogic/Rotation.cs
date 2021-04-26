using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Rotate(Vector3.back * (speed * Time.deltaTime));
    }
}
