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
    public FileFinder finder;

    public List<String_List_Class> localisationList;

    public String_List_Class chosenNamesList;

    public Dropdown trooperNamesDropdown;
    public Dropdown hierachyNamesDropdown;

    public void FindLocalisationFiles()
    {
        SaveDefaults();
        localisationList = new List<String_List_Class>();
        List<string> fileLocations = finder.Retrieve("Localisation", ".meta");
        List<string> trooperNames = new List<string>();
        List<string> hierarchyNames = new List<string>();

        foreach (string s in fileLocations)
        {
            String_List_Class temp = Serializer.Deserialize<String_List_Class>(s);
            localisationList.Add(temp);
            if (temp.listType == "Names")
            {
                trooperNames.Add(temp.name);
            } 
            else if (temp.listType == "Hierarchy")
            {

            }
        }

        trooperNamesDropdown.ClearOptions();
        trooperNamesDropdown.AddOptions(trooperNames);
        ChangedTemplateDropdown(trooperNamesDropdown);
    }

    public void ChangedTemplateDropdown(Dropdown dropdown)
    {
    }

    //Creates default trooper names String Lists
    public void DefaultTrooperNames()
    {
        //English Troopers List
        String_List_Class tempSL = new String_List_Class();
        tempSL.name = "English Names";
        tempSL.listType = "Names";
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

        var file = File.Create(finder.defaultPath + "/Core/Localisation/EnglishLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Core/Localisation/EnglishLocalisation.xml");

        //English Troopers List
        tempSL = new String_List_Class();
        tempSL.name = "American Names";
        tempSL.listType = "Names";
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

        file = File.Create(finder.defaultPath + "/Core/Localisation/AmericanLocalisation.xml");
        file.Close();
        Serializer.Serialize(tempSL, finder.defaultPath + "/Core/Localisation/AmericanLocalisation.xml");
    }

    //Creates default hierarchy names String Lists
    public void DefaultSlotNames()
    {

    }

    public void SaveDefaults()
    {
        DefaultTrooperNames();
    }
}
