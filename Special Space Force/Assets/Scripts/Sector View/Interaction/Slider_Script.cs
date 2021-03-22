using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Script : MonoBehaviour
{
    public Vector2 origin;
    public RectTransform rect;

    public Image headerImage;
    public Sprite pressedImage;
    public Sprite normalImage;

    public GameObject[] siblingSliders;

    public float xDistance;
    public float yDistance;
    public bool slide = false;
    public bool pulledOut = false;

    private bool alreadyClosed;

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        origin = rect.anchoredPosition;
    }

    public void Slide()
    {
        if (headerImage.enabled)
        {
            if (slide)
            {
                slide = false;
                headerImage.sprite = normalImage;

                foreach (GameObject go in siblingSliders)
                {
                    if (alreadyClosed == false)
                    {
                        go.SetActive(true);
                        go.GetComponent<Slider_Script>().headerImage.enabled = true;
                    }
                }
            }
            else
            {
                slide = true;
                InvokeRepeating("DoSlide", 0.01f, 0.01f);
                foreach (GameObject go in siblingSliders)
                {
                    if (go.activeSelf == false)
                    {
                        alreadyClosed = true;
                    }
                    go.SetActive(false);
                    go.GetComponent<Slider_Script>().headerImage.enabled = false;
                }
                headerImage.sprite = pressedImage;
            }
        }
    }

    private void DoSlide()
    {
        if (slide)
        {
            if (!pulledOut)
            {
                if (yDistance >= 0)
                {
                    if (rect.anchoredPosition.x <= origin.x - xDistance && rect.anchoredPosition.y <= origin.y - yDistance)
                    {
                        pulledOut = true;
                    }
                    else
                    {
                        headerImage.rectTransform.anchoredPosition -= new Vector2(xDistance / 30, yDistance / 30);
                        rect.anchoredPosition -= new Vector2(xDistance / 30, yDistance / 30);
                    }
                }
                if(yDistance < 0)
                {
                    if (rect.anchoredPosition.x >= origin.x - xDistance && rect.anchoredPosition.y >= origin.y - yDistance)
                    {
                        pulledOut = true;
                    }
                    else
                    {
                        rect.anchoredPosition -= new Vector2(xDistance / 30, yDistance / 30);
                    }
                }
            }
        }
        else if (!slide)
        {
            if (rect.anchoredPosition.x >= origin.x - 0.5 && rect.anchoredPosition.x <= origin.x + 0.5 && rect.anchoredPosition.y >= origin.y - 0.5 && rect.anchoredPosition.y <= origin.y + 0.5)
            {
                //This may cause issues later down the line
                if (!alreadyClosed)
                {
                    foreach (GameObject go in siblingSliders)
                    {
                        go.SetActive(true);
                        go.GetComponent<Slider_Script>().headerImage.enabled = true;
                    }
                }
                CancelInvoke("DoSlide");
                rect.anchoredPosition = origin;
            }
            else
            {
                if (yDistance >= 0)
                {
                    headerImage.rectTransform.anchoredPosition += new Vector2(xDistance / 30, yDistance / 30);
                }
                rect.anchoredPosition += new Vector2(xDistance / 30, yDistance / 30);
            }
            pulledOut = false;
        }
    }


    public void HidePlanets()
    {
        if (pulledOut != true)
        {
            ToggleVisiblePlanets.TogglePlanetsOn(false);
        } 
        else
        {
            ToggleVisiblePlanets.TogglePlanetsOn(true);
        }
    }
}
