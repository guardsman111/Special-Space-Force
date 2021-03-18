using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threat_Manager : MonoBehaviour
{
    public Manager_Script modManager;
    public GameObject contentThreats;
    public GameObject contentMissions;
    public GameObject N1;
    public GameObject N2;
    public GameObject prefabThreat;
    public Planet_Script currentPlanet;
    public Threat_Script selectedThreat;

    private List<Threat_Script> threats;
    private List<Mission_Script> missions;

    public List<Threat_Script> Threats
    {
        get { return threats; }

        set
        {
            if(value != threats)
            {
                threats = value;
            }
        }
    }

    private void Start()
    {
        threats = new List<Threat_Script>();
        missions = new List<Mission_Script>();
    }

    //Sets up new planet's threats
    public void SetupThreats(Planet_Script newPlanet)
    {
        while(threats.Count > 0)
        {
            Destroy(threats[0].gameObject);
            threats.RemoveAt(0);
        }

        currentPlanet = newPlanet;

        foreach(Defined_Threat_Class dtc in currentPlanet.ThreatsOnPlanet)
        {
            GameObject temp = Instantiate(prefabThreat, contentThreats.transform);
            Threat_Script tempTS = temp.GetComponent<Threat_Script>();
            tempTS.manager = this;
            tempTS.CreateThreat(dtc);
            temp.transform.localPosition = N1.transform.localPosition;
            temp.transform.localPosition += new Vector3(0, (-200 * threats.Count), 0);
            threats.Add(tempTS);
        }
        contentThreats.GetComponent<RectTransform>().rect.Set(0,0,0, 200 * threats.Count);
    }

    //Setup new threat's missions
    public void SetupMissions(Threat_Script threat)
    {
        while (missions.Count > 0)
        {
            Destroy(missions[0].gameObject);
            missions.RemoveAt(0);
        }

        selectedThreat = threat;

    }
}
