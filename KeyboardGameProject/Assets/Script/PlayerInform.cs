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

    public void PlayerDamaged(float damage)//플레이어가 적에게 데미지 입음
    {
        hp -= damage;
        hpBar.GetComponent<Slider>().value = (int)(hp / maxHp);
        if(hp <= 0)
        {
            Debug.Log("게임 종료");
        }
    }
}
