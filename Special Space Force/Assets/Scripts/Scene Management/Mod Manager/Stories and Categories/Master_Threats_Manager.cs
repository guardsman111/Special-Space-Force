using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Threats_Manager : MonoBehaviour
{
    private List<Threat_Class> threats;

    public List<Threat_Class> Threats
    {
        get { return threats; }

        set
        {
            if (value != threats)
            {
                threats = value;
            }
        }
    }

    private void Start()
    {
        threats = new List<Threat_Class>();
    }

    public void AddThreats(List<Threat_Class> newThreats)
    {
        foreach(Threat_Class t in newThreats)
        {
            threats.Add(t);
        }
    }

}
