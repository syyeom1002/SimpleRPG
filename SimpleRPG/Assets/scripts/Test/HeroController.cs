using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class HeroController : MonoBehaviour
    {
        private Vector3 targetPosition;
        private Coroutine moveRoutine;
        private Animator anim;
        public float radius = 1f;

        public System.Action onMoveComplete;
        private MonsterController target;
        // Start is called before the first frame update
        void Start()
        {
            this.anim = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        public void Move(MonsterController target) //타겟 (몬스터?)
        {
            this.target = target;
            this.targetPosition = this.target.gameObject.transform.position;
            this.anim.SetInteger("Run", 1);
            if (this.moveRoutine != null)
            {
                //이미 코루틴이 실행중이다 --> 기존꺼를 중지
                this.StopCoroutine(this.moveRoutine);
            }
            this.moveRoutine = StartCoroutine(this.CoMove());
        }

        public void Move(Vector3 targetPosition)//위치(몬스터X?)
        {
            //타겟을 지움
            this.target = null;

            this.targetPosition = targetPosition;
            
            if (this.moveRoutine != null)
            {
                //이미 코루틴이 실행중이다 --> 기존꺼를 중지
                this.StopCoroutine(this.moveRoutine);
            }
            this.moveRoutine= StartCoroutine(this.CoMove());
        }

        private IEnumerator CoMove()
        {
            while (true)
            {
                
                this.transform.LookAt(this.targetPosition);
                //이동
                this.transform.Translate(Vector3.forward * 1f * Time.deltaTime);
                anim.SetInteger("Run", 1);
                float distance = Vector3.Distance(this.transform.position, this.targetPosition);//거리측정
                Debug.Log(distance);
                //타겟이 있을경우
                if (this.target != null)
                {
                    if (distance <= 1f + 1f)
                    {
                        break;//도착하면 멈춤
                    }
                }
                else// 타겟이 없을 경우
                {
                    if (distance <= 0.1f)
                    {
                        break;
                    }
                }
                
                
                
                yield return null;//다음 프레임 시작 
            }
            Debug.Log("<color=yellow>도착</color>");
            anim.SetInteger("Run", 0);
            this.onMoveComplete();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, this.radius);
        }

    }
}
