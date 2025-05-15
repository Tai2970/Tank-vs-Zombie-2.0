using UnityEngine;

public class GhostFadeIn : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    private Material ghostMat;
    private Color originalColor;
    private float fadeTimer;

    void Start()
    {
        // Get material from the ghost's Renderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            ghostMat = renderer.material;
            originalColor = ghostMat.color;
            ghostMat.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // fully transparent
        }

        fadeTimer = 0f;
    }

    void Update()
    {
        if (ghostMat == null) return;

        fadeTimer += Time.deltaTime;
        float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);
        ghostMat.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        // Once faded in, destroy this script
        if (alpha >= 1f)
        {
            Destroy(this);
        }
    }
}
