using System.Collections.Generic;
using UnityEngine;

public class StarVisualizer : MonoBehaviour
{
  public CSVLoader csvLoader; // Assign in editor
  public GameObject starPrefab; // Assign your star prefab in editor


  void Start()
  {
    List<StarData> starList = csvLoader.LoadStarData();

    foreach (StarData starData in starList)
    {
      // Create a new game object from the prefab
      GameObject starObject = Instantiate(starPrefab, transform.position, transform.rotation);

      // Set star position based on X0, Y0, Z0
      starObject.transform.localPosition = new Vector3(starData.X0, starData.Y0, starData.Z0);

      // Adjust scale based on ABSMAG (assuming higher magnitude means smaller star)
      float starScale = Mathf.Clamp(1.0f - starData.ABSMAG, 0.1f, 1.0f); // Clamp scale between 0.1 and 1.0
      starObject.transform.localScale = Vector3.one * starScale;

      // Adjust material color based on SPECT (replace with your logic)
      MeshRenderer starRenderer = starObject.GetComponent<MeshRenderer>();
      if (starRenderer != null)
      {
        switch (starData.SPECT)
        {
          case "O": // Example: Set blue color for O-type stars
            starRenderer.material.color = Color.blue;
            Debug.Log("Max");
            break;
          case "B": // Example: Set white color for B-type stars
            starRenderer.material.color = Color.white;
            break;
          case "A": // Example: Set yellow color for A-type stars
            starRenderer.material.color = Color.yellow;
            break;
          case "F": // Example: Set light yellow color for F-type stars
            starRenderer.material.color = new Color(1f, 0.9f, 0.5f); // Light yellow
            break;
          case "G": // Example: Set orange color for G-type stars
            starRenderer.material.color = new Color(1f, 0.6f, 0f); // Orange
            break;
          case "K": // Example: Set reddish color for K-type stars
            starRenderer.material.color = Color.red;
            break;
          case "M": // Example: Set deep red color for M-type stars
            starRenderer.material.color = new Color(0.7f, 0f, 0f); // Deep red
            break;
          default:
            Debug.LogWarning($"Unknown spectral type: {starData.SPECT}");
            break;
          }

      }
      else
      {
        Debug.LogWarning("Star object missing renderer component!");
      }
    }
  }

}
