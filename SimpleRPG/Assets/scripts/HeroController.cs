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
        public void Move(MonsterController target) //Ÿ�� (����?)
        {
            this.target = target;
            this.targetPosition = this.target.gameObject.transform.position;
            this.PlayAnimation(eState.Run);
            if (this.moveRoutine != null)
            {
                //�̹� �ڷ�ƾ�� �������̴� --> �������� ����
                this.StopCoroutine(this.moveRoutine);
            }
            this.moveRoutine = StartCoroutine(this.CoMove());
        }

        public void Move(Vector3 targetPosition)//��ġ(����X?)
        {
            //Ÿ���� ����
            this.target = null;

            this.targetPosition = targetPosition;
            
            if (this.moveRoutine != null)
            {
                //�̹� �ڷ�ƾ�� �������̴� --> �������� ����
                this.StopCoroutine(this.moveRoutine);
            }
            this.moveRoutine= StartCoroutine(this.CoMove());
        }

        private IEnumerator CoMove()
        {
            while (true)
            {
                
                this.transform.LookAt(this.targetPosition);
                //�̵�
                this.transform.Translate(Vector3.forward * 1f * Time.deltaTime);
                this.PlayAnimation(eState.Run);
                float distance = Vector3.Distance(this.transform.position, this.targetPosition);//�Ÿ�����
                Debug.Log(distance);
                //Ÿ���� �������
                if (this.target != null)
                {
                    if (distance <= 1f + 1f)
                    {

                       break;//�����ϸ� ����
                    }
                }
                else// Ÿ���� ���� ���
                {
                    if (distance <= 0.1f)
                    {
                        break;
                    }
                }
                
                yield return null;//���� ������ ���� 
            }
            Debug.Log("<color=yellow>����</color>");
            this.PlayAnimation(eState.Idle);
            this.onMoveComplete();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.radius);
        }
        //--------------------����--------------------------------------------------------------------------------
        public void Attack(MonsterController target)
        {
            this.target = target;
            this.transform.LookAt(this.target.transform);//���콺 �÷����� transform ����� �߱淡 ���ϱ� ��
            this.PlayAnimation(eState.Attack);
            this.target.hp -= this.attackDamage;

        }
        private void PlayAnimation(eState state)
        {
            //this.state : prev , state : current
            //�ߺ� ���� 
            if (this.state != state)
            {
                Debug.LogFormat("{0} -> {1}", this.state, state);
                this.state = state;
                this.anim.SetInteger("State", (int)state);   //���� -> enum 

                switch (state)
                {
                    case eState.Attack:
                        //�ڷ�ƾ �Լ��� StartCoroutine�� �����ؾ� �� 
                        this.StartCoroutine(this.WaitForCompleteAnimation());
                        break;
                }
            }
            else
            {
                Debug.LogFormat("{0}�� ���� ���¿� ������ ���� �Դϴ�.", state);
            }

        }
        private IEnumerator WaitForCompleteAnimation()
        {
            yield return null;  //������ ���� (1������ �ǳʶ�, next frame)

            Debug.Log("���� �ִϸ��̼��� ���������� ��ٸ�");
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
                Debug.LogFormat("Attack State�� �ƴմϴ�.");
            }
            yield return new WaitForSeconds(this.impactTime);//impact
            Debug.Log("impact");
            ////��󿡰� ���ظ� ����
            this.target.HitDamage();
            yield return new WaitForSeconds(animStateInfo.length - this.impactTime);
            this.PlayAnimation(eState.Idle);
        }
        //------------------------------�ڷ���Ʈ---------------------------------------------------------------------
        private void OnTriggerEnter(Collider other)
        {
            //var item = other.gameObject.GetComponent<ItemController>();

            //if (item != null)
            //{
            //    Debug.LogFormat("<color=red>{0}������ ����ħ</color>", item.ItemType);
            //    other.gameObject.SetActive(false);
            //}
            if (other.gameObject.tag == "Portal")
            {

                SceneManager.LoadScene("Test_BossScene");

            }
        }
        

    }

