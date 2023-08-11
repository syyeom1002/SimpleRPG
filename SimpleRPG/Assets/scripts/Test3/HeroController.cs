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
        public void Move(Vector3 targetPosition)//위치(몬스터X?)
        {
            this.targetPosition = targetPosition;
            Debug.Log("움직여라");
            this.transform.LookAt(this.targetPosition);
            //이동
            
            this.moveRoutine = StartCoroutine(this.CoMove());


        }

        private IEnumerator CoMove()
        {
            while (true)
            {

                
                //이동
                this.transform.Translate(Vector3.forward * 1.5f * Time.deltaTime);
                anim.SetInteger("State", 1);
                float distance = Vector3.Distance(this.transform.position, this.targetPosition);//거리측정
                Debug.Log(distance);
                
                    if (distance <= 0.1f)
                    {
                        break;
                    }
                

                yield return null;//다음 프레임 시작 
            }
            Debug.Log("<color=yellow>도착</color>");
            anim.SetInteger("State", 0);
            
        }
        private void OnTriggerEnter(Collider other)
        {
            
                Debug.Log("아이템 지나침");
            
        }
    }
}
