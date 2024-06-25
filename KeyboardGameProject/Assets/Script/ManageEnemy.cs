using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEnemy : MonoBehaviour
{
    public GameObject manageKeyboard;
    public GameObject enemyPrefab;
    List<ManageKeyBoard.key> keyBoard;

    // Start is called before the first frame update
    void Start()
    {
         keyBoard = manageKeyboard.GetComponent<ManageKeyBoard>().keyBoard;//리스트 가져오기
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void SummonEnemy()// 적 생성
    {
        int num = Random.Range(1, ManageKeyBoard.enemyV + 1);// 적이 생성될 수 있는 칸의 범주
        int i, now = 0;
        
        for(i = 0; i < ManageKeyBoard.numV; ++i)
        {
            if (keyBoard[i].isSuburb && !keyBoard[i].isEnemy)// 적이 생성 가능할 시에 +1
            {
                now += 1;
            }
            if(now >= num)// 랜덤 값과 동일해지면 중단
            {
                break;
            }
        }
        if (i < ManageKeyBoard.numV)
        {
            Debug.Log("num: " + num);
            Debug.Log("i: " + i);
            keyBoard[i].SetEnemy(true);// 중단 값에서 적 생성
        }
    }

    public void CalcDelay()
    {
        for (int i = 0; i < ManageKeyBoard.numV; ++i)
        {
            keyBoard[i].Countdelay(enemyPrefab);
        }

        SummonEnemy();
    }

}