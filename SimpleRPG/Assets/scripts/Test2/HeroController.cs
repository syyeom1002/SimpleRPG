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
        private eState state;//�ʱ�ȭ 0
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
            this.transform.LookAt(this.target.transform);//���콺 �÷����� transform ����� �߱淡 ���ϱ� ��
            this.PlayAnimation(eState.Attack);
            this.target.hp -= this.attackDamage;

        }
        private void PlayAnimation(eState state)
        {
            
            //�ߺ�����
            if (this.state != state)
            {
                Debug.LogFormat("{0}->{1}", this.state, state);
                this.state = state;
                this.anim.SetInteger("State", (int)state);//�ڵ����� �ߺ� �����༭ if else�� �Ƚᵵ��
                switch (state)
                {
                    case eState.Attack:
                        Debug.Log("���� �ִϸ��̼��� ���������� ��ٸ�");
                       
                        this.StartCoroutine(this.WaitForCompleteAttackAnimation());
                        break;
                }

            }
            else
            {
                Debug.Log("���� state�Դϴ�.");
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
                Debug.LogFormat("Attack state�� �ƴմϴ�");
            }
            //0.833�ʸ� ���
            yield return new WaitForSeconds(this.impactTime);//impact
            Debug.Log("impact");
            ////��󿡰� ���ظ� ����
            this.target.HitDamage();

            yield return new WaitForSeconds(animStateInfo.length - this.impactTime);
            this.PlayAnimation(eState.Idle);
        }
    }
}
