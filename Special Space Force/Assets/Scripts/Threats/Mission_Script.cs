using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Script : MonoBehaviour
{
    private Mission_Class missionC;

    public Mission_Class MissionC
    {
        get { return missionC; }
        set
        {
            if (value != missionC)
            {
                missionC = value;
            }
        }
    }
}
