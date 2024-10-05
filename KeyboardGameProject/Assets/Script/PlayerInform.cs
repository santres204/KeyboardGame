using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInform : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHp;
    public float hp;
    public GameObject hpBar;

    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDamaged(float damage)//�÷��̾ ������ ������ ����
    {
        hp -= damage;
        hpBar.GetComponent<Slider>().value = (int)(hp / maxHp);
        if(hp <= 0)
        {
            Debug.Log("���� ����");
        }
    }
}
