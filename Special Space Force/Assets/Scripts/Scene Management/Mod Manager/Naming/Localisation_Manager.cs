using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Localisation_Manager : MonoBehaviour
{
    /// <summary>
    /// Manages the localisation files that can be inputted into the game. Loads files from resources and breaks them into strings.
    /// </summary>
    public FileFinder finder;

    public List<String_List_Class> trooperNameStrings;
    public List<String_List_Class> hierachyNameStrings;
    public List<String_List_Class> slotNameStrings;
    public List<String_List_Class> fleetNameStrings;
    public List<String_List_Class> craftNameStrings;

    public String_List_Class chosenTrooperNamesList;
    public String_List_Class chosenHierachyNamesList;
    public String_List_Class chosenSlotNamesList;
    public String_List_Class chosenFleetNamesList;
    public String_List_Class chosenCraftNamesList;
    public List<string> surnames;
    public List<string> forenamesM;
    public List<string> forenamesF;

    public List<string> hierachyNames;
    public List<string> squadHierachyNames;

    public List<string> slotNames;
    public List<string> squadNames;
    public List<string> fleetNames;
    public List<string> fleetHierachyNames;
    public List<string> craftNames;
    public string craftPrefix;

    public Dropdown trooperNamesDropdown;
    public Dropdown hierachyNamesDropdown;
    public Dropdown slotNamesDropdown;
    public Dropdown fleetNamesDropdown;
    public Dropdown craftNamesDropdown;

    public InputField forceName;

    //Finds the localisation files from the resources folder and filters them by type. Expandable
    public void FindLocalisationFiles()
    {
        SaveDefaults();
        trooperNameStrings = new List<String_List_Class>();
        hierachyNameStrings = new List<String_List_Class>();
        slotNameStrings = new List<String_List_Class>();
        fleetNameStrings = new List<String_List_Class>();
        craftNameStrings = new List<String_List_Class>();
        List<string> fileLocations = finder.Retrieve("Localisation", ".meta");
        List<string> trooperNames = new List<string>();
        List<string> hierachyNames = new List<string>();
        List<string> squadHierachyNames = new List<string>();
        List<string> slotNames = new List<string>();
        List<string> squadNames = new List<string>();
        List<string> fleetNames = new List<string>();
        List<string> craftNames = new List<string>();

        foreach (string s in fileLocations)
        {
            try
            {
                String_List_Class temp = Serializer.Deserialize<String_List_Class>(s);

                if (temp.listType == "TrooperNames")
                {
                    trooperNameStrings.Add(temp);
                    trooperNames.Add(temp.name);
                }
                else if (temp.listType == "HierachyNames")
                {
                    hierachyNameStrings.Add(temp);
                    hierachyNames.Add(temp.name);
                }
                else if (temp.listType == "SlotNames")
                {
                    slotNameStrings.Add(temp);
                    slotNames.Add(temp.name);
                }
                else if (temp.listType == "FleetNames")
                {
                    fleetNameStrings.Add(temp);
                    fleetNames.Add(temp.name);
                }
                else if (temp.listType == "VoidcraftNames")
                {
                    craftNameStrings.Add(temp);
                    craftNames.Add(temp.name);
                }
            }
            catch 
            {
                Debug.Log("This File Failed: " + s);
            }
        }

        trooperNamesDropdown.ClearOptions();
        trooperNamesDropdown.AddOptions(trooperNames);
        ChangedTemplateDropdown(trooperNamesDropdown);

        hierachyNamesDropdown.ClearOptions();
        hierachyNamesDropdown.AddOptions(hierachyNames);
        ChangedTemplateDropdown(hierachyNamesDropdown);

        slotNamesDropdown.ClearOptions();
        slotNamesDropdown.AddOptions(slotNames);
        ChangedTemplateDropdown(slotNamesDropdown);

        fleetNamesDropdown.ClearOptions();
        fleetNamesDropdown.AddOptions(fleetNames);
        ChangedTemplateDropdown(fleetNamesDropdown);

        craftNamesDropdown.ClearOptions();
        craftNamesDropdown.AddOptions(craftNames);
        ChangedTemplateDropdown(craftNamesDropdown);
    }

    //Handles changes to the dropdowns on the customisation menu
    public void ChangedTemplateDropdown(Dropdown dropdown)
    {
        if (dropdown.name == "Trooper Names")
        {
            chosenTrooperNamesList = trooperNameStrings[dropdown.value];
        } 
        else if (dropdown.name == "Hierachy Names")
        {
            chosenHierachyNamesList = hierachyNameStrings[dropdown.value];
        }
        else if (dropdown.name == "Slot Names")
        {
            chosenSlotNamesList = slotNameStrings[dropdown.value];
        }
        else if (dropdown.name == "Fleet Names")
        {
            chosenFleetNamesList = fleetNameStrings[dropdown.value];
        }
        else if (dropdown.name == "Craft Names")
        {
            chosenCraftNamesList = craftNameStrings[dropdown.value];
        }
    }

    //Returns the names of all the currently selected string list classes
    public List<string> FindChosenLocalisation()
    {
        List<string> localisations = new List<string>();

        if(chosenTrooperNamesList == null)
        {
            LoadStringListClass(trooperNamesDropdown.options[trooperNamesDropdown.value].text, "TrooperNames");
        }
        if (chosenHierachyNamesList == null)
        {
            LoadStringListClass(hierachyNamesDropdown.options[hierachyNamesDropdown.value].text, "HierarchyNames");
        }
        if (chosenSlotNamesList == null)
        {
            LoadStringListClass(slotNamesDropdown.options[slotNamesDropdown.value].text, "SlotNames");
        }
        if (chosenFleetNamesList == null)
        {
            LoadStringListClass(fleetNamesDropdown.options[fleetNamesDropdown.value].text, "FleetNames");
        }
        if (chosenCraftNamesList == null)
        {
            LoadStringListClass(craftNamesDropdown.options[craftNamesDropdown.value].text, "CraftNames");
        }

        localisations.Add(chosenTrooperNamesList.name);
        localisations.Add(chosenHierachyNamesList.name);
        localisations.Add(chosenSlotNamesList.name);
        localisations.Add(chosenFleetNamesList.name);
        localisations.Add(chosenCraftNamesList.name);

        return localisations;
    }

    //Finds the selected string list from the given name
    public void LoadStringListClass(string name, string type)
    {
        if (type == "TrooperNames")
        {
            foreach (String_List_Class slc in trooperNameStrings)
            {
                if (slc.name == name)
                {
                    chosenTrooperNamesList = slc;
                }
            }
        }
        else if (type == "HierachyNames")
        {
            foreach (String_List_Class slc in hierachyNameStrings)
            {
                if (slc.name == name)
                {
                    chosenHierachyNamesList = slc;
                }
            }
        }
        else if (type == "SlotNames")
        {
            foreach (String_List_Class slc in slotNameStrings)
            {
                if (slc.name == name)
                {
                    chosenTrooperNamesList = slc;
                }
            }
        }
        else if (type == "FleetNames")
        {
            foreach (String_List_Class slc in fleetNameStrings)
            {
                if (slc.name == name)
                {
                    chosenFleetNamesList = slc;
                }
            }
        }
        else if (type == "CraftNames")
        {
            foreach (String_List_Class slc in craftNameStrings)
            {
                if (slc.name == name)
                {
                    chosenCraftNamesList = slc;
                }
            }
        }
        else 
        {
            Debug.Log("Couldn't Find Localisation called " + name);
        }
    }

    //Seperates the string lists into individual strings
    public void SeperateStringLists()
    {
        int sectionCounter = -1;
        surnames = new List<string>();
        forenamesM = new List<string>();
        forenamesF = new List<string>();
        slotNames = new List<string>();
        hierachyNames = new List<string>();
        fleetNames = new List<string>();
        fleetHierachyNames = new List<string>();

        foreach (string s in chosenTrooperNamesList.stringList)
        {
            if (sectionCounter == -1)
            {
                if (s.Contains("/Surnames"))
                {
                    sectionCounter = 0;
                }
                else
                {
                    surnames.Add(s);
                }
            }
            else if (sectionCounter == 0)
            {
                if (s.Contains("/"))
                {
                    sectionCounter = 1;
                }
                else
                {
                    surnames.Add(s);
                }
            }
            else if (sectionCounter == 1)
            {
                if (s.Contains("/"))
                {
                    sectionCounter = 2;
                }
                else
                {
                    forenamesM.Add(s);
                }
            }
            else if (sectionCounter == 2)
            {
                forenamesF.Add(s);
            }
        }
        
        sectionCounter = -1;
        foreach (string s in chosenSlotNamesList.stringList)
        {
            if (sectionCounter == -1)
            {
                if (s.Contains("/Slots"))
                {
                    sectionCounter = 0;
                }
                else
                {
                    slotNames.Add(s);
                }
            }
            else if (sectionCounter == 0)
            {
                if (s.Contains("/"))
                {
                    sectionCounter = 1;
                }
                else
                {
                    slotNames.Add(s);
                }
            }
            else if (sectionCounter == 1)
            {
                squadNames.Add(s);
            }
        }

        sectionCounter = -1;
        foreach (string s in chosenHierachyNamesList.stringList)
        {
            if (sectionCounter == -1)
            {
                if (s.Contains("/Slots"))
                {
                    sectionCounter = 0;
                }
                else
                {
                    hierachyNames.Add(s);
                }
            }
            else if (sectionCounter == 0)
            {
                if (s.Contains("/"))
                {
                    sectionCounter = 1;
                }
                else
                {
                    hierachyNames.Add(s);
                }
            }
            else if (sectionCounter == 1)
            {
                squadHierachyNames.Add(s);
            }
        }

        sectionCounter = -1;
        foreach (string s in chosenFleetNamesList.stringList)
        {
            if (sectionCounter == -1)
            {
                if (s.Contains("/Fleet"))
                {
                    sectionCounter = 0;
                }
                else
                {
                    fleetNames.Add(s);
                }
            }
            else if (sectionCounter == 0)
            {
                if (s.Contains("/"))
                {
                    sectionCounter = 1;
                }
                else
                {
                    fleetNames.Add(s);
                }
            }
            else if (sectionCounter == 1)
            {
                if (s.Contains("/"))
                {
                    sectionCounter = 2;
                }
                else
                {
                    fleetHierachyNames.Add(s);
                }
            }
        }
        sectionCounter = -1;
        foreach (string s in chosenCraftNamesList.stringList)
        {
            if (sectionCounter == -1)
            {
                if (s.Contains("/Craft"))
                {
                    sectionCounter = 0;
                }
                else
                {
                    craftPrefix = s;
                }
            }
            else if (sectionCounter == 0)
            {
                if (s.Contains("/"))
                {
                    sectionCounter = 1;
                }
                else
                {
                    craftNames.Add(s);
                }
            }
        }
    }

    //Creates trooper name
    public string CreateTrooperName(string type, int integer)
    {
        string name = "Name";
        if (type == "TrooperNames")
        {
            if (integer == 0)
            {
                name = forenamesF[UnityEngine.Random.Range(0, forenamesF.Count)];
            }
            else if (integer == 1)
            {
                name = forenamesM[UnityEngine.Random.Range(0, forenamesM.Count)];
            }
            else
            {
                Debug.Log("Gender Int out of range");
            }
            name += " " + surnames[UnityEngine.Random.Range(0, surnames.Count)];
        } 
        else if (type == "TrooperNames")
        {

        }
        else
        {
            Debug.Log("Type was not recognized");
        }

        return name;
    }

    //Creates slot name
    public string CreateName(string type, Slot_Script slot)
    {
        string name = "Name";
        if (type == "SlotNames")
        {
            int number = slot.ID - 1;

            string firstString;
            string secondString;



            if (slot.squad)
            {
                firstString = squadNames[number];
                secondString = squadHierachyNames[0];

                name = firstString + " " + secondString;
            } 
            else
            {
                if (number >= 0)
                {
                    firstString = slotNames[number];
                    try
                    {
                        secondString = hierachyNames[slot.slotHeight];
                    }
                    catch
                    {
                        secondString = "Sub - " + hierachyNames[hierachyNames.Count - 1];
                        if (number == 0)
                        {
                            firstString = forceName.text;
                            secondString = "";
                        }
                    }
                    name = firstString + " " + secondString;
                }
            }
        }
        else
        {
            Debug.Log("Type was not recognized");
        }

        return name;
    }

    //Creates slot name
    public string CreateName(string type, Fleet_Script fleet)
    {
        string name = "Name";
        if (type == "FleetNames")
        {
            int number = fleet.ID;

            string firstString;
            string secondString;
            secondString = fleetHierachyNames[0];

            if (number >= 0)
            {
                firstString = fleetNames[number - 1];
                name = firstString + " " + secondString;
            }
            
        }
        else
        {
            Debug.Log("Type was not recognized");
        }

        return name;
    }
    //Creates slot name
    public string CreateName(string type, Voidcraft_Script craft)
    {
        string name = "Name";
        if (type == "CraftNames")
        {
            string secondString;

            int random = UnityEngine.Random.Range(0, craftNames.Count);
            secondString = craftNames[random];

            name = craftPrefix + " " + secondString;

        }
        else
        {
            Debug.Log("Type was not recognized");
        }

        return name;
    }

    //Creates default trooper names String Lists
    public void DefaultTrooperNames()
    {
        //English Troopers List
        String_List_Class tempSL = new String_List_Class();
        tempSL.name = "English Names";
        tempSL.listType = "TrooperNames";
        tempSL.stringList = new List<string>
        {
            "/Surnames",
            "Abbot", "Aberman", "Achilles", "Addington", "Ainsley", "Arnold", "Auberry",
            "Backman", "Baker", "Bennett", "Bradley", "Bradley", "Butler",
            "Carter", "Chapman", "Cole", "Cooper",
            "Davidson", "Dawson", "Dixon", "Dodds",
            "Earhart", "Edwards", "Elliott", "Evans",
            "Fisher", "Fletcher", "Ford", "Foster",
            "Gibson", "Graham", "Grant", "Griffiths",
            "Hall", "Harris", "Henderson", "Hughes",
            "Idle", "Irish", "Izard",
            "Jackson", "Jenkins", "Johnston", "Jones",
            "Kelly", "Kerbain", "King", "Knight",
            "Lawrence", "Lee", "Lewis", "Lloyd",
            "Marshall", "Martin", "Miller", "Moore", "Morgan", "Morris", "Murphy",
            "Needleman", "Nicholes", "Nutley",
            "O’Bryan", "O’Hara", "Oddy", "Owen",
            "Palmer", "Parsons", "Pearson", "Phillips", "Price",
            "Queer", "Quigg", "Quill",
            "Reid", "Reynolds", "Roberts", "Rogers", "Rose", "Ross", "Russell",
            "Saunders", "Scott", "Simpson", "Smith", "Smith", "Smith", "Spencer", "Stevens", "Stewart",
            "Taylor", "Taylor", "Thomas", "Thompson", "Turner",
            "Underhill",
            "Valor", "Vickers",
            "Walker", "Walsh", "Webb", "West", "White", "Wilkinson", "Williamson", "Wood", "Wright",
            "Yank", "Young",
            "/ForenamesMale",
            "Aaron", "Adam", "Alex", "Alexander", "Alfie", "Albert", "Archie", "Arthur",
            "Blake", "Billy", "Bobby",
            "Callum", "Charlie", "Charles",
            "Daniel", "David", "Dexter", "Dylan",
            "Ethan", "Edward", "Elliot", "Ellis", "Elijah",
            "Freddie", "Finley", "Frank", "Frankie", "Frederick",
            "Gabriel", "George",
            "Harry", "Harrison", "Harvey", "Henry", "Hugo",
            "Isaac",
            "Jack", "Jacob", "Jake", "James", "Jamie", "Jaxon", "Joshua", "Joseph", "Jude",
            "Kai", "Kyle",
            "Leo", "Lewis", "Liam", "Logan", "Louie", "Lucas", "Luke",
            "Max", "Mason", "Matthew", "Michael",
            "Nathan", "Noah",
            "Oliver", "Ollie", "Oscar",
            "Patrick", "Peter",
            "Quin",
            "Reuben", "Riley", "Rory", "Roman", "Ryan",
            "Samuel", "Sebastien", "Seth", "Sonny", "Stanley",
            "Teddy", "Theo", "Thomas", "Toby", "Tommy", "Tyler",
            "Victor",
            "William", "William", "Will",
            "Xander",
            "Zach", "Zachary", "Zak",
            "/ForenamesFemale",
            "Abigail", "Alexandra", "Alice", "Amelia", "Ava",
            "Beatrice", "Bethany",
            "Charlotte", "Chloe", "Connie",
            "Daisy", "Darcie",
            "Eleanor", "Ella", "Emily", "Erin", "Eva", "Eve", "Evie",
            "Florence", "Freya",
            "Georgia", "Georgina", "Grace",
            "Harrison", "Harriet", "Holly",
            "Isla", "Imogen", "Isabella", "Ivy",
            "Jackie", "Jacolyn", "Jasmine", "Jess", "Jessica",
            "Katherine", "Katie", "Kayley",
            "Layla", "Lily", "Lola", "Lucy",
            "Matilda", "Maya", "Masie", "Mia", "Millie", "Molly",
            "Nancy", "Nicole",
            "Olivia", "Orla",
            "Paige", "Penelope", "Poppy",
            "Rachel", "Rosie", "Ruby",
            "Scarlett", "Sienna", "Sophia", "Sophie", "Summer",
            "Tara", "Tilly",
            "Victoria",
            "Wendy", "Willow",
            "Zara", "Zoe",
        };

        var file = File.Create(finder.defaultPath + "/Localisation/Trooper Names/EnglishLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Localisation/Trooper Names/EnglishLocalisation.xml");

        //English Troopers List
        tempSL = new String_List_Class();
        tempSL.name = "American Names";
        tempSL.listType = "TrooperNames";
        tempSL.stringList = new List<string>
        {
            "/Surnames",
            "Abbot", "Aberman", "Achilles", "Addington", "Ainsley", "Arnold", "Auberry",
            "Backman", "Baker", "Bennett", "Bradley", "Bradley", "Butler",
            "Carter", "Chapman", "Cole", "Cooper",
            "Davidson", "Dawson", "Dixon", "Dodds",
            "Earhart", "Edwards", "Elliott", "Evans",
            "Fisher", "Fletcher", "Ford", "Foster",
            "Gibson", "Graham", "Grant", "Griffiths",
            "Hall", "Harris", "Henderson", "Hughes",
            "Idle", "Irish", "Izard",
            "Jackson", "Jenkins", "Johnston", "Jones",
            "Kelly", "Kerbain", "King", "Knight",
            "Lawrence", "Lee", "Lewis", "Lloyd",
            "Marshall", "Martin", "Miller", "Moore", "Morgan", "Morris", "Murphy",
            "Needleman", "Nicholes", "Nutley",
            "O’Bryan", "O’Hara", "Oddy", "Owen",
            "Palmer", "Parsons", "Pearson", "Phillips", "Price",
            "Queer", "Quigg", "Quill",
            "Reid", "Reynolds", "Roberts", "Rogers", "Rose", "Ross", "Russell",
            "Saunders", "Scott", "Simpson", "Smith", "Smith", "Smith", "Spencer", "Stevens", "Stewart",
            "Taylor", "Taylor", "Thomas", "Thompson", "Turner",
            "Underhill",
            "Valor", "Vickers",
            "Walker", "Walsh", "Webb", "West", "White", "Wilkinson", "Williamson", "Wood", "Wright",
            "Yank", "Young",
            "/ForenamesMale",
            "Aaron", "Adam", "Alex", "Alexander", "Alfie", "Albert", "Archie", "Arthur",
            "Blake", "Billy", "Bobby",
            "Callum", "Charlie", "Charles",
            "Daniel", "David", "Dexter", "Dylan",
            "Ethan", "Edward", "Elliot", "Ellis", "Elijah",
            "Freddie", "Finley", "Frank", "Frankie", "Frederick",
            "Gabriel", "George",
            "Harry", "Harrison", "Harvey", "Henry", "Hugo",
            "Isaac",
            "Jack", "Jacob", "Jake", "James", "Jamie", "Jaxon", "Joshua", "Joseph", "Jude",
            "Kai", "Kyle",
            "Leo", "Lewis", "Liam", "Logan", "Louie", "Lucas", "Luke",
            "Max", "Mason", "Matthew", "Michael",
            "Nathan", "Noah",
            "Oliver", "Ollie", "Oscar",
            "Patrick", "Peter",
            "Quin",
            "Reuben", "Riley", "Rory", "Roman", "Ryan",
            "Samuel", "Sebastien", "Seth", "Sonny", "Stanley",
            "Teddy", "Theo", "Thomas", "Toby", "Tommy", "Tyler",
            "Victor",
            "William", "William", "Will",
            "Xander",
            "Zach", "Zachary", "Zak",
            "/ForenamesFemale",
            "Abigail", "Alexandra", "Alice", "Amelia", "Ava",
            "Beatrice", "Bethany",
            "Charlotte", "Chloe", "Connie",
            "Daisy", "Darcie",
            "Eleanor", "Ella", "Emily", "Erin", "Eva", "Eve", "Evie",
            "Florence", "Freya",
            "Georgia", "Georgina", "Grace",
            "Harrison", "Harriet", "Holly",
            "Isla", "Imogen", "Isabella", "Ivy",
            "Jackie", "Jacolyn", "Jasmine", "Jess", "Jessica",
            "Katherine", "Katie", "Kayley",
            "Layla", "Lily", "Lola", "Lucy",
            "Matilda", "Maya", "Masie", "Mia", "Millie", "Molly",
            "Nancy", "Nicole",
            "Olivia", "Orla",
            "Paige", "Penelope", "Poppy",
            "Rachel", "Rosie", "Ruby",
            "Scarlett", "Sienna", "Sophia", "Sophie", "Summer",
            "Tara", "Tilly",
            "Victoria",
            "Wendy", "Willow",
            "Zara", "Zoe",
        };

        file = File.Create(finder.defaultPath + "/Localisation/Trooper Names/AmericanLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Localisation/Trooper Names/AmericanLocalisation.xml");
    }

    //Creates default Slot names String Lists
    public void DefaultSlotNames()
    {
        //Number Slots List
        String_List_Class tempSL = new String_List_Class();
        tempSL.name = "Number Slots";
        tempSL.listType = "SlotNames";
        tempSL.stringList = new List<string>
        {
            "/Slots",
            "1st",
            "2nd",
            "3rd",
            "4th",
            "5th",
            "6th",
            "7th",
            "8th",
            "9th",
            "/Squads",
            "1st",
            "2nd",
            "3rd",
            "4th",
            "5th",
            "6th",
            "7th",
            "8th",
            "9th",
        };

        var file = File.Create(finder.defaultPath + "/Localisation/Slot Names/NumberLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Localisation/Slot Names/NumberLocalisation.xml");

        //Colour Slots List
        tempSL = new String_List_Class();
        tempSL.name = "Colour Slots";
        tempSL.listType = "SlotNames";
        tempSL.stringList = new List<string>
        {
            "/Slots",
            "White",
            "Red",
            "Blue",
            "Green",
            "Black",
            "Yellow",
            "Purple",
            "Orange",
            "Cyan",
            "/Squads",
            "White",
            "Red",
            "Blue",
            "Green",
            "Black",
            "Yellow",
            "Purple",
            "Orange",
            "Cyan",
        };

        file = File.Create(finder.defaultPath + "/Localisation/Slot Names/ColourLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Localisation/Slot Names/ColourLocalisation.xml");

        //Alphabetical Slots List
        tempSL = new String_List_Class();
        tempSL.name = "Alphabet Slots";
        tempSL.listType = "SlotNames";
        tempSL.stringList = new List<string>
        {
            "/Slots",
            "Alpha",
            "Bravo",
            "Charlie",
            "Delta",
            "Echo",
            "Foxtrot",
            "Gamma",
            "Hotel",
            "India",
            "/Squads",
            "Able",
            "Baker",
            "Charlie",
            "Dog",
            "Easy",
            "Fox",
            "Golf",
            "Hotel",
            "Indigo",
        };

        file = File.Create(finder.defaultPath + "/Localisation/Slot Names/AlphabeticalLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Localisation/Slot Names/AlphabeticalLocalisation.xml");
    }

    //Creates default hierarchy names String Lists
    public void DefaultHierachyNames()
    {
        //US Hierachy List
        String_List_Class tempSL = new String_List_Class();
        tempSL.name = "American Hierachy";
        tempSL.listType = "HierachyNames";
        tempSL.stringList = new List<string>
        {
            "/Slots",
            "Battalion",
            "Company",
            "Platoon",
            "Squad",
            "/Squads",
            "Section",
        };

        var file = File.Create(finder.defaultPath + "/Localisation/Hierachy Names/AmericanLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Localisation/Hierachy Names/AmericanLocalisation.xml");

        //British Hierachy List
        tempSL = new String_List_Class();
        tempSL.name = "British Hierachy";
        tempSL.listType = "HierachyNames";
        tempSL.stringList = new List<string>
        {
            "/Slots",
            "Battalion",
            "Company",
            "Platoon",
            "/Squads",
            "Squad",
        };

        file = File.Create(finder.defaultPath + "/Localisation/Hierachy Names/BritishLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Localisation/Hierachy Names/BritishLocalisation.xml");
    }

    //Creates default craft names String Lists
    public void DefaultCraftNames()
    {
        //US Hierachy List
        String_List_Class tempSL = new String_List_Class();
        tempSL.name = "American Craft";
        tempSL.listType = "VoidcraftNames";
        tempSL.stringList = new List<string>
        {
            "/Prefix",
            "USS",
            "/Craft",
            "Abraham Lincoln",
            "America",
            "Barrack Obama",
            "Benfold",
            "California",
            "Colorado",
            "Devastator",
            "Eviee",
            "Essex",
            "Florida",
            "Freedom",
            "Gerald R. Ford",
            "George Washington",
            "Gladiator",
            "Georgia",
            "Harry S. Truman",
            "Hopper",
            "Independence",
            "Iwo Jima",
            "Ivanka Trump",
            "Jefferson",
            "Jimmy Carter",
            "Kentucky",
            "Kidd",
            "Lassen",
            "Louisiana",
            "Maine",
            "Mississippi",
            "New Mexico",
            "New York",
            "Nimitz",
            "Ohio",
            "Omaha",
            "Patriot",
            "Pearl Harbour",
            "Questor",
            "Ronald Reagan",
            "Roosevelt",
            "Sentry",
            "Springfield",
            "Tom Kirkman",
            "Theodore Roosevelt",
            "Umbrella",
            "Vermont",
            "Virginia",
            "Washington",
            "West Virginia",
            "Wyoming",
            "Zephr",
            "Zumwalt",
        };

        var file = File.Create(finder.defaultPath + "/Localisation/Craft Names/AmericanLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Localisation/Craft Names/AmericanLocalisation.xml");


        //British Hierachy List
        tempSL = new String_List_Class();
        tempSL.name = "British Craft";
        tempSL.listType = "VoidcraftNames";
        tempSL.stringList = new List<string>
        {
            "/Prefix",
            "HMS",
            "/Craft",
            "Albion",
            "Astute",
            "Bulwark",
            "Bridgewater",
            "Caesar",
            "Challenger",
            "Dagger",
            "Daring",
            "Devon",
            "Emperor",
            "Essex",
            "Falcon",
            "Fearless",
            "George IV",
            "Gorgon",
            "Hero",
            "Hunter",
            "Illustrious",
            "Intrepid",
            "Jackal",
            "Jupiter",
            "Kent",
            "King Arthur",
            "Lion",
            "Locust",
            "Mars",
            "Minotaur",
            "Nelson",
            "Northampton",
            "Oceanic",
            "Onyx",
            "Pearl",
            "Pegasus",
            "Quebec",
            "Quality",
            "Raider",
            "Resolute",
            "Salamander",
            "Saturn",
            "Storm",
            "Tabard",
            "Tribune",
            "Union",
            "Ursula",
            "Valentine",
            "Volunteer",
            "Warrior",
            "Westminister",
            "Xenophon",
            "York",
            "Yarmouth",
            "Zulu",
        };

        file = File.Create(finder.defaultPath + "/Localisation/Craft Names/BritishLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Localisation/Craft Names/BritishLocalisation.xml");
    }
    //Saves the default localisation files
    public void SaveDefaults()
    {
        DefaultTrooperNames();
        DefaultSlotNames();
        DefaultHierachyNames();
        DefaultCraftNames();
    }
}
