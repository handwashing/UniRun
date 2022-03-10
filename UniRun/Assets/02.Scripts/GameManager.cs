using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//게임오버 상태를 표현하고, 게임 점수와 UI를 관리하는 매니저 씬에는
//단 하나의 게임 매니저만 존재할 수 있음
public class GameManager : MonoBehaviour
{
    public static GameManager instance; //싱글턴을 할당할 전역 변수

    public bool isGameover = false; //게임오버 상태
    public Text scoreText; //점수를 출력할 UI 텍스트
    public GameObject gameoverUI; // 게임오버시 활성화 할 UI 오브젝트

    private int score = 0; //게임 점수

    //게임 시작과 동시에 싱글턴을 구성
    private void Awake()
    {
        //싱글턴 변수 instance가 비어 있나요?
        if(instance == null)
        {
            // instance가 비어 있다면 그곳에 내 자신을 할당
            instance = this;
        }
        else
        {
            //instance에 이미 다른 게임매니저 오브젝트가 할당 되어 있다면...

            //하나의 씬에 두 개 이상의 게임매니저 오브젝트가 존재한다는 의미
            //싱글턴 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            //Debug.log("씬에 두 개 이상의 게임 매니저가 존재한다.")
            Destroy(gameObject);
        }
    }
 

    // 게임오버 상태에서 게임을 재시작 할 수 있게 하는 처리
    void Update()
    {
        // 게임오버 상태에서 마우스 왼쪽 버튼을 클릭한다면...
        if (isGameover && Input.GetMouseButtonDown(0)) //isGameover가 트루라면...
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // Dodge에서는 ("Samplescene")
        }
        
    }

    //점수를 증가시키는 메서드
    public void AddScore(int newScore) // (int newScore)-> 정수로 표현
    {
        //게임오버가 아니라면
        if(!isGameover) // =if (isGameover) return;
        {
            //점수를 증가시켜라
            score += newScore;
            scoreText.text = "Score :" + score;
        }
    }

    


    //플레이어캐릭터가 사망시 게임오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        // 현재 상태를 게임오버 상태로 변경
        isGameover = true;
        //게임오버 UI를 활성화
        gameoverUI.SetActive(true);
    }
}
