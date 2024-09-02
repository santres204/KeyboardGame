using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxhp;//최대 hp
    public int attackDamage;//공격 데미지
    public int moveCycle;//이동 싸이클 딜레이
    public int deathEXP;//플레이어 획득 경험치
    public float hp;//현재 hp


    private GameObject enemyHpBar;//hpBar 게이지
    private int nowCycle;//현재 딜레이
    private Dictionary<string, List<string>> adjList;
    private List<ManageKeyBoard.key> keyBoard;

    void Start()
    {
        ManageKeyBoard manageKeyBoard = FindObjectOfType<ManageKeyBoard>();
        enemyHpBar = Instantiate(Resources.Load<GameObject>("Prefab/" + "EnemyHp"), this.transform);
        enemyHpBar.SetActive(false);

        nowCycle = moveCycle;
        hp = maxhp;

        adjList = manageKeyBoard.adjList;
        keyBoard = manageKeyBoard.keyBoard;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToPlayer()
    {
        if (nowCycle > 0)
        {
            nowCycle -= 1;
            this.transform.Find("Canvas").transform.Find("MoveCycleText").GetComponent<TextMeshProUGUI>().text = (nowCycle).ToString();
            return;
        }

        Vector2 playerPosition = GameObject.Find("Player").transform.position;

        bool moved = false;

        // 현재 타일에서 적 플래그 제거
        foreach (ManageKeyBoard.key key in keyBoard)
        {
            if (key.name.Equals(this.transform.parent.name))
            {
                key.isEnemy = false;
                break;
            }
        }

        // 플레이어에 가까운 타일을 찾기
        float minDistance = float.MaxValue;
        string bestKey = null;

        foreach (string keyName in adjList[this.transform.parent.name])
        {
            foreach (ManageKeyBoard.key key in keyBoard)
            {
                if (key.name.Equals(keyName) && !key.isEnemy)
                {
                    float distanceToPlayer = Vector2.Distance(GameObject.Find(keyName).transform.position, playerPosition);
                    if (distanceToPlayer < minDistance)
                    {
                        minDistance = distanceToPlayer;
                        bestKey = keyName;
                        moved = true;
                    }
                }
            }
        }

        // 최적의 타일로 이동
        if (moved && bestKey != null)
        {
            MoveToKey(bestKey);
            foreach (ManageKeyBoard.key key in keyBoard)
            {
                if (key.name.Equals(bestKey))
                {
                    key.isEnemy = true;
                    break;
                }
            }
        }

        nowCycle = moveCycle;
        this.transform.Find("Canvas").transform.Find("MoveCycleText").GetComponent<TextMeshProUGUI>().text = (nowCycle).ToString();
    }

    public void MoveToKey(string key)
    {
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
            foreach (ManageKeyBoard.key key in keyBoard)
            {
                if (key.name.Equals(this.transform.parent.name))
                {
                    key.isEnemy = false;
                    break;
                }
            }

            FindAnyObjectByType<Experience>().AddEXP(deathEXP);
            Destroy(this.gameObject);
        }
        else
        {
            if (enemyHpBar.activeInHierarchy == false)
            {
                enemyHpBar.SetActive(true);
            }
            enemyHpBar.transform.GetComponentInChildren<Slider>().value = hp / maxhp;
        }
    }

}
