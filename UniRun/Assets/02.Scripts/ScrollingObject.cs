using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 오브젝트를 지속적으로 왼쪽으로 움직이는 스크립트
public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f; //이동 속도
  
    void Update()
    {
        if(!GameManager.instance.isGameover)
        {
            //초당 speed의 속도로 왼쪽으로 평행이동 구현
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            //Translate -> 평행이동 메서드
            // transform.Translate(new Vector2(10,0)); z값이 0인 상태로 넘어가서 
            // -> transform.Translate(10,0,0);
        }



    }
}
