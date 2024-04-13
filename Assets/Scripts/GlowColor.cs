using UnityEngine;

public class GlowColor : MonoBehaviour
{
    public Color startColor = Color.black; // Start color is black
    public Color endColor = Color.white; // End color is white
    public float duration = 2f; // Duration of the color transition in seconds

    private Renderer rend;
    private float elapsedTime = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Check if the object has a renderer component
        if (rend == null)
        {
            Debug.LogError("Renderer component not found!");
            enabled = false; // Disable the script if there is no renderer
        }
    }

    void Update()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate the interpolation factor based on elapsed time and duration
        float t = Mathf.Clamp01(elapsedTime / duration);

        // Lerp between start and end colors
        Color lerpedColor = Color.Lerp(startColor, endColor, t);

        // Change the albedo color of the material
        rend.material.color = lerpedColor;

        // Reset elapsed time when the transition is complete
        if (t >= 1f)
        {
            elapsedTime = 0f;
            // Swap start and end colors for next transition
            Color temp = startColor;
            startColor = endColor;
            endColor = temp;
        }
    }
}
