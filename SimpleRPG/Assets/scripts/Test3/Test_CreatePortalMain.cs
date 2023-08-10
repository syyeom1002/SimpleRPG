using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Test3
{
    public class Test_CreatePortalMain : MonoBehaviour
    {
        [SerializeField]
        private GameObject heroPrefab;
        private HeroController heroController;
        [SerializeField]
        private MonsterGenerator monsterGenerator;
        private List<MonsterController> monsterList;
        [SerializeField]
        private ItemGenerator itemGenerator;
        private List<ItemController> itemList;
        [SerializeField]
        private GameObject portalPrefab;


        // Start is called before the first frame update
        void Start()
        {
            this.CreateHero();
            this.monsterList = new List<MonsterController>();
            MonsterController turtle = this.monsterGenerator.Generate(GameEnums.eMonsterType.Turtle, new Vector3(-3, 0, 0));//제너레이터야 만들어라
            turtle.onDie = (rewardItemType) =>
            {
                var pos = turtle.gameObject.transform.position;
                this.CreateItem(rewardItemType, pos);
                Destroy(turtle.gameObject);
            };
            MonsterController slime = this.monsterGenerator.Generate(GameEnums.eMonsterType.Slime, new Vector3(0, 0, 3));
            slime.onDie = (rewardItemType) =>
            {
                var pos = slime.gameObject.transform.position;
                this.CreateItem(rewardItemType, pos);
                Destroy(slime.gameObject);
            };
            this.monsterList.Add(turtle);
            this.monsterList.Add(slime);
            Debug.LogFormat("this.monsterList.Count: {0}", this.monsterList.Count);
            foreach (MonsterController monster in monsterList)
            {
                Debug.LogFormat("monster{0}", monster);
            }
            this.itemList = new List<ItemController>();


        }
        private void CreateHero()
        {
            GameObject heroGo = Instantiate(this.heroPrefab);
            this.heroController = heroGo.GetComponent<HeroController>();
            Debug.Log(this.heroController);
        }
        // Update is called once per frame
        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 3f);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                                       
                    MonsterController controller = hit.collider.gameObject.GetComponent<MonsterController>();
                    if (controller != null)
                    {
                        this.monsterList.Remove(controller);
                        controller.Die();
                    }
                    else
                    {
                        if (this.heroController != null)
                        {
                            this.heroController.Move(hit.point);
                        }
                    }
                    
                    Debug.LogFormat("this.monsterList.Count: {0}", this.monsterList.Count);
                    if (this.monsterList.Count <= 0)
                    {
                        this.CreatePortal();
                    }
                }
                
            }
            
        }
        private void CreateItem(GameEnums.eItemType itemType, Vector3 position)
        {
            this.itemGenerator.Generate(itemType, position);
        }
        private void CreatePortal()
        {
            int x = Random.Range(-4, 4);
            int z = Random.Range(-4, 4);
            GameObject portalGo = Instantiate(portalPrefab);
            portalGo.transform.position = new Vector3(x, 0, z);
        }
    }
}



