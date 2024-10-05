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
        if (Input.GetKeyDown(KeyCode.Escape))//ESC �Է�
        {
            if (Option.activeInHierarchy)//���� ���ü� ����
                HideOption();
            else
                ShowOption();
        }
    }

    public void ShowOption()//�ɼ� ǥ��
    {
        Option.SetActive(true);
        timer.GetComponent<Timer>().Pause();//���� ����
    }

    public void HideOption()//�ɼ� ��ǥ��
    {
        Option.SetActive(false);
        if (!FindAnyObjectByType<Experience>().selectOption)//����ġ ȹ������ ���� �ð� ���� �Ǵ�
        {
            timer.GetComponent<Timer>().Resume();//������ �ð� ������ �ƴϿ��ٸ� �簳
        }
    }

}
