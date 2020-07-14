using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoForUI : MonoBehaviour
{
    [SerializeField]
    private int indexSelected;
    public int IndexSelected
    {
        get
        {
            return indexSelected;
        }
        set
        {
            indexSelected = value;
        }
    }
    [SerializeField]
    private int buttonSelected;
    public int ButtonSelected
    {
        get
        {
            return buttonSelected;
        }
        set
        {
            buttonSelected = value;
        }
    }
    [SerializeField]
    private GameObject ArrowUp;
    [SerializeField]
    private GameObject ArrowDown;
    private Sprite arrowSprite;
    private int startMenuFrom;
    
    
    
    public bool isFinishedChoosing; 
    public List<GameObject> MenuOptions { get; set; }
    public List<string> CurrentMenuNames { get; set; }

    /// <summary>
    /// Updates the text displayed on the menu buttons
    /// </summary>
    /// <param name="actionsList"> List of strings of names of actions taken </param>
    /// <param name="start"> Where in the list to start displaying from, displays 4 actions from the starting point </param>
    /// <returns></returns>
    public bool UpdateButtons(List<string> actionsList, int start)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i + start > actionsList.Count - 1)
            {
                MenuOptions[i].GetComponent<Text>().text = "";
            }
            else {
                MenuOptions[i].GetComponent<Text>().text = (i + 1 + start).ToString() + ": " + actionsList[i+start];
            }
        }
        return true;
    }

    /// <summary>
    /// Basic movement allowing flexibility in axis, movement amount
    /// Can inverse the positive and negative keys without changing the controls
    /// </summary>
    /// <param name="axis"> Unity axis used for movement </param>
    /// <param name="incrementAmount"> Amount index increases by </param>
    /// <param name="inverse"> Allows for the positive and negative keys to decrease and increase the index respectively </param>
    /// <returns> Returns if the movement was made successfully </returns>
    private bool MoveInMenu(string axis, int incrementAmount, bool inverse)
    {
        int inverser = 1;
        if (inverse)
        {
            inverser = -1;
        }
        //check buttons
        if (Input.GetButtonDown(axis))
        {
            //positive button
            if (Input.GetAxis(axis) * inverser > 0)
            {
                if ((IndexSelected + incrementAmount) < CurrentMenuNames.Count)
                {
                    ButtonSelected += incrementAmount;
                    IndexSelected += incrementAmount;
                }
                else if ((IndexSelected + incrementAmount - 1) < CurrentMenuNames.Count) {
                    ButtonSelected += incrementAmount - 1;
                    IndexSelected += incrementAmount - 1;
                }
            }
            //negative button
            else if (Input.GetAxis(axis) * inverser < 0)
            {
                //checks if Index will go negative
                if (IndexSelected >= incrementAmount)
                {
                    ButtonSelected -= incrementAmount;
                    IndexSelected -= incrementAmount;
                }
                else
                {
                    //fails to move
                    return false;
                }
            }
        }

        //Scroll down
        //4 = MenuOptions.Count
        if (ButtonSelected >= 4)
        {
            startMenuFrom += 4;
            if (startMenuFrom > CurrentMenuNames.Count - 4 - 1)
            {
                ArrowDown.GetComponent<SpriteRenderer>().sprite = null;
            }
            ArrowUp.GetComponent<SpriteRenderer>().sprite = arrowSprite;
            UpdateButtons(CurrentMenuNames, startMenuFrom);
            
        }
        //scroll up
        if (ButtonSelected < 0)
        {
            startMenuFrom -= 4;
            if (startMenuFrom == 0)
            {
                ArrowUp.GetComponent<SpriteRenderer>().sprite = null;
            }
            ArrowDown.GetComponent<SpriteRenderer>().sprite = arrowSprite;
            UpdateButtons(CurrentMenuNames, startMenuFrom);
        }
        ButtonSelected = IndexSelected % 4;

        return true;
    }

    void Start() {
        MenuOptions = new List<GameObject>();
        IndexSelected = 0;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("UIMenu"))
        {
            MenuOptions.Add(obj.gameObject);
        }
        startMenuFrom = 0;
        arrowSprite = ArrowUp.GetComponent<SpriteRenderer>().sprite;
        ArrowUp.GetComponent<SpriteRenderer>().sprite = null;
    }

    
    /** Keyboard Input Style Finished 7/1/20
     * private int ChangeSelected(int n) {
        if (Input.GetKeyDown((n + 1).ToString()))
        {
            return n;
        }
        return -1;
    }**/

    void Update()
    {
        MoveInMenu("Horizontal", 1, false);
        MoveInMenu("Vertical", 2, true);
        foreach (GameObject button in MenuOptions) {
            button.GetComponent<Text>().color = new Color(0, 0, 0);
        }
        MenuOptions[ButtonSelected].GetComponent<Text>().color = new Color(.95f,.95f,.95f);
        Debug.Log(IndexSelected);
        if (Input.GetKeyDown("return")) {
            isFinishedChoosing = true;
        }
    }
}
