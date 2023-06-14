using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    Button btn;
    // Start is called before the first frame update
    void OnEnable()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("InGame");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
