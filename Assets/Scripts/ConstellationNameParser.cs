using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationNameParser : MonoBehaviour
{
    // Method to parse constellation_names.eng.fab file and extract constellation names
    public Dictionary<string, string> ParseConstellationNamesFile(string filePath)
    {
        Dictionary<string, string> constellationNames = new Dictionary<string, string>();

        // Read each line from the file
        string[] lines = System.IO.File.ReadAllLines("Assets/modern/constellation_names.eng.fab");
        foreach (string line in lines)
        {
            // Split the line by tab character
            string[] parts = line.Split('\t');

            // Ensure the line has at least two parts
            if (parts.Length >= 2)
            {
                // Trim whitespace and remove quotation marks from constellation abbreviation and name
                string abbreviation = parts[0].Trim();
                string fullName = parts[1].Trim('\"');

                // Add constellation abbreviation and name to the dictionary
                constellationNames[abbreviation] = fullName;
            }
            else
            {
                Debug.LogWarning("Invalid constellation name format: " + line);
            }
        }

        return constellationNames;
    }
}
