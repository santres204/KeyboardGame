using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject manageEnemy;
    public GameObject manageKeyBoard;
    public GameObject attack;
    public GameObject player;
    public static float cycle = 2;//�� ����Ŭ�� ����
    public static bool GameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resume()
    {
        player.GetComponent<Player>().turnMove = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        player.GetComponent<Player>().turnMove = false;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void GameStart1()
    {
        manageEnemy.GetComponent<ManageEnemy>().CalcDelay();//�� ���� ĭ ����
        player.GetComponent<Player>().turnMove = true; //�÷��̾� �̵�
    }
    public void GameStart2()
    {
        attack.GetComponent<Attack>().SummonAttack();//���� ����
        manageEnemy.GetComponent<ManageEnemy>().moveEnemy();//��ü �� �̵�(����Ŭ ���� �Ǵ�)
        Invoke("GameStart1",0.2f);
    }
}
