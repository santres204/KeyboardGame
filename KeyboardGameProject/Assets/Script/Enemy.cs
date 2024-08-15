using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHp;
    public int attackDamage;
    public int moveCycle;
    public int deathEXP;

    private float hp;
    private int nowCycle;
    private Dictionary<string, List<string>> adjList;
    private List<ManageKeyBoard.key> keyBoard;

    void Start()
    {
        ManageKeyBoard manageKeyBoard = FindObjectOfType<ManageKeyBoard>();
        deathEXP = 1;
        adjList = manageKeyBoard.adjList;
        nowCycle = moveCycle;
        keyBoard = manageKeyBoard.keyBoard;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToPlayer()//플레이어 향해서 이동
    {
        if(nowCycle > 0)//자신의 이동 싸이클 수에 맞춰서 행동
        {
            nowCycle -= 1;
            return;
        }

        bool addOK = false;// 임시

        foreach (string keyName in adjList[this.transform.parent.name])//주변 칸 중 다른 적 없는 곳으로 이동
        {
            foreach (ManageKeyBoard.key key in keyBoard)
            {

                if (key.name.Equals(keyName))
                {
                    if (!key.isEnemy)
                    {
                        MoveToKey(keyName);
                        key.isEnemy = true;
                        addOK = true;
                    }
                    break;
                }
            }
            if (addOK)
                break;
        }

        foreach (ManageKeyBoard.key key in keyBoard)
        {
            if (key.name.Equals(this.transform.parent.name))
            {
                key.isEnemy = false;
                break;
            }
        }
        nowCycle = moveCycle;
    }
    public void MoveToKey(string key)//이동된 키값에 오브젝트와 부모 이동
    {
        //key = "back_" + key;
        GameObject keyObj = GameObject.Find(key);
        if (keyObj != null)
        {
            this.transform.parent = keyObj.transform;
            this.transform.position = keyObj.transform.position;
        }
    }

    public void GetDamage(int damage)//플레이어에게 데미지 입었을 때 처리
    {
        hp -= damage;
        if (hp <= 0)//경험치 획득 및 적 삭제
        {
            FindAnyObjectByType<Experience>().AddEXP(deathEXP);
            foreach (ManageKeyBoard.key key in keyBoard)
            {
                if (key.name.Equals(this.transform.parent.name))
                {
                    key.isEnemy = false;
                    break;
                }
            }
            Destroy(this.gameObject);
        }
        else//살면 hpbar 띄우기
        {
            GameObject hpPrefab = Resources.Load<GameObject>("Prefab/" + "EnemyHp");//프리펩 가져오기
            GameObject hpBar = Instantiate(hpPrefab, this.transform);
            hpBar.SetActive(true);
            hpBar.transform.Find("EnemyHp").GetComponent<Slider>().value = (hp / maxHp);//현재 hp비율에 따라 hpBar 조절
        }
    }

}
