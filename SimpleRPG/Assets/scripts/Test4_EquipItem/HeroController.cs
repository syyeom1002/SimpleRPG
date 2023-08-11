using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test4_EquipItem
{
    public class HeroController : MonoBehaviour
    {
        //[SerializeField]
        //public GameObject weaponGo;//�������� ���� ���ӿ�����Ʈ 
        [SerializeField]
        private Transform weaponTrans;//����� �������� �ڽ� ��Ʈ�� �ڽ� pelvis�� �ڽ� Weapon trans(sword�� �θ�)
        [SerializeField]
        private Transform shieldTrans;
        public Transform ShieldTrans
        {
            get
            {
                return this.shieldTrans;
            }
        }
        public Transform WeaponTrans
        {
            get
            {
                return this.weaponTrans;
            }
        }
        public bool HasWeapon()
        {
            return this.weaponTrans.childCount > 0;
        }
        public bool HasShield()
        {
            return this.shieldTrans.childCount > 0;
        }
        public void unEquipWeapon()
        {
            //if (this.weaponGo != null)
            //{
            //    Destroy(this.weaponGo);
            //}
            //else
            //{
            //    Debug.Log("�������� ���Ⱑ �����ϴ�.");
            //}
            Debug.LogFormat("�ڽ��� ��:{0}", this.weaponTrans.childCount);
            if (this.weaponTrans.childCount == 0)
            {
                //�������� ���Ⱑ ���� .
                Debug.Log("�������� ���Ⱑ �����ϴ�.");

            }
            else
            {
                //�������� ���Ⱑ �ִ�
                Transform child =this.weaponTrans.GetChild(0);//ù��° �ڽ� 
                //���⸦ ����
                Destroy(child.gameObject);
            }
        }
        //���� ���� 
        public void unEquipShield()
        {
            if (this.shieldTrans.childCount == 0)
            {
                Debug.Log("�������� ���а� �����ϴ�.");
            }
            else
            {
                Transform child = this.shieldTrans.GetChild(0);
                //child.transform.SetParent(null);
                //child.transform.localPosition = new Vector3(child.transform.position.x, 0, child.transform.position.z);
                //child.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, -90));
                Destroy(child.gameObject);
            }
        }
    }
}