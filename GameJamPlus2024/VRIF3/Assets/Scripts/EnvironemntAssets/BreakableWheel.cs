using UnityEngine;
using UnityEngine.Events;

public class BreakableWheel : MonoBehaviour
{
    public GameObject confettiPrefab;

    [SerializeField] private bool[] destroyedTargets;

    public UnityEvent onDestroyed;

    public void DestroyTarget(int index)
    {
        destroyedTargets[index] = true;
        checkAllTargets();
    }

    public void RespawnTarget(int index)
    {
        destroyedTargets[index] = false;
        checkAllTargets();
    }

    private void checkAllTargets()
    {
        for (int i = 0; i < destroyedTargets.Length; i++)
        {
            if (destroyedTargets[i] == false) return;
        }
        //if it gets here, all targets were destroyed
        GameObject confetti = Instantiate(confettiPrefab, transform.position, Quaternion.identity);
        Destroy(confetti, 5f);
        onDestroyed.Invoke();
    }
}
