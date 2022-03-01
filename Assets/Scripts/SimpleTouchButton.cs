using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleTouchButton : MonoBehaviour
{
    public Button button;
    AudioSource audio;
    float delay = 0.5f;
    bool waited = true;

    private void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (waited)
        {
            button.onClick.Invoke();
            //print("button pressed");
            StartCoroutine(ResetButton());
            audio.Play();
        }
    }

    IEnumerator ResetButton()
    {
        waited = false;
        button.GetComponent<Image>().color = Color.grey;
        yield return new WaitForSeconds(0.25f);
        button.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(delay - 0.25f);
        waited = true;
    }
}
