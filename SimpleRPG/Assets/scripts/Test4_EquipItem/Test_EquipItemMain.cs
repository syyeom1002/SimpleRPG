using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_EquipItemMain : MonoBehaviour
{
    [SerializeField]
    private Button btnRemoveSword;
    [SerializeField]
    private Button btnEquipSword0;//������ �θ�����
    [SerializeField]
    private Button btnEquipSword1;//���� �� �θ�����


    [SerializeField]
    private GameObject swordPrefab;
    [SerializeField]
    private Test4_EquipItem.HeroController heroController;


    [SerializeField]
    private Button btnRemoveShield;
    [SerializeField]
    private Button btnEquipShield0;//���� �� �θ� ����
    [SerializeField]
    private Button btnEquipShield1;//���� �� �θ�����

    [SerializeField]
    private GameObject shieldPrefab;
    // Start is called before the first frame update
    void Start()
    {
        this.btnRemoveSword.onClick.AddListener(() =>
        {
            Debug.Log("�������Լ� Į�� �ִٸ� �ſ��� ����");
            //���⸦ ����
            this.heroController.unEquipWeapon();
        });

        this.btnEquipSword0.onClick.AddListener(() =>
        {
            Debug.Log("������ �θ� ����");
            //���Ӱ� ������
            bool hasWeapon= this.heroController.HasWeapon();
            if (!hasWeapon)
            {
                Instantiate(this.swordPrefab, this.heroController.WeaponTrans);
            }
            else
            {
                Debug.Log("�̹� �������Դϴ�.");
            }
        });

        this.btnEquipSword1.onClick.AddListener(() =>
        {
            Debug.Log("���� �� �θ� ����");
            bool hasWeapon = this.heroController.HasWeapon();
            if (!hasWeapon)
            {
                GameObject go = Instantiate(this.swordPrefab);
                //�θ� ����
                go.transform.SetParent(this.heroController.WeaponTrans);
                //��ġ�� �ʱ�ȭ
                go.transform.localPosition = Vector3.zero;
                //ȸ���� �ʱ�ȭ
                go.transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.Log("�̹� �������Դϴ�.");
            }
        });



        //--------------------------���� ���� ����----------------------------------------------
        this.btnRemoveShield.onClick.AddListener(() =>
        {
            Debug.Log("���� ����");
            this.heroController.unEquipShield();
        });

        this.btnEquipShield0.onClick.AddListener(() =>
        {
            Debug.Log("���� �� ���� ����");
            bool hasShield = this.heroController.HasShield();
            if (!hasShield)//���� �ֳ�? 
            {
                //���� ����
                GameObject shieldGo=Instantiate(this.shieldPrefab, this.heroController.ShieldTrans);
                shieldGo.transform.localRotation = Quaternion.Euler(new Vector3(-0.106f, -104.917f, 0.077f));
            }
            else
            {
                //���� �ִ�.
                Debug.Log("�̹� ���� �������Դϴ�.");
            }
        });
        this.btnEquipShield1.onClick.AddListener(() =>
        {
            Debug.Log("���� �� ���� ����");
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
                Debug.Log("�̹� ���� �������Դϴ�.");
            }
        });
    }
    
}
