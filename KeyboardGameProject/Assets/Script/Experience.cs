using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    public int EXPLevel;
    public GameObject levelText;
    public GameObject EXPBar;
    public GameObject levelUp;
    public GameObject timer;
    public bool selectOption = false;

    private int EXP;
    private List<int> EXPList;
    private const int maxEXPLevel = 10;

    // Start is called before the first frame update
    void Start()
    {
        EXPList = new List<int>(10);
        EXP = 0;
        EXPLevel = 0;
        if (EXPLevel < maxEXPLevel - 1)
        {
            InitEXPList();
        }
        FindAnyObjectByType<ManageEnemy>().ChangeDelay(9 - EXPLevel > 0 ? 9 - EXPLevel : 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectOption)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelectLevelUp();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SelectLevelUp();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelectLevelUp();
            }
        }
    }

    public void AddEXP(int addedEXP)//�� óġ�� �۵�
    {
        EXP += addedEXP;
        CalcEXPLevel();
    }

    public void InitLevelUpOption()//���� �� ������ �ʱ�ȭ
    {

    }

    public void SelectLevelUp()
    {
        selectOption = false;
        levelUp.SetActive(false);
        timer.GetComponent<Timer>().Resume();
        FindAnyObjectByType<ManageEnemy>().ChangeDelay(9 - EXPLevel > 0 ? 9 - EXPLevel : 1);
    }

    private void CalcEXPLevel()//����ġ�� ���� ����ġ ���� ó��
    {
        if(EXPList[EXPLevel] <= EXP)
        {
            EXP -= EXPList[EXPLevel];
            EXPLevel += 1;
            levelText.GetComponent<TextMeshProUGUI>().text = "Level " + (EXPLevel+1).ToString();
            levelUp.SetActive(true);
            selectOption = true;
            timer.GetComponent<Timer>().Pause();
        }
        EXPBar.GetComponent<Slider>().value = (float)EXP / (float)EXPList[EXPLevel];
    }

    private void InitEXPList()//ó�� ����ġ ���̺� �ʱ�ȭ / ���� JSON���
    {
        for(int i = 1; i <= 10; ++i)
        {
            EXPList.Add(i * 2);
        }
    }
}
