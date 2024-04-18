using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CAVE2ClusterManager;

public class MatManager : MonoBehaviour
{
    // First array of materials
    public Material[] StarMaterials = new Material[7];

    // Second array of materials
    public Material[] Star2Materials = new Material[7];

    public bool show_exposure;
    // Start is called before the first frame update
    void Start()
    {
        show_exposure = false;
    }

    public void show_exposure_planets(bool showexo)
    {
        GameObject[] stars = GameObject.FindGameObjectsWithTag("star"); // Get all game objects tagged with "star"

        foreach (GameObject star in stars)
        {
            // Accessing the starmeta data script
            starMetaData starScript = star.GetComponent<starMetaData>();

            if (starScript != null)
            {
                if (show_exposure)
                {
                    // Access planet_count and do TODO
                    int planetCount = starScript.planet_count;
                    Renderer renderer = star.GetComponentInChildren<Renderer>();
                    switch (planetCount)
                    {
                        case 0:
                            renderer.material = Star2Materials[0];
                            break;

                        case 1:
                            renderer.material = Star2Materials[1];
                            break;

                        case 2:
                            renderer.material = Star2Materials[2];
                            break;

                        case 3:
                            renderer.material = Star2Materials[3];
                            break;

                        case 4:
                            renderer.material = Star2Materials[4];
                            break;

                        case 5:
                            renderer.material = Star2Materials[5];
                            break;

                        case 6:
                            renderer.material = Star2Materials[6];
                            break;

                        default:
                            Debug.Log("Value is not 1, 2, or 3");
                            break;
                    }
                }
                else
                {
                    // Access spec and do TODO2
                    char spec = starScript.spectrum;
                    Renderer renderer = star.GetComponentInChildren<Renderer>();
                    switch (spec)
                    {
                        case 'O':
                            renderer.material = StarMaterials[0];
                            break;

                        case 'B':
                            renderer.material = StarMaterials[1];
                            break;

                        case 'A':
                            renderer.material = StarMaterials[2];
                            break;

                        case 'F':
                            renderer.material = StarMaterials[3];
                            break;

                        case 'G':
                            renderer.material = StarMaterials[4];
                            break;

                        case 'K':
                            renderer.material = StarMaterials[5];
                            break;

                        case 'M':
                            renderer.material = StarMaterials[6];
                            break;

                        default:
                            Debug.Log("Value is not 1, 2, or 3");
                            break;
                    }

                }
            }
        }
    }

    public void toggle_exo()
    {
        if(show_exposure)
        {
            show_exposure = false;
        }
        else
        {
            show_exposure = true;
        }

        show_exposure_planets(show_exposure);
    }

    public void update_exo()
    {
        show_exposure_planets(show_exposure);
    }
}
