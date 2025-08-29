using UnityEngine;

/*
    Épület elemre kell tenni. Ez adja meg a életét.
*/
public class BuildingHealth : MonoBehaviour
{
    [Header("Building health setting")]
    [SerializeField] int maxHealth = 1;                     // mennyi élet ereje legyen
    [SerializeField] BreakController breakPrefab;           // Prefabb egy széttört Modelre
    [SerializeField] int destroyScore = 10;                 // ennyi pontot kapok ha szétlövöm

    Buildings _buildings;                                   // Hívatkozás a building componensre
    int _health;                                            // aktuális élet


    // Kezdő beállítások 
    public void Setup(Buildings buildings)
    {
        _health = maxHealth;
        _buildings = buildings;
    }

    // Pusztítás Kapott sebzéssel pusztít és az aktuális élettel pedig a bombát gyengít
    public int Damager(int damage)
    {
        int result = _health;
        _health -= damage;
        if (_health <= 0)
        {
            _buildings.AddScore(destroyScore);
            DestroyItem();
        }
        return result;
    }

    // Töröt model létrehozása és indítása, majd az épület model visszarakása a poolba
    public void DestroyItem()
    {
        // effect
        if (breakPrefab != null)
        {
            BreakController breakController = Instantiate(breakPrefab, transform.position, transform.rotation);
            breakController.Setup();
        }
        // Report Buildings
        _buildings.BackPoolItem(gameObject);
    }
}
