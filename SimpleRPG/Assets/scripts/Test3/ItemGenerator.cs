using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test3
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> itemList;
        private Vector3 monsterPosition;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public ItemController Generate(GameEnums.eItemType itemType, Vector3 monsterposition)
        {
            int index = (int)itemType;

            GameObject prefab = this.itemList[index];
            GameObject itemGo = Instantiate(prefab);


            itemGo.transform.position = monsterposition;

            return itemGo.GetComponent<ItemController>();
        }
    }
}
