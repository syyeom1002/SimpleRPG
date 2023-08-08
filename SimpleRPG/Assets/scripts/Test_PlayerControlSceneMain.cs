using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test;
using UnityEngine.UI;
public class Test_PlayerControlSceneMain : MonoBehaviour
{
    [SerializeField]
    private HeroController heroController;
    [SerializeField]
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        //this.Fade();
        // this.StartCoroutine(this.CoFade());
        this.heroController.onMoveComplete = () =>
        {
            Debug.Log("이동을 완료했습니다.");
        };
    }

    // Update is called once per frame
    void Update()
    {
        //화면을 클릭하면 클릭한 위치로 Hero가 이동 
        if (Input.GetMouseButtonDown(0))
        {
            float maxDistance = 100f;
            //ray를 만든다
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 3f);
            //충돌검사
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, maxDistance)){
                //Debug.Log(hit.point);//null이 나올 수 없음 -> struct-> 값형식 -> 스택에 값 저장-> 힙 안씀 (참조 안함) null레퍼런스 나올수가 없음//월드 상의 충돌 지점 위치 
                //hero 게임 오브젝트를 이동해라 라고 컨트롤러에게 명령
                
                //Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Monster")
                {
                    //몬스터랑 히어로의 거리
                    float distance = Vector3.Distance(this.heroController.transform.position, hit.collider.gameObject.transform.position);

                    Debug.Log(distance);

                    MonsterController monsterController=hit.collider.gameObject.GetComponent<MonsterController>();
                    //몬스터랑 히어로 사정거리 반지름 더한 값
                    float sumRadius = this.heroController.radius + monsterController.radius;

                    Debug.Log(sumRadius);

                    //사거리 안에 들어옴
                    if (distance <= sumRadius)
                    {
                        //공격
                        //이동하는거에서 벗어나야함

                    }
                    else
                    {
                        this.heroController.Move(monsterController);
                    }
                }
                else if (hit.collider.tag == "Ground")
                {
                    this.heroController.Move(hit.point);
                }
            }
        }
    }
    void Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = this.image.color;
            Debug.Log(f);
            c.a = f;
            this.image.color = c;
        }
    }
    IEnumerator CoFade()
    {
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            Color c = this.image.color;
            Debug.Log(f);
            c.a = f;
            this.image.color = c;
            yield return null;
        }
    }
}
