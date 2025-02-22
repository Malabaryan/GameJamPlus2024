using BNG;
using System.Collections;
using UnityEngine;

public class IcebergBehavior : MonoBehaviour
{
    [SerializeField] private GameObject mesh;
    [SerializeField] private GameObject smokeParticles;
    private bool melting = false;
    [SerializeField] private float scaleMultiplier = .5f;

    void Update()
    {
        if (!melting) return;

        mesh.transform.localScale = new Vector3(
            mesh.transform.localScale.x - Time.deltaTime * scaleMultiplier,
            mesh.transform.localScale.y - Time.deltaTime * scaleMultiplier,
            mesh.transform.localScale.z - Time.deltaTime * scaleMultiplier
        );
        if(mesh.transform.localScale.x < 0 )
        {
            mesh.SetActive( false );
            melting = false;
            StartCoroutine(HideSmoke());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Arrow")) {
            Arrow arrow = other.transform.GetComponent<Arrow>();
            if ( arrow.type == ArrowType.Fire)
            {
                melting = true;
                smokeParticles.SetActive( true );
            }
        }
    }

    IEnumerator HideSmoke()
    {
        yield return new WaitForSeconds(2f);
        foreach (ParticleSystem particles in smokeParticles.GetComponentsInChildren<ParticleSystem>()) { 
            particles.Stop();
        }
    }
}
