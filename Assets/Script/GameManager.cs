using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //event
    bool inputDown;
    bool inputJump;
    public GameOverEvent gameoverEvent = null;
    public GameObject player;

    public static GameManager inst = null;//싱글턴
    CactusPool cactusPool;
    BirdPool birdPool;
    public Button pauseButton;
    public GameObject cloud;
    public GameObject mountainBg;
    public GameObject ground;
    public GameObject gameoverScreen;
    public Text scoreText;
    int score
    {
        get
        {
            return Mathf.RoundToInt(rawscore);
        }
    }
    float rawscore;
    public int stage;
    float mountainY = 0f;
    float groundY = 0f;
    void Awake()
    {
        inst = this;
    }
    void Start()
    {
        gameoverEvent = new GameOverEvent();
        gameoverEvent.AddListener(GameOver);
        cactusPool = GetComponent<CactusPool>();
        birdPool = GetComponent<BirdPool>();
        gameoverScreen.SetActive(false);

        //초기화
        mountainY = mountainBg.transform.position.y;
        groundY = ground.transform.position.y;
        rawscore = 0;
        stage = 1;
        scoreText.text = score.ToString();

        //일시정지
        pauseButton.onClick.AddListener(() =>
        {
            pause = !pause;
            if (pause)
            {
                pauseButton.GetComponent<Text>().text = "시작";
            }
            else
            {
                pauseButton.GetComponent<Text>().text = "일시정지";
            }
        });
    }
    public bool pause = false;
    float spawnDelay = 1f;
    void Update()
    {
        scoreText.text = score.ToString();
        if (pause)
        {

        }
        else
        {
            //ingame
            rawscore += 1 * Time.deltaTime;
            stage = 1 + score / 10;
            MoveGround();
            MoveCloud();
            MoveMountain();
            if (spawnDelay <= 0f)
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        if(Random.Range(0, 2)==0 ){
                            SpawnCactus();
                        }
                        SpawnCactus();
                        break;
                    case 1:
                        if (Random.Range(0, 2) == 0)
                        {
                            SpawnBird();
                        }
                        SpawnBird();
                        break;
                }
                spawnDelay = 1f;
            }
            spawnDelay -= Time.deltaTime;
            PlayerMove();
        }
    }

    bool playerJump = false;
    void PlayerMove()
    {
        if(Input.GetMouseButton(0)){
            if (Input.mousePosition.x < Screen.width / 2)
            {
                inputJump = true;
            }
            if (Input.mousePosition.x >= Screen.width / 2)
            {
                inputDown = true;
            }
        }


        if (playerJump == false && (inputDown || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            //숙이기
            player.transform.position = downPlayerVec;
        }
        if (playerJump == false && !(inputDown || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            //들기
            player.transform.position = playerInitVec;
        }
        if (playerJump == false && (inputJump||Input.GetKey(KeyCode.Space)))
        {
            playerJump = true;
            StartCoroutine(jumpRoutine());
            //점프
        }
        inputDown = false;
        inputJump = false;
    }
    Vector2 playerInitVec = new Vector2(-7.42f, -2.86f);
    Vector2 downPlayerVec = new Vector2(-7.42f, -3.86f);

    IEnumerator jumpRoutine()
    {
        var t = 0f;
        while (t < 0.43f)
        {
            var pos = player.transform.position;
            pos.y = ease(-2.86f, 0f, t * 2);
            player.transform.position = pos;
            yield return null;
            t += Time.deltaTime;
        }
        {
            var pos = player.transform.position;
            pos.y = 0f;
            player.transform.position = pos;
        }
        t = 0;
        while (t < 0.5f)
        {
            var pos = player.transform.position;
            pos.y = linear(0f, -2.86f, t * 2);
            player.transform.position = pos;
            yield return null;
            t += Time.deltaTime;
        }
        player.transform.position = playerInitVec;
        playerJump = false;
    }
    public static float linear(float startValue, float endValue, float currentTime)
    {
        float t = currentTime;
        float b = startValue;
        float c = endValue - startValue;
        float d = 1f; // 전체 이동 시간 (1초로 가정)

        t /= d;

        return b + c * t;
    }
    public static float ease(float startValue, float endValue, float currentTime)
    {
        float t = currentTime;
        float b = startValue;
        float c = endValue - startValue;
        float d = 1f; // 전체 이동 시간 (1초로 가정)

        t /= d;
        t--;

        return c * (t * t * t * t * t + 1) + b;
    }

    void SpawnCactus()
    {
        var obj = cactusPool.Spawn();
        obj.transform.position = new Vector2(9.55f, -3.39f);
    }

    void SpawnBird()
    {
        var obj = birdPool.Spawn();
        obj.transform.position = new Vector2(9.55f, -1.607501f);
    }

    // 지면을 왼쪽으로 이동시킵니다.
    void MoveGround()
    {
        // 지면 객체를 일정량 왼쪽으로 이동시킵니다.
        ground.transform.Translate(new Vector2(-(7f+stage) * Time.deltaTime, 0));

        // 지면 객체가 너무 왼쪽으로 이동한 경우 화면의 오른쪽으로 위치를 재설정합니다.
        if (ground.transform.position.x <= -8.99)
        {
            ground.transform.position = new Vector2(8.99f, groundY);
        }
    }

    // 구름을 왼쪽으로 이동시킵니다.
    void MoveCloud()
    {
        // 구름 객체를 일정량 왼쪽으로 이동시킵니다.
        cloud.transform.Translate(new Vector2((-0.3f - stage) * Time.deltaTime, 0));


        // 구름 객체가 너무 왼쪽으로 이동한 경우 화면의 오른쪽으로 위치를 재설정합니다.
        if (cloud.transform.position.x <= -11.26)
        {
            cloud.transform.position = new Vector2(11.26f, Random.Range(0f, 1f));
        }
    }

    // 산을 왼쪽으로 이동시킵니다.
    void MoveMountain()
    {
        // 산 배경 객체를 일정량 왼쪽으로 이동시킵니다.
        mountainBg.transform.Translate(new Vector2((-0.5f - stage) * Time.deltaTime, 0));

        // 산 배경 객체가 너무 왼쪽으로 이동한 경우 화면의 오른쪽으로 위치를 재설정합니다.
        if (mountainBg.transform.position.x <= 38.88f - 6.61f * 4.842075f)
        {
            mountainBg.transform.position = new Vector2(38.88f, mountainY);
        }
    }

    public void GameOver()
    {
        if (!pause)
        {
            SaveScoreToLeaderBoard();
            pauseButton.onClick.RemoveAllListeners();
            gameoverScreen.SetActive(true);
            pause = true;
        }
    }

    public void SaveScoreToLeaderBoard()
    {
        GameScore gameScore = new GameScore(score, stage);
        var scores = GameScore.LoadPlayerScores();
        scores.Add(gameScore);
        GameScore.SavePlayerScores(scores);
    }
}
