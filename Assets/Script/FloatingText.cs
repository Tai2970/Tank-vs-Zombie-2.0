using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 50f;
    public float fadeDuration = 1.0f;
    private TextMeshProUGUI text;
    private Color startColor;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        startColor = text.color;

        // Ignore raycast layer (cannot be detected by collision)
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    void Update()
    {
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

        Color newColor = text.color;
        newColor.a -= Time.deltaTime / fadeDuration;
        text.color = newColor;

        if (text.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
