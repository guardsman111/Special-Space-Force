using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class System_Craft : MonoBehaviour
{
    public System_Voidcraft_Script manager;

    public Voidcraft_Class linkedCraft;

    public Image craftOutline;
    public Toggle selected;

    public Text craftName;
    public Text craftClass;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Create(Voidcraft_Class craft, System_Voidcraft_Script managerS)
    {
        linkedCraft = craft;
        manager = managerS;
        foreach (Voidcraft_Pack vp in manager.modManager.voidcraftManager.voidcraftPacks) {
            if (vp.className == craft.className)
            {
                craftOutline.sprite = vp.containedSprites[0];
            }
        }
        craftName.text = craft.craftName;
        craftClass.text = craft.className;
    }
}
