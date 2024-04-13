using System.Collections.Generic;
using UnityEngine;

public class StarVisualizer1 : MonoBehaviour
{
  public CSVLoader csvLoader; // Assign in editor
  public GameObject starPrefab; // Assign your star prefab in editor

  void Start(){
    List<StarData> starList = csvLoader.LoadStarData();

    foreach (StarData starData in starList)
    {
      // Create a new game object from the prefab
      GameObject starObject = Instantiate(starPrefab, transform.position, transform.rotation);

      // Set star position based on X0, Y0, Z0
      starObject.transform.localPosition = new Vector3(starData.X0, starData.Y0, starData.Z0);

      // Adjust scale based on ABSMAG (assuming higher magnitude means smaller star)
      float starScale = Mathf.Clamp(1.0f - starData.ABSMAG, 0.1f, 1.0f); // Clamp scale between 0.1 and 1.0
      starObject.transform.localScale = (Vector3.one * starScale) / 2;
    }
  }

}
