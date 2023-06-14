using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class PlayerScoresWrapper
{
    public GameScore[] playerScores;

    public PlayerScoresWrapper(List<GameScore> playerScores)
    {
        this.playerScores = playerScores.ToArray();
    }
}
[System.Serializable]
public class GameScore
{
    public int score;
    public int stage;
    public GameScore(int score, int stage){
        this.score = score;
        this.stage = stage;
    }
    public static void SavePlayerScores(List<GameScore> scores)
    {
        // 플레이어 스코어 목록을 JSON 형식으로 직렬화
        string json = JsonUtility.ToJson(new PlayerScoresWrapper(scores));
        Debug.Log(json);

        // PlayerPrefs에 JSON 데이터 저장
        PlayerPrefs.SetString("PlayerScores", json);

        // PlayerPrefs 저장
        PlayerPrefs.Save();
    }

    public static List<GameScore> LoadPlayerScores()
    {
        // PlayerPrefs에서 저장된 JSON 데이터 가져오기
        string json = PlayerPrefs.GetString("PlayerScores");
        Debug.Log(json);

        // JSON 데이터가 비어있지 않은 경우
        if (!string.IsNullOrEmpty(json))
        {
            // JSON 데이터를 플레이어 스코어 목록으로 역직렬화
            PlayerScoresWrapper wrapper = JsonUtility.FromJson<PlayerScoresWrapper>(json);

            // 역직렬화된 스코어 목록 가져오기
            try{
                if (wrapper != null)
                    return wrapper.playerScores.ToList();
            }catch(System.Exception e){
                Debug.Log(e);
                return new List<GameScore>();
            }
        }

        // 저장된 스코어가 없는 경우 빈 목록 반환
        return new List<GameScore>();
    }
}