using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generation_Settings_Director : MonoBehaviour
{
    public Galaxy_Generation_Manager generationManager;

    private int height;
    private int width;
    private int numberofStars;
    private int minimumPlanets;
    private int maximumPlanets;

    void Start()
    {
        
    }

    public void StartGeneration()
    {
        generationManager.Generate();
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

}
