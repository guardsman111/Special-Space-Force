using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Story_Manager : MonoBehaviour
{
    public Manager_Script manager;
    public FileFinder finder;

    private List<Story_Class> moveStories;
    private List<Story_Class> fightStories;
    public List<string> storyFiles;

    private Encounter_Class currentEncounter;
    private Slot_Class slot;
    private Slot_Class squad;

    public Trooper_Class currentTrooper;

    // Start is called before the first frame update
    public void Run()
    {
        moveStories = new List<Story_Class>();
        fightStories = new List<Story_Class>();

        storyFiles = finder.Retrieve("Stories.xml", ".meta");
        FindStories();
    }

    public void FindStories()
    {
        List<Story_Collection> collection = new List<Story_Collection>();

        foreach(string s in storyFiles)
        {
            collection.Add(Serializer.Deserialize<Story_Collection>(s));
        }

        foreach(Story_Collection col in collection)
        {
            foreach(Story_Class story in col.Stories)
            {
                if(story.storyType == "Move")
                {
                    moveStories.Add(story);
                }

                if (story.storyType == "Fight")
                {
                    fightStories.Add(story);
                }
            }
        }

    }

    public Story_Class FindStory(Encounter_Class encounter)
    {
        Story_Class returner = new Story_Class();

        List<int> noGood = new List<int>();

        if (encounter.stepType == "Move")
        {

            int random = Random.Range(0, moveStories.Count);

            returner = moveStories[random];

            int count = 0;

            while ((returner.nTInjuries > encounter.stepInjuredTroopers.Count || returner.nTIncapacitated > encounter.stepIncapacitatedTroopers.Count || returner.nTDeath > encounter.stepDeadTroopers.Count || returner.nEDeath > encounter.nDeadEnemies || returner.storyEnvironment != encounter.environment))
            {
                if(count > 100)
                {
                    break;
                }
                else if((returner.nTInjuries > encounter.stepInjuredTroopers.Count && returner.nTIncapacitated > encounter.stepIncapacitatedTroopers.Count && returner.nTDeath > encounter.stepDeadTroopers.Count && returner.nEDeath > encounter.nDeadEnemies) && returner.storyEnvironment == "Both")
                {
                    break;
                }
                noGood.Add(random);

                int count2 = 0;

                while (noGood.Contains(random))
                {
                    random = Random.Range(0, moveStories.Count);
                    count2 += 1;
                    if(count2 > 10)
                    {
                        break;
                    }
                }

                returner = moveStories[random];

                count += 1;
            }
        }
        else if (encounter.stepType == "Fight")
        {

            int random = Random.Range(0, fightStories.Count);

            returner = fightStories[random];

            int count = 0;

            while ((returner.nTInjuries > encounter.stepInjuredTroopers.Count || returner.nTIncapacitated > encounter.stepIncapacitatedTroopers.Count || returner.nTDeath > encounter.stepDeadTroopers.Count || returner.nEDeath > encounter.nDeadEnemies || returner.storyEnvironment != encounter.environment))
            {
                if (count > 10)
                {
                    break;
                }
                else if ((returner.nTInjuries > encounter.stepInjuredTroopers.Count && returner.nTIncapacitated > encounter.stepIncapacitatedTroopers.Count && returner.nTDeath > encounter.stepDeadTroopers.Count && returner.nEDeath > encounter.nDeadEnemies) && returner.storyEnvironment == "Both")
                {
                    break;
                }
                noGood.Add(random);

                int count2 = 0;

                while (noGood.Contains(random))
                {
                    random = Random.Range(0, moveStories.Count);
                    count2 += 1;
                    if (count2 > 10)
                    {
                        break;
                    }
                }

                returner = fightStories[random];

                count += 1;
            }
        }

        return returner;
    }

    public void Decode(Story_Class story, Encounter_Class encounter)
    {
        currentEncounter = encounter;
        slot = encounter.slots[Random.Range(0, encounter.slots.Count)];

        if (slot.squad == false)
        {
            squad = slot.containedSlots[Random.Range(0, slot.containedSlots.Count)];
            currentTrooper = squad.containedTroopers[Random.Range(0, slot.containedTroopers.Count)];
        }
        else
        {
            squad = slot;
            currentTrooper = slot.containedTroopers[Random.Range(0, slot.containedTroopers.Count)];
        }

        if (manager.combatManager.combatReadout.text != "")
        {
            manager.combatManager.combatReadout.text += "\n \n";
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
                returner = currentTrooper.trooperName;
                return returner;
            }
            if (code == "TrooperRandomName")
            {
                returner = squad.containedTroopers[Random.Range(1, squad.containedTroopers.Count)].trooperName;
                return returner;
            }
            if (code == "TrooperLastName")
            {
                string[] breakdown = currentTrooper.trooperName.Split(' ');
                returner = breakdown[1];
                return returner;
            }
            if (code == "TrooperGenderC")
            {
                if (currentTrooper.gender == 0)
                {
                    returner = "She";
                    return returner;
                }
                else 
                {
                    returner = "He";
                    return returner;
                }
            }
            if (code == "TrooperGender")
            {
                if (currentTrooper.gender == 0)
                {
                    returner = "she";
                    return returner;
                }
                else
                {
                    returner = "he";
                    return returner;
                }
            }
            if (code == "TrooperGenderReflexive")
            {
                if (currentTrooper.gender == 0)
                {
                    returner = "herself";
                    return returner;
                }
                else
                {
                    returner = "himself";
                    return returner;
                }
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
            if (code == "SquadNameFull")
            {
                if (squad != null)
                {
                    returner = slot.slotName + "'s " + squad.slotName;
                    return returner;
                }
                else
                {
                    returner = squad.slotName;
                    return returner;
                }
            }
            if (code == "SquadName")
            {
                returner = squad.slotName;
                return returner;

            }
        }

        if (code.Contains("Leader"))
        {
            if (code == "LeaderName")
            {
                returner = squad.containedTroopers[0].trooperName;
                return returner;
            }
            if (code == "LeaderLastName")
            {
                string[] breakdown = squad.containedTroopers[0].trooperName.Split(' ');
                returner = breakdown[1];
                return returner;
            }
            if (code == "LeaderRank")
            {
                returner = squad.containedTroopers[0].trooperRank;
                return returner;
            }
            if (code == "LeaderGenderObject")
            {
                if (currentTrooper.gender == 0)
                {
                    returner = "her";
                    return returner;
                }
                else
                {
                    returner = "him";
                    return returner;
                }
            }
        }

        if (code.Contains("Enemy"))
        {
            if(code == "EnemyRace")
            {
                returner = currentEncounter.enemyUnits[0].enemies[0].enemyName;
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
                        if (returner.Contains("//:"))
                        {
                            var count = returner.Count(x => x == ':');
                            if (count > 1)
                            {
                                string[] split = returner.Split(':');
                                for (int i = 0; i < split.Length; i++)
                                {
                                    split[i] = split[i].Replace("//", "");
                                    split[i] = split[i].Replace(" ", "");
                                    Debug.Log(split[i] + " " + i + " section");
                                }

                                string sentence = FindCode(split[1], split[2]);
                                returner = sentence;
                            }
                            else
                            {
                                string codes = returner.Replace("//:", "");
                                string sentence = FindCode(codes);
                                returner = sentence;
                            }
                        }
                        
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

        if (code1.Contains("Trooper"))
        {
            if (code1.Contains("Injured"))
            {
                if (code1.Contains("01"))
                {
                    if (code2 == "Name")
                    {
                        returner = currentEncounter.stepInjuredTroopers[0].trooperClass.trooperName;
                        return returner;
                    }

                    if (code2 == "WeaponHit")
                    {
                        foreach(Category_Class cat in currentEncounter.stepInjuredTroopers[0].effects[currentEncounter.stepInjuredTroopers[0].effects.Count - 1].effectingWeapon.categories)
                        {
                            if(cat.categoryName == "Weapon" && cat.categoryType == "Injure")
                            {
                                returner = cat.snippets[Random.Range(0, cat.snippets.Count)];
                            }
                        }
                    }
                }
            }
        }

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
                            if (returner.Contains("/:")) //if the returner contains a code
                            {
                                var count = returner.Count(x => x == ':');
                                List<string> sending = new List<string>();
                                //if (count > 2) //if more than one code
                                //{
                                //    string[] split = returner.Split(':'); //Split the returner 
                                //    for (int j = 0; j < split.Length; j++)
                                //    {
                                //        if (split[j].Contains(":")) //If the section contains : then add it to the sende
                                //        {
                                //            split[j] = split[j].Replace("//", "");
                                //            split[j] = split[j].Replace(" ", "");
                                //            sending.Add(split[j]);
                                //        }
                                //        Debug.Log(split[j] + " " + j + " section");
                                //    }

                                //    string sentence = FindCode(sending[0], sending[1]);

                                //    string newReturner = "";

                                //    foreach(string s in split)
                                //    {
                                //        if (!sending.Contains(s))
                                //        {
                                //            newReturner += s;
                                //        }
                                //    }

                                //    returner = newReturner;
                                //} 
                                //else 
                                //{
                                char seperator = '/';
                                string[] split = returner.Split(seperator);
                                returner = "";
                                foreach(string s in split)
                                {
                                    if (s.Contains(":"))
                                    {
                                        string codes = s.Replace(":", "");
                                        string sentence = FindCode(codes);
                                        returner += sentence;
                                    }
                                    else
                                    {
                                        returner += s;
                                    }
                                }
                                //}
                            }
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
