using UnityEngine;

public class BombMoving : MonoBehaviour
{
    [Header("Bomb moving")]
    [SerializeField] float speed = 5f;
    float _curSpeed;

    void Start()
    {
        PlayerData playerData = PlayerDM.Instance().GetPlayerData();
        _curSpeed = speed * playerData.BombFallSpeed;
    }

    void Update()
    {
        Vector3 direction = Vector3.down.normalized;
        transform.Translate(_curSpeed * Time.deltaTime * direction);
    }
}
