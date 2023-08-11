using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test3
{
    public class HeroController : MonoBehaviour
    {
        
        private Vector3 targetPosition;
        private Coroutine moveRoutine;
        private Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            this.anim = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        public void Move(Vector3 targetPosition)//��ġ(����X?)
        {
            this.targetPosition = targetPosition;
            Debug.Log("��������");
            this.transform.LookAt(this.targetPosition);
            //�̵�
            
            this.moveRoutine = StartCoroutine(this.CoMove());


        }

        private IEnumerator CoMove()
        {
            while (true)
            {

                
                //�̵�
                this.transform.Translate(Vector3.forward * 1.5f * Time.deltaTime);
                anim.SetInteger("State", 1);
                float distance = Vector3.Distance(this.transform.position, this.targetPosition);//�Ÿ�����
                Debug.Log(distance);
                
                    if (distance <= 0.1f)
                    {
                        break;
                    }
                

                yield return null;//���� ������ ���� 
            }
            Debug.Log("<color=yellow>����</color>");
            anim.SetInteger("State", 0);
            
        }
        private void OnTriggerEnter(Collider other)
        {
            
                Debug.Log("������ ����ħ");
            
        }
    }
}
