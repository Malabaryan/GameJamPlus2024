using UnityEngine;

public class Lock : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float startPostion = 0;
    void Start()
    {
        startPostion = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, startPostion, transform.position.z);
    }
}
