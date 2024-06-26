using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private List<ManageKeyBoard.key> keyBoard; // ��������Ʈ
    public GameObject attack;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeKeyBoard());
    }

    private IEnumerator InitializeKeyBoard()
    {
        // ManageKeyBoard �ν��Ͻ��� ã�� adjList�� ������
        ManageKeyBoard manageKeyBoard = null;
        while (manageKeyBoard == null)
        {
            manageKeyBoard = FindObjectOfType<ManageKeyBoard>();
            yield return null; // �� ������ ��ٸ�
        }

        while (manageKeyBoard.keyBoard == null || manageKeyBoard.keyBoard.Count == 0)
        {
            yield return null; // �� ������ ��ٸ�
        }

        keyBoard = manageKeyBoard.keyBoard;
        CreateAttackField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateAttackField()
    {
        for(int i = 0; i < keyBoard.Count; ++i)
        {
            GameObject attack1 = Instantiate(attack, GameObject.Find(keyBoard[i].name).transform);
            attack1.SetActive(true);
        }
    }

    public void SummonAttack()// �� ����
    {
        int i, num = 0;
        for(i = 0; i < ManageKeyBoard.numV; ++i)
        {
            num = Random.Range(0, ManageKeyBoard.numV);// ������ ������ �� �ִ� ĭ�� ����
            if (!keyBoard[num].isAttack)
            {
                break;
            }
        }
        if (i < ManageKeyBoard.numV)
        {
            Debug.Log(num + "���� �Ҵ�");
            GameObject.Find(keyBoard[num].name).GetComponent<Transform>().Find("Attack(Clone)").GetComponent<SpriteRenderer>().sprite 
                = Resources.Load<Sprite>("Image/" + "attack");
        }
    }

    class Attack_kind
    {

    }

}
