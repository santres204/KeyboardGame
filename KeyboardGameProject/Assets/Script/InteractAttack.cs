using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAttack : MonoBehaviour
{
    public GameObject player;
    public string currentKey; // 현재 위치
    public GameObject attackPrefab;

    private List<ManageKeyBoard.key> keyBoard;
    private Stack<int> attackStack;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeKeyBoard());
        attackStack = new Stack<int>();
        //CreateStack();
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
    }

    // Update is called once per frame
    void Update()
    {
        Input_Attack();
    }

    private void Input_Attack()//공격 입력 받기
    {
        if (Input.GetKeyDown(KeyCode.Space))//스페이스바 입력 받기
        {
          
        }
    }

    private void CreateStack()
    {
        for (int i = 1; i <= 3; ++i)
        {
            GameObject attack1 = Instantiate(attackPrefab, GameObject.Find("back_space").transform);
            attack1.SetActive(true);
            attack1.name = "Attack" + i.ToString();
        }
    }

    public void InputStack(string currentKey)
    {
        foreach (ManageKeyBoard.key key in keyBoard)
        {
            if (key.name.Equals(currentKey))
            {
                if (key.isAttack)
                {
                    if (attackStack.Count < 3)
                    {
                        attackStack.Push(key.attack);
                        GameObject.Find("back_space").GetComponent<Transform>().Find("Attack" + attackStack.Count.ToString()).GetComponent<SpriteRenderer>().sprite
                    = Resources.Load<Sprite>("Image/" + "attack" + key.attack.ToString());
                    }

                    GameObject.Find(key.name).GetComponent<Transform>().Find("Attack(Clone)").GetComponent<SpriteRenderer>().sprite = null;
                    key.isAttack = false;
                    key.attack = 0;
                }
                else
                    Debug.Log("공격 할당 안됨");
                break;
            }
        }
        
    }

}
