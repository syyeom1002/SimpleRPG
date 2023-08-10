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
        private List<GameObject> prefabList;//���� �迭(�÷����� ����� �ݵ�� �ν��Ͻ�ȭ)
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
        /// ���� ����
        /// </summary>
        /// <param name="monsterType">�����Ϸ��� �ϴ� ������ Ÿ��</param>
        /// <param name="initPosition">������ ������ ���� ��ǥ</param>
        public MonsterController Generate(GameEnums.eMonsterType monsterType,Vector3 initPosition)
        {
            Debug.LogFormat("monsterType{0}", monsterType);
            //���� Ÿ�Կ� ���� � ���������� ������ ���纻(�ν��Ͻ�)�� �����Ҥ� �Ӱ��� �ؾ���
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
