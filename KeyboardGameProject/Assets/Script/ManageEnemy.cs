using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEnemy : MonoBehaviour
{
    public GameObject manageKeyboard;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ManageKeyBoard.numV);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SummonEnemy()// 적 생성
    {
        int num = Random.Range(1, ManageKeyBoard.enemyV);// 적이 생성될 수 있는 칸의 범주
        int i, now = 0;
        List<ManageKeyBoard.key> keyBoard = manageKeyboard.GetComponent<ManageKeyBoard>().keyBoard;//리스트 가져오기
        for(i = 0; i < ManageKeyBoard.numV; ++i)
        {
            if (keyBoard[i].isSuburb)// 적이 생성 가능할 시에 +1
            {
                now += 1;
            }
            if(now == num)// 랜덤 값과 동일해지면 중단
            {
                break;
            }
        }

        keyBoard[i].setEnemy(true);// 중단 값에서 적 생성
    }

}