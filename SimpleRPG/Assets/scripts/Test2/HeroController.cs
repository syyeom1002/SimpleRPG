using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Test2
{
    public class HeroController : MonoBehaviour
    {
        public enum eState
        {
            Idle,Attack
        }
        private float radius = 1f;
        private Animator anim;
        private eState state;//초기화 0
        private float impactTime = 0.399f;
        private MonsterController target;
        private int attackDamage = 1;

        public float Radius
        {
            get
            {
                return this.radius;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            this.anim = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, radius, 40);
        }
        public void Attack(MonsterController target)
        {
            this.target = target;
            this.transform.LookAt(this.target.transform);//마우스 올려보니 transform 쓰라고 뜨길래 쓰니까 됨
            this.PlayAnimation(eState.Attack);
            this.target.hp -= this.attackDamage;

        }
        private void PlayAnimation(eState state)
        {
            
            //중복막기
            if (this.state != state)
            {
                Debug.LogFormat("{0}->{1}", this.state, state);
                this.state = state;
                this.anim.SetInteger("State", (int)state);//자동으로 중복 막아줘서 if else문 안써도됨
                switch (state)
                {
                    case eState.Attack:
                        Debug.Log("공격 애니메이션이 끝날때까지 기다림");
                       
                        this.StartCoroutine(this.WaitForCompleteAttackAnimation());
                        break;
                }

            }
            else
            {
                Debug.Log("같은 state입니다.");
            }
            
        }
        private IEnumerator WaitForCompleteAttackAnimation()
        {
            yield return null;
            AnimatorStateInfo animStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
            bool isAttackState = animStateInfo.IsName("Attack01");
            Debug.LogFormat("isAttackState{0}", isAttackState);
            if (isAttackState)
            {
                Debug.LogFormat("animStateInfo.length:{0}", animStateInfo.length);
            }
            else
            {
                Debug.LogFormat("Attack state가 아닙니다");
            }
            //0.833초를 대기
            yield return new WaitForSeconds(this.impactTime);//impact
            Debug.Log("impact");
            ////대상에게 피해를 입힘
            this.target.HitDamage();

            yield return new WaitForSeconds(animStateInfo.length - this.impactTime);
            this.PlayAnimation(eState.Idle);
        }
    }
}
