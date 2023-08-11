using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_EquipItemMain : MonoBehaviour
{
    [SerializeField]
    private Button btnRemoveSword;
    [SerializeField]
    private Button btnEquipSword0;//생성시 부모지정
    [SerializeField]
    private Button btnEquipSword1;//생성 후 부모지정


    [SerializeField]
    private GameObject swordPrefab;
    [SerializeField]
    private Test4_EquipItem.HeroController heroController;


    [SerializeField]
    private Button btnRemoveShield;
    [SerializeField]
    private Button btnEquipShield0;//생성 시 부모 지정
    [SerializeField]
    private Button btnEquipShield1;//생성 후 부모지정

    [SerializeField]
    private GameObject shieldPrefab;
    // Start is called before the first frame update
    void Start()
    {
        this.btnRemoveSword.onClick.AddListener(() =>
        {
            Debug.Log("영웅에게서 칼이 있다면 신에서 제거");
            //무기를 제거
            this.heroController.unEquipWeapon();
        });

        this.btnEquipSword0.onClick.AddListener(() =>
        {
            Debug.Log("생성시 부모를 지정");
            //새롭게 장착할
            bool hasWeapon= this.heroController.HasWeapon();
            if (!hasWeapon)
            {
                Instantiate(this.swordPrefab, this.heroController.WeaponTrans);
            }
            else
            {
                Debug.Log("이미 착용중입니다.");
            }
        });

        this.btnEquipSword1.onClick.AddListener(() =>
        {
            Debug.Log("생성 후 부모를 지정");
            bool hasWeapon = this.heroController.HasWeapon();
            if (!hasWeapon)
            {
                GameObject go = Instantiate(this.swordPrefab);
                //부모를 지정
                go.transform.SetParent(this.heroController.WeaponTrans);
                //위치를 초기화
                go.transform.localPosition = Vector3.zero;
                //회전을 초기화
                go.transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.Log("이미 착용중입니다.");
            }
        });



        //--------------------------방패 착용 제거----------------------------------------------
        this.btnRemoveShield.onClick.AddListener(() =>
        {
            Debug.Log("방패 제거");
            this.heroController.unEquipShield();
        });

        this.btnEquipShield0.onClick.AddListener(() =>
        {
            Debug.Log("생성 시 방패 착용");
            bool hasShield = this.heroController.HasShield();
            if (!hasShield)//방패 있냐? 
            {
                //방패 없다
                GameObject shieldGo=Instantiate(this.shieldPrefab, this.heroController.ShieldTrans);
                shieldGo.transform.localRotation = Quaternion.Euler(new Vector3(-0.106f, -104.917f, 0.077f));
            }
            else
            {
                //방패 있다.
                Debug.Log("이미 방패 착용중입니다.");
            }
        });
        this.btnEquipShield1.onClick.AddListener(() =>
        {
            Debug.Log("생성 후 방패 착용");
            bool hasShield = this.heroController.HasShield();
            if (!hasShield)
            {
                GameObject shieldGo= Instantiate(this.shieldPrefab);
                shieldGo.transform.SetParent(this.heroController.ShieldTrans);
                shieldGo.transform.localPosition= Vector3.zero;
                shieldGo.transform.localRotation = Quaternion.Euler(new Vector3(-0.106f,-104.917f,0.077f));
            }
            else
            {
                Debug.Log("이미 방패 착용중입니다.");
            }
        });
    }
    
}
