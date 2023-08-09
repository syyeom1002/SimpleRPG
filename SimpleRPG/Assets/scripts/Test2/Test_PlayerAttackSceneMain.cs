using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test2
{
    public class Test_PlayerAttackSceneMain : MonoBehaviour
    {
        [SerializeField]
        private HeroController heroController;
        [SerializeField]
        private MonsterController monsterController;
        public GameObject hitFxPrefab;
        public GameObject gravePrefab;//묘지 프리팹
        // Start is called before the first frame update
        void Start()
        {
            this.monsterController.onHit = () => {
                Debug.Log("이펙트 생성");
                
                Vector3 offset = new Vector3(0, 0.5f, 0);
                Vector3 tpos = this.monsterController.transform.position + offset;
                Debug.LogFormat("생성위치{0}", tpos);
                GameObject fxGo = Instantiate(this.hitFxPrefab);
                fxGo.transform.position = tpos;

                fxGo.GetComponent<ParticleSystem>().Play();
            };
            this.monsterController.onDie = () =>
            {
                GameObject graveGo = Instantiate(this.gravePrefab);
                graveGo.transform.position = this.monsterController.transform.position;
            };
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void TestAttack()
        {
            //(버튼 누르면)사거리 체크
            //두점과의 거리, 두 오브젝트의 radius의 합
            Vector3 b = this.monsterController.gameObject.transform.position;
            Vector3 a = this.heroController.gameObject.transform.position;
            Vector3 c = b - a;
            float distance = c.magnitude;//벡터의 길이 반환

            float radius = this.heroController.Radius + this.monsterController.Radius;
            Debug.LogFormat("distance:{0},radius: {1}", distance, radius);
            Debug.LogFormat("IsWithinRange:{0}", this.isWithinRange(distance, radius));

            if (this.isWithinRange(distance, radius)){
                //heroController 에게 공격하게 시켜야한다 
                this.heroController.Attack(this.monsterController);
                
            }
        }
        //사거리 체크 
        private bool isWithinRange(float distance, float radius){
            return distance <= radius;//true
        }
    }
}
