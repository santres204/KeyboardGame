using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEnemy : MonoBehaviour
{
    public GameObject manageKeyboard;
    //public GameObject enemyPrefab1;
    //public GameObject enemyPrefab2;

    private int summonDelay = 2; //몹 소환 주기(플레이어 이동 기준)
    private int nowDelay;
    List<int> random;
    List<GameObject> enemyPrefabList;
    List<ManageKeyBoard.key> keyBoard;

    // Start is called before the first frame update
    void Start()
    {
        InitEnemyPrefab();
        keyBoard = manageKeyboard.GetComponent<ManageKeyBoard>().keyBoard;//리스트 가져오기\
        random = new List<int>();
        nowDelay = summonDelay;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void InitEnemyPrefab()//적 프리펩 리스트화
    {
        enemyPrefabList = new List<GameObject>();
        for (int i = 1; i <= 3; i++)
        {
            enemyPrefabList.Add(Resources.Load<GameObject>("Prefab/" + "Enemy" + i.ToString()));//에셋에서 프리펩 가져오기
        }
    }

    public void SummonEnemy()// 적 생성
    {
        if(nowDelay > 0)//적 소환 주기 계산
        {
            nowDelay -= 1;
            return;
        }
        random.Clear();
        int num = 0;
        int i;
        
        for(i = 0; i < ManageKeyBoard.numV; ++i)
        {
            do
            {
                num = Random.Range(0, ManageKeyBoard.numV);// 적이 생성될 수 있는 칸의 범주
            } while (random.Contains(num));

            Player player = FindObjectOfType<Player>();

            if (keyBoard[num].isSuburb && !keyBoard[num].isEnemy && player.currentKey != keyBoard[num].name)// 적이 생성 가능할 시에 +1
                break;
            else
                random.Add(num);
        }
        if (i < ManageKeyBoard.numV)
        {
            keyBoard[num].SetEnemy(true);// 중단 값에서 적 생성
        }
        nowDelay = summonDelay;
    }

    public void CalcDelay()
    {
        for (int i = 0; i < ManageKeyBoard.numV; ++i)
        {
            int enemyN = Random.Range(0, 3);//적 종류 중 랜덤 1개 선택
            keyBoard[i].Countdelay(enemyPrefabList[enemyN]);//적 생성 딜레이에 넘겨주기
        }

        SummonEnemy();
    }

    public void moveEnemy()//전체 적 찾아서 이동
    {
        foreach(ManageKeyBoard.key key in keyBoard)
        {
            try
            {
                GameObject.Find(key.name).GetComponent<Transform>().Find("Enemy1(Clone)").GetComponent<Enemy>().MoveToPlayer();
            }
            catch
            {
                Debug.Log("적 존재 X");
            }
        }
    }

}