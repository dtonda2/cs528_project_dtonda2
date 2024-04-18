using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Billboard : MonoBehaviour
{
    public float gizmoSize = 0.5f;
    public GameObject ship_target;
    private GameObject[] stars;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckStarsCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(stars != null )
        {
            foreach (GameObject star in stars)
            {
                if (star != null)
                {
                    Gizmos.DrawSphere(star.transform.position, gizmoSize);
                }
            }

        }
 
    }

    // Coroutine to check stars every 3 seconds
    // Coroutine to check stars every 3 seconds
    IEnumerator CheckStarsCoroutine()
    {
        while (true)
        {
            // Find all GameObjects with the "star" tag
            stars = GameObject.FindGameObjectsWithTag("star");
            //ship_target = GameObject.FindGameObjectWithTag("head");
            foreach (GameObject star in stars)
            {
                if (star != null && ship_target != null)
                {
                    // Face the star towards the ship_target
                    star.transform.rotation = Quaternion.LookRotation(star.transform.position - ship_target.transform.position);
                }
            }
            Debug.Log("stars count: " + stars.Length);
            // Wait for 3 seconds before checking stars again
            yield return new WaitForSeconds(3f);
        }
    }
}
