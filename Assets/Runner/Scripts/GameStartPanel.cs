using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartPanel : MonoBehaviour
{
    private bool mFaded = false;
    public float Duration = 0.4f;

    private static GameStartPanel instance;
    public static GameStartPanel Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameStartPanel>();
            return instance;
        }
    }
    // public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {   
        Time.timeScale = 0;
        // startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame(){
        Time.timeScale = 1;
        Fade();
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
