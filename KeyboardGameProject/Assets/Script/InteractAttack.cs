using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAttack : MonoBehaviour
{
    public GameObject player;
    public string currentKey; // 현재 위치
    private List<ManageKeyBoard.key> keyBoard;

    // Start is called before the first frame update
    void Start()
    {
        
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
            if(keyBoard == null)//리스트가 비어있을 시 받아오기
            {
                ManageKeyBoard manageKeyBoard =  FindObjectOfType<ManageKeyBoard>();
                keyBoard = manageKeyBoard.keyBoard;
            }
            currentKey = player.GetComponent<Player>().currentKey;
            foreach (ManageKeyBoard.key key in keyBoard)
            {
                if (key.name.Equals(currentKey))
                {
                    if (key.isAttack)
                    {
                        InvokeAttack(key);
                    }
                    else
                        Debug.Log("공격 할당 안됨");
                    break;
                }
            }
        }

    }

    public void InvokeAttack(ManageKeyBoard.key key)//공격 발동
    {
        GameObject.Find(key.name).GetComponent<Transform>().Find("Attack(Clone)").GetComponent<SpriteRenderer>().sprite = null;
        key.isAttack = false;
    }

}
