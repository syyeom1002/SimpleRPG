using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_FadeInMain : MonoBehaviour
{
    [SerializeField]
    private Image fadein;
    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    private IEnumerator FadeIn()
    {
        Color color = this.fadein.color;
        while (true)
        {
            color.a -= 0.01f;
            this.fadein.color = color;

            if (this.fadein.color.a <= 0)
            {
                break;
            }
            yield return null;
        }
        
    }
}
