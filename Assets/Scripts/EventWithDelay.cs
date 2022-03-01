using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventWithDelay : MonoBehaviour
{
    public float delay = 2f;
    public UnityEvent onEnableWithDelay;

    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        onEnableWithDelay.Invoke();
    }
}
