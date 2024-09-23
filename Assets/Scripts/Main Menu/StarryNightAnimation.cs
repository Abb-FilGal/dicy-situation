using UnityEngine;
using UnityEngine.UI;

public class StarryNightAnimation : MonoBehaviour
{
    public float animationSpeed = 1f; // Speed of the animation
    public Color targetColor = new Color(0.48f, 0.72f, 0.49f); // Target color (7AB77E in RGB)
    public float mouseSensitivity = 0.1f; // Sensitivity of the mouse movement

    private Color originalColor;
    private Image image;
    private Vector3 initialPosition;

    void Start()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            originalColor = image.color;
        }
        initialPosition = transform.position;
    }

    void Update()
    {
        AnimateStars();
        MoveWithMouse();
    }

    void AnimateStars()
    {
        if (image != null)
        {
            float t = (Mathf.Sin(Time.time * animationSpeed) + 1) / 2; // Normalized value between 0 and 1
            image.color = Color.Lerp(originalColor, targetColor, t);
        }
    }

    void MoveWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.position = initialPosition + new Vector3(mouseX, mouseY, 0);
    }
}