using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test3;

namespace Test3
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField]
        private GameEnums.eItemType itemType;

        public GameEnums.eItemType ItemType
        {
            get
            {
                return this.itemType;
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
