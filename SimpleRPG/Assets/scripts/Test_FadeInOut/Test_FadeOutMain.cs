using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_FadeOutMain : MonoBehaviour
{
    [SerializeField]
    private Image fadeout;
    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(this.FadeOut());
    }

    // Update is called once per frame
    private IEnumerator FadeOut()
    {
        Color color = this.fadeout.color;

        while(true){
            color.a += 0.01f;
            this.fadeout.color = color;

            if (this.fadeout.color.a >= 1)//완전 까매지면
            {
                break;
            }
            yield return null;
        }
        Debug.LogFormat("fadeout complete!");
    }
}
