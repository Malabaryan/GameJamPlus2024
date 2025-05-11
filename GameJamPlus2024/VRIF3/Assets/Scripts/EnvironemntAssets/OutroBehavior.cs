using UnityEngine;

public class OutroBehavior : MonoBehaviour
{
    [SerializeField] private Material globeMaterial;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool showMaterial = true;
    private bool showLogo = false;

    private void Start()
    {
        globeMaterial.color = new Color(1, 1, 1, 0f);
        spriteRenderer.color = new Color(1, 1, 1, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(globeMaterial.color != Color.white)
        {
            globeMaterial.color = globeMaterial.color + new Color(1, 1, 1, Time.deltaTime / 3f);
        }

        //Start logo animation
        if(globeMaterial.color.a > 0.95f) showLogo = true;

        if (showLogo) { 
            spriteRenderer.color = spriteRenderer.color + new Color(1, 1, 1, Time.deltaTime / 3f);
        }
    }
}
