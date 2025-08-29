using UnityEngine;


/*
    Épület modelre kell tenni.
*/
public class BuildingItem : MonoBehaviour
{
    [Header("Building item settings")]
    [SerializeField] float fallSpeed = 2f;              // sebbeség amivel zuhan az épület elem
    Buildings _buildings;                               // hívatkozás az épületekre
    public Vector3 buildPos;                            // épület elem pozíciója
    public Vector2 indexPos;                            // épület elem pozíciója a tömbben
    bool _isFall;                                       // leesés bekapcsolása


    // Kezdő beállítások 
    public void Setup(Buildings buildings, Vector3 pos, Vector2 index)
    {
        _buildings = buildings;
        buildPos = pos;
        indexPos = index;
        _isFall = false;
    }

    // ha ezt hívom akkor beállítom az épületre a zuhanást
    public void StartFall()
    {
        _isFall = true;
    }

    // Zuhanás animáció --- ezt át írhatnám majd coorutinosra
    void Update()
    {
        if (!_isFall) return;

        if (Vector2.Distance(transform.position, buildPos) > .05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, buildPos, Time.deltaTime * fallSpeed);
        }
        else
        {
            transform.position = new Vector3(buildPos.x, buildPos.y, 0f);
            _buildings.SetBuildingsArray(indexPos, this);// .allGems[_posIndex.x, _posIndex.y] = this;
            _isFall = false;
        }
    }
}
