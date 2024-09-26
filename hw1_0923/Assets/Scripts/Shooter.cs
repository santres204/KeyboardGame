using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Rigidbody greenBullet;
    public Rigidbody blueBullet;
    public Rigidbody yellowBullet;
    public float power = 1500f;
    public float moveSpeed = 2f;
    public float duration = 1.0f;  // 흔들리는 시간
    public float magnitude = 0.1f; // 흔들림의 강도
    private float shakeTimeRemaining = 0f; // 흔들림이 남은 시간

    void Update()
    {
        Vector3 deltaMove = new Vector3(
            Input.GetKeyDown(KeyCode.A) ? -moveSpeed : (Input.GetKeyDown(KeyCode.D) ? moveSpeed : 0),
            Input.GetKeyDown(KeyCode.E) ? -moveSpeed : (Input.GetKeyDown(KeyCode.Q) ? moveSpeed : 0),
            Input.GetKeyDown(KeyCode.S) ? -moveSpeed : (Input.GetKeyDown(KeyCode.W) ? moveSpeed : 0)
        );

        transform.Translate(deltaMove);

        if (Input.GetButtonUp("Fire1"))
        {
            // 초록색 총알 발사
            Rigidbody instance = Instantiate(greenBullet, transform.position, transform.rotation) as Rigidbody;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            instance.AddForce(fwd * Random.Range(0.0f, power));

            // 흔들림 시작
            shakeTimeRemaining = duration;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            // 파란색 총알 발사
            Rigidbody instance = Instantiate(blueBullet, transform.position, transform.rotation) as Rigidbody;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            instance.AddForce(fwd * Random.Range(0.0f, power));

            // 흔들림 시작
            shakeTimeRemaining = duration;
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            // 노란색 총알 발사
            Rigidbody instance = Instantiate(yellowBullet, transform.position, transform.rotation) as Rigidbody;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            instance.AddForce(fwd * Random.Range(0.0f, power));

            // 흔들림 시작
            shakeTimeRemaining = duration;
        }

        // 카메라 흔들림 처리
        if (shakeTimeRemaining > 0)
        {
            // 흔들림 시간 감소
            shakeTimeRemaining -= Time.deltaTime;

            // 카메라를 무작위로 이동
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.Translate(new Vector3(x, y, 0));
        }
    }
}
