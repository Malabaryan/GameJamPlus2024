using UnityEngine;
using BNG;

public class BouqueteBehavior : MonoBehaviour
{
    [SerializeField] SnapZone flower1;
    [SerializeField] SnapZone flower2;
    [SerializeField] SnapZone flower3;
    [SerializeField] GameObject finalMessage;

    // Update is called once per frame
    void Update()
    {
        if (flower1 != null && flower2 != null && flower3 != null)
        {
            finalMessage.SetActive(flower1.HeldItem != null && flower2.HeldItem != null && flower3.HeldItem != null);
        }
    }
}
