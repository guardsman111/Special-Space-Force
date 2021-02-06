using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generation_Settings_Director : MonoBehaviour
{
    /// <summary>
    /// This script collects users generation settings and outputs them to the Galaxy_Generation_Manager
    /// This script contains event handles from all of the UI Customization game obejcts
    /// </summary>
    public Galaxy_Generation_Manager generationManager;
    public Slot_Generator slotGenerator;
    public Fleet_Generator fleetGenerator;
    public Localisation_Manager localisationManager;
    public Trait_Manager traitManager;
    public Faction_Manager factionManager;

    private int height = 2000;
    private int width = 2000;
    private int numberofStars = 100;
    private int minimumPlanets = 1;
    private int maximumPlanets = 7;
    private int averagePlanetSize = 50;
    private int habitableChance = 50;
    private int habitationChance = 50;
    private int resourceAbundancy = 50;
    private int playerStrength = 2;
    private int xenophobia = 50;
    private int militarism = 2;
    private int expansionism = 50;
    private int industrialism = 2;
    private int funding = 50;
    private int identifierLoc = 0;
    public int nSlotLocations;

    private bool[] AIBoolArray;

    public FileFinder fileFinder;
    public Race_Manager raceManager;
    public Equipment_Manager equipmentManager;
    public Voidcraft_Manager voidcraftManager;

    //AI Stuff, used to remember which AI is set to what.
    public GameObject[] AIBoxes;
    [SerializeField]
    public int[] AIDifficulty;
    [SerializeField]
    public int[] AIStartingThreat;
    [SerializeField]
    public Image[] AIColour;
    public Toggle[] AIToggles;
    public Dropdown[] AIBoxesDropDowns; //Race Selection Dropdown
    [SerializeField]
    public Image[] PlayerColours;
    public Image[] FleetColours;
    public Page_Manager[] Pages;


    public ToggleVisiblePlanets planetToggle;
    public bool generateOnPlay;

    //This starts on Game Start
    void Start()
    {
        //Start File Finder
        fileFinder.Run();

        //Setup Races
        raceManager.Run();
        foreach(Dropdown d in AIBoxesDropDowns)
        {
            d.ClearOptions();
            List<string> raceNames = new List<string>();
            foreach(Race_Class r in raceManager.Races)
            {
                raceNames.Add(r.raceName);
            }
            d.AddOptions(raceNames);
        }

        //Set Default Races
        AIBoolArray = new bool[5] { true, true, true, false, false};
        AIBoxesDropDowns[0].value = 0;
        AIBoxesDropDowns[1].value = 1;
        AIBoxesDropDowns[2].value = 2;
        AIBoxesDropDowns[3].value = 0;
        AIBoxesDropDowns[4].value = 0;

        //Turn off AI 4 and 5 for fun
        AIToggle(3);
        AIToggle(4);


        slotGenerator.SetupTemplateDropdown();
        fleetGenerator.SetupTemplateDropdown();
        localisationManager.FindLocalisationFiles();
        equipmentManager.Begin();
        voidcraftManager.Begin();
        traitManager.Run();

        if (generateOnPlay)
        {
            StartGeneration(true);
        }
    }

    //Starts the generation, grabs all the values and packages them into a Generation_Class
    public void StartGeneration(bool loading)
    {
        if (!loading)
        {
            TurnOnLoadScreen();
            Invoke("StartGen", 0.1f);
        }
        else
        {
            TurnOnLoadScreen();
            Invoke("StartLoad", 0.1f);
        }
    }

    private void StartGen()
    {
        Generation_Class product = new Generation_Class();

        foreach (Page_Manager p in Pages)
        {
            p.gameObject.SetActive(true);
        }
        product.height = height * 10;
        product.width = width * 10;
        product.numberofStars = numberofStars;
        product.minimumPlanets = minimumPlanets;
        product.maximumPlanets = maximumPlanets;
        product.averagePlanetSize = averagePlanetSize;
        product.habitableChance = habitableChance;
        product.inhabitedChance = habitationChance;
        product.resourceAbundancy = resourceAbundancy;
        product.playerStrength = playerStrength;
        product.xenophobia = xenophobia;
        product.militarism = militarism;
        product.expansionism = expansionism;
        product.industrialism = industrialism;
        product.funding = funding;
        product.identifierLoc = identifierLoc;
        product.factions = factionManager.GenerateFactions(SortToggledAI());
        product.chosenLocalisationList = localisationManager.FindChosenLocalisation();
        product.playerColours = equipmentManager.GetColours(PlayerColours);
        product.playerFleetColours = voidcraftManager.GetColours(FleetColours);
        localisationManager.SeperateStringLists();
        product.selectedTraits = traitManager.GetTraits();
        product.defaultEquipment = equipmentManager.GetDefault("Equipment");
        product.defaultPatterns = equipmentManager.GetDefault("Patterns");
        generationManager.Generate(false, product);
        Invoke("DisableCustomization", 1.0f);
    }

    private void StartLoad()
    {
        Generation_Class product = new Generation_Class();

        generationManager.Generate(true, product);
        Invoke("DisableCustomization", 1.0f);
    }

    //Collects the AI data, packages them into an AI_Class Individually then puts them in a list for easy access
    private List<AI_Class> SortToggledAI()
    {
        //Create empty values
        List<AI_Class> ToggledAI;
        ToggledAI = new List<AI_Class>();

        //For each AI, if its toggled then copy its data
        if(AIBoolArray[0] == true)
        {
            AI_Class AI = new AI_Class();
            AI.race = raceManager.Races[AIBoxesDropDowns[0].value];
            AI.difficulty = AIDifficulty[0];
            AI.startThreat = AIStartingThreat[0];
            AI.colour = AIColour[0].color;
            AI.nPlanets = 0;

            ToggledAI.Add(AI);
        }

        if (AIBoolArray[01] == true)
        {
            AI_Class AI = new AI_Class();
            AI.race = raceManager.Races[AIBoxesDropDowns[1].value];
            AI.difficulty = AIDifficulty[1];
            AI.startThreat = AIStartingThreat[1];
            AI.colour = AIColour[1].color;
            AI.nPlanets = 0;

            ToggledAI.Add(AI);
        }

        if (AIBoolArray[2] == true)
        {
            AI_Class AI = new AI_Class();
            AI.race = raceManager.Races[AIBoxesDropDowns[2].value];
            AI.difficulty = AIDifficulty[2];
            AI.startThreat = AIStartingThreat[2];
            AI.colour = AIColour[2].color;
            AI.nPlanets = 0;

            ToggledAI.Add(AI);
        }
        if (AIBoolArray[3] == true)
        {
            AI_Class AI = new AI_Class();
            AI.race = raceManager.Races[AIBoxesDropDowns[3].value];
            AI.difficulty = AIDifficulty[3];
            AI.startThreat = AIStartingThreat[3];
            AI.colour = AIColour[3].color;
            AI.nPlanets = 0;

            ToggledAI.Add(AI);
        }

        //Return the List
        return ToggledAI;
    }

    //Turns off the Customization menu
    private void DisableCustomization()
    {
        planetToggle.Run();
        this.gameObject.SetActive(false);
    }

    //
    // Height
    //
    public void Height(InputField input)
    {
        height = int.Parse(input.text);
        if(height < 1000)
        {
            height = 1000;
        }
        input.text = height.ToString();
    }

    public void AddHeight(InputField input)
    {
        int change = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            change = 5;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            change = 50;
        }
        int temp = int.Parse(input.text) + change;
        height = temp;
        if (height > 9999)
        {
            height = 9999;
        }
        input.text = height.ToString();
    }

    public void SubtractHeight(InputField input)
    {
        int change = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            change = 5;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            change = 50;
        }
        int temp = int.Parse(input.text) - change;
        height = temp;
        if (height < 1000)
        {
            height = 1000;
        }
        input.text = height.ToString();
    }

    //
    // Width
    //
    public void Width(InputField input)
    {
        width = int.Parse(input.text);
        if (width < 1000)
        {
            width = 1000;
        }
        input.text = width.ToString();
    }

    public void AddWidth(InputField input)
    {
        int change = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            change = 5;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            change = 50;
        }
        int temp = int.Parse(input.text) + change;
        width = temp;
        if (width > 9999)
        {
            width = 9999;
        }
        input.text = width.ToString();
    }

    public void SubtractWidth(InputField input)
    {
        int change = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            change = 5;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            change = 50;
        }
        int temp = int.Parse(input.text) - change;
        width = temp;
        if (width < 1000)
        {
            width = 1000;
        }
        input.text = width.ToString();
    }

    //
    // Number of Stars
    //
    public void NumberOfStars(InputField input)
    {
        numberofStars = int.Parse(input.text);
        if (numberofStars < 5)
        {
            numberofStars = 5;
        }
        input.text = numberofStars.ToString();
    }

    public void AddStars(InputField input)
    {
        int change = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            change = 5;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            change = 50;
        }
        int temp = int.Parse(input.text) + change;
        numberofStars = temp;
        if (numberofStars < 5)
        {
            numberofStars = 5;
        }
        input.text = numberofStars.ToString();
    }

    public void SubtractStars(InputField input)
    {
        int change = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            change = 5;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            change = 50;
        }
        int temp = int.Parse(input.text) - change;
        numberofStars = temp;
        if (numberofStars < 5)
        {
            numberofStars = 5;
        }
        input.text = numberofStars.ToString();
    }

    //
    // Minimum Planets
    //
    public void AddMinPlanets(Text input)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            minimumPlanets = 6;
            input.text = minimumPlanets.ToString();
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            minimumPlanets = 6;
            input.text = minimumPlanets.ToString();
        }
        else if (minimumPlanets < 6)
        {
            minimumPlanets += 1;
            input.text = minimumPlanets.ToString();
        }
        else
        {
            minimumPlanets = 6;
            input.text = minimumPlanets.ToString();
        }
    }

    public void SubMinPlanets(Text input)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            minimumPlanets = 0;
            input.text = minimumPlanets.ToString();
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            minimumPlanets = 0;
            input.text = minimumPlanets.ToString();
        }
        else if (minimumPlanets > 0)
        {
            minimumPlanets -= 1;
            input.text = minimumPlanets.ToString();
        }
        else
        {
            minimumPlanets = 0;
            input.text = minimumPlanets.ToString();
        }
    }

    //
    // Maximum Planets
    //
    public void AddMaxPlanets(Text input)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maximumPlanets = 7;
            input.text = maximumPlanets.ToString();
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            maximumPlanets = 7;
            input.text = maximumPlanets.ToString();
        }
        else if (maximumPlanets < 7)
        {
            maximumPlanets += 1;
            input.text = maximumPlanets.ToString();
        }
        else
        {
            maximumPlanets = 7;
            input.text = maximumPlanets.ToString();
        }
    }

    public void SubMaxPlanets(Text input)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maximumPlanets = 1;
            input.text = maximumPlanets.ToString();
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            maximumPlanets = 1;
            input.text = maximumPlanets.ToString();
        }
        else if (maximumPlanets > 1)
        {
            maximumPlanets -= 1;
            input.text = maximumPlanets.ToString();
        }
        else
        {
            maximumPlanets = 1;
            input.text = maximumPlanets.ToString();
        }
    }

    //
    // Average Planet Size
    //
    public void ChangeAvgPlanetSize(Text input)
    {
        int temp = (int)input.transform.GetComponentInParent<Slider>().value;
        input.text = temp.ToString();
        averagePlanetSize = temp;
    }

    //
    // Average Habitable planets
    //
    public void ChangeHabitable(Text input)
    {
        int temp = (int)input.transform.GetComponentInParent<Slider>().value;
        input.text = temp.ToString();
        habitableChance = temp;
    }

    //
    // Average Inhabited planets
    //
    public void ChangeInhabited(Text input)
    {
        int temp = (int)input.transform.GetComponentInParent<Slider>().value;
        input.text = temp.ToString();
        habitationChance = temp;
    }

    //
    // Average Resource Abundancy
    //
    public void ChangeAbundancy(Text input)
    {
        int temp = (int)input.transform.GetComponentInParent<Slider>().value;
        input.text = temp.ToString();
        resourceAbundancy = temp;
    }

    //
    // Player Strength Changed
    //
    public void PlayerStrengthChanged(Dropdown dropdown)
    {
        playerStrength = dropdown.value;
    }

    //
    // Xenophobia Changed
    //
    public void ChangeXenophobia(Text input)
    {
        int temp = (int)input.transform.GetComponentInParent<Slider>().value;
        if(temp < 5)
        {
            input.text = "Space Commies";
        }
        else if (temp < 15)
        {
            input.text = "Xenophilic";
        }
        else if (temp < 25)
        {
            input.text = "Very Friendly";
        }
        else if (temp < 35)
        {
            input.text = "Friendly";
        }
        else if (temp < 45)
        {
            input.text = "Polite";
        }
        else if (temp < 55)
        {
            input.text = "Passive";
        }
        else if (temp < 65)
        {
            input.text = "Cautious";
        }
        else if (temp < 75)
        {
            input.text = "Insulting";
        }
        else if (temp < 85)
        {
            input.text = "Open Aggression";
        }
        else if (temp < 95)
        {
            input.text = "Xenophobic";
        }
        else if (temp <= 100)
        {
            input.text = "Genocidal Purist";
        }
        xenophobia = temp;
    }

    //
    // Player Militarism Changed
    //
    public void PlayerMilitaryChanged(Dropdown dropdown)
    {
        militarism = dropdown.value;
    }

    //
    // Expansionism Changed
    //
    public void ChangeExpansionism(Text input)
    {
        int temp = (int)input.transform.GetComponentInParent<Slider>().value;
        if (temp < 5)
        {
            input.text = "Scared of Change";
        }
        else if (temp < 25)
        {
            input.text = "Internalist";
        }
        else if (temp < 45)
        {
            input.text = "Scientific Annexation";
        }
        else if (temp < 55)
        {
            input.text = "Explorer";
        }
        else if (temp < 75)
        {
            input.text = "Expansion Protocols";
        }
        else if (temp < 95)
        {
            input.text = "Aggressive Expansionist";
        }
        else if (temp <= 100)
        {
            input.text = "Galactic Dominator";
        }
        expansionism = temp;
    }

    //
    // Player Industrialism Changed
    //
    public void PlayerIndustrialChanged(Dropdown dropdown)
    {
        industrialism = dropdown.value;
    }

    //
    // Funding Changed
    //
    public void ChangeFunding(Text input)
    {
        int temp = (int)input.transform.GetComponentInParent<Slider>().value;
        if (temp < 15)
        {
            input.text = "Mothballed";
        }
        else if (temp < 25)
        {
            input.text = "Scaling Down";
        }
        else if (temp < 45)
        {
            input.text = "Budget tightening";
        }
        else if (temp < 55)
        {
            input.text = "Regular Funding";
        }
        else if (temp < 75)
        {
            input.text = "Budget Excess";
        }
        else if (temp < 85)
        {
            input.text = "Growth Funding";
        }
        else if (temp <= 90)
        {
            input.text = "No Expense Spared";
        }
        funding = temp;
    }

    //
    // AI Toggle (AI Number)
    //
    public void AIToggle(int AIN)
    {
        if (AIToggles[AIN].isOn == false)
        {
            AIBoxes[AIN].SetActive(false);
            AIBoolArray[AIN] = false;
        } 
        else
        {
            AIBoxes[AIN].SetActive(true);
            AIBoolArray[AIN] = true;
        }
    }

    //
    // AI Difficulty Changed
    //
    public void AIDifficultyChanged(Dropdown dropdown)
    {
        AIDifficulty[dropdown.gameObject.GetComponent<Dropdown_Helper>().aiNumber] = dropdown.value;
    }

    //
    // AI Start Threat Changed
    //
    public void AIStartThreatChanged(Dropdown dropdown)
    {
        AIStartingThreat[dropdown.gameObject.GetComponent<Dropdown_Helper>().aiNumber] = dropdown.value;
    }

    public void TurnOnLoadScreen()
    {
        Pages[0].gameObject.SetActive(true);
    }

    public void SetIdentifierLocation(int modifier)
    {
        
        if (identifierLoc < nSlotLocations)
        {
            if (modifier == +1 && identifierLoc == nSlotLocations - 1)
            {
                identifierLoc = 0;
            }
            else if (modifier == -1 && identifierLoc == 0)
            {
                identifierLoc = nSlotLocations - 1;
            }
            else
            {
                identifierLoc += modifier;
            }
        }
    }
}
