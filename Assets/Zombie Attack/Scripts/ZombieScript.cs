using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl aicc;
    public ZombieSpawnerScript parentSpawner;

    private void Awake() 
    {
        if(aicc == null)
        {
            aicc = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        }
    }

    public void Init(Transform target, ZombieSpawnerScript spawner)
    {
        if(aicc == null)
        {
            aicc = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        }
        aicc.target = target;
        parentSpawner = spawner;
    }

    public void KillZombie()
    {
        parentSpawner.ZombieHasDied();
        Destroy(gameObject);
    }
}
