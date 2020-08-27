using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Manager : MonoBehaviour
{
    public Slot_Generator generator;
    public GameObject prefabSlot;
    public Slot_Script viewedSlot;
    public List<Slot_Class> slots;
    public GameObject slotN1;
    public GraphicRaycaster raycaster;

    public void NewSlotTop()
    {
        GameObject temp = Instantiate(prefabSlot, viewedSlot.transform);
        Slot_Script tempS = temp.GetComponent<Slot_Script>();
        tempS.input.text = "New Slot";
        tempS.slotName = "New Slot";
        tempS.slotHeight = viewedSlot.slotHeight + 1;
        tempS.ID = viewedSlot.containedSlots.Count + 1;
        tempS.MakeSlot(tempS, viewedSlot, this);
        tempS.SetPosition(viewedSlot, viewedSlot);
        viewedSlot.containedSlots.Add(tempS);
        slots = new List<Slot_Class>();
        slots.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
    }

    public void OpenSlot(Slot_Script newViewed)
    {
        viewedSlot = newViewed;
        foreach(Slot_Script ss in slotN1.GetComponent<Slot_Script>().containedSlots)
        {
            ss.SetPosition(ss.slotParent, ss.slotParent.containedSlots.Count, viewedSlot);
        }
    }

    void Awake()
    {
        // Get both of the components we need to do this
        raycaster = GetComponent<GraphicRaycaster>();
    }

    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            pointerData.position = Input.mousePosition;
            this.raycaster.Raycast(pointerData, results);

            Slot_Script Highest = new Slot_Script();
            Highest.slotHeight = -1;
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<Slot_Script>() != null)
                {
                    Slot_Script temp = result.gameObject.GetComponent<Slot_Script>();
                    if (temp.slotHeight > Highest.slotHeight)
                    {
                        Highest = temp;
                    }
                    Debug.Log("Hit " + result.gameObject.name);
                }
            }

            OpenSlot(Highest);
            Debug.Log("Opening " + Highest.slotName);
        }
    }
}
