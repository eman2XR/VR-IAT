using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhaseFour : IATPhase
{
    public RaceCategory RaceCategoryLeft { get; private set; }
    public RaceCategory RaceCategoryRight { get; private set; }

    public WordCategory WordCategoryLeft { get; private set; }
    public WordCategory WordCategoryRight { get; private set; }

    public RawImage Portrait;
    public TMP_Text StimulusText;

    public enum associationType { WhiteGood, BlackGood};
    public associationType AssociationType;

    bool lastStimuliWasImage;

    public override void LoadPhase(RaceIAT raceIAT)
    {
        base.LoadPhase(raceIAT);

        // Assign Race and Word categories. (Should follow order of assignment in Phase 1 and 2).
        RaceCategoryLeft = raceIAT.RaceCategory1;
        RaceCategoryRight = raceIAT.RaceCategory2;
        WordCategoryLeft = raceIAT.WordCategory1;
        WordCategoryRight = raceIAT.WordCategory2;

        // Set Category names.
        CategoryTextLeft.text = RaceCategoryLeft.CategoryName + "<color=#000000>\nOR\n</color> <color=#0032ad>" + WordCategoryLeft.CategoryName + "</color>";
        CategoryTextRight.text = RaceCategoryRight.CategoryName + "<color=#000000>\nOR\n</color> <color=#0032ad>" + WordCategoryRight.CategoryName + "</color>";

        //catergory name in instructions #0032ad
        instructionsCategoryTextLeft.text = "<color=#005500><b>" + RaceCategoryLeft.CategoryName + "</color></b> and for <color=#0032ad><b>" + WordCategoryLeft.CategoryName + "</color></b>";
        instructionsCategoryTextRight.text = "<color=#005500><b>" + RaceCategoryRight.CategoryName + "</color></b> and for <color=#0032ad><b>" + WordCategoryRight.CategoryName + "</color></b>";

        if (RaceCategoryLeft.name.Contains("White") && WordCategoryLeft.name.Contains("Good"))
        {
            print(RaceCategoryLeft.name);
            AssociationType = associationType.WhiteGood;
            print("this is a WhiteGood associatian phase");
        }
        else if (RaceCategoryLeft.name.Contains("Black") && WordCategoryLeft.name.Contains("Good"))
        {
            print(RaceCategoryRight.name);
            AssociationType = associationType.BlackGood;
            print("this is a BlackGood associatian phase");
        }

        if (RaceCategoryRight.name.Contains("White") && WordCategoryRight.name.Contains("Good"))
        {
            AssociationType = associationType.WhiteGood;
            print("this is a WhiteGood associatian phase");
        }
        else if (RaceCategoryRight.name.Contains("Black") && WordCategoryRight.name.Contains("Good"))
        {
            AssociationType = associationType.BlackGood;
            print("this is a BlackGood associatian phase");
        }
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
                StimulusText.text = WordCategoryLeft.GetWord();

                Portrait.gameObject.SetActive(false);
                StimulusText.gameObject.SetActive(true);

                lastStimuliWasImage = false;
            }
            else
            {
                Portrait.texture = RaceCategoryLeft.GetPortrait();

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
                StimulusText.text = WordCategoryRight.GetWord();

                Portrait.gameObject.SetActive(false);
                StimulusText.gameObject.SetActive(true);

                lastStimuliWasImage = false;
            }
            else
            {
                Portrait.texture = RaceCategoryRight.GetPortrait();

                Portrait.gameObject.SetActive(true);
                StimulusText.gameObject.SetActive(false);

                lastStimuliWasImage = true;
            }
        }
    }
}
