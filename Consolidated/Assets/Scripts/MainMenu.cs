using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    public GameObject Startbtn;
    public GameObject Starttext;
    public GameObject Quitbtn;
    public GameObject Quittext;
    public GameObject Camera;
    public GameObject titletext;
    public GameObject blocker;
    public GameObject Gold1;
    public GameObject Gold2;
    public GameObject GameManager;
    public GameObject wave1;
    public GameObject wave2;
    public GameObject daymanager;
    private Color c_b;

    private void Awake(){
        Startbtn.GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(FadeScreen()); });
        Quitbtn.GetComponent<Button>().onClick.AddListener(QuitGame);
        daymanager.GetComponent<DayNightController>().daySpeedMultiplier = 0f;
    }
    
    private IEnumerator FadeScreen () {
        Vector3 startpos = Camera.transform.position;
        Vector3 endpos = startpos;
        daymanager.GetComponent<DayNightController>().daySpeedMultiplier = .25f;
        endpos.y = 12;
        float i = 0f;
        float alpha = 1f;
        Color c = Color.white;
        Color e = Color.white;
        e.a = 0f;
        while (i < 1f) {
            i += Time.deltaTime;
            alpha -= Time.deltaTime;
            c.a = alpha;
            Camera.transform.position = Vector3.Lerp(startpos,endpos,i);
            Starttext.GetComponent<TextMeshProUGUI>().color = c;
            Quittext.GetComponent<TextMeshProUGUI>().color = c;
            titletext.GetComponent<TextMesh>().color = c;
            blocker.GetComponent<Image>().color = Color.Lerp(c_b, e, i);

            yield return null;
        }

        StartGame();
    }

    private void StartGame(){
        Camera.GetComponent<CameraController>().exploration = 0;
        Startbtn.SetActive(false);
        Quitbtn.SetActive(false);
        titletext.SetActive(false);
        blocker.GetComponent<Image>().color = c_b;
        blocker.SetActive(false);
        Gold1.SetActive(true);
        Gold2.SetActive(true);
        GameManager.SetActive(true);
        wave1.SetActive(true);
        wave2.SetActive(true);
    }


        private void QuitGame () {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        c_b = blocker.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
