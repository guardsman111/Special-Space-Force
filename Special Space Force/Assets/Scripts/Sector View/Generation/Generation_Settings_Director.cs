﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generation_Settings_Director : MonoBehaviour
{
    public Galaxy_Generation_Manager generationManager;

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
    public GameObject[] AIBoxes;
    [SerializeField]
    public int[] AIDifficulty;
    [SerializeField]
    public int[] AIStartingThreat;
    [SerializeField]
    public Image[] AIColour;
    public Toggle[] AIToggles;
    public Dropdown[] AIBoxesDropDowns;


    public ToggleVisiblePlanets planetToggle;

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

        AIBoolArray = new bool[4] { true, true, true, false};
        AIBoxesDropDowns[0].value = 0;
        AIBoxesDropDowns[1].value = 1;
        AIBoxesDropDowns[2].value = 2;
        AIBoxesDropDowns[3].value = 0;


        AIToggle(3);
    }

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
        generationManager.Generate(loading, product);
        Invoke("DisableCustomization", 1.0f);
    }

    private List<AI_Class> SortToggledAI()
    {
        List<AI_Class> ToggledAI;
        ToggledAI = new List<AI_Class>();

        if(AIBoolArray[0] == true)
        {
            AI_Class AI = new AI_Class();
            AI.race = raceManager.Races[AIBoxesDropDowns[0].value];
            AI.difficulty = AIDifficulty[0];
            AI.startThreat = AIStartingThreat[0];
            AI.colour = AIColour[0].color;

            ToggledAI.Add(AI);
        }

        if (AIBoolArray[01] == true)
        {
            AI_Class AI = new AI_Class();
            AI.race = raceManager.Races[AIBoxesDropDowns[1].value];
            AI.difficulty = AIDifficulty[1];
            AI.startThreat = AIStartingThreat[1];
            AI.colour = AIColour[1].color;

            ToggledAI.Add(AI);
        }

        if (AIBoolArray[2] == true)
        {
            AI_Class AI = new AI_Class();
            AI.race = raceManager.Races[AIBoxesDropDowns[2].value];
            AI.difficulty = AIDifficulty[2];
            AI.startThreat = AIStartingThreat[2];
            AI.colour = AIColour[2].color;

            ToggledAI.Add(AI);
        }
        if (AIBoolArray[3] == true)
        {
            AI_Class AI = new AI_Class();
            AI.race = raceManager.Races[AIBoxesDropDowns[3].value];
            AI.difficulty = AIDifficulty[3];
            AI.startThreat = AIStartingThreat[3];
            AI.colour = AIColour[3].color;

            ToggledAI.Add(AI);
        }

        return ToggledAI;
    }

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
    // AI Selection Changed (AI Number)
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


    ////
    //// AI Selection Changed (Dropdown Input)
    ////
    //public void AISelectionChanged(Dropdown input)
    //{
    //    input.gameObject

    //    if (input.gameObject.name == "Difficulty")
    //    {
    //        if (AIN == 0)
    //        {
    //            AIDifficulty[0] = input.value;
    //        }
    //        if (AIN == 1)
    //        {
    //            AIDifficulty[1] = input.value;
    //        }
    //        if (AIN == 2)
    //        {
    //            AIDifficulty[2] = input.value;
    //        }
    //        if (AIN == 3)
    //        {
    //            AIDifficulty[3] = input.value;
    //        }
    //    }

    //    if (input.gameObject.name == "Starting Threat")
    //    {
    //        if (AIN == 0)
    //        {
    //            AIStartingThreat[0] = input.value;
    //        }
    //        if (AIN == 1)
    //        {
    //            AIStartingThreat[1] = input.value;
    //        }
    //        if (AIN == 2)
    //        {
    //            AIStartingThreat[2] = input.value;
    //        }
    //        if (AIN == 3)
    //        {
    //            AIStartingThreat[3] = input.value;
    //        }
    //    }

    //    if (input2.gameObject.name == "Image")
    //    {
    //        if (AIN == 0)
    //        {
    //            AIColour[0] = input2.color;
    //        }
    //        if (AIN == 1)
    //        {
    //            AIColour[1] = input2.color;
    //        }
    //        if (AIN == 2)
    //        {
    //            AIColour[2] = input2.color;
    //        }
    //        if (AIN == 3)
    //        {
    //            AIColour[3] = input2.color;
    //        }
    //    }

    //}

    ////
    //// AI Selection Changed (Image Input)
    ////
    //public void AISelectionChanged(Image input)
    //{

    //}
}
