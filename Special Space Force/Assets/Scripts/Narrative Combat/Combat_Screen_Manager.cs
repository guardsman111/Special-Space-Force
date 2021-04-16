using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Screen_Manager : MonoBehaviour
{
    public RawImage background;
    public TextMeshProUGUI title;
    public Text combatReadout;
    public Button nextStep;
    public Slider balance;
    public Combat_Setup_Manager setupManager;
    public List<Combat_Slot_Script> selectedSlots;
    public Encounter_Manager eManager;

    public void StartCombatScreen(Planet_Script planet, Mission_Script mission, List<Combat_Slot_Script> selecteds)
    {
        background.texture = planet.Stats.Biome.GetImageForBiome();
        title.text = mission.missionName.text + " on " + planet.planetName;
        selectedSlots = selecteds;
        eManager.CreateEncounters(selecteds, mission);
    }

    public void Close()
    {
        setupManager.CloseManager();
        gameObject.SetActive(false);
    }
}
