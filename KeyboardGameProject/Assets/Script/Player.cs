using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string currentKey; // 현재 위치
    private Dictionary<string, List<string>> adjList; // 인접리스트

    void Start()
    {
        StartCoroutine(InitializeAdjList()); // 키보드 인접리스트가 완성될때까지 대기하기위함
    }

    private IEnumerator InitializeAdjList()
    {
        // ManageKeyBoard 인스턴스를 찾아 adjList를 가져옴
        ManageKeyBoard manageKeyBoard = null;
        while (manageKeyBoard == null)
        {
            manageKeyBoard = FindObjectOfType<ManageKeyBoard>();
            yield return null; // 한 프레임 기다림
        }

        while (manageKeyBoard.adjList == null || manageKeyBoard.adjList.Count == 0)
        {
            yield return null; // 한 프레임 기다림
        }

        adjList = manageKeyBoard.adjList;

        currentKey = "A";
        MoveToKey(currentKey);

        PrintAdjList(); // adjList 출력
    }


    public void MoveToKey(string key)
    {
        currentKey = key;
        GameObject keyObj = GameObject.Find(key);
        if (keyObj != null)
        {
            transform.position = keyObj.transform.position;
        }
    }

    void Update()
    {
        KeyInput();
    }

    private void KeyInput()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    string key = keyCode.ToString();
                    if (key.Length == 1 && char.IsLetter(key[0]))
                    {
                        AttemptMove(key.ToUpper());
                    }
                }
            }
        }
    }


    // 키로 이동시도 (거리가 1인경우 이동가능)
    private void AttemptMove(string key)
    {
        Debug.Log(currentKey + "에서 " + key + "로 이동 시도");

        if (adjList[currentKey].Contains(key))
        {
            MoveToKey(key);
            Debug.Log(currentKey + "에서 " + key + "로 이동");
        }
        else
        {
            Debug.Log(currentKey + "와 " + key + "로의 연결 없음");
        }
    }

    // 디버깅용
    private void PrintAdjList()
    {
        string[] row1 = { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P" };
        string[] row2 = { "A", "S", "D", "F", "G", "H", "J", "K", "L" };
        string[] row3 = { "Z", "X", "C", "V", "B", "N", "M", "<" };

        Debug.Log("Row 1:");
        PrintRowAdjList(row1);

        Debug.Log("Row 2:");
        PrintRowAdjList(row2);

        Debug.Log("Row 3:");
        PrintRowAdjList(row3);
    }

    private void PrintRowAdjList(string[] row)
    {
        foreach (var key in row)
        {
            if (adjList.ContainsKey(key))
            {
                string adjacentKeys = string.Join(", ", adjList[key]);
                Debug.Log("Key: " + key + " -> Adjacent Keys: " + adjacentKeys);
            }
        }
    }
}
