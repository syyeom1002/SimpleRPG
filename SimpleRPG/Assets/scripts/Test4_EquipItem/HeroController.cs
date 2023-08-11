using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test4_EquipItem
{
    public class HeroController : MonoBehaviour
    {
        //[SerializeField]
        //public GameObject weaponGo;//착용중인 무기 게임오브젝트 
        [SerializeField]
        private Transform weaponTrans;//히어로 프리팹의 자식 루트의 자식 pelvis의 자식 Weapon trans(sword의 부모)
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
            //    Debug.Log("착용중인 무기가 없습니다.");
            //}
            Debug.LogFormat("자식의 수:{0}", this.weaponTrans.childCount);
            if (this.weaponTrans.childCount == 0)
            {
                //착용중인 무기가 없다 .
                Debug.Log("착용중인 무기가 없습니다.");

            }
            else
            {
                //착용중인 무기가 있다
                Transform child =this.weaponTrans.GetChild(0);//첫번째 자식 
                //무기를 제거
                Destroy(child.gameObject);
            }
        }
        //방패 제거 
        public void unEquipShield()
        {
            if (this.shieldTrans.childCount == 0)
            {
                Debug.Log("착용중인 방패가 없습니다.");
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