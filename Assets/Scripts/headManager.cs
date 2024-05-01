using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class headManager : MonoBehaviour
{
    public GameObject dashboard;
    public GameObject head_Object;
    private Vector3 headZPosition;
    private float Head_Pos;
    // Start is called before the first frame update
    void Start()
    {
        Head_Pos = head_Object.transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (head_Object != null)
        {

            headZPosition = head_Object.transform.localPosition;
            // Get the text object of the dashboard
            Text textComponent = dashboard.GetComponentInChildren<Text>();

            if (textComponent != null)
            {
                // head's local pos
                Vector3 headLocalPos = head_Object.transform.localPosition;
                Quaternion headLocalRot = head_Object.transform.localRotation;
                

                textComponent.text = ("head's position: " + headZPosition.x + " : " + headZPosition.y + " :" + headZPosition.z+"\n"+ "head orientation: " + headLocalRot.w + " : " + headLocalRot.x + " :" + headLocalRot.y+" :"+ headLocalRot.z);
            }
        }

    }

    void ForwardState()
    {
        dashboard.SetActive(true);
    }

    void BackwardState()
    {
        dashboard.SetActive(false);
    }
}
