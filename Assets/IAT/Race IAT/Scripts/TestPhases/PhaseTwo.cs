using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhaseTwo : IATPhase
{
    private WordCategory _wordCategoryLeft;
    private WordCategory _wordCategoryRight;

    public TMP_Text StimulusText;

    public override void LoadPhase(RaceIAT raceIAT)
    {
        base.LoadPhase(raceIAT);

        // Set Word categories. 
        _wordCategoryLeft = raceIAT.WordCategory1;
        _wordCategoryRight = raceIAT.WordCategory2;

        // Set Category names.
        CategoryTextLeft.text = _wordCategoryLeft.CategoryName;
        CategoryTextRight.text = _wordCategoryRight.CategoryName;

        //< color =#005500>left</color>
        instructionsCategoryTextLeft.text = _wordCategoryLeft.CategoryName;
        instructionsCategoryTextRight.text = _wordCategoryRight.CategoryName;
    }

    protected override IEnumerator TestCoroutine()
    {
        StimulusText.gameObject.SetActive(true);
        return base.TestCoroutine();
    }

    protected override void ShowStimuli()
    {
        // Show stimuli from either the left or right category.
        if (Random.value > 0.5f)
        {
            CorrectSortCategory = SortCategory.Left;
            StimulusText.text = _wordCategoryLeft.GetWord();
        }
        else
        {
            CorrectSortCategory = SortCategory.Right;
            StimulusText.text = _wordCategoryRight.GetWord();
        }
    }
}
