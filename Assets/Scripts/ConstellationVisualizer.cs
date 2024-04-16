using UnityEngine;
using System.Collections.Generic;

public class ConstellationVisualizer : MonoBehaviour
{
    public ConstellationParser constellationParser; // Reference to the constellation parser component
    public GameObject starPrefab; // Prefab for representing stars
    public Transform starsParent; // Parent object for stars

    void Start()
    {
        // Parse constellation data
        List<ConstellationParser.StarPair> starPairs = constellationParser.ParseConstellationFile("Assets/modern/constellationship.fab");

        // Create stars and constellations
        CreateStars(starPairs);
    }

    void CreateStars(List<ConstellationParser.StarPair> starPairs)
    {
        foreach (ConstellationParser.StarPair starPair in starPairs)
        {
            // Instantiate star prefabs
            GameObject star1 = Instantiate(starPrefab, GetStarPosition(starPair.Star1HipparcosID), Quaternion.identity, starsParent);
            GameObject star2 = Instantiate(starPrefab, GetStarPosition(starPair.Star2HipparcosID), Quaternion.identity, starsParent);

            // Connect stars with a line renderer
            DrawLineBetweenStars(star1.transform.position, star2.transform.position);
        }
    }

    Vector3 GetStarPosition(int hipparcosID)
    {
        // Here you would implement logic to retrieve the position of the star based on its Hipparcos ID.
        // For the sake of this example, let's just return a random position.
        return Random.insideUnitSphere * 10f;
    }

    void DrawLineBetweenStars(Vector3 startPos, Vector3 endPos)
    {
        GameObject lineObject = new GameObject("ConstellationLine");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);

        // Customize line renderer properties as needed (material, width, etc.)
        // For example:
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }
}
