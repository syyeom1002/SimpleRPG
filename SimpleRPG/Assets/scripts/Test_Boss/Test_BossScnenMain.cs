using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Test_Boss
{
    public class Test_BossSceneMain : MonoBehaviour
    {
        //[SerializeField]
        //private GameObject bossPrefab;
        //[SerializeField]
        //private GameObject heroPrefab;
        [SerializeField]
        private Button btnMove;
        [SerializeField]
        private Button btnRemove;
        [SerializeField]
        private Bull bull;
        [SerializeField]
        private Transform targetTrans;
        [SerializeField]
        private HeroController heroController;
        // Start is called before the first frame update
        void Start()
        {
            this.bull.onDetectTarget = () => {
                this.bull.DetectAndMove();
            };

            this.bull.onLoseTarget = () => {
                this.bull.Idle();
            };
            this.bull.onAttack = () =>
            {
                this.bull.Attack();
            };
            //this.bull.onAttackCancel = () =>
            //{
            //    this.BullMoveAndAttack();//거리계산
            //};
            //this.bull.onMoveComplete = () =>
            //{
            //    //Debug.LogError("!");
            //    this.BullMoveAndAttack();//거리계산
            //};
            ////this.CreateBoss();
            ////this.CreateHero();
            //this.btnMove.onClick.AddListener(() =>
            //{
            //    Debug.Log("move");
            //    this.bull.MoveForward(targetTrans);
            //});

            //this.btnRemove.onClick.AddListener(() =>
            //{
            //    Debug.Log("remove");
            //    //Destroy(this.bull.gameObject);
            //    //Destroy(this.gameObject);
            //    Destroy(this.targetTrans.gameObject);

            //});

            this.bull.Init();

        }
        //private void BullMoveAndAttack()
        //{
        //    Debug.Log("사거리 계산");
        //    var dis = Vector3.Distance(this.transform.position, this.targetTrans.position);
        //    if (dis <= this.bull.Range)
        //    {
        //        Debug.Log("공격");
        //        this.bull.Attack(targetTrans);
        //    }
        //    else
        //    {
        //        Debug.Log("이동");
        //        this.bull.MoveForward(targetTrans);
        //    }
        //}
        //private void CreateBoss()
        //{
        //    GameObject bossGo = Instantiate(this.bossPrefab);
        //    bossGo.transform.position = new Vector3(6, 0, -9);
        //}
        //private void CreateHero()
        //{
        //    GameObject heroGo = Instantiate(this.heroPrefab);
        //}
        //// Update is called once per frame

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction*100f, Color.red, 2f);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit, 100f)){
                    this.heroController.Move(hit.point);
                }
            }
        }
    }
}
