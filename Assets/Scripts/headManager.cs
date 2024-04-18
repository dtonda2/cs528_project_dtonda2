using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class headManager : MonoBehaviour
{
    public GameObject dashboard;
    public GameObject headObject;
    private Vector3 headZPosition;
    private float startingHeadPos;
    // Start is called before the first frame update
    void Start()
    {
        startingHeadPos = headObject.transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (headObject != null)
        {

            headZPosition = headObject.transform.localPosition;
            // Get the text object of the dashboard
            Text textComponent = dashboard.GetComponentInChildren<Text>();

            // Set the local position of the text object to match the head's local position
            if (textComponent != null)
            {
                // Get the head's local position
                Vector3 headLocalPosition = headObject.transform.localPosition;
                Quaternion headLocalRotation = headObject.transform.localRotation;
                // Set the text of the text component
                textComponent.text = ("head position: " + headZPosition.x + " : " + headZPosition.y + " :" + headZPosition.z+"\n"+ "head orientation: " + headLocalRotation.w + " : " + headLocalRotation.x + " :" + headLocalRotation.y+" :"+ headLocalRotation.z);
            }

            /*
            if (headZPosition > startingHeadPos)
            {
                //SetStateForward();
            }
            else
            {
                //SetStateBackward();
            }
            */
        }

    }

    void SetStateForward()
    {
        // Implement code to set the state to forward
        Debug.Log("set to forward"+ headZPosition);
        dashboard.SetActive(true);
    }

    void SetStateBackward()
    {
        // Implement code to set the state to backward
        Debug.Log("set to backward"+ headZPosition);
        dashboard.SetActive(false);
    }
}
