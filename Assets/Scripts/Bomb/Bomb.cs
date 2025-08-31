using UnityEngine;

/*
    Bomba főosztály
*/
public class Bomb : MonoBehaviour
{
    [Header("Bomb settings")]
    [SerializeField] ParticleSystem ps;                         // effect robanáskor
    public int bombDMG;                             // ha ez nulla akkor pusztul el a bomba.
    public float shrapnelDMG;                       // shrapnel dmg
    public float fallSpeed;                         // bomba zuhanási sebessége
    public int limitedBomb;                         // ha nem nulla akkor vonni kell
    public bool isLimited;                          // ha igaz akkor számolom a limitedBomb-ot
    protected int destructiveEffect;
    protected DropBomb bombDrop = null;


    // Kezdő beállítások
    public virtual void Setup(DropBomb dropBomb)
    {
        PlayerData playerData = PlayerDM.Instance().GetPlayerData();
        bombDrop = dropBomb;
        destructiveEffect = bombDMG + playerData.PlusBombDmg;
    }

    // Ütközés detektálás
    protected virtual void OnTriggerEnter(Collider other)
    {
        // Ha halál zonával ütközünk akkor kiment a képről és elpusztítja az Objectet
        if (other.TryGetComponent(out DeathZone deathZone))
        {
            BombDestroy();
        }
    }

    // Az épületből levonunk annyit mint amennyi a pusztítóhatás
    protected virtual int BombHit(BuildingHealth buildingHealth)
    {
        return buildingHealth.Damager(destructiveEffect);
    }

    // Particle effekt ha a bomba elpusztul
    protected virtual void BombEffect()
    {
#warning "BOMB:BOMBEFFECT - BOMB PARTICLE EFFECT"
        if (ps != null) {
            ps.Play();
        }
    }

    // Bomba visszarakása a poolba.
    protected virtual void BombDestroy(bool isBuilding = true)
    {
        if (bombDrop != null)
        {
            bombDrop.Reload(gameObject, isBuilding);
        }
    }
}
