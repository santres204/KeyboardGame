using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject manageEnemy;
    public GameObject manageKeyBoard;
    public GameObject attack;
    public static float cycle = 2;//한 사이클의 단위
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= cycle)
        {
            manageEnemy.GetComponent<ManageEnemy>().CalcDelay();
            attack.GetComponent<Attack>().SummonAttack();
            manageEnemy.GetComponent<ManageEnemy>().moveEnemy();
            time = 0;
        }
    }
}
