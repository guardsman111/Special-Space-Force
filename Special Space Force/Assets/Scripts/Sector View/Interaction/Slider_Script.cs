﻿using System.Collections;
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

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        origin = rect.anchoredPosition;
    }

    public void Slide()
    {
        if (slide)
        {
            slide = false;
            headerImage.sprite = normalImage;
        }
        else
        {
            slide = true;
            InvokeRepeating("DoSlide", 0.01f, 0.01f);
            foreach (GameObject go in siblingSliders)
            {
                go.SetActive(false);
            }
            headerImage.sprite = pressedImage;
        }
    }

    private void DoSlide()
    {
        if (slide)
        {
            if (!pulledOut)
            {
                if(rect.anchoredPosition.x <= origin.x - xDistance && rect.anchoredPosition.y <= origin.y - yDistance)
                {
                    pulledOut = true;
                }
                else
                {
                    rect.anchoredPosition -= new Vector2(xDistance / 30, yDistance / 30);
                }
            }
        }
        else if (!slide)
        {
            if (rect.anchoredPosition.x >= origin.x && rect.anchoredPosition.y >= origin.y)
            {
                foreach (GameObject go in siblingSliders)
                {
                    go.SetActive(true);
                }
                CancelInvoke("DoSlide");
                rect.anchoredPosition = origin;
            }
            else
            {
                rect.anchoredPosition += new Vector2(xDistance / 30, yDistance / 30);
            }
            pulledOut = false;
        }
    }
}
