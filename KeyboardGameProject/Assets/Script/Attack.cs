using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject attack;
    public int attackDelay;//공격 생성 딜레이

    private List<int> random;//공격 생성 중복 처리 리스트
    private int nowDelay;
    private List<ManageKeyBoard.key> keyBoard; // 칸 리스트

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeKeyBoard());
        random = new List<int>();
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
        if (nowDelay > 0)
        {
            nowDelay -= 1;
            return;
        }

        nowDelay = attackDelay;
        int i, num = 0;
        random.Clear();
        Player player = FindObjectOfType<Player>();

        for (i = 0; i < ManageKeyBoard.numV; ++i)
        {
            do
            {
                num = Random.Range(0, ManageKeyBoard.numV);// 적이 생성될 수 있는 칸의 범주
            } while (random.Contains(num));

            if (!keyBoard[num].isAttack && (player.currentKey != keyBoard[num].name))// 적이 생성 가능할 시에 +1
                break;
            else
                random.Add(num);
        }
        if (i < ManageKeyBoard.numV)
        {
            int attackID = Random.Range(1, 2);//공격 종류 정하기
            GameObject.Find(keyBoard[num].name).GetComponent<Transform>().Find("Attack(Clone)").GetComponent<SpriteRenderer>().sprite
                = Resources.Load<Sprite>("Image/" + "attack" + attackID.ToString());
            keyBoard[num].isAttack = true;
            keyBoard[num].attack = attackID;
        }

        
    }

    public class Attack_kind
    {
        public int damage;
    }

}
