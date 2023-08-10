using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSceneMain : MonoBehaviour
{
    [SerializeField]
    private HeroController heroController;
    //[SerializeField]
    //private Image image;
    // Start is called before the first frame update
    void Start()
    {
        //this.Fade();
        // this.StartCoroutine(this.CoFade());
        this.heroController.onMoveComplete = () =>
        {
            Debug.Log("�̵��� �Ϸ��߽��ϴ�.");
        };
    }

    // Update is called once per frame
    void Update()
    {
        //ȭ���� Ŭ���ϸ� Ŭ���� ��ġ�� Hero�� �̵� 
        if (Input.GetMouseButtonDown(0))
        {
            float maxDistance = 100f;
            //ray�� �����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 3f);
            //�浹�˻�
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                //Debug.Log(hit.point);//null�� ���� �� ���� -> struct-> ������ -> ���ÿ� �� ����-> �� �Ⱦ� (���� ����) null���۷��� ���ü��� ����//���� ���� �浹 ���� ��ġ 
                //hero ���� ������Ʈ�� �̵��ض� ��� ��Ʈ�ѷ����� ���

                //Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Monster")
                {
                    //���Ͷ� ������� �Ÿ�
                    float distance = Vector3.Distance(this.heroController.transform.position, hit.collider.gameObject.transform.position);

                    Debug.Log(distance);

                    MonsterController monsterController = hit.collider.gameObject.GetComponent<MonsterController>();
                    //���Ͷ� ����� �����Ÿ� ������ ���� ��
                    float sumRadius = this.heroController.radius + monsterController.radius;

                    Debug.Log(sumRadius);

                    //��Ÿ� �ȿ� ����
                    if (distance <= sumRadius)
                    {
                        //����
                        //�̵��ϴ°ſ��� �������

                    }
                    else
                    {
                        this.heroController.Move(monsterController);
                    }
                }
                else if (hit.collider.tag == "Ground")
                {
                    this.heroController.Move(hit.point);
                }
            }
        }
    }
    //void Fade()
    //{
    //    for (float f = 1f; f >= 0; f -= 0.1f)
    //    {
    //        Color c = this.image.color;
    //        Debug.Log(f);
    //        c.a = f;
    //        this.image.color = c;
    //    }
    //}
    //IEnumerator CoFade()
    //{
    //    for (float f = 1f; f >= 0; f -= 0.01f)
    //    {
    //        Color c = this.image.color;
    //        Debug.Log(f);
    //        c.a = f;
    //        this.image.color = c;
    //        yield return null;
    //    }
    //}
}
