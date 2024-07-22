using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    private int EXP;
    private List<int> EXPList;

    public int EXPLevel;

    // Start is called before the first frame update
    void Start()
    {
        EXP = 0;
        EXPLevel = 0;
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
        if(EXPList[EXPLevel] > EXP)
        {
            EXPLevel += 1;
        }
    }

    private void InitEXPList()
    {
        for(int i = 0; i < 10; ++i)
        {
            EXPList.Add(i);
        }
    }
}
