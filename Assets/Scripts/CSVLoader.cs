using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CSVLoader : MonoBehaviour
{
    public TextAsset starDataCSV; // Assign in editor

    public List<StarData> LoadStarData()
{
    List<StarData> starList = new List<StarData>();
    string[] dataLines = starDataCSV.text.Split('\n');
    for (int i = 1; i < dataLines.Length - 1; i++)
    {
        string[] entries = dataLines[i].Split(',');
        StarData star = new StarData();

        if (float.TryParse(entries[0], out float hip))
        {
            star.HIP = (int)hip;
        }
        else
        {
            Debug.Log("Failed to parse HIP: " + entries[0]);
            continue;
        }
        if (float.TryParse(entries[1], out float dist))
        {
            star.DIST = dist;
        }
        else
        {
            Debug.Log("Failed to parse DIST: " + entries[1]);
            continue;
        }
        if (float.TryParse(entries[2], out float x0))
        {
            star.X0 = x0;
        }
        else
        {
            Debug.Log("Failed to parse X0: " + entries[2]);
            continue;
        }
        if (float.TryParse(entries[3], out float y0))
        {
            star.Y0 = y0;
        }
        else
        {
            Debug.Log("Failed to parse Y0: " + entries[3]);
            continue;
        }
        if (float.TryParse(entries[4], out float z0))
        {
            star.Z0 = z0;
        }
        else
        {
            Debug.Log("Failed to parse Z0: " + entries[4]);
            continue;
        }
        if (float.TryParse(entries[5], out float absmag))
        {
            star.ABSMAG = absmag;
        }
        else
        {
            Debug.Log("Failed to parse ABSMAG: " + entries[5]);
            continue;
        }
        if (float.TryParse(entries[6], out float mag))
        {
            star.MAG = mag;
        }
        else
        {
            Debug.Log("Failed to parse MAG: " + entries[6]);
            continue;
        }
        if (float.TryParse(entries[7], out float vx))
        {
            star.VX = vx;
        }
        else
        {
            Debug.Log("Failed to parse VX: " + entries[7]);
            continue;
        }
        if (float.TryParse(entries[8], out float vy))
        {
            star.VY = vy;
        }
        else
        {
            Debug.Log("Failed to parse VY: " + entries[8]);
            continue;
        }
        if (float.TryParse(entries[9], out float vz))
        {
            star.VZ = vz;
        }
        else
        {
            Debug.Log("Failed to parse HIP: " + entries[9]);
            continue;
        }
            star.SPECT = entries[10];

        starList.Add(star);
    }
    return starList;
}

}