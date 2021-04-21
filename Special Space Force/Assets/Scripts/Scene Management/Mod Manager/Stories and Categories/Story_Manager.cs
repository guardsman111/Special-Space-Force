using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Story_Manager : MonoBehaviour
{
    public Manager_Script manager;

    private Encounter_Class currentEncounter;
    private Slot_Class slot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Decode(Story_Class story, Encounter_Class encounter)
    {
        currentEncounter = encounter;
        slot = encounter.slots[Random.Range(0, encounter.slots.Count)];
        if(slot.squad == false)
        {
            slot = slot.containedSlots[Random.Range(0, slot.containedSlots.Count)];
        }
        if (manager.combatManager.combatReadout.text != "")
        {
            manager.combatManager.combatReadout.text += "\n";
        }
        foreach (string s in story.strings)
        {
            if (s.Contains("//:"))
            {
                var count = s.Count(x => x == ':');
                if(count > 1)
                {
                    string[] split = s.Split(':');
                    for(int i = 0; i < split.Length; i++)
                    {
                        split[i] = split[i].Replace("//", "");
                        split[i] = split[i].Replace(" ", "");
                        Debug.Log(split[i] + " " + i + " section");
                    }

                    string sentence = FindCode(split[1], split[2]);
                    manager.combatManager.combatReadout.text += sentence;
                }
                else
                {
                    string code = s.Replace("//:", "");
                    string sentence = FindCode(code);
                    manager.combatManager.combatReadout.text += sentence;
                }
            }
            else
            {
                if (s == "")
                {
                    manager.combatManager.combatReadout.text += " ";
                }
                else
                {
                    manager.combatManager.combatReadout.text += s;
                }
            }
        }
    }

    public string FindCode(string code) //Returns a string from a single code
    {
        string returner = "";

        if (code.Contains("Trooper"))
        {
            if (code == "TrooperName")
            {
                returner = slot.containedTroopers[Random.Range(0, slot.containedTroopers.Count)].trooperName;
                return returner;
            }
        }

        if (code.Contains("Slot"))
        {
            if (code == "HighestSlotName")
            {
                int height = 100;
                foreach(Slot_Class sc in currentEncounter.slots)
                {
                    if(sc.slotHeight < height)
                    {
                        returner = currentEncounter.slots[0].slotName;
                    }
                }
                return returner;
            }
        }

        if (code.Contains("Squad"))
        {
            if (code == "SquadName")
            {
                returner = slot.slotName;
                return returner;
            }
        }

        if (code.Contains("Leader"))
        {
            if (code == "LeaderName")
            {
                returner = slot.containedTroopers[0].trooperName;
                return returner;
            }
            if (code == "LeaderLastName")
            {
                string[] breakdown = slot.containedTroopers[0].trooperName.Split(' ');
                returner = breakdown[1];
                return returner;
            }
            if (code == "LeaderRank")
            {
                returner = slot.containedTroopers[0].trooperRank;
                return returner;
            }
        }

        if (code.Contains("Biome"))
        {

        }

        foreach(Race_Class race in manager.raceManager.Races)
        {
            if (code.Contains(race.raceName))
            {
                foreach(Category_Class category in race.Categories)
                {
                    if(code == category.categoryName)
                    {
                        returner = category.snippets[Random.Range(0, category.snippets.Count)];
                        return returner;
                    }
                }
            }
        }

        if (returner == "") //if the returner is blank check to see if there are any custom codes it matches
        {
            foreach (Category_Collection catCol in manager.categoryManager.collections)
            {
                for (int i = 0; i < catCol.Categories.Count; i++)
                {
                    if (code == catCol.Categories[i].categoryName)
                    {
                        returner = catCol.Categories[i].snippets[Random.Range(0, catCol.Categories[i].snippets.Count)];
                        return returner;
                    }
                }
            }
        }

        if (returner == "") //if still blank fill the text with an error
        {
            returner = code + " could not be found";
        }

        return returner;
    }


    public string FindCode(string code1, string code2) //Returns a string from a double code
    {
        string returner = "";

        if (code1.Contains("Squad"))
        {
        }

        if (code1.Contains("Leader"))
        {
        }

        if (code1.Contains("Biome"))
        {
            foreach(Biome_Class biome in manager.biomeManager.Biomes)
            {
                if(biome.biomeName == manager.combatManager.setupManager.SelectedPlanet.biome)
                {
                    foreach(Category_Class cat in biome.Categories)
                    {
                        if (code1 == cat.categoryName)
                        {
                            if (code2 == cat.categoryType)
                            {
                                returner = cat.snippets[Random.Range(0, cat.snippets.Count)];
                                return returner;
                            }
                        }
                    }
                }
            }
        }

        foreach (Race_Class race in manager.raceManager.Races)
        {
            if (code1.Contains(race.raceName))
            {
                foreach (Category_Class category in race.Categories)
                {
                    if (code1 == category.categoryName)
                    {
                        returner = category.snippets[Random.Range(0, category.snippets.Count)];
                    }
                }
            }
        }

        if (returner == "") //if the returner is blank check to see if there are any custom codes it matches
        {
            foreach (Category_Collection catCol in manager.categoryManager.collections)
            {
                for (int i = 0; i < catCol.Categories.Count; i++)
                {
                    if (code1 == catCol.Categories[i].categoryName)
                    {
                        if (code2 == catCol.Categories[i].categoryType)
                        {
                            returner = catCol.Categories[i].snippets[Random.Range(0, catCol.Categories[i].snippets.Count)];
                            return returner;
                        }
                    }
                }
            }
        }

        if (returner == "") //if still blank fill the text with an error
        {
            returner = code1 + " " + code2 + " could not be found";
        }

        return returner;
    }
}
