using UnityEngine;

public class EnemyMagic : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float defaultDuration = 1f;
    [SerializeField] private GameObject hitEffect;

    private void Start()
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Wall":
            case "Player":
            case "EarthSkill":
                GameObject FX = Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(FX, 1f);
                Destroy(gameObject);
                break;
        }
    }
}