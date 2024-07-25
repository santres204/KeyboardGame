using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    private int EXP;
    private List<int> EXPList;

    public int EXPLevel;
    public GameObject levelText;
    public GameObject EXPBar;

    // Start is called before the first frame update
    void Start()
    {
        EXPList = new List<int>(10);
        EXP = 0;
        EXPLevel = 0;
        InitEXPList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEXP(int addedEXP)
    {
        EXP += addedEXP;
        CalcEXPLevel();
        
    }
    
    private void CalcEXPLevel()
    {
        if(EXPList[EXPLevel] <= EXP)
        {
            Debug.Log("asdasdasdasd");

            EXP -= EXPList[EXPLevel];
            EXPLevel += 1;
            levelText.GetComponent<TextMeshProUGUI>().text = "Level " + (EXPLevel+1).ToString();
        }
        EXPBar.GetComponent<Slider>().value = (float)EXP / (float)EXPList[EXPLevel];
    }

    private void InitEXPList()
    {
        for(int i = 0; i < 10; ++i)
        {
            EXPList.Add(i * 2);
        }
    }
}
