using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuBestTimes : MonoBehaviour
{
    [SerializeField] TMP_Text level1;
    [SerializeField] string level1key;
    [SerializeField] TMP_Text level2;
    [SerializeField] string level2key;
    [SerializeField] TMP_Text level3;
    [SerializeField] string level3key;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(level1key))
        {
            level1.text = "Best Time\n" + PlayerPrefs.GetFloat(level1key);
        }
        else
        {
            level1.text = "Best Time\nN/A";
        }

        if (PlayerPrefs.HasKey(level2key))
        {
            level2.text = "Best Time\n" + PlayerPrefs.GetFloat(level2key);
        }
        else
        {
            level2.text = "Best Time\nN/A";
        }

        if (PlayerPrefs.HasKey(level3key))
        {
            level3.text = "Best Time\n" + PlayerPrefs.GetFloat(level3key);
        }
        else
        {
            level3.text = "Best Time\nN/A";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
