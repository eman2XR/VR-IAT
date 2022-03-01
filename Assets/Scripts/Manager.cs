using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public bool rightButtonPressed;
    public bool leftButtonPressed;

    public Material maleDarkSkinHands;
    public Material maleLightSkinHands;
    public GameObject maleLeftHand;
    public GameObject maleRightHand;
    public GameObject leftDefaultHand;
    public GameObject rightDefaultHand;

    public Material femaleDarkSkinHands;
    public Material femaleLightSkinHands;
    public GameObject femaleLeftHand;
    public GameObject femaleRightHand;

    public GameObject startButton;

    private void Awake()
    {
        instance = this;
    }

    public void RightButtonPressed()
    {
        rightButtonPressed = true;
        //leftButtonPressed = false;
        //print("rightButtonPressed");
        StartCoroutine(ToggleWithDelay(rightButtonPressed));
    }

    public void LeftButtonPressed()
    {
        leftButtonPressed = true;
        //leftButtonPressed = false;
        //print("leftButtonPressed");
        StartCoroutine(ToggleWithDelay(leftButtonPressed));
    }

    IEnumerator ToggleWithDelay(bool button)
    {
        yield return new WaitForEndOfFrame();

        if (button = rightButtonPressed)
            rightButtonPressed = false;
        else
            leftButtonPressed = false;
    }

    public void MaleDarkSkinHandsChosen()
    {
        maleLeftHand.SetActive(true);
        maleRightHand.SetActive(true);
        maleLeftHand.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = maleDarkSkinHands;
        maleRightHand.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = maleDarkSkinHands;

        leftDefaultHand.SetActive(false);
        rightDefaultHand.SetActive(false);
        femaleLeftHand.SetActive(false);
        femaleRightHand.SetActive(false);
    }

    public void MaleLightSkinHandsChosen()
    {
        maleLeftHand.SetActive(true);
        maleRightHand.SetActive(true);
        maleLeftHand.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = maleLightSkinHands;
        maleRightHand.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = maleLightSkinHands;

        leftDefaultHand.SetActive(false);
        rightDefaultHand.SetActive(false);
        femaleLeftHand.SetActive(false);
        femaleRightHand.SetActive(false);
    }

    public void FemaleDarkSkinHandsChosen()
    {
        femaleLeftHand.SetActive(true);
        femaleRightHand.SetActive(true);
        femaleLeftHand.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = femaleDarkSkinHands;
        femaleRightHand.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = femaleDarkSkinHands;

        leftDefaultHand.SetActive(false);
        rightDefaultHand.SetActive(false);
        maleLeftHand.SetActive(false);
        maleRightHand.SetActive(false);
    }

    public void FemaleLightSkinHandsChosen()
    {
        femaleLeftHand.SetActive(true);
        femaleRightHand.SetActive(true);
        femaleLeftHand.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = femaleLightSkinHands;
        femaleRightHand.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = femaleLightSkinHands;

        leftDefaultHand.SetActive(false);
        rightDefaultHand.SetActive(false);
        maleLeftHand.SetActive(false);
        maleRightHand.SetActive(false);
    }

    public void DefaultHandsChosen()
    {
        leftDefaultHand.SetActive(true);
        rightDefaultHand.SetActive(true);
        maleLeftHand.SetActive(false);
        maleRightHand.SetActive(false);
        femaleLeftHand.SetActive(false);
        femaleRightHand.SetActive(false);
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
