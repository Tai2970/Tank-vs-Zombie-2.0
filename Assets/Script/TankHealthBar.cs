using UnityEngine;
using UnityEngine.UI;

public class TankHealthBar : MonoBehaviour
{
    public Slider slider;          // Link to the slider in the prefab
    public Transform target;       // The tank to follow
    public Vector3 offset = new Vector3(0, 2f, 0);

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.rotation = Camera.main.transform.rotation; // Face camera
        }
    }

    public void SetHealth(float current, float max)
    {
        slider.value = current / (float)max;
    }
}
