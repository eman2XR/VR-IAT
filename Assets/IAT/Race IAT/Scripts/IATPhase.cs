using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SortCategory
{
    Left,
    Right
}

/// <summary>
/// Controls base IATPhase behaviour. Concrete IATPhase classes control how to show stimuli.
/// </summary>
public abstract class IATPhase : MonoBehaviour
{
    /// <summary>
    /// Called when a phase is complete.
    /// </summary>
    public event Action PhaseCompleted;

    public event Action IncorrectSort;
    public event Action CorrectSort;

    public TMP_Text CategoryTextLeft;
    public TMP_Text CategoryTextRight;

    public TMP_Text instructionsCategoryTextLeft;
    public TMP_Text instructionsCategoryTextRight;

    public Graphic ErrorGraphic;
    public TMP_Text InstructionsText;

    // How long the user took in this phase.
    public float Duration { get; protected set; } = 0;
    public float IncorrectSortPenalty = 1.0f;
    public int Mistakes { get; protected set; } = 0;

    public int StimuliCount { get; set; }
    public bool PhaseStarted { get; set; } = false;
    public SortCategory CorrectSortCategory { get; protected set; }

    protected virtual void ShowStimuli() { }

    public virtual void EndPhase()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void OnPhaseCompleted()
    {
        PhaseCompleted?.Invoke();
    }

    public virtual void OnCorrectSort()
    {
        CorrectSort?.Invoke();
    }

    public virtual void OnIncorrectSort()
    {
        Duration += IncorrectSortPenalty;
        IncorrectSort?.Invoke();
        Mistakes++;
        Debug.Log(Mistakes);
    }

    public virtual void LoadPhase(RaceIAT raceIAT)
    {
        this.gameObject.SetActive(true);
        IncorrectSortPenalty = raceIAT.IncorrectSortPenalty;

        // Show Instructions and make sure test images are hidden.
        InstructionsText.gameObject.SetActive(true);
        ErrorGraphic.gameObject.SetActive(false);

        // Set the stimuli count.
        StimuliCount = raceIAT.StimuliPerPhase;
    }

    // Update is called once per frame
    void Update()
    {
        // Turn off Instructions and start the test when the User presses the space key.
        if (this.PhaseStarted == false)
            if (Input.GetKeyDown(KeyCode.Space) || Manager.instance.leftButtonPressed || Manager.instance.rightButtonPressed)
            {
                { 
                    InstructionsText.gameObject.SetActive(false);
                    StartCoroutine(StartPhaseWithDelay());
                }
             }
        }

    // Starts the Test.
    protected virtual IEnumerator TestCoroutine()
    {
        while (StimuliCount > 0)
        {
            ShowStimuli();

            // Poll for input.
            while (true)
            {
                // Track 
                Duration += Time.deltaTime;

                // Sort to Left Category.
                if (Input.GetKeyDown(KeyCode.S) || Manager.instance.leftButtonPressed)
                {
                    if (CorrectSortCategory == SortCategory.Left)
                    {
                        StimuliCount--;
                        ErrorGraphic.gameObject.SetActive(false);
                        OnCorrectSort();

                        break;
                    }
                    else
                    {
                        ErrorGraphic.gameObject.SetActive(true);
                        OnIncorrectSort();
                    }
                }

                // Sort to Right Category.
                if (Input.GetKeyDown(KeyCode.L) || Manager.instance.rightButtonPressed)
                {
                    if (CorrectSortCategory == SortCategory.Right)
                    {
                        StimuliCount--;
                        ErrorGraphic.gameObject.SetActive(false);
                        OnCorrectSort();

                        break;
                    }
                    else
                    {
                        ErrorGraphic.gameObject.SetActive(true);
                        OnIncorrectSort();
                    }
                }

                yield return 0;
            }

            yield return 0;
        }

        // Call PhaseCompleted when stimulus count = 0.
        OnPhaseCompleted();
    }

    IEnumerator StartPhaseWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        this.PhaseStarted = true;
        StartCoroutine(TestCoroutine());
    }
}