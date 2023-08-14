using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_Boss
{
    public class Bull : MonoBehaviour
    {
        public System.Action onDetectTarget;
        public System.Action onLoseTarget;
        public System.Action onMoveComplete;
        public System.Action onAttackCancel;
        public System.Action onAttack;
        private Transform target;
        [SerializeField]
        private float sight = 7f;
        [SerializeField]
        private float range = 3f;
        public float Range => this.range;
        private Animator anim;
        private bool isWithinSight;

        private Coroutine stateRoutine;
        
        void Start()
        {
            this.anim = this.GetComponent<Animator>();
        }

        //초기화
        public void Init()
        {
            this.FindTarget();//타겟을 찾는다
            this.stateRoutine = this.StartCoroutine(this.CoCheckWithinHeroTheRange());  
        }

        //타겟찾기
        private void FindTarget()
        {
            var go = GameObject.Find("HeroPrefab");
            if (go != null)
                this.target = go.transform;
        }

        private IEnumerator CoCheckWithinHeroTheRange()//범위내에 히어로 있는지 체크(움직이지 X)
        {
            while (true) {
                var dis = Vector3.Distance(this.transform.position, this.target.transform.position);
                //Debug.Log(dis);
                this.isWithinSight = dis <= this.sight;//시야내에 들어오면 true 밖이면 false
                
                if (this.isWithinSight) {
                    //if (dis <= 1f)
                    //{
                    //    this.Attack();
                    //}
                    this.onDetectTarget();
                    break;
                }

                yield return null;
            }
        }
        //탐지하고 움직이기
        public void DetectAndMove()
        {
            if (this.stateRoutine != null) {//범위내에 히어로가 있으면 
                this.StopCoroutine(this.stateRoutine);//찾는걸 멈춘다.
            }

            this.stateRoutine = this.StartCoroutine(this.CoDetectAndMove());//범위내에 히어로 없으면 계속 탐지?
        }

        private IEnumerator CoDetectAndMove()
        {
            this.transform.LookAt(this.target);
            this.anim.SetInteger("State", 1);
            //이동 애니메이션 실행 

            while (true)
            {
                var dis = Vector3.Distance(this.transform.position, this.target.transform.position);
                //Debug.Log(dis);
                this.isWithinSight = dis <= this.sight;

                if (this.isWithinSight == false)//시야 내에 없으면 멈춘다
                {
                    this.onLoseTarget();//타켓이 시야에서 나가면 idle()호풀, idle 출력
                    break;
                }
                
                this.transform.Translate(Vector3.forward * 1f * Time.deltaTime);//있으면 움직인다.
                yield return null;
            }
        }
        //idle 상태일때 
        public void Idle()
        {
            Debug.Log("Idle");
            this.anim.SetInteger("State", 0);
            this.StartCoroutine(this.CoCheckWithinHeroTheRange());//범위에 있는지 체크한다(idle상태일때)
        }

        public void Attack()
        {
            //var dis = Vector3.Distance(this.transform.position, this.target.transform.position);
            //if (dis <= 1f)
            //{
                this.anim.SetInteger("State", 3);
            //}
        }
        //public void MoveForward(Transform targetTrans)
        //{
        //    this.targetTrans = targetTrans;
        //    //this.StartCoroutine(coMoveForward());
        //    this.StartCoroutine(this.CoMoveForward());
        //}

        //private IEnumerator CoMoveForward()
        //{
        //    while (true){

        //        if (targetTrans == null)
        //            break;

        //        var dis = Vector3.Distance(this.transform.position, this.targetTrans.position);
        //        if (dis > 7)//시야 안에 들어오면
        //        {
        //            yield return null;
        //            continue;
        //        }
        //        this.transform.LookAt(this.targetTrans);
        //        this.transform.Translate(Vector3.forward * 1f * Time.deltaTime);

        //        if (dis <= this.range)//공격사거리안에 들어오면
        //        {
        //            Debug.Log("<color=red>공격사거리 안에 들어왔습니다.</color>");
        //            break;
        //        }
        //        yield return null;
        //    }
        //    if (this.targetTrans == null)
        //    {
        //        Debug.Log("타겟을 잃었습니다.");
        //        this.anim.SetInteger("State", 0);
        //    }
        //    else
        //    {
        //        Debug.Log("이동을 완료함");
        //        this.onMoveComplete();
        //    }

        //}
        //public void Attack(Transform targetTrans)
        //{
        //    this.targetTrans = targetTrans;
        //    this.StartCoroutine(this.CoAttack());
        //}
        //private IEnumerator CoAttack()
        //{
        //    yield return null;
        //    var dis = Vector3.Distance(this.transform.position, this.targetTrans.position);
        //    if (dis < this.range)
        //    {
        //        anim.SetInteger("State", 3);

        //    }
        //    else
        //    {
        //        this.onAttackCancel();
        //        anim.SetInteger("State", 1);
        //    }
        //}
        private void OnDrawGizmos()
        {
            //시야 
            if (isWithinSight)//범위 안에 있으면 빨간색
            {
                Gizmos.color = Color.red;
            }
            else 
            {
                Gizmos.color = Color.white;
            }
            
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.sight, 20);
            //공격사거리
            //Gizmos.color = Color.red;
            //GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.range, 20);
        }
    }
}
