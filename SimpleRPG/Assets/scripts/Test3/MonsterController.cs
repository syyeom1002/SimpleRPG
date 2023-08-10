using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test3
{
    public class MonsterController : MonoBehaviour
    {
        private Animator anim;
        public GameEnums.eItemType rewardItemType;

        public System.Action<GameEnums.eItemType> onDie;
        // Start is called before the first frame update
        void Start()
        {
            this.anim = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Die()
        {
            StartCoroutine(this.CoDie());
        }
        private IEnumerator CoDie()
        {
            this.anim.SetInteger("State", 3);
            yield return new WaitForSeconds(2.0f);
            this.onDie(this.rewardItemType);
        }
    }
}
