using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Test_Boss
{
    
    public class HeroController : MonoBehaviour
    {
        private Vector3 targetPosition;
        private Coroutine moveRoutine;
        private float radius = 1f;
        private Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            this.anim = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Move(Vector3 hitPosition)
        {
            this.targetPosition = hitPosition;
            if (this.moveRoutine != null)
            {
                this.StopCoroutine(this.moveRoutine);
            }
            this.moveRoutine = StartCoroutine(CoMove());
        }
        private IEnumerator CoMove()
        {
            while (true)
            {
                this.transform.LookAt(this.targetPosition);
                this.anim.SetInteger("State", 1);
                this.transform.Translate(Vector3.forward * 2f * Time.deltaTime);
                float dis = Vector3.Distance(this.transform.position, this.targetPosition);
                if (dis <= 0.1f)
                {
                    this.anim.SetInteger("State", 0);
                    break;
                }
                yield return null;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.radius, 20);
            
        }
    }
}
