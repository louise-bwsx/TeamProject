using UnityEngine;

public class ShootingWaterSkill : MonoBehaviour
{
    [SerializeField] private float explodeRadius = 5f;
    [SerializeField] private float force = 700f;
    [SerializeField] private float duration;
    [SerializeField] private float defaultDuration = 1f;
    [SerializeField] private string hitEfffectName;
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        duration = defaultDuration;
        collider.enabled = true;
    }

    private void Update()
    {
        if (GameStateManager.Inst.CurrentState == GameState.Pausing)
        {
            return;
        }

        duration -= Time.deltaTime;
        if (duration <= 0f)
        {
            AudioManager.Inst.PlaySFX("WaterSkill");
            Explode();
        }
    }

    private void Explode()
    {
        collider.enabled = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider nearbyObject in colliders)
        {
            ObjectPool.Inst.SpawnFromPool(hitEfffectName, nearbyObject.transform.position, transform.rotation, duration: 1f);
            Rigidbody rigidbody = nearbyObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(force, transform.position, explodeRadius);
            }
        }
        ObjectPool.Inst.PutBackInPool(gameObject, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" ||
            other.gameObject.tag == "Default"//為了不打到隱形牆
            )
        {
            return;
        }
        switch (other.tag)
        {
            case "Monster":
            case "Wall":
            case "EarthSkill":
                Explode();
                break;
        }
    }
}
