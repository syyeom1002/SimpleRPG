using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


    public class HeroController : MonoBehaviour
    {
        private Vector3 targetPosition;
        private Coroutine moveRoutine;
        private Animator anim;
        public float radius = 1f;
        private float impactTime = 0.399f;
        public System.Action onMoveComplete;
        private MonsterController target;
        private int attackDamage = 1;
        public enum eState
        {
            Idle, Run, Attack
        }
        private eState state;
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
            this.PlayAnimation(eState.Run);
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
                this.PlayAnimation(eState.Run);
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
            this.PlayAnimation(eState.Idle);
            this.onMoveComplete();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.radius);
        }
        //--------------------공격--------------------------------------------------------------------------------
        public void Attack(MonsterController target)
        {
            this.target = target;
            this.transform.LookAt(this.target.transform);//마우스 올려보니 transform 쓰라고 뜨길래 쓰니까 됨
            this.PlayAnimation(eState.Attack);
            this.target.hp -= this.attackDamage;

        }
        private void PlayAnimation(eState state)
        {
            //this.state : prev , state : current
            //중복 막기 
            if (this.state != state)
            {
                Debug.LogFormat("{0} -> {1}", this.state, state);
                this.state = state;
                this.anim.SetInteger("State", (int)state);   //정수 -> enum 

                switch (state)
                {
                    case eState.Attack:
                        //코루틴 함수는 StartCoroutine로 실행해야 함 
                        this.StartCoroutine(this.WaitForCompleteAnimation());
                        break;
                }
            }
            else
            {
                Debug.LogFormat("{0}는 현재 상태와 동일한 상태 입니다.", state);
            }

        }
        private IEnumerator WaitForCompleteAnimation()
        {
            yield return null;  //다음의 시작 (1프레임 건너뜀, next frame)

            Debug.Log("공격 애니메이션이 끝날때까지 기다림");
            //layerIndex : 0
            AnimatorStateInfo animStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
            bool isAttackState = animStateInfo.IsName("Attack01");
            Debug.LogFormat("isAttackState: {0}", isAttackState);
            if (isAttackState)
            {
                Debug.LogFormat("animStateInfo.length: {0}", animStateInfo.length);
            }
            else
            {
                Debug.LogFormat("Attack State가 아닙니다.");
            }
            yield return new WaitForSeconds(this.impactTime);//impact
            Debug.Log("impact");
            ////대상에게 피해를 입힘
            this.target.HitDamage();
            yield return new WaitForSeconds(animStateInfo.length - this.impactTime);
            this.PlayAnimation(eState.Idle);
        }
        //------------------------------텔레포트---------------------------------------------------------------------
        private void OnTriggerEnter(Collider other)
        {
            //var item = other.gameObject.GetComponent<ItemController>();

            //if (item != null)
            //{
            //    Debug.LogFormat("<color=red>{0}아이템 지나침</color>", item.ItemType);
            //    other.gameObject.SetActive(false);
            //}
            if (other.gameObject.tag == "Portal")
            {

                SceneManager.LoadScene("Test_BossScene");

            }
        }
        

    }

