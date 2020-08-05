using System.Collections;
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

    public int AI1I;
    public int AI2I;
    public int AI3I;
    public int AI4I;
    private bool[] AIBoolArray;

    public FileFinder fileFinder;
    public Race_Manager raceManager;
    public GameObject[] AIBoxes;
    public Toggle[] AIToggles;
    public Dropdown[] AIBoxesDropDowns;

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

        AI1I = 0;
        AI2I = 1;
        AI3I = 2;
        AI4I = 0;

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
        generationManager.Generate(loading, product);
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
    // AI Toggle (AI Number)
    //
    public void AIToggle(int AIN)
    {
        if (AIToggles[AIN].isOn == false)
        {
            AIBoxes[AIN].SetActive(false);
        } 
        else
        {
            AIBoxes[AIN].SetActive(true);
        }
    }

    //
    // AI Selection Changed (AI Number)
    //
    public void AISelectionChanged(int AIN)
    {
        if (AIN == 0)
        {
            AI1I = AIBoxesDropDowns[AIN].value;
        }
        if (AIN == 1)
        {
            AI2I = AIBoxesDropDowns[AIN].value;
        }
        if (AIN == 2)
        {
            AI3I = AIBoxesDropDowns[AIN].value;
        }
        if (AIN == 3)
        {
            AI4I = AIBoxesDropDowns[AIN].value;
        }
    }
}
