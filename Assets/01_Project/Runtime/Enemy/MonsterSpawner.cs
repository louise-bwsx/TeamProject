using Sirenix.OdinInspector;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] monster;
    [SerializeField] private BossHealth bossHealth;
    [SerializeField, ReadOnly] private float timer;
    private float spawnDuration = 3f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnDuration)
        {
            if (bossHealth.GetState() == BossState.Stage2)
            {
                Spwanmonster();
                timer = 0;
            }
        }
    }

    private void Spwanmonster()
    {
        float x = Random.Range(72f, 82f);
        float z = Random.Range(67f, 77f);
        int random = Random.Range(0, monster.Length);
        Instantiate(monster[random], new Vector3(x, 16f, z), transform.rotation);
    }
}