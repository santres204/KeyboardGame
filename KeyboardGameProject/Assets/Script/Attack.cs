using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject attack;

    private List<ManageKeyBoard.key> keyBoard; // 칸 리스트
    private int attackNum;  //총 공격 개수(현재 스테이지 기준)
    private int attackDelay = 2;
    private int nowDelay;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeKeyBoard());
        nowDelay = attackDelay;
    }

    private IEnumerator InitializeKeyBoard()
    {
        // ManageKeyBoard 인스턴스를 찾아 adjList를 가져옴
        ManageKeyBoard manageKeyBoard = null;
        while (manageKeyBoard == null)
        {
            manageKeyBoard = FindObjectOfType<ManageKeyBoard>();
            yield return null; // 한 프레임 기다림
        }

        while (manageKeyBoard.keyBoard == null || manageKeyBoard.keyBoard.Count == 0)
        {
            yield return null; // 한 프레임 기다림
        }

        keyBoard = manageKeyBoard.keyBoard;
        CreateAttackField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateAttackField()//전체 맵 공격 필드 생성
    {
        for(int i = 0; i < keyBoard.Count; ++i)
        {
            GameObject attack1 = Instantiate(attack, GameObject.Find(keyBoard[i].name).transform);
            attack1.SetActive(true);
        }
    }

    public void SummonAttack()// 공격 소환
    {
        if (nowDelay > 0)//적 소환 주기 계산
        {
            nowDelay -= 1;
            return;
        }
        int i, num = 0;
        for(i = 0; i < ManageKeyBoard.numV; ++i)//로직 수정해야함
        {
            num = Random.Range(0, ManageKeyBoard.numV);// 랜덤 범위로 공격 할당
            Player player = FindObjectOfType<Player>();

            if (!keyBoard[num].isAttack && keyBoard[num].name != player.currentKey)
            {
                break;
            }
        }
        if (i < ManageKeyBoard.numV)
        {
            int attackID = Random.Range(1, 2);
            GameObject.Find(keyBoard[num].name).GetComponent<Transform>().Find("Attack(Clone)").GetComponent<SpriteRenderer>().sprite 
                = Resources.Load<Sprite>("Image/" + "attack" + attackID.ToString());
            keyBoard[num].isAttack = true;
            keyBoard[num].attack = attackID;
        }
        nowDelay = attackDelay;
    }

    public class Attack_kind
    {
        public int damage;
    }

}
