using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Test2.HeroController;

namespace Test2
{
    public class MonsterController : MonoBehaviour
    {
        private float radius = 1f;
        private Animator anim;
        public int hp=5;
        public enum eState
        {
            Idle,GetHit
        }
        public System.Action onHit;
        public System.Action onDie;
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
        public void HitDamage()
        {
            Debug.Log("피해애니메이션을 실행합니다.");
            //대리자 호출
            this.onHit();
            this.anim.SetInteger("State", 2);
            this.StartCoroutine(this.CompleteGetHitAnimation());
            
            if (this.hp <= 0)
            {
                Destroy(this.gameObject);
                this.onDie();
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
}

