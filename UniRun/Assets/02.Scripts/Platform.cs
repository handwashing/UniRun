using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//발판으롬서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour
{
    //장애물 오브젝트들을 담는 배열
    public GameObject[] obstacles;
    //플레이어 캐릭터가 밟았는지
    private bool stepped = false;

    //새로운 유니티 이벤트 메서드를 확인
    private void OnEnable()
    {
        //Awake()나 Start() 같은 유니티 이벤트 메서드임 /
        //Start() 메서드처럼 컴퍼넌트가 활성화 될 때 자동으로 한 번 실행되는 메서드임...
        //그런데 처음 한 번만 실행되는 Start() 메서드와 달리 OnEnable() 메서드는
        //컴퍼넌트가 활성화될 때마다 매번 다시 실행되는 메서드라서, 컴퍼넌트를 끄고
        //다시 켜는 방식으로 재실행할 수 있는 메서드임.

        //발판을 리셋하는 처리

        //밟힘 상태를 리셋
        stepped = false;

        //장애물의 수만큼 루프
        for(int i = 0; i<obstacles.Length; i++)
        {
            //현재 순번의 장애물을 1/3의 확률로 활성화
            if (Random.Range(0,3) == 0) //0부터 3개의 숫자를 추출/ 이때 그 숫자가0과 같다면..
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
            //obstacles[i].SetActive(Random.Range(0,3)==0?true:false) 이게 더 빠르긴 함
        }
    }

    // 플레이어 캐릭터가 자신을 밟았을 때 점수를 추가하는 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌한 상대방의 태그가 Player이고 이전에 플레이어 캐릭터가 밟지 않았다면
        if(collision.collider.tag=="Player"&&!stepped)
        {
            //점수를 추가하고 밟힘 상태를 참으로 변경
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }

}