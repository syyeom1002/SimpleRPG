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
        public void Move(MonsterController target) //Ÿ�� (����?)
        {
            this.target = target;
            this.targetPosition = this.target.gameObject.transform.position;
            this.anim.SetInteger("Run", 1);
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
                anim.SetInteger("Run", 1);
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
