using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhaseThree : IATPhase
{
    private RaceCategory _raceCategoryLeft;
    private RaceCategory _raceCategoryRight;

    private WordCategory _wordCategoryLeft;
    private WordCategory _wordCategoryRight;

    public RawImage Portrait;
    public TMP_Text StimulusText;

    bool lastStimuliWasImage;

    public override void LoadPhase(RaceIAT raceIAT)
    {
        base.LoadPhase(raceIAT);

        // Assign Race and Word categories. (Should follow order of assignment in Phase 1 and 2).
        _raceCategoryLeft = raceIAT.RaceCategory1;
        _raceCategoryRight = raceIAT.RaceCategory2;
        _wordCategoryLeft = raceIAT.WordCategory1;
        _wordCategoryRight = raceIAT.WordCategory2;

        // Set Category names.
        CategoryTextLeft.text = _raceCategoryLeft.CategoryName + "<color=#000000>\nOR\n</color> <color=#0032ad>" + _wordCategoryLeft.CategoryName + "</color>";
        CategoryTextRight.text = _raceCategoryRight.CategoryName + "<color=#000000>\nOR\n</color> <color=#0032ad>" + _wordCategoryRight.CategoryName + "</color>";

        //catergory name in instructions #0032ad
        instructionsCategoryTextLeft.text ="<color=#005500><b>" + _raceCategoryLeft.CategoryName + "</color></b> and for <color=#0032ad><b>" + _wordCategoryLeft.CategoryName + "</color></b>";
        instructionsCategoryTextRight.text ="<color=#005500><b>" + _raceCategoryRight.CategoryName + "</color></b> and for <color=#0032ad><b>" + _wordCategoryRight.CategoryName + "</color></b>";
    }

    protected override void ShowStimuli()
    {
        // Show stimuli from either the left or right categories.
        if (Random.value > 0.5f)
        {
            CorrectSortCategory = SortCategory.Left;

            // Then pick either a word or portrait assigned to the Left category.
            if (lastStimuliWasImage)
            {
                StimulusText.text = _wordCategoryLeft.GetWord();

                Portrait.gameObject.SetActive(false);
                StimulusText.gameObject.SetActive(true);

                lastStimuliWasImage = false;
            }
            else
            {
                Portrait.texture = _raceCategoryLeft.GetPortrait();

                Portrait.gameObject.SetActive(true);
                StimulusText.gameObject.SetActive(false);

                lastStimuliWasImage = true;
            }
        }
        else
        {
            CorrectSortCategory = SortCategory.Right;

            // Then pick either a word or portrait assigned to the Right category.
            if (lastStimuliWasImage)
            {
                StimulusText.text = _wordCategoryRight.GetWord();

                Portrait.gameObject.SetActive(false);
                StimulusText.gameObject.SetActive(true);

                lastStimuliWasImage = false;
            }
            else
            {
                Portrait.texture = _raceCategoryRight.GetPortrait();

                Portrait.gameObject.SetActive(true);
                StimulusText.gameObject.SetActive(false);

                lastStimuliWasImage = true;
            }
        }
    }
}
