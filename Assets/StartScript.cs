using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{

    private Button button;

    private GameObject scene;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        scene = GameObject.FindGameObjectWithTag("GameScene");
        scene.SetActive(false);
    }

    void TaskOnClick()
    {
        scene.SetActive(true);
        GameObject.FindGameObjectWithTag("UIStart").SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
