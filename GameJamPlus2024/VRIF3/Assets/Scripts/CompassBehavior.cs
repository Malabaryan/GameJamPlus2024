using UnityEngine;

public class CompassBehavior : MonoBehaviour
{
    [SerializeField] private Transform northTransfrom;
    [SerializeField] private Transform needle;
    [SerializeField] private float maxShakingAngle;
    [SerializeField] private float shakingSpeed;

    private bool increasing = true;
    private float angle = 0f;

    void Start()
    {
        //North have to be always the same, unparent to read its angles permanently
        northTransfrom.SetParent(null);
    }

    void FixedUpdate()
    {
        if(increasing && needle.eulerAngles.y > maxShakingAngle && needle.eulerAngles.y < 360 - maxShakingAngle - 5f) increasing = false;
        if (!increasing && needle.eulerAngles.y < 360-maxShakingAngle && needle.eulerAngles.y > 360-maxShakingAngle-5f) increasing = true;
        Debug.Log(needle.eulerAngles);

        angle += (increasing ? 1 : -1) * shakingSpeed * Time.fixedDeltaTime;
        needle.transform.eulerAngles = northTransfrom.eulerAngles + new Vector3(0f, angle, 0f);
    }
}
