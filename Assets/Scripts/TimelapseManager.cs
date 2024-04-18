using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class TimelapseManager : MonoBehaviour
{
    private Text textComponent;
    public GameObject textBox;
    public GameObject spaceShip;
    public GameObject starMove_Obj;
    starMover starMoverScript;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = textBox.GetComponent<Text>();
        starMoverScript = starMove_Obj.GetComponent<starMover>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(spaceShip.transform.position, Vector3.zero);
        string formattedDistance = distance.ToString("F2");
        string infoxBox = "----Dashboard----";
        string dist  = "sol distance: "+ formattedDistance + " parsecs";
        string time = "time elapsed: " + starMoverScript.time_elapsed.ToString() + "years";
        string scale =  "scale: " + starMoverScript.current_scale.ToString() + "parsecs/feet";
        textComponent.text = infoxBox + "\n" + dist + "\n" + time + "\n" + scale;
    }

}
