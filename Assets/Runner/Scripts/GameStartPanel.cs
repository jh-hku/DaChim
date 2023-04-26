using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartPanel : MonoBehaviour
{
    private bool mFaded = false;
    public float Duration = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        Fade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade(){
        var canvaGroup = GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(canvaGroup, canvaGroup.alpha, mFaded ? 1 : 0));
        mFaded = !mFaded;
    }

    public IEnumerator DoFade(CanvasGroup canvaGroup, float start, float end) {
        float counter = 0f;
        while (counter < Duration) {
            counter += Time.deltaTime;
            canvaGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
            yield return null;
        }
    }
}
