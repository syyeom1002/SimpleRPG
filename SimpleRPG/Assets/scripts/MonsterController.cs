using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float radius = 1f;
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
        Gizmos.color = Color.yellow;
        GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, radius, 40);
    }
}
