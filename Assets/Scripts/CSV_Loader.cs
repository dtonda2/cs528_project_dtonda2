using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine.Profiling.Memory.Experimental;

public class OctreeBlock
{
    public Vector3Int block_position;
    public List<GameObject> stars_in_block = new List<GameObject>();
}
public struct Point3D
{
    public Vector3 init_position;
    public Vector3 init_velocity;
    // Constructor to initialize the point
    public Point3D(Vector3 pos, Vector3 vel)
    {
        this.init_position = pos;
        this.init_velocity = vel;
    }
}
public class CSV_Loader : MonoBehaviour
{
    public GameObject O_prefab;
    public GameObject B_prefab;
    public GameObject A_prefab;
    public GameObject F_prefab;
    public GameObject G_prefab;
    public GameObject K_prefab;
    public GameObject M_prefab;
    int star_counter = 0;

    public Dictionary<int, GameObject> starDictionary;
    public Dictionary<int, GameObject> allStarDictionary;
    public Dictionary<float, float> hip_exo_Dictionary;
    public Dictionary<Vector3Int, OctreeBlock> octreeBlocks;


    ConstellationLoader ConstellationLoaderScript;
    public GameObject ConstellationLoaderObj;

    List<GameObject> starObjects;
    List<GameObject> allstarObjects;
    List<Point3D> allstarMetaData;

    void spawnStar(char spect,Vector3 cubePosition, Vector3 cubeVelocity,int star_hip_id, int planet_count, int star_id)
    {
        
        GameObject spawnedStar = null;
        switch (spect)
        {
            case 'O':
                spawnedStar  = Instantiate(O_prefab, cubePosition, Quaternion.identity);

                break;

            case 'B':
                spawnedStar = Instantiate(B_prefab, cubePosition, Quaternion.identity);
                break;

            case 'A':
                spawnedStar = Instantiate(A_prefab, cubePosition, Quaternion.identity);
                break;

            case 'F':
                spawnedStar = Instantiate(F_prefab, cubePosition, Quaternion.identity);
                break;

            case 'G':
                spawnedStar = Instantiate(G_prefab, cubePosition, Quaternion.identity);
                break;

            case 'K':
                spawnedStar = Instantiate(K_prefab, cubePosition, Quaternion.identity);
                break;

            case 'M':
                spawnedStar = Instantiate(M_prefab, cubePosition, Quaternion.identity);
                break;

            default:
                Debug.Log("Value not set");
                break;
        }

        
        
        if (!allStarDictionary.ContainsKey(star_id))
        {
            
            allStarDictionary.Add(star_id, spawnedStar);
        }

        if (!starDictionary.ContainsKey(star_hip_id))
        {
           
            starDictionary.Add(star_hip_id, spawnedStar);
        }
        allstarObjects.Add(spawnedStar);
        allstarMetaData.Add(new Point3D(cubePosition, cubeVelocity));
        starMetaData starScript = spawnedStar.GetComponent<starMetaData>();

        if (starScript != null)
        {
            
            starScript.hip_id = star_hip_id;
            starScript.init_pos = cubePosition;
            starScript.curr_pos = cubePosition;
            starScript.init_vel = cubeVelocity;
            starScript.planet_count = planet_count;
            starScript.spectrum = spect;
        }

        if (spawnedStar != null)
        {
            Vector3 starPosition = spawnedStar.transform.position;
            Vector3Int blockPosition = new Vector3Int(
                Mathf.FloorToInt(starPosition.x / 20) * 20,
                Mathf.FloorToInt(starPosition.y / 20) * 20,
                Mathf.FloorToInt(starPosition.z / 20) * 20
            );

            // Create octree block if it doesn't exist
            if (!octreeBlocks.ContainsKey(blockPosition))
            {
                octreeBlocks[blockPosition] = new OctreeBlock { block_position = blockPosition };
            }

            // Add star to the corresponding octree block
            octreeBlocks[blockPosition].stars_in_block.Add(spawnedStar);

            /*
            // Disable the renderer
            Renderer objectRenderer = spawnedStar.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                objectRenderer.enabled = false;
            }
            */

            spawnedStar.SetActive(false);
        }
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        starDictionary = new Dictionary<int, GameObject>();
        allStarDictionary = new Dictionary<int, GameObject>();
        allstarObjects = new List<GameObject>();
        allstarMetaData = new List<Point3D>();
        octreeBlocks = new Dictionary<Vector3Int, OctreeBlock>();
        hip_exo_Dictionary = new Dictionary<float, float>();
        starObjects = new List<GameObject>();
        StartCoroutine(LoadCSVFile());
    }

    IEnumerator LoadCSVFile()
    {
        // Specify the path to your CSV file
        string csvFileName = "shiningstar_dataset.csv";
        string csvPath = Path.Combine(Application.streamingAssetsPath, csvFileName);
        CultureInfo culture = new CultureInfo("en-US");
        // If loading from Resources folder, modify the path
        // string csvPath = "Assets/Resources/cleaned_data.csv";

        // Check if the file exists
        if (File.Exists(csvPath))
        {
            // Open a StreamReader to read the CSV file
            using (StreamReader reader = new StreamReader(csvPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    
                    string[] values = line.Split(',');

                    float x0, y0, z0, v_x0, v_y0, v_z0, hip_id, planets_count,star_id;

                    if (float.TryParse(values[3], NumberStyles.Float, culture, out x0) &&
                        float.TryParse(values[4], NumberStyles.Float, culture, out y0) &&
                        float.TryParse(values[5], NumberStyles.Float, culture, out z0) &&
                        float.TryParse(values[8], NumberStyles.Float, culture, out v_x0) &&
                        float.TryParse(values[9], NumberStyles.Float, culture, out v_y0) &&
                        float.TryParse(values[10], NumberStyles.Float, culture, out v_z0) &&
                        float.TryParse(values[0], NumberStyles.Float, culture, out star_id) &&
                        float.TryParse(values[1], NumberStyles.Float, culture, out hip_id) &&
                        float.TryParse(values[12], NumberStyles.Float, culture, out planets_count)
                       )
                    {
                        Vector3 cubePosition = new Vector3(x0, z0, y0);
                        Vector3 cubeVelocity = new Vector3(v_x0, v_y0, v_z0);
                        spawnStar(values[11][0], cubePosition, cubeVelocity, (int)hip_id, (int)planets_count,(int)star_id);
                        star_counter  = star_counter+1;
                    }
                    else
                    {
                        Debug.LogError("Failed to parse float values for x0, y0, z0.");
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"CSV file not found at path: {csvPath}");
        }
        //loadConstellation();
        //load the exoplnets count file 


        // Specify the path to your CSV file
        string csvExoFileName = "processed_exoplanets.csv";
        string csvExoPath = Path.Combine(Application.streamingAssetsPath, csvExoFileName);

        // Check if the file exists
        if (File.Exists(csvExoPath))
        {
            // Open a StreamReader to read the CSV file
            using (StreamReader reader = new StreamReader(csvExoPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                   string[] values = line.Split(',');

                    float num_planets, hip_id;

                    if (float.TryParse(values[0], NumberStyles.Float, culture, out hip_id) &&
                        float.TryParse(values[1], NumberStyles.Float, culture, out num_planets)
                       )
                    {
                        hip_exo_Dictionary.Add(hip_id, num_planets);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse float values for hip id and num of planets");
                    }

                }
            }
        }
        else
        {
            Debug.LogError($"CSV file not found at path: {csvPath}");
        }


        int numberOfElements = octreeBlocks.Count;

        int totalStars = 0;
        int totalBlocks = octreeBlocks.Count;

        foreach (var block in octreeBlocks.Values)
        {
            totalStars += block.stars_in_block.Count;
        }

        float averageStarsPerBlock = (float)totalStars / totalBlocks;

        yield return null;

    }

    public void new_reset()
    {
        octreeBlocks.Clear();
        if (allstarObjects.Count == allstarMetaData.Count)
        {
            for (int i = 0; i < allstarObjects.Count; i++)
            {
                GameObject star = allstarObjects[i];
                Point3D metaData = allstarMetaData[i];

                star.transform.position = metaData.init_position;

                Vector3 starPosition = star.transform.position;
                Vector3Int blockPosition = new Vector3Int(
                    Mathf.FloorToInt(starPosition.x / 20) * 20,
                    Mathf.FloorToInt(starPosition.y / 20) * 20,
                    Mathf.FloorToInt(starPosition.z / 20) * 20
                );

                // Create octree block if it doesn't exist
                if (!octreeBlocks.ContainsKey(blockPosition))
                {
                    octreeBlocks[blockPosition] = new OctreeBlock { block_position = blockPosition };
                }

                // Add star to the corresponding octree block
                octreeBlocks[blockPosition].stars_in_block.Add(star);
            }
        }
        else
        {
            Debug.LogError("Lists have different lengths!");
        }

    }
    public void new_scale_star_halve()
    {
        octreeBlocks.Clear();

        foreach (GameObject star in allstarObjects)
        {
            star.transform.position = star.transform.position / 2;

            Vector3 starPosition = star.transform.position;
            Vector3Int blockPosition = new Vector3Int(
                Mathf.FloorToInt(starPosition.x / 20) * 20,
                Mathf.FloorToInt(starPosition.y / 20) * 20,
                Mathf.FloorToInt(starPosition.z / 20) * 20
            );

            // Create octree block if it doesn't exist
            if (!octreeBlocks.ContainsKey(blockPosition))
            {
                octreeBlocks[blockPosition] = new OctreeBlock { block_position = blockPosition };
            }

            // Add star to the corresponding octree block
            octreeBlocks[blockPosition].stars_in_block.Add(star);
        }

    }

    public void new_scale_star_double()
    {
        octreeBlocks.Clear();

        foreach (GameObject star in allstarObjects)
        {
            star.transform.position = star.transform.position*2;

            Vector3 starPosition = star.transform.position;
            Vector3Int blockPosition = new Vector3Int(
                Mathf.FloorToInt(starPosition.x / 20) * 20,
                Mathf.FloorToInt(starPosition.y / 20) * 20,
                Mathf.FloorToInt(starPosition.z / 20) * 20
            );

            // Create octree block if it doesn't exist
            if (!octreeBlocks.ContainsKey(blockPosition))
            {
                octreeBlocks[blockPosition] = new OctreeBlock { block_position = blockPosition };
            }

            // Add star to the corresponding octree block
            octreeBlocks[blockPosition].stars_in_block.Add(star);
        }

    }


    public void jump_10k_years(int rev_time)
    {
        
        if (allstarObjects.Count == allstarMetaData.Count)
        {
            for (int i = 0; i < allstarObjects.Count; i++)
            {
                GameObject star = allstarObjects[i];
                Point3D metaData = allstarMetaData[i];

                Vector3 displacement = metaData.init_velocity * 10000* rev_time;
                star.transform.position+= displacement;

            }
        }
        else
        {
            Debug.LogError("different lengths!");
        }
    }

    public void stop_time()
    {
        octreeBlocks.Clear();

        if (allstarObjects.Count == allstarMetaData.Count)
            foreach (GameObject star in allstarObjects)
            {
                Vector3 starPosition = star.transform.position;
                Vector3Int blockPosition = new Vector3Int(
                    Mathf.FloorToInt(starPosition.x / 20) * 20,
                    Mathf.FloorToInt(starPosition.y / 20) * 20,
                    Mathf.FloorToInt(starPosition.z / 20) * 20
                );

                // Create octree block if it doesn't exist
                if (!octreeBlocks.ContainsKey(blockPosition))
                {
                    octreeBlocks[blockPosition] = new OctreeBlock { block_position = blockPosition };
                }

                // Add star to the corresponding octree block
                octreeBlocks[blockPosition].stars_in_block.Add(star);
            }
    }

}

