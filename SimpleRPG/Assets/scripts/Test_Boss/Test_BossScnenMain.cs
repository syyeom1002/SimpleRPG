using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test_BossSceneMain : MonoBehaviour
{
    [SerializeField]
    private GameObject bossPrefab;
    [SerializeField]
    private GameObject heroPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
        this.CreateBoss();
        this.CreateHero();
        
    }

    private void CreateBoss()
    {
        GameObject bossGo = Instantiate(this.bossPrefab);
        bossGo.transform.position = new Vector3(6, 0, -9);
    }
    private void CreateHero()
    {
        GameObject heroGo = Instantiate(this.heroPrefab);
    }
    // Update is called once per frame

    

    void Update()
    {

    }
}
