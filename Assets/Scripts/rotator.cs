using UnityEngine;

public class rotator : MonoBehaviour
{

    public Transform main;

    // Update is called once per frame
    void Update()
    {
        transform.position = main.position;
    }

    
}
