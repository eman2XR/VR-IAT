﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Defines how stimulus for RaceIAT Phase 1 should be shown.
/// </summary>
public class PhaseOne : IATPhase
{
    private RaceCategory _raceCategoryLeft;
    private RaceCategory _raceCategoryRight;

    public RawImage Portrait;

    public override void LoadPhase(RaceIAT raceIAT)
    {
        base.LoadPhase(raceIAT);

        // Assign Race categories.
        _raceCategoryLeft = raceIAT.RaceCategory1;
        _raceCategoryRight = raceIAT.RaceCategory2;

        // Set Category names.
        CategoryTextLeft.text = _raceCategoryLeft.CategoryName;
        CategoryTextRight.text = _raceCategoryRight.CategoryName;

        //< color =#005500>left</color>
        instructionsCategoryTextLeft.text = _raceCategoryLeft.CategoryName;
        instructionsCategoryTextRight.text = _raceCategoryRight.CategoryName;
    }

    protected override IEnumerator TestCoroutine()
    {
        Portrait.gameObject.SetActive(true);
        return base.TestCoroutine();
    }

    protected override void ShowStimuli()
    {
        // Show stimuli from either the left or right category.
        if (Random.value > 0.5f)
        {
            CorrectSortCategory = SortCategory.Left;
            Portrait.texture = _raceCategoryLeft.GetPortrait();
        }
        else
        {
            CorrectSortCategory = SortCategory.Right;
            Portrait.texture = _raceCategoryRight.GetPortrait();
        }
    }
}