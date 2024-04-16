using UnityEngine;
using System.Collections.Generic;

public class ConstellationParser : MonoBehaviour
{
    // Class to represent a star pair
    [System.Serializable]
    public class StarPair
    {
        public int Star1HipparcosID { get; set; }
        public int Star2HipparcosID { get; set; }

        public StarPair(int star1, int star2)
        {
            Star1HipparcosID = star1;
            Star2HipparcosID = star2;
        }
    }

    // Method to parse constellationship.fab file and extract star pairs
    public List<StarPair> ParseConstellationFile(string filePath)
    {
        List<StarPair> starPairs = new List<StarPair>();

        // Read each line from the file
        string[] lines = System.IO.File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            // Split the line by spaces
            string[] starIDs = line.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            // Convert star IDs to integers
            if (starIDs.Length >= 2)
            {
                int star1HipparcosID;
                int star2HipparcosID;

                if (int.TryParse(starIDs[0], out star1HipparcosID) && int.TryParse(starIDs[1], out star2HipparcosID))
                {
                    // Create a new star pair and add it to the list
                    StarPair starPair = new StarPair(star1HipparcosID, star2HipparcosID);
                    starPairs.Add(starPair);
                }
                else
                {
                    Debug.LogWarning("Failed to parse star pair: " + line);
                }
            }
            else
            {
                Debug.LogWarning("Invalid star pair format: " + line);
            }
        }

        return starPairs;
    }
}
