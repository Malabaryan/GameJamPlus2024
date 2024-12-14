using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButtonBehavior : MonoBehaviour
{
    [SerializeField] private float resetDelay = 1f;

    public void Reset()
    {
        StartCoroutine(ResetSceneDelayed());
    }

    private IEnumerator ResetSceneDelayed()
    {
        yield return new WaitForSeconds(resetDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
