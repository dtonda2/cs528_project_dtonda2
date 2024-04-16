using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class StellariumLoader : MonoBehaviour
{
    // URL of the SIMBAD API
    private string simbadAPIUrl = "http://simbad.u-strasbg.fr/simbad/sim-id?output.format=ASCII&Ident=HIP";

    // Define star pairs that form constellations (dummy data)
    private Dictionary<string, List<int>> constellationStarPairs = new Dictionary<string, List<int>>
    {
        { "Andromeda", new List<int> { 198, 215, 3881, 337, 136, 3092, 224, 428, 9640 } },
    { "Antlia", new List<int> { 4, 84, 51172, 72, 69, 47758, 42, 120, 48926 } },
    { "Apus", new List<int> { 66, 60, 81852, 76, 87, 81065, 163, 110, 72370 } },
    { "Aquarius", new List<int> { 8943, 14240, 10644, 113963, 115892 } },
    { "Aquila", new List<int> { 97649, 97365, 97603, 98036, 97534 } },
    { "Ara", new List<int> { 84709, 83153, 82127, 82073, 81997 } },
    // Add more constellation data
    { "Boötes", new List<int> { 61174, 61421, 61317, 61359, 61941 } },
    { "Caelum", new List<int> { 19587, 23311, 25606, 31398, 3821 } },
    { "Camelopardalis", new List<int> { 36850, 28305, 31771, 33579, 35904 } },
    { "Canes Venatici", new List<int> { 51172, 50728, 58001, 60129, 52727 } },
    { "Canis Major", new List<int> { 29655, 37279, 32349, 34444, 30324 } },
    { "Canis Minor", new List<int> { 36850, 28305, 31771, 33579, 35904 } },
    { "Capricornus", new List<int> { 105881, 106985, 104139, 102485, 109289 } },
    { "Carina", new List<int> { 45193, 47670, 46485, 50099, 52911 } },
    { "Cassiopeia", new List<int> { 12311, 14879, 12929, 16826, 13702 } },
    { "Centaurus", new List<int> { 71683, 73334, 72105, 71683, 71075 } },
    { "Cepheus", new List<int> { 105199, 107259, 110991, 112122, 105199 } },
    { "Cetus", new List<int> { 3419, 35497, 43109, 4128, 43409 } },
    { "Chamaeleon", new List<int> { 54719, 54461, 55503, 56559, 57088 } },
    { "Circinus", new List<int> { 78918, 79672, 80582, 79664, 80558 } },
    { "Columba", new List<int> { 43587, 42313, 44511, 43109, 42402 } },
    { "Coma Berenices", new List<int> { 57803, 56553, 55687, 56770, 56343 } },
    { "Corona Australis", new List<int> { 92175, 93962, 92564, 92370, 92946 } },
    { "Corona Borealis", new List<int> { 76297, 76952, 78072, 76297, 77233 } },
    { "Corvus", new List<int> { 61281, 61941, 63090, 62757, 61941 } },
    { "Crater", new List<int> { 56343, 56633, 57936, 56343, 57029 } },
    { "Crux", new List<int> { 60718, 61262, 61421, 60718, 61262 } },
    { "Cygnus", new List<int> { 102098, 102488, 102098, 102488, 104732 } },
    { "Delphinus", new List<int> { 101421, 101958, 101093, 101421, 101958 } },
    { "Dorado", new List<int> { 39794, 37677, 38901, 39794, 37677 } },
    { "Draco", new List<int> { 87937, 87833, 87108, 87833, 87585 } },
    { "Equuleus", new List<int> { 104858, 104858, 104858, 104858, 104858 } },
    { "Eridanus", new List<int> { 21683, 23453, 23480, 21393, 21683 } },
    { "Fornax", new List<int> { 20156, 21683, 23453, 23480, 21393 } },
    { "Gemini", new List<int> { 62509, 60129, 60179, 61845, 62509 } },
    };

    void Start()
    {
        // Start a coroutine to fetch star data from the SIMBAD API
        StartCoroutine(LoadStarsFromSimbad());
    }

    IEnumerator LoadStarsFromSimbad()
    {
        // Iterate over the constellation star pairs
        foreach (KeyValuePair<string, List<int>> constellationPair in constellationStarPairs)
        {
            List<int> hipNumbers = constellationPair.Value;

            foreach (int hipNumber in hipNumbers)
            {
                using (UnityWebRequest www = UnityWebRequest.Get(simbadAPIUrl + hipNumber))
                {
                    yield return www.SendWebRequest();

                    if (!www.isNetworkError && !www.isHttpError)
                    {
                        string[] lines = www.downloadHandler.text.Split('\n');

                        // Parse the response to extract star data
                        SStarData sstarData = ParseSimbadResponse(lines);

                        // Instantiate a star game object using the extracted data
                        if (sstarData != null)
                        {
                            GameObject starObject = new GameObject("Star_" + sstarData.HIP);
                            starObject.transform.position = new Vector3(sstarData.X0, sstarData.Y0, sstarData.Z0);
                            // Add any additional properties or components to the star object as needed
                        }
                    }
                    else
                    {
                        Debug.LogError("Failed to fetch star data from SIMBAD API: " + www.error);
                    }
                }
            }

            // Create constellation lines based on the fetched star data
            CreateConstellation(constellationPair.Key, hipNumbers);
        }
    }

    SStarData ParseSimbadResponse(string[] lines)
    {
        SStarData sstarData = new SStarData();

        foreach (string line in lines)
        {
            if (line.StartsWith("HIP"))
            {
                string[] tokens = line.Split('|');

                foreach (string token in tokens)
                {
                    if (token.StartsWith("HIP"))
                    {
                        string[] hipTokens = token.Split(' ');
                        if (hipTokens.Length >= 2)
                        {
                            sstarData.HIP = float.Parse(hipTokens[1]);
                        }
                    }
                    else if (token.StartsWith("RA"))
                    {
                        string[] raTokens = token.Split(' ');
                        if (raTokens.Length >= 3)
                        {
                            float raHours = float.Parse(raTokens[1]);
                            float raMinutes = float.Parse(raTokens[2]);
                            float raSeconds = float.Parse(raTokens[3]);
                            sstarData.X0 = (raHours + raMinutes / 60f + raSeconds / 3600f) * 15f; // Convert hours to degrees
                        }
                    }
                    else if (token.StartsWith("DEC"))
                    {
                        string[] decTokens = token.Split(' ');
                        if (decTokens.Length >= 3)
                        {
                            float decDegrees = float.Parse(decTokens[1]);
                            float decMinutes = float.Parse(decTokens[2]);
                            float decSeconds = float.Parse(decTokens[3]);
                            sstarData.Y0 = decDegrees + decMinutes / 60f + decSeconds / 3600f; // Dec is already in degrees
                        }
                    }
                    // Add additional properties as needed
                }
            }
        }

        return sstarData;
    }

    void CreateConstellation(string constellationName, List<int> hipNumbers)
{
    // Create a new game object to hold the constellation lines
    GameObject constellationObject = new GameObject("Constellation_" + constellationName);

    // Add a LineRenderer component to the constellation object
    LineRenderer lineRenderer = constellationObject.AddComponent<LineRenderer>();
    lineRenderer.startWidth = 0.1f; // Adjust line width as needed
    lineRenderer.endWidth = 0.1f;
    lineRenderer.useWorldSpace = true;

    // Set the number of positions for the line renderer
    lineRenderer.positionCount = hipNumbers.Count;

    // Generate a random color for the constellation lines (rainbow color)
    Gradient gradient = new Gradient();
    gradient.SetKeys(
        new GradientColorKey[] {
            new GradientColorKey(Color.red, 0.0f),
            new GradientColorKey(Color.yellow, 0.25f),
            new GradientColorKey(Color.green, 0.5f),
            new GradientColorKey(Color.cyan, 0.75f),
            new GradientColorKey(Color.blue, 1.0f)
        },
        new GradientAlphaKey[] {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(1.0f, 1.0f)
        }
    );
    lineRenderer.colorGradient = gradient;

    // Iterate over the list of HIP numbers
    for (int i = 0; i < hipNumbers.Count; i++)
    {
        // Generate a random position for the star
        Vector3 starPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));

        // Set the position of the line renderer
        lineRenderer.SetPosition(i, starPosition);
    }
}


Vector3 GetStarPosition(int hipNumber)
{
    // Fetch the position of the star based on the HIP number
    // You would need to implement the logic to retrieve the position of the star
    // This could involve querying a star database or using a predefined star catalog
    // For demonstration purposes, we'll just return a random position
    return new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
}

}

[System.Serializable]
public class SStarData
{
    public float HIP;
    public float DIST, X0, Y0, Z0, ABSMAG, MAG, VX, VY, VZ;
    public string SPECT;
}
