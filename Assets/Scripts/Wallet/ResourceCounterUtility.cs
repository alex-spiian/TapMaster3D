using System.Collections;
using TMPro;
using UnityEngine;

public static class ResourceCounterUtility
{
    public static IEnumerator CountResources(TextMeshProUGUI amountText, MonoBehaviour controller, float time, float startValue, float targetValue)
    {
        float currentTime = 0;

        while (currentTime < time)
        {
            float value = Mathf.Lerp(startValue, targetValue, currentTime / time);

            currentTime += Time.deltaTime;
            amountText.text = value.ToString("0");

            yield return null;
        }
    }
}