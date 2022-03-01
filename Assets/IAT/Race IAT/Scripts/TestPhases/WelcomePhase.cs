using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WelcomePhase : IATPhase
{
    public TMP_Text WelcomeText;

    public override void LoadPhase(RaceIAT raceIAT)
    {
        this.gameObject.SetActive(true);
    }

    protected override void ShowStimuli()
    {
        WelcomeText.gameObject.SetActive(true);
    }

    // Override IATPhase.Update(), we only need to show the Welcome Text.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Manager.instance.leftButtonPressed || Manager.instance.rightButtonPressed)
        {
            OnPhaseCompleted();
        }
    }
}
