using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class ConstellationLoader : MonoBehaviour
{
    public int current_Const = -1;
    CSV_Loader csv_Loader;
    public GameObject csv_Reader;
    Dictionary<int, GameObject> StarsDict;
    GameObject getStarby_Hip(int hip_id)
    {
        if (StarsDict.ContainsKey(hip_id))
        {
            return StarsDict[hip_id];
        }
        else
        {
            return null;
        }
    }
     
    void DrawStarsConstellation(Vector3 star1_Pos, Vector3 star2_Pos, Material random_Material)
    {


        GameObject lineRendererObj = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineRendererObj.AddComponent<LineRenderer>();
        lineRenderer.material = random_Material;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, star1_Pos);
        lineRenderer.SetPosition(1, star2_Pos);
    }
    void loadConstellation(string constellationFileName)
    {
        string constellationFile_Path = Path.Combine(Application.streamingAssetsPath, constellationFileName);
        Debug.Log("loading constallations!!");


        // Check if the file exists
        if (File.Exists(constellationFile_Path))
        {
            // read the CSV file
            using (StreamReader streamreader = new StreamReader(constellationFile_Path))
            {
                while (!streamreader.EndOfStream)
                {
                    string line = streamreader.ReadLine();

                    string[] values = Regex.Split(line, @"\s+");

                    Debug.Log("  length:" + values.Length);
                    if (values.Length > 2) 
                    {
                        // Load materials from Resources
                        Material Mat_a = Resources.Load<Material>("mat_A");
                        Material Mat_b = Resources.Load<Material>("mat_B");
                        Material Mat_f = Resources.Load<Material>("mat_F");
                        Material Mat_g = Resources.Load<Material>("mat_G");
                        Material Mat_k = Resources.Load<Material>("mat_K");
                        Material Mat_m = Resources.Load<Material>("mat_M");
                        Material Mat_o = Resources.Load<Material>("mat_O");
                        // Select a random material
                        Material[] materials = { Mat_a, Mat_b, Mat_f, Mat_g, Mat_k, Mat_m, Mat_o };
                        Material randMat_o = materials[Random.Range(0, materials.Length)];

                        int num_pairs = int.Parse(values[1]);
                        if (true)
                        {
                            for (int i = 0; i < (num_pairs * 2) - 1; i = i + 2)
                            {
                                int id1 = int.Parse(values[2 + i]);
                                int id2 = int.Parse(values[2 + i + 1]);

                                GameObject star1 = getStarby_Hip(id1);
                                GameObject star2 = getStarby_Hip(id2);


                                if (star1 != null && star2 != null)
                                {
                                    DrawStarsConstellation(star1.transform.position, star2.transform.position, randMat_o);
                                }
                            }
                        }

                    }


                }
            }
        }
        else
        {
            Debug.LogError("CSV file not found");
        }
    }
    public void loadModernConstellation(bool isOn)
    {
        if(isOn)
        {
            StarsDict = csv_Loader.StarsDict;
            Debug.Log("Loading Modern constellation!!");
            ClearConstellations();
            loadConstellation("modern_constellation.txt");
            current_Const = 0;
        }
        
    }
    public void loadGreekConstellation(bool isOn)
    {
        if (isOn)
        {
            StarsDict = csv_Loader.StarsDict;
            ClearConstellations();
            loadConstellation("greek_constellation.txt");
            Debug.Log("Loading Greek constellation!!");
            current_Const = 1;
        }
    }
    public void loadChineseConstellation(bool isOn)
    {
        if (isOn)
        {
            StarsDict = csv_Loader.StarsDict;
            ClearConstellations();
            loadConstellation("chinese_constellation.txt");
            Debug.Log("Loading Chinese constellation!!");
            current_Const = 2;
        }
    }
    public void loadIndianConstellation(bool isOn)
    {
        if (isOn)
        {
            StarsDict = csv_Loader.StarsDict;
            ClearConstellations();
            loadConstellation("indian_constellation.txt");
            Debug.Log("Loading Indian constellation!!");
            current_Const = 3;
        }
    }
    public void loadEgyptConstellation(bool isOn)
    {
        if (isOn)
        {
            StarsDict = csv_Loader.StarsDict;
            ClearConstellations();
            loadConstellation("egypt_constellation.txt");
            Debug.Log("Loading Egyptian constellation!!");
            current_Const = 4;
        }
    }
    public void loadNoConstellation(bool isOn)
    {
        if (isOn)
        {
            ClearConstellations();
            Debug.Log("Constellation Loading error!!");
            current_Const = 5;
        }
    }

    public void ClearConstellations()
    {
        // Find all GameObjects with LineRenderer components
        LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();

        // Iterate through each LineRenderer and destroy its GameObject
        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        csv_Loader = csv_Reader.GetComponent<CSV_Loader>();
        if(csv_Loader.StarsDict == null)
            {
                Debug.LogError("  csv_Loader is null!");
            }

    }

}
