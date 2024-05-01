using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CAVE2ClusterManager;

public class ColorManager : MonoBehaviour
{
    // mat array 1
    public Material[] StarMaterials = new Material[7];

    // mat array 2
    public Material[] Star2Materials = new Material[7];

    public bool Exp_Show_b;
    void Start()
    {
        Exp_Show_b = false;
    }

    public void Exp_Show_P(bool showexo)
    {
        GameObject[] stars = GameObject.FindGameObjectsWithTag("star"); // Get all game objects tagged with "star"

        foreach (GameObject star in stars)
        {
            // Accessing the starmeta data script
            Star_Data starScript = star.GetComponent<Star_Data>();

            if (starScript != null)
            {
                if (Exp_Show_b)
                {
                    int P_countS = starScript.planet_count;
                    Renderer StarMatRenderer = star.GetComponentInChildren<Renderer>();

                    switch (P_countS)
                    {
                        case 0:
                            StarMatRenderer.material = Star2Materials[0];
                            break;

                        case 1:
                            StarMatRenderer.material = Star2Materials[1];
                            break;

                        case 2:
                            StarMatRenderer.material = Star2Materials[2];
                            break;

                        case 3:
                            StarMatRenderer.material = Star2Materials[3];
                            break;

                        case 4:
                            StarMatRenderer.material = Star2Materials[4];
                            break;

                        case 5:
                            StarMatRenderer.material = Star2Materials[5];
                            break;

                        case 6:
                            StarMatRenderer.material = Star2Materials[6];
                            break;

                        default:

                            Debug.Log("Value not Set");
                            break;
                    }
                }
                else
                {
                    char spectrum_S = starScript.spectrum;
                    Renderer StarMatRenderer = star.GetComponentInChildren<Renderer>();
                    switch (spectrum_S)
                    {
                        case 'A':
                            StarMatRenderer.material = StarMaterials[2];
                            break;

                        case 'B':
                            StarMatRenderer.material = StarMaterials[1];
                            break;

                        case 'F':
                            StarMatRenderer.material = StarMaterials[3];
                            break;

                        case 'G':
                            StarMatRenderer.material = StarMaterials[4];
                            break;

                        case 'K':
                            StarMatRenderer.material = StarMaterials[5];
                            break;

                        case 'M':
                            StarMatRenderer.material = StarMaterials[6];
                            break;

                        case 'O':
                            StarMatRenderer.material = StarMaterials[0];
                            break;

                        default:
                            Debug.Log("Value not Set");
                            break;
                    }

                }
            }
        }
    }

    public void toggle_exo()
    {
        if(Exp_Show_b)
        {
            Exp_Show_b = false;
        }
        else
        {
            Exp_Show_b = true;
        }

        Exp_Show_P(Exp_Show_b);
    }

    public void update_exo()
    {
        Exp_Show_P(Exp_Show_b);
    }
}
