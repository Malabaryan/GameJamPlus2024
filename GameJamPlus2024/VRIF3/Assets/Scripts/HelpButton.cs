using UnityEditor;
using UnityEngine;

public class HelpButton : MonoBehaviour
{

    public enum HelpState
    {
        ShowingHelp = 0,
        ShowingTutorial = 1,
        Nothing = 2
    }

    public GameObject Help;
    public GameObject Tutorial;
    private HelpState currentState = HelpState.ShowingTutorial;


    public void ToggleHelp()
    {
        if (currentState == HelpState.ShowingTutorial)
        {
            currentState = HelpState.ShowingHelp;
            Help.SetActive(true);
            Tutorial.SetActive(false);
        }
        else if (currentState == HelpState.ShowingHelp)
        {
            currentState = HelpState.Nothing;
            Help.SetActive(false);
            Tutorial.SetActive(false);
        }
        else if (currentState == HelpState.Nothing)
        {
            currentState = HelpState.ShowingTutorial;
            Help.SetActive(false);
            Tutorial.SetActive(true);

        }        

    }

}

