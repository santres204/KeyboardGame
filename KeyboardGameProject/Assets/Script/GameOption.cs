using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOption : MonoBehaviour
{

    public GameObject Option;
    public GameObject timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//ESC 입력
        {
            if (Option.activeInHierarchy)//현재 가시성 유무
                HideOption();
            else
                ShowOption();
        }
    }

    public void ShowOption()//옵션 표시
    {
        Option.SetActive(true);
        timer.GetComponent<Timer>().Pause();//게임 정지
    }

    public void HideOption()//옵션 비표시
    {
        Option.SetActive(false);
        if (!FindAnyObjectByType<Experience>().selectOption)//경험치 획득으로 인한 시간 정지 판단
        {
            timer.GetComponent<Timer>().Resume();//기존에 시간 정지가 아니였다면 재개
        }
    }

}
