using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int planeIndex;
    public int coins;
    public List<LevelScore> levelScores;

    // fejlesztések ha 0 akkor nincs fejleszve.
    public int PlusDropBomb;                        // DropBomb -ban használom fel
    public float BombFallSpeed;                    // BombMoving -ban használom fel
    public int PlusBombDmg;                         // Bomb -ban használom fel
    public int PlusShrapnelDmg;                     // Shrapnel -ban használom fel
    public float PlaneSpeedReduction;              // PlayerMoving -ban használom fel
    public int MoreBomb;                            // TOVÁBBI FEJLESZTÉSRE FENTARTVA :)
    public int FreezeHeight;                        // PlayerMoving -ban használom fel
}

[System.Serializable]
public class LevelScore
{
    public int highScore;
    public int score;
}
