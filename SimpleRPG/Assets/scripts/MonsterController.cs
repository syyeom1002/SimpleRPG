using System.Collections;
using System.Collections.Generic;

using UnityEngine;


    public class MonsterController : MonoBehaviour
    {
        public float radius = 1f;
        private Animator anim;
        public int hp = 5;
        public enum eState
        {
            Idle, GetHit
        }
        public System.Action onHit;
        public System.Action onDie;
        // Start is called before the first frame update
        void Start()
        {
            this.anim = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        //사정거리
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.radius);
        }
        public void HitDamage()
        {
            Debug.Log("피해애니메이션을 실행합니다.");
            //대리자 호출
            this.onHit();
            this.anim.SetInteger("State", 2);
            this.StartCoroutine(this.CompleteGetHitAnimation());

            if (this.hp <= 0)
            {
                this.anim.SetInteger("State", 3);
                Destroy(this.gameObject);
                
            }
        }

        private IEnumerator CompleteGetHitAnimation()
        {
            yield return null;
            AnimatorStateInfo animStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
            bool isGetHit = animStateInfo.IsName("GetHit");
            Debug.LogFormat("<color=lime>isGetHit:{0}</color>", isGetHit);
            yield return new WaitForSeconds(animStateInfo.length);
            this.anim.SetInteger("State", 0);
        }
    }

