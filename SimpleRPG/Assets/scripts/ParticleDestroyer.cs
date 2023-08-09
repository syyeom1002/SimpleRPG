using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private ParticleSystem ps;
    private float elaspedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.ps = this.GetComponent<ParticleSystem>();
        this.StartCoroutine(this.coWaitForPlayAfterDestroy());
        
    }
    private IEnumerator coWaitForPlayAfterDestroy()
    {
        //duration��ŭ ��ٷȴٰ� �ı��ض�(������ ���)
        yield return new WaitForSeconds(this.ps.main.duration);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    //void Update()
    //{
    //    this.elaspedTime += Time.deltaTime;
    //    if (this.elaspedTime >= this.ps.main.duration)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
}
