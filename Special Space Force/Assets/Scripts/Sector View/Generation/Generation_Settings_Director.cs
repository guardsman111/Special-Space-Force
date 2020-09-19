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
    public Localisation_Manager localisationManager;

    [SerializeField]
    private int height;
    [SerializeField]
    private int width;
    [SerializeField]
    private int numberofStars;
    [SerializeField]
    private int minimumPlanets;
    [SerializeField]
    private int maximumPlanets;
    [SerializeField]
    private int averagePlanetSize;
    [SerializeField]
    private int habitableChance;
    [SerializeField]
    private int habitationChance;
    [SerializeField]
    private int resourceAbundancy;
    [SerializeField]
    private int playerStrength;

    private bool[] AIBoolArray;

    public FileFinder fileFinder;
    public Race_Manager raceManager;
    public Equipment_Manager equipmentManager;

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
        AIBoolArray = new bool[4] { true, true, true, false};
        AIBoxesDropDowns[0].value = 0;
        AIBoxesDropDowns[1].value = 1;
        AIBoxesDropDowns[2].value = 2;
        AIBoxesDropDowns[3].value = 0;

        //Turn off AI 4 for fun
        AIToggle(3);

        if (generateOnPlay)
        {
            StartGeneration(true);
        }

        slotGenerator.SetupTemplateDropdown();
        localisationManager.FindLocalisationFiles();
        equipmentManager.Begin();
    }

    //Starts the generation, grabs all the values and packages them into a Generation_Class
    public void StartGeneration(bool loading)
    {
        Generation_Class product = new Generation_Class();
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
        product.toggledAI = SortToggledAI();
        product.trooperNamesList = localisationManager.chosenTrooperNamesList.name;
        product.playerColours = equipmentManager.GetColours(PlayerColours);
        localisationManager.SeperateStringLists();
        generationManager.Generate(loading, product);
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
        input.text = temp.ToString();
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
        input.text = temp.ToString();
    }

    //
    // Width
    //
    public void Width(InputField input)
    {
        width = int.Parse(input.text);
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
        input.text = temp.ToString();
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
        input.text = temp.ToString();
    }

    //
    // Number of Stars
    //
    public void NumberOfStars(InputField input)
    {
        numberofStars = int.Parse(input.text);
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
        input.text = temp.ToString();
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
        input.text = temp.ToString();
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


}
