using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class TimelapseManager : MonoBehaviour
{
    private Text textComponent;
    public GameObject Sol_Dist_text;
    public GameObject Cave2_Player;
    public GameObject starMove_Obj;
    starMover Star_Move;
    void Start()
    {
        textComponent = Sol_Dist_text.GetComponent<Text>();
        Star_Move = starMove_Obj.GetComponent<starMover>();
    }

    void Update()
    {
        float distance = Vector3.Distance(Cave2_Player.transform.position, Vector3.zero);
        string formattedDistance = distance.ToString("F2");
        string infoxBox = "----Dashboard----";
        string dist  = "sol distance: "+ formattedDistance + " parsecs";
        string time = "time elapsed: " + Star_Move.time_elapsed.ToString() + "years";
        string scale =  "scale: " + Star_Move.current_scale.ToString() + "parsecs/feet";
        textComponent.text = infoxBox + "\n" + dist + "\n" + time + "\n" + scale;
    }

}
