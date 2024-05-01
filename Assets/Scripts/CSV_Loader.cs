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
    public List<GameObject> starsBlocks = new List<GameObject>();
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
    public GameObject Star_A, Star_B, Star_F, Star_G, Star_K, Star_M, Star_O;
    int StarsCount = 0;

    public Dictionary<int, GameObject> StarsDict;
    public Dictionary<int, GameObject> allStarsDict;
    public Dictionary<float, float> hip_exo_Dict;
    public Dictionary<Vector3Int, OctreeBlock> octree_Blocks;


    ConstellationLoader ConstellationLoaderScript;
    public GameObject ConstellationLoaderObj;

    List<GameObject> StarsP;
    List<GameObject> allStarsP;
    List<Point3D> allStarsData;

    void spawnStar(char spect,Vector3 Cube_Pos, Vector3 cubeVelocity,int star_hip_id, int planet_count, int star_id)
    {
        GameObject Spw_Star = null;
        switch (spect)
        {
            case 'A':
                Spw_Star = Instantiate(Star_A, Cube_Pos, Quaternion.identity);
                break;
            
            case 'B':
                Spw_Star = Instantiate(Star_B, Cube_Pos, Quaternion.identity);
                break;

            case 'F':
                Spw_Star = Instantiate(Star_F, Cube_Pos, Quaternion.identity);
                break;

            case 'G':
                Spw_Star = Instantiate(Star_G, Cube_Pos, Quaternion.identity);
                break;

            case 'K':
                Spw_Star = Instantiate(Star_K, Cube_Pos, Quaternion.identity);
                break;

            case 'M':
                Spw_Star = Instantiate(Star_M, Cube_Pos, Quaternion.identity);
                break;

            case 'O':
                Spw_Star  = Instantiate(Star_O, Cube_Pos, Quaternion.identity);
                break;

            default:
                Debug.Log("Value of spawned star is not set");
                break;
        }

        
        
        if (!allStarsDict.ContainsKey(star_id))
        {
            
            allStarsDict.Add(star_id, Spw_Star);
        }

        if (!StarsDict.ContainsKey(star_hip_id))
        {
           
            StarsDict.Add(star_hip_id, Spw_Star);
        }
        allStarsP.Add(Spw_Star);
        allStarsData.Add(new Point3D(Cube_Pos, cubeVelocity));
        Star_Data starScript = Spw_Star.GetComponent<Star_Data>();

        if (starScript != null)
        {
            
            starScript.hip_id = star_hip_id;
            starScript.init_pos = Cube_Pos;
            starScript.curr_pos = Cube_Pos;
            starScript.init_vel = cubeVelocity;
            starScript.planet_count = planet_count;
            starScript.spectrum = spect;
        }

        if (Spw_Star != null)
        {
            Vector3 starPosition = Spw_Star.transform.position;
            Vector3Int block_Pos = new Vector3Int(
                Mathf.FloorToInt(starPosition.x / 20) * 20,
                Mathf.FloorToInt(starPosition.y / 20) * 20,
                Mathf.FloorToInt(starPosition.z / 20) * 20
            );

            // Create octree block if it doesn't exist
            if (!octree_Blocks.ContainsKey(block_Pos))
            {
                octree_Blocks[block_Pos] = new OctreeBlock { block_position = block_Pos };
            }

            // Add star to octree block
            octree_Blocks[block_Pos].starsBlocks.Add(Spw_Star);
            Spw_Star.SetActive(false);
        }
    }
    
    void Start()
    {
        StarsDict = new Dictionary<int, GameObject>();
        allStarsDict = new Dictionary<int, GameObject>();
        allStarsP = new List<GameObject>();
        allStarsData = new List<Point3D>();
        octree_Blocks = new Dictionary<Vector3Int, OctreeBlock>();
        hip_exo_Dict = new Dictionary<float, float>();
        StarsP = new List<GameObject>();
        StartCoroutine(LoadCSVFile());
    }

    IEnumerator LoadCSVFile()
    {
        //path of CSV file
        string csvFileName = "shiningstar_dataset.csv";
        string csvPath = Path.Combine(Application.streamingAssetsPath, csvFileName);
        CultureInfo culture = new CultureInfo("en-US");

        // Check if file exists
        if (File.Exists(csvPath))
        {
            //read the CSV file
            using (StreamReader streamreader = new StreamReader(csvPath))
            {
                while (!streamreader.EndOfStream)
                {
                    string line = streamreader.ReadLine();
                    
                    string[] values = line.Split(',');

                    float x0, y0, z0, v_x0, v_y0, v_z0, hip_id, planets_count,star_id;

                    if (float.TryParse(values[3], NumberStyles.Float, culture, out x0) && float.TryParse(values[4], NumberStyles.Float, culture, out y0) &&
                        float.TryParse(values[5], NumberStyles.Float, culture, out z0) && float.TryParse(values[8], NumberStyles.Float, culture, out v_x0) &&
                        float.TryParse(values[9], NumberStyles.Float, culture, out v_y0) && float.TryParse(values[10], NumberStyles.Float, culture, out v_z0) &&
                        float.TryParse(values[0], NumberStyles.Float, culture, out star_id) && float.TryParse(values[1], NumberStyles.Float, culture, out hip_id) &&
                        float.TryParse(values[12], NumberStyles.Float, culture, out planets_count)
                       )
                    {
                        Vector3 Cube_Pos = new Vector3(x0, z0, y0);
                        Vector3 cubeVelocity = new Vector3(v_x0, v_y0, v_z0);
                        spawnStar(values[11][0], Cube_Pos, cubeVelocity, (int)hip_id, (int)planets_count,(int)star_id);
                        StarsCount = StarsCount + 1;
                    }
                    else
                    {
                        Debug.LogError("Failed to parse, x0, y0, z0.");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("CSV file not found");
        }


        // Specify CSV file path
        string csvExoFileName = "processed_exoplanets.csv";
        string csvExoPath = Path.Combine(Application.streamingAssetsPath, csvExoFileName);

        // Check if the file exists
        if (File.Exists(csvExoPath))
        {
            // Read CSV file
            using (StreamReader streamreader = new StreamReader(csvExoPath))
            {
                while (!streamreader.EndOfStream)
                {
                    string line = streamreader.ReadLine();
                   string[] values = line.Split(',');
                    float num_planets, hip_id;
                    if (float.TryParse(values[0], NumberStyles.Float, culture, out hip_id) &&
                        float.TryParse(values[1], NumberStyles.Float, culture, out num_planets))
                    {
                        hip_exo_Dict.Add(hip_id, num_planets);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse float values");
                    }

                }
            }
        }
        else
        {
            Debug.LogError("CSV file not found");
        }


        int numberOfElements = octree_Blocks.Count;

        int totalStars = 0;
        int totalBlocks = octree_Blocks.Count;

        foreach (var block in octree_Blocks.Values)
        {
            totalStars += block.starsBlocks.Count;
        }
        float avgStarsPerBlock = (float)totalStars / totalBlocks;

        yield return null;

    }

    public void Block_Clear()
    {
        octree_Blocks.Clear();
        if (allStarsP.Count == allStarsData.Count)
        {
            for (int i = 0; i < allStarsP.Count; i++)
            {
                GameObject star = allStarsP[i];
                Point3D metaData = allStarsData[i];

                star.transform.position = metaData.init_position;

                Vector3 starPosition = star.transform.position;
                Vector3Int block_Pos = new Vector3Int(
                    Mathf.FloorToInt(starPosition.x / 20) * 20,
                    Mathf.FloorToInt(starPosition.y / 20) * 20,
                    Mathf.FloorToInt(starPosition.z / 20) * 20
                );

                // Create octree block if it doesn't exist
                if (!octree_Blocks.ContainsKey(block_Pos))
                {
                    octree_Blocks[block_Pos] = new OctreeBlock { block_position = block_Pos };
                }

                // Add star to the corresponding octree block
                octree_Blocks[block_Pos].starsBlocks.Add(star);
            }
        }
        else
        {
            Debug.LogError("Lists have different lengths!");
        }

    }
    public void StarHalfScaler()
    {
        octree_Blocks.Clear();

        foreach (GameObject star in allStarsP)
        {
            star.transform.position = star.transform.position / 2;

            Vector3 starPosition = star.transform.position;
            Vector3Int block_Pos = new Vector3Int(
                Mathf.FloorToInt(starPosition.x / 20) * 20,
                Mathf.FloorToInt(starPosition.y / 20) * 20,
                Mathf.FloorToInt(starPosition.z / 20) * 20
            );

            // Create octree block if it doesn't exist
            if (!octree_Blocks.ContainsKey(block_Pos))
            {
                octree_Blocks[block_Pos] = new OctreeBlock { block_position = block_Pos };
            }

            // Add star to the corresponding octree block
            octree_Blocks[block_Pos].starsBlocks.Add(star);
        }

    }

    public void StarDoubleScaler()
    {
        octree_Blocks.Clear();

        foreach (GameObject star in allStarsP)
        {
            star.transform.position = star.transform.position*2;

            Vector3 starPosition = star.transform.position;
            Vector3Int block_Pos = new Vector3Int(
                Mathf.FloorToInt(starPosition.x / 20) * 20,
                Mathf.FloorToInt(starPosition.y / 20) * 20,
                Mathf.FloorToInt(starPosition.z / 20) * 20
            );

            // Create octree block if it doesn't exist
            if (!octree_Blocks.ContainsKey(block_Pos))
            {
                octree_Blocks[block_Pos] = new OctreeBlock { block_position = block_Pos };
            }

            // Add star to the corresponding octree block
            octree_Blocks[block_Pos].starsBlocks.Add(star);
        }

    }


    public void ForwardYears_10K(int rev_time)
    {
        
        if (allStarsP.Count == allStarsData.Count)
        {
            for (int i = 0; i < allStarsP.Count; i++)
            {
                GameObject star = allStarsP[i];
                Point3D metaData = allStarsData[i];

                Vector3 displacement = metaData.init_velocity * 10000* rev_time;
                star.transform.position+= displacement;

            }
        }
        else
        {
            Debug.LogError("different lengths!");
        }
    }

    public void Timer_Stop()
    {
        octree_Blocks.Clear();

        if (allStarsP.Count == allStarsData.Count)
            foreach (GameObject star in allStarsP)
            {
                Vector3 starPosition = star.transform.position;
                Vector3Int block_Pos = new Vector3Int(
                    Mathf.FloorToInt(starPosition.x / 20) * 20,
                    Mathf.FloorToInt(starPosition.y / 20) * 20,
                    Mathf.FloorToInt(starPosition.z / 20) * 20
                );

                // Create octree block if it doesn't exist
                if (!octree_Blocks.ContainsKey(block_Pos))
                {
                    octree_Blocks[block_Pos] = new OctreeBlock { block_position = block_Pos };
                }

                // Add star to octree block
                octree_Blocks[block_Pos].starsBlocks.Add(star);
            }
    }

}

