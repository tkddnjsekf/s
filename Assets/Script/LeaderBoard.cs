using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LeaderBoard : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        var ranktxt = "";
        //상위 10위 추출
        List<GameScore> rank = GameScore.LoadPlayerScores();
        if(rank.Count > 0){
            rank = rank.OrderByDescending(score => score.score).ToList();
            if (rank.Count >= 10)
            {
                rank = rank.Take(10).ToList();
            }
            int i = 0;
            foreach (var element in rank)
            {
                ranktxt += $"{i}. {element.stage}스테이지 {element.score}점\n";
                i++;
            }
            text.text = ranktxt;
        }else{
            text.text = "랭킹이 없습니다.";
        }
    }
}
