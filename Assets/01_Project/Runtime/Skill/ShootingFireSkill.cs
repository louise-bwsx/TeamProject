using UnityEngine;

public class ShootingFireSkill : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float defaultDuration = 1f;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject fireTornado;

    private void OnEnable()
    {
        duration = defaultDuration;
    }

    private void Update()
    {
        if (!GameStateManager.Inst.IsGaming())
        {
            return;
        }

        duration -= Time.deltaTime;
        if (duration <= 0f)
        {
            ObjectPool.Inst.PutBackInPool(gameObject, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Wall":
            case "EarthSkill":
            case "Monster":
                ObjectPool.Inst.PutBackInPool(gameObject, 0);
                break;
            case "WindAttack":
                //拿火的位置太高了
                Destroy(Instantiate(fireTornado, other.transform.position, transform.rotation), 5f);
                ObjectPool.Inst.PutBackInPool(gameObject, 0);
                ObjectPool.Inst.PutBackInPool(other.gameObject, 0);
                break;
            case "Poison":
                Destroy(Instantiate(bomb, transform.position, transform.rotation), 1f);
                ObjectPool.Inst.PutBackInPool(gameObject, 0);
                ObjectPool.Inst.PutBackInPool(other.gameObject, 0);
                break;
        }
        //不希望打到其他東西消失
    }
}
