using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsPhase : IATPhase
{
    public TMP_Text ResultsText;

    public TMP_Text whiteGoodAssociationText;
    public TMP_Text blackGoodAssociationText;
    int whiteGoodMistakes;
    int blackGoodMistakes;

    public PhaseFour phaseFour;
    public PhaseSeven phaseSeven;

    [Header("Thresholds")]
    public float NoPreference, Slight, Moderate, Strong = 0;

    private void Update()
    { }

    public override void LoadPhase(RaceIAT raceIAT)
    {
        this.gameObject.SetActive(true);

        // Get Phase4 and Phase7 Results. 
        PhaseFour p4 = raceIAT.IATPhases.FirstOrDefault(phase => phase is PhaseFour) as PhaseFour;
        PhaseSeven p7 = raceIAT.IATPhases.FirstOrDefault(phase => phase is PhaseSeven) as PhaseSeven;

        // Compare the time it took to finish each phase.
        // Less time to complete implies automatic preference for the associated categories in that phase.
        // In this IAT, we are interested in which Race-Good association the user has an automatic preference for.

        float whiteGood = 0;
        float blackGood = 0;

        // Look for the "Good" category in p4.
        if (p4.gameObject.GetComponent<PhaseFour>().AssociationType == PhaseFour.associationType.WhiteGood)
        {
            // Find out which Race it was associated with.
            // If "Good" is on the Left then the Race on the Left is associated with it.
            //if (p4.RaceCategoryLeft.CategoryName.Contains("White"))
            //{
                whiteGood = p4.Duration;
                whiteGoodMistakes = p4.Mistakes;

                // If the White-Good pair is in p4, then Black-Good must be in p7.
                blackGood = p7.Duration;
                blackGoodMistakes = p7.Mistakes;

        }
        else
            {
                blackGood = p4.Duration;
                blackGoodMistakes = p4.Mistakes;

                whiteGood = p7.Duration;
                whiteGoodMistakes = p7.Mistakes;
            }
        //}

        // Get percentage difference in durations.
        float pDelta = (whiteGood < blackGood) ? (blackGood - whiteGood) / whiteGood : (whiteGood - blackGood) / blackGood;

        // Assign result texts depending on results.
        string racePreference = (whiteGood < blackGood) ? "White People over Black People" : "Black People over White People";
        string preferenceLevel = "";
        
        if (pDelta < NoPreference)
        {
            preferenceLevel = "<b>no significant preference</b>";
        }
        else if (pDelta < Slight)
        {
            preferenceLevel = "<b>a slight automatic preference</b>";
        }
        else if (pDelta < Moderate)
        {
            preferenceLevel = "<b>a moderate automatic preference</b>";
        }
        else
        {
            preferenceLevel = "<b>a strong automatic preference</b>";
        }

        ResultsText.text = "During the IAT you just completed:\n" +
                           "Your responses suggested <b>" + preferenceLevel + "</b> for <b>" + racePreference + "</b>";

        //print(ResultsText.text);

        whiteGoodAssociationText.text = "The <b>White-Good</b> association took <b>" + whiteGood.ToString("F2") + "s</b> to complete.<b>" + whiteGoodMistakes + "</b> mistakes were made.";
        blackGoodAssociationText.text = "The <b>Black-Good</b> association took <b>" + blackGood.ToString("F2") + "s</b> to complete.<b>" + blackGoodMistakes + "</b> mistakes were made.";

        //print(whiteGood);
        //print(blackGood);
    }
}
