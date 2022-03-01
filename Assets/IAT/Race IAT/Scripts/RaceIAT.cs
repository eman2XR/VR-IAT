using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceIAT : MonoBehaviour
{
    public string IatName;
    public int StimuliPerPhase = 10;
    public float IncorrectSortPenalty = 1.0f;

    public RaceCategory RaceCategory1;
    public RaceCategory RaceCategory2;

    public WordCategory WordCategory1;
    public WordCategory WordCategory2;

    public List<IATPhase> IATPhases;

    public int CurrentPhaseIndex { get; set; } = 0;

    public float TotalDuration { get; set; } = 0;

    private void Awake()
    {
        foreach (IATPhase phase in IATPhases)
        {
            phase.PhaseCompleted += HandlePhaseCompleted;
        }

        RaceCategory1.ShuffleImages();
        RaceCategory2.ShuffleImages();

        WordCategory1.ShuffleWords();
        WordCategory1.ShuffleWords();
    }

    private void Start()
    {
        StartTest();
    }

    public void StartTest()
    {
        // Reorder the race categories at random (50-50 chance to reverse initial order).
        if (Random.value > 0.5f)
        {
            // Store temporarily, then reassign.
            var temp = RaceCategory1;

            RaceCategory1 = RaceCategory2;
            RaceCategory2 = temp;
        }

        // Do the same for word categories.
        if (Random.value > 0.5f)
        {
            // Store temporarily, then reassign.
            var temp = WordCategory1;

            WordCategory1 = WordCategory2;
            WordCategory2 = temp;
        }

        // Load the Welcome Phase.
        CurrentPhaseIndex = 0;
        IATPhases[0].LoadPhase(this);
    }

    private void HandlePhaseCompleted()
    {
        Debug.Log("Phase " + CurrentPhaseIndex + " completed. Duration = " + IATPhases[CurrentPhaseIndex].Duration);

        IATPhases[CurrentPhaseIndex].EndPhase();
        CurrentPhaseIndex++;

        // Move on to the next phase.
        if (CurrentPhaseIndex < IATPhases.Count)
        {
            StartCoroutine(LoadPhaseWithDelay());
        }
        else
        {
            // IAT completed. Calculate Results.
            Debug.Log("Test Complete. Calculate Results.");
        }
    }

    IEnumerator LoadPhaseWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        IATPhases[CurrentPhaseIndex].LoadPhase(this);
    }
}