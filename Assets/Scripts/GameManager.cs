using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BubbleSpawner[] BubbleSpawners;
    public BubbleManager BubbleManager;
    public Text TimerText;
    public Text ScoreText;
    public Slider BonusSlider;
    public GameObject Menu;
    public Text RecordText;

    private float _score;
    private float _recordScore;
    private float _time;
    private bool _isGameOver;

    private void Start()
    {
        _recordScore = SaveGame.Load<int>("Record");
        RecordText.text = "Record: " + _recordScore;
    }

    private void Update()
    {
        if (_time > 0)
        {
            _time = _time - Time.deltaTime;
        }
        else if (!_isGameOver)
        {
            GameOver();
        }

        TimerText.text = "Time: " + string.Format("{0:f0}", _time);
        ScoreText.text = "Score: " + _score.ToString();
    }

    public void GameStart()
    {
        Menu.SetActive(false);

        _time = 30;
        _score = 10;
        _isGameOver = false;

        for (int i = 0; i < BubbleSpawners.Length; i++)
        {
            BubbleSpawners[i].EnableSpawner();
        }
    }

    public void GameOver()
    {
        _time = 0;
        BonusSlider.value = BonusSlider.minValue;
        _isGameOver = true;

        for (int i = 0; i < BubbleSpawners.Length; i++)
        {
            BubbleSpawners[i].DisableSpawner();
        }

        BubbleManager.DestroyBubbles();

        if (_score > _recordScore)
        {
            _recordScore = _score;
            RecordText.text = "New Record: " + _recordScore;

            SaveGame.Save("Record", (int)_recordScore);
        }

        Menu.SetActive(true);
    }

    public void AddScore()
    {
        _score = _score + 1;

        AddBonusBar();
    }

    public void RemoveScore()
    {
        _score = _score - 1;

        if (_score <= 0)
        {
            GameOver();
        }
    }

    private void AddBonusBar()
    {
        BonusSlider.value = BonusSlider.value + 1;

        if (BonusSlider.value >= BonusSlider.maxValue)
        {
            BubbleSpawner bubbleSpawner = BubbleSpawners[Random.Range(0, BubbleSpawners.Length - 1)];
            bubbleSpawner.SpawnBubble(BubbleType.Bonus);

            BonusSlider.value = BonusSlider.minValue;
        }
    }

    public void ResetBonusBar()
    {
        BonusSlider.value = BonusSlider.minValue;
    }

    public void AddTime()
    {
        _time = _time + 3;
    }
}
