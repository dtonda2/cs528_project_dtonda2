using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class ConstellationLoader : MonoBehaviour
{
    public int currConstellation = -1;
    CSV_Loader CSVLoaderScript;
    public GameObject CSVReaderObj;
    Dictionary<int, GameObject> mstarDictionary;
    GameObject getStarByID(int hip_id)
    {
        if (mstarDictionary.ContainsKey(hip_id))
        {
            return mstarDictionary[hip_id];
        }
        else
        {
            return null;
        }
    }
     
    void DrawLinebetweenStars(Vector3 star_01Pos, Vector3 star_02Pos, Material randMaterial)
    {


        GameObject lineRendererObject = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineRendererObject.AddComponent<LineRenderer>();
        lineRenderer.material = randMaterial;
        lineRenderer.startWidth = 0.06f;
        lineRenderer.endWidth = 0.06f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, star_01Pos);
        lineRenderer.SetPosition(1, star_02Pos);
    }
    void loadConstellation(string constellationFileName)
    {
        Debug.Log("  loading constallations...");
        string constellationPath = Path.Combine(Application.streamingAssetsPath, constellationFileName);

        // Check if the file exists
        if (File.Exists(constellationPath))
        {
            // Open a StreamReader to read the CSV file
            using (StreamReader reader = new StreamReader(constellationPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    string[] values = Regex.Split(line, @"\s+");

                    Debug.Log("  length:" + values.Length);
                    if (values.Length > 2) //sanity check
                    {
                        // Load materials from Resources folder
                        Material aMat = Resources.Load<Material>("A_mat");
                        Material bMat = Resources.Load<Material>("B_mat");
                        Material fMat = Resources.Load<Material>("F_mat");
                        Material gMat = Resources.Load<Material>("G_mat");
                        Material kMat = Resources.Load<Material>("K_mat");
                        Material mMat = Resources.Load<Material>("M_mat");
                        Material oMat = Resources.Load<Material>("O_mat");
                        // Select a random material
                        Material[] materials = { aMat, bMat, fMat, gMat, kMat, mMat, oMat };
                        Material randomMaterial = materials[Random.Range(0, materials.Length)];

                        int num_pairs = int.Parse(values[1]);
                        Debug.Log("  constellation:" + values[0] + "num of pairs:" + num_pairs); ;
                        if (true)
                        {
                            for (int i = 0; i < (num_pairs * 2) - 1; i = i + 2)
                            {
                                int id1 = int.Parse(values[2 + i]);
                                int id2 = int.Parse(values[2 + i + 1]);

                                Debug.Log("  constellation pair id1:" + id1 + " id2:" + id2);

                                GameObject star_01 = getStarByID(id1);
                                GameObject star_02 = getStarByID(id2);


                                if (star_01 != null && star_02 != null)
                                {
                                    DrawLinebetweenStars(star_01.transform.position, star_02.transform.position, randomMaterial);
                                }
                            }
                        }

                    }


                }
            }
        }
        else
        {
            Debug.LogError($"CSV file not found at path: {constellationPath}");
        }
    }
    public void loadModernConstellation(bool isOn)
    {
        if(isOn)
        {
            mstarDictionary = CSVLoaderScript.starDictionary;
            Debug.Log("  Contents of mstarDictionary:" + mstarDictionary.Count);
            Debug.Log("Modern constellation loading!!");
            ClearAllLineRenderers();
            loadConstellation("modern_constellationship.txt");
            currConstellation = 0;
        }
        
    }
    public void loadGreekConstellation(bool isOn)
    {
        if (isOn)
        {
            mstarDictionary = CSVLoaderScript.starDictionary;
            ClearAllLineRenderers();
            loadConstellation("greek_constellationship.txt");
            Debug.Log("Greek constellation loading!!");
            currConstellation = 1;
        }
    }
    public void loadChineseConstellation(bool isOn)
    {
        if (isOn)
        {
            mstarDictionary = CSVLoaderScript.starDictionary;
            ClearAllLineRenderers();
            loadConstellation("chinese_constellationship.txt");
            Debug.Log("Chinese constellation loading!!");
            currConstellation = 2;
        }
    }
    public void loadIndianConstellation(bool isOn)
    {
        if (isOn)
        {
            mstarDictionary = CSVLoaderScript.starDictionary;
            ClearAllLineRenderers();
            loadConstellation("indian_constellationship.txt");
            Debug.Log("Indian constellation loading!!");
            currConstellation = 3;
        }
    }
    public void loadEgyptConstellation(bool isOn)
    {
        if (isOn)
        {
            mstarDictionary = CSVLoaderScript.starDictionary;
            ClearAllLineRenderers();
            loadConstellation("egypt_constellationship.txt");
            Debug.Log("Egyptian constellation loading!!");
            currConstellation = 4;
        }
    }
    public void loadNoConstellation(bool isOn)
    {
        if (isOn)
        {
            ClearAllLineRenderers();
            Debug.Log("Constellation not loading!!");
            currConstellation = 5;
        }
    }

    public void ClearAllLineRenderers()
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
        CSVLoaderScript = CSVReaderObj.GetComponent<CSV_Loader>();
        if(CSVLoaderScript.starDictionary == null)
            {
                Debug.LogError("  CSVLoaderScript is null!");
            }

    }

}
