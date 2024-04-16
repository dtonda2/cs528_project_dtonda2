using UnityEngine;
using System.Collections.Generic;

public class ConstellationNameLoader : MonoBehaviour
{
    public ConstellationNameParser nameParser; // Reference to the constellation name parser component

    public Dictionary<string, string> constellationNames; // Dictionary to store constellation names

    void Start()
    {
        // Load constellation names from constellation_names.eng.fab
        constellationNames = nameParser.ParseConstellationNamesFile("Assets/modern/constellation_names.eng.fab");
    }
}
