using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starMover : MonoBehaviour
{
    public float time_elapsed;
    public float current_scale;
    CSV_Loader csv_Loader;
    public GameObject CSVReaderObj;
    public GameObject player;
    ConstellationLoader Constellation_Loader;
    public GameObject Constellation_Loader_obj;
    Dictionary<int, GameObject> mStarsDict;
    Dictionary<int, GameObject> allStarsDict;
    List<GameObject> starObjects;
    private Coroutine coroutine;
    public bool rev_time;

    public void ToggleRevTime()
    {
        rev_time = !rev_time;
    }
    public void scale_halve()
    {
        csv_Loader.StarHalfScaler();
        
        current_scale = current_scale/2;

        int current_Const = Constellation_Loader.current_Const;
        if (current_Const == 0)
        {
            Constellation_Loader.loadModernConstellation(true);
        }
        else if (current_Const == 1)
        {
            Constellation_Loader.loadGreekConstellation(true);
        }
        else if (current_Const == 2)
        {
            Constellation_Loader.loadChineseConstellation(true);
        }
        else if (current_Const == 3)
        {
            Constellation_Loader.loadIndianConstellation(true);
        }
        else if (current_Const == 4)
        {
            Constellation_Loader.loadEgyptConstellation(true);
        }
        else if (current_Const == 5)
        {
            Constellation_Loader.loadNoConstellation(true);
        }

        
    }
    public void scale_double()
    {
        csv_Loader.StarDoubleScaler();

        current_scale = current_scale * 2;

        int current_Const = Constellation_Loader.current_Const;
        if (current_Const == 0)
        {
            Constellation_Loader.loadModernConstellation(true);
        }
        else if (current_Const == 1)
        {
            Constellation_Loader.loadGreekConstellation(true);
        }
        else if (current_Const == 2)
        {
            Constellation_Loader.loadChineseConstellation(true);
        }
        else if (current_Const == 3)
        {
            Constellation_Loader.loadIndianConstellation(true);
        }
        else if (current_Const == 4)
        {
            Constellation_Loader.loadEgyptConstellation(true);
        }
        else if (current_Const == 5)
        {
            Constellation_Loader.loadNoConstellation(true);
        }


    }

    public void forward10K_years()
    {
        if(rev_time)
        {
            csv_Loader.ForwardYears_10K(-1);
        }
        else
        {
            csv_Loader.ForwardYears_10K(1);
        }
        
        mStarsDict = csv_Loader.StarsDict;
        Constellation_Loader = Constellation_Loader_obj.GetComponent<ConstellationLoader>();
        Debug.Log("Time ahead 10k years!");
        time_elapsed = time_elapsed+10000;
 
        Debug.Log("Time ahead by 25k years!!");
        int current_Const = Constellation_Loader.current_Const;
        if(current_Const == 0)
        {
            Constellation_Loader.loadModernConstellation(true);
        }
        else if(current_Const == 1)
        {
            Constellation_Loader.loadGreekConstellation(true);
        }
        else if( current_Const == 2)
        {
            Constellation_Loader.loadChineseConstellation(true);
        }
        else if( current_Const == 3)
        {
            Constellation_Loader.loadIndianConstellation(true);
        }
        else if( current_Const == 4)
        {
            Constellation_Loader.loadEgyptConstellation(true);
        }
        else if( current_Const == 5)
        {
            Constellation_Loader.loadNoConstellation(true);
        }

    }

    public void StartFunction()
    {
        // Start the coroutine and store a reference to it
        coroutine = StartCoroutine(InfiniteLoopFunction());
    }

    public void StopFunction()
    {
        // Stop the coroutine if it's running
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

    }

    IEnumerator InfiniteLoopFunction()
    {
        // call the Infinite function
        while (true)
        {
            forward10K_years();
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void resetBlocks()
    {
        time_elapsed = 0;
        current_scale = 1;
        player.transform.position = new Vector3(0f, -1.5f, 0.5f);
        player.transform.rotation = Quaternion.Euler(0f, -180f, 0f);

        csv_Loader.Block_Clear();


        int current_Const = Constellation_Loader.current_Const;
        if (current_Const == 0)
        {
            Constellation_Loader.loadModernConstellation(true);
        }
        else if (current_Const == 1)
        {
            Constellation_Loader.loadGreekConstellation(true);
        }
        else if (current_Const == 2)
        {
            Constellation_Loader.loadChineseConstellation(true);
        }

    }
        // Start is called before the first frame update
        void Start()
    {
        rev_time = false;
        time_elapsed = 0;
        current_scale = 1;
        starObjects = new List<GameObject>();
        csv_Loader = CSVReaderObj.GetComponent<CSV_Loader>();
        if (csv_Loader.StarsDict == null)
        {
            Debug.LogError("csv_Loader not found");
        }
        else
        {
            mStarsDict = csv_Loader.StarsDict;
        }

        Constellation_Loader = Constellation_Loader_obj.GetComponent<ConstellationLoader>();
    }
}
