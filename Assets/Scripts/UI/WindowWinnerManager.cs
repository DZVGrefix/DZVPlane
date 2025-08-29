using System.Collections;
using TMPro;
using UnityEngine;

public class WindowWinnerManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text coinsText;

    float _displayScore;
    float _score;
    float _displayCoins;
    float _coins;

    float _displaySpeed = 2f;

    public void Setup(int highScore, float score, float coins)
    {
        _displayScore = 0;
        _score = score;
        _displayCoins = 0;
        _coins = coins;

        highScoreText.text = highScore.ToString("0");

        StartCoroutine(Show());
    }

    /*void Update()
    {
        if (_displayCoins != _score || _displayCoins != _coins)
        {
            float speed = _displaySpeed * Time.deltaTime;
            _displayCoins = Mathf.Clamp(_displayScore, _score, speed);
            _displayCoins = Mathf.Clamp(_displayCoins, _coins, speed);

            scoreText.text = _displayScore.ToString("0");
            coinsText.text = _displayCoins.ToString("0");

            
        }
    }*/
    IEnumerator Show()
    {
        while (_displayCoins != _score || _displayCoins != _coins)
        {
            float speed = _displaySpeed * Time.deltaTime;
            _displayScore = Mathf.Lerp(_displayScore, _score, speed);
            _displayCoins = Mathf.Lerp(_displayCoins, _coins, speed);

            scoreText.text = _displayScore.ToString("0");
            coinsText.text = _displayCoins.ToString("0");

            yield return null;
        }
    }
}
