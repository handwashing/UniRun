using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치 처리
public class BackgroundLoop : MonoBehaviour
{
    //배경의 가로 길이(를 담아낼 변수)
    private float width;

    // Unity Event Method
    private void Awake()
    {
        //Awake()메서드는 Start()메서드 처럼 초기 1회 자동 실행되는 유니티 이벤트 메서드임.
        //하지만 Start()메서드보다 실행 시점이 한 프레임 더 빠름
        //참조하셈 : Unity Method LifeCycle

        //가로 길이를 측정
        //BoxCollider2D 컴퍼넌트의 Sizw 필드의 X값을 가로 길이로 사용
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;

    }
  

    // Update is called once per frame
    void Update()
    {
        //현재 위치가 원점에서 왼쪽으로 width 이상 이동했을 때 위치를 재배지 
        if(transform.position.x <= -width)
        {
            Reposition();
        }
    }
    void Reposition() //위치를 재배치하는 메서드
    {
        //현재 위치에서 오른쪽으로 가로 길이 *2만큼 이동
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
        //width : 20.48 * 2 = 40.~
        //-20.48 + 40.48 = 20.48;
    }
}
