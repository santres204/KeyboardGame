using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject manageEnemy;
    public GameObject manageKeyBoard;
    public GameObject attack;
    public GameObject player;
    public static float cycle = 2;//한 사이클의 단위
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        GameStart1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GameStart1()
    {
        manageEnemy.GetComponent<ManageEnemy>().CalcDelay();//적 생성 칸 지정
        player.GetComponent<Player>().turnMove = true; //플레이어 이동
    }
    public void GameStart2()
    {
        attack.GetComponent<Attack>().SummonAttack();//공격 생성
        manageEnemy.GetComponent<ManageEnemy>().moveEnemy();//전체 적 이동(싸이클 각자 판단)
        Invoke("GameStart1",0.2f);
    }

}
