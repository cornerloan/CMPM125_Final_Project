using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField] TMP_Text currentTimeText;
    [SerializeField] TMP_Text bestTimeText;
    [SerializeField] string levelKey;

    private float currentTime;
    private float bestTime;

    // Start is called before the first frame update
    void Start()
    {
        //hide best time if there isn't one yet
        bestTime = PlayerPrefs.GetFloat(levelKey, -1f);
        bestTimeText.text = "Best Time\n" + convertFormat(bestTime);
        if (bestTime == -1f) bestTimeText.gameObject.SetActive(false);

        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        changeTimer();
    }

    void changeTimer()
    {
        currentTime += Time.deltaTime;
        currentTimeText.text = convertFormat(currentTime);
    }

    string convertFormat(float num)
    {
        int minutes = Mathf.FloorToInt(num / 60);
        int seconds = Mathf.FloorToInt(num % 60);
        int milliseconds = Mathf.FloorToInt((num - Mathf.Floor(num)) * 1000);

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void updateBestTime()
    {
        if(bestTime == -1f || currentTime < bestTime)
        {
            PlayerPrefs.SetFloat(levelKey, currentTime);
        }
    }
}
