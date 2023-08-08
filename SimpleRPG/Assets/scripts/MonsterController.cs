using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float radius = 0.9f;
    [SerializeField]
    private HeroController heroController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

    //사정거리
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, this.radius);
    }
}
