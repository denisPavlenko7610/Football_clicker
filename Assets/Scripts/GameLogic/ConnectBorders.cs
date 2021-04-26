using UnityEngine;

public class ConnectBorders : MonoBehaviour
{
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private GameObject bottomBorder;

    private void Start()
    {
        Camera mainCamera = Camera.main;
        float dist = (transform.position - mainCamera.transform.position).z;
        float leftScreenBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightScreenBorder = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float bottomScreenBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        leftBorder.transform.position = new Vector3(leftScreenBorder, transform.position.y, transform.position.z);
        rightBorder.transform.position = new Vector3(rightScreenBorder, transform.position.y, transform.position.z);
        bottomBorder.transform.position =
            new Vector3(transform.position.x, bottomScreenBorder - 2.5f, transform.position.z);
    }
}