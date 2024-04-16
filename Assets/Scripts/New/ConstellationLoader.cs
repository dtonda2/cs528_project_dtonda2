using UnityEngine;
using System.Collections.Generic;

public class ConstellationLoader : MonoBehaviour
{
    public ConstellationParser constellationParser; // Reference to the constellation parser component

    public List<ConstellationParser.StarPair> starPairs; // List to store star pairs

    void Start()
    {
        // Load star pairs from constellationship.fab
        starPairs = constellationParser.ParseConstellationFile("Assets/modern/constellationship.fab");
    }
}
