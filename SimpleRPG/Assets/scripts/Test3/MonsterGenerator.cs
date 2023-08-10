using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test3
{
    public class MonsterGenerator : MonoBehaviour
    {
        //[SerializeField] private GameObject turtlePrefab;
        //[SerializeField] private GameObject slimePrefab;

        [SerializeField]
        private List<GameObject> prefabList;//동적 배열(컬렉션은 사용전 반드시 인스턴스화)
        // Start is called before the first frame update
        void Start()
        {
            foreach(GameObject prefab in this.prefabList)
            {
                Debug.LogFormat("prefabs{0}", prefab);
            }
        }

        // Update is called once per frame
       
        /// <summary>
        /// 몬스터 생성
        /// </summary>
        /// <param name="monsterType">생성하려고 하는 몬스터의 타입</param>
        /// <param name="initPosition">생성된 몬스터의 월드 좌표</param>
        public MonsterController Generate(GameEnums.eMonsterType monsterType,Vector3 initPosition)
        {
            Debug.LogFormat("monsterType{0}", monsterType);
            //몬스터 타입에 따라 어떤 프리팹으로 프리팹 복사본(인스턴스)를 생성할ㅈ ㅣ결정 해야함
            int index = (int)monsterType;
            Debug.LogFormat("index{0}", index);

            GameObject prefab = this.prefabList[index];
            Debug.LogFormat("prefab{0}", prefab);
            GameObject go = Instantiate(prefab);
            
            go.transform.position = initPosition;

            return go.GetComponent<MonsterController>();
        }
    }
}
