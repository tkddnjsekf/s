using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GotoLeaderBoardButton : MonoBehaviour
{
    Button btn;
    // Start is called before the first frame update
    void OnEnable()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("LeaderBoard");
            });
    }

}
