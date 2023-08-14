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

        //�ʱ�ȭ
        public void Init()
        {
            this.FindTarget();//Ÿ���� ã�´�
            this.stateRoutine = this.StartCoroutine(this.CoCheckWithinHeroTheRange());  
        }

        //Ÿ��ã��
        private void FindTarget()
        {
            var go = GameObject.Find("HeroPrefab");
            if (go != null)
                this.target = go.transform;
        }

        private IEnumerator CoCheckWithinHeroTheRange()//�������� ����� �ִ��� üũ(�������� X)
        {
            while (true) {
                var dis = Vector3.Distance(this.transform.position, this.target.transform.position);
                //Debug.Log(dis);
                this.isWithinSight = dis <= this.sight;//�þ߳��� ������ true ���̸� false
                
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
        //Ž���ϰ� �����̱�
        public void DetectAndMove()
        {
            if (this.stateRoutine != null) {//�������� ����ΰ� ������ 
                this.StopCoroutine(this.stateRoutine);//ã�°� �����.
            }

            this.stateRoutine = this.StartCoroutine(this.CoDetectAndMove());//�������� ����� ������ ��� Ž��?
        }

        private IEnumerator CoDetectAndMove()
        {
            this.transform.LookAt(this.target);
            this.anim.SetInteger("State", 1);
            //�̵� �ִϸ��̼� ���� 

            while (true)
            {
                var dis = Vector3.Distance(this.transform.position, this.target.transform.position);
                //Debug.Log(dis);
                this.isWithinSight = dis <= this.sight;

                if (this.isWithinSight == false)//�þ� ���� ������ �����
                {
                    this.onLoseTarget();//Ÿ���� �þ߿��� ������ idle()ȣǮ, idle ���
                    break;
                }
                
                this.transform.Translate(Vector3.forward * 1f * Time.deltaTime);//������ �����δ�.
                yield return null;
            }
        }
        //idle �����϶� 
        public void Idle()
        {
            Debug.Log("Idle");
            this.anim.SetInteger("State", 0);
            this.StartCoroutine(this.CoCheckWithinHeroTheRange());//������ �ִ��� üũ�Ѵ�(idle�����϶�)
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
        //        if (dis > 7)//�þ� �ȿ� ������
        //        {
        //            yield return null;
        //            continue;
        //        }
        //        this.transform.LookAt(this.targetTrans);
        //        this.transform.Translate(Vector3.forward * 1f * Time.deltaTime);

        //        if (dis <= this.range)//���ݻ�Ÿ��ȿ� ������
        //        {
        //            Debug.Log("<color=red>���ݻ�Ÿ� �ȿ� ���Խ��ϴ�.</color>");
        //            break;
        //        }
        //        yield return null;
        //    }
        //    if (this.targetTrans == null)
        //    {
        //        Debug.Log("Ÿ���� �Ҿ����ϴ�.");
        //        this.anim.SetInteger("State", 0);
        //    }
        //    else
        //    {
        //        Debug.Log("�̵��� �Ϸ���");
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
            //�þ� 
            if (isWithinSight)//���� �ȿ� ������ ������
            {
                Gizmos.color = Color.red;
            }
            else 
            {
                Gizmos.color = Color.white;
            }
            
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.sight, 20);
            //���ݻ�Ÿ�
            //Gizmos.color = Color.red;
            //GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.range, 20);
        }
    }
}
