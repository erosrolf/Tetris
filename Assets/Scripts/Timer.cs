using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int sec = 0;
    private int min = 0;
    private TextMeshProUGUI timerText;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TimeFlow());
    }

    IEnumerator TimeFlow()
    {
        while (true)
        {
            if (GameManager.GetCurrentState() == GameManager.State.Playing)
            {
                if (++sec == 60)
                {
                    min++;
                    sec = 0;
                }
                timerText.text = min.ToString("D2") + " : " + sec.ToString("D2");
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }
        }
    }
}
