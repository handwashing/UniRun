using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlayerController는 플레이어 캐릭터로
//Player 게임 오브젝트 제어함
public class PlayerController : MonoBehaviour
{
    // 플레이어가 사망시 재생할 오디오 클립
    public AudioClip deathClip;
    // 점프 힘
    public float jumpForce = 700f;

    //누적 점프 횟수
    private int jumpCount = 0;
    //플레이어가 바닥에 닿았는지 확인
    private bool isGrounded = false;
    //플레이어가 죽었냐 살았냐 = 사망 상태
    private bool isDead = false;
    //사용할 리지드바디 컴퍼넌트
    private Rigidbody2D playerRigidbody;
    //사용할 오디오 소스 컴퍼넌트
    private AudioSource playerAudio;
    //사용할 애니메이터 컴퍼넌트
    private Animator animator;
    void Start()
    {
        // 전역변수의 초기화 진행
        // 게임 오브젝트로부터 사용 할 컴퍼넌트를 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 사용자 입력을 감지하고 점프하는 처리
        // 1. 현재 상황에 알맞은 애니메이션을 재생
        // 2. 마우스 왼쪽 클릭을 감지하고 점프
        // 3. 마우스 왼쪽 버튼을 오래 누르면 높이 점프
        // 4. 최대 점프 횟수에 도달하면 점프를 못하게 막기

        // 사망 시 더이상 처리를 진행하지 않고 종료
        if (isDead) return;

        //마우스 왼쪽 버튼을 눌렀으면 & 최대 점프 횟수(2)에 도달하지 않았다면,
        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            //jump 횟수 증가
            jumpCount++;
            //jump 직전에 속도를 순간적으로 제로(0,0)로 변경
            //=jump직전까지의 힘 또는 속도가 상쇄되거나 합쳐져서
            //점프 높이가 비일관적으로 되는 현상을 막으려고
            playerRigidbody.velocity = Vector2.zero; // <-함축표현법(0,0) 

            //리지드바디에 위쪽으로 힘주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            //오디오 소스 재생
            playerAudio.Play();
            
        }
        else if(Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            //마우스 왼쪽 버튼에서 손을 떼는 순간과 속도의 y값이
            //양수라면(위로 상승 중) 현재 속도를 절반으로 변경
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
            
        }
        //애니메이터 Grounded 파라미터를 isGrounded 값으로 갱신
        animator.SetBool("Grounded", isGrounded);

    }

    void Die()
    {
        //사망 처리
        //애니메이터의 Die 트리거 파라미터를 셋
        animator.SetTrigger("Die");

        //오디오 소스에 할당된 오디오 클립을 deathClip으로 변경
        playerAudio.clip = deathClip;
        //사망 효과음 재생
        playerAudio.Play();

        // 속도를 제로(0,0)로 변경
        playerRigidbody.velocity = Vector2.zero; //=new Vector2(0,0)
        //나 사망했어~ 사망 상태를 true로 변경
        isDead = true;
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥에 닿자 마자 감지하는 처리 - 구현
        //어떤 콜라이더와 닿았으며, 충돌 표면이 위쪽을 보고 있는지
        if(collision.contacts[0].normal.y > 0.7f)
        {
            // contacts : 충돌 지점들의 정보를 담는 ContactPoint 타입의
            // 데이터를 contact라는 배열 변수로 제공 받는다.
            // normal : 충돌 지점에서 충돌 표면의 방향(노말벡터)를 알려주는 변수임

            //isGrounded를 true로 변경하고, 누적 점프 횟수를 0으로 리셋
            isGrounded = true;
            jumpCount = 0;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어나자마자 처리
        // 어떤 콜라이더에서 떼어진 경우 isGrounded를 false로 변경
        isGrounded = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //트리거 콜라이더를 가진 장애물과의 충돌 감지

        //충돌한 상대방의 태그가 Dead면서 아직 사망하지 않았다면
        if(collision.tag == "Dead" && !isDead)
        {
            Die();
        }
    }
}
