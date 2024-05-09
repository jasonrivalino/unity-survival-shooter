using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompleteProject
{
    public class PetManager : MonoBehaviour
    {
        public Transform player;
        public GameObject dogPrefab;
        public GameObject cactusPrefab;
        public GameObject bombPrefab;
        public GameObject mushroomPrefab;
        public GameObject rabbitPrefab;
        public GameObject ghostPrefab;
        public GameObject turtlePrefab;
        public GameObject batPrefab;
        public GameObject slimePrefab;


        public string enemyTag = "Enemy";

        private List<GameObject> buffedEnemy = new List<GameObject>();

        void Start()
        {
            // spawn attacker
            if (PlayerPrefs.HasKey("dog"))
            {
                for (int i = 0; i < PlayerPrefs.GetInt("dog"); i++)
                {
                    Instantiate(dogPrefab, player.position, player.rotation);
                }
            }
            if (PlayerPrefs.HasKey("bomb"))
            {
                for (int i = 0; i < PlayerPrefs.GetInt("bomb"); i++)
                {
                    Instantiate(bombPrefab, player.position, player.rotation);
                }
            }
            if (PlayerPrefs.HasKey("cactus"))
            {
                for (int i = 0; i < PlayerPrefs.GetInt("cactus"); i++)
                {
                    Instantiate(cactusPrefab, player.position, player.rotation);
                }
            }

            // spawn healer
            if (PlayerPrefs.HasKey("mushroom"))
            {
                for (int i = 0; i < PlayerPrefs.GetInt("mushroom"); i++)
                {
                    Instantiate(mushroomPrefab, player.position, player.rotation);
                }
            }
            if (PlayerPrefs.HasKey("ghost"))
            {
                for (int i = 0; i < PlayerPrefs.GetInt("ghost"); i++)
                {
                    Instantiate(ghostPrefab, player.position, player.rotation);
                }
            }
            if (PlayerPrefs.HasKey("rabbit"))
            {
                for (int i = 0; i < PlayerPrefs.GetInt("rabbit"); i++)
                {
                    Instantiate(rabbitPrefab, player.position, player.rotation);
                }
            }

        }

        public void spawnEnemyPet(Transform owner, int petCount)
        {
            for (int  i = 0;  i < petCount;  i++)
            {
                turtlePrefab.TryGetComponent<Buff>(out var buff);
                buff.owner = owner;
                Instantiate(turtlePrefab, owner.position, owner.rotation);
            }
        }
        void Update()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            foreach (GameObject obj in enemies)
            {
                // Perform processing for each GameObject
                if (!buffedEnemy.Contains(obj))
                {
                    BossEffect bossEffect = obj.GetComponent<BossEffect>();
                    if (bossEffect != null)
                    {
                        buffedEnemy.Add(obj);
                        int petCount = Random.Range(0, 3);
                        Debug.Log("BOSS DETECTED. " + petCount + "PET(S) SPAWNED");
                        spawnEnemyPet(bossEffect.gameObject.transform, petCount);
                    }
                    DevilEffect devilEffect = obj.GetComponent<DevilEffect>();
                    if (devilEffect != null)
                    {
                        buffedEnemy.Add(obj);
                        int petCount = Random.Range(0, 2);
                        Debug.Log("DEVIL DETECTED. " + petCount + "PET(S) SPAWNED");
                        spawnEnemyPet(devilEffect.gameObject.transform, petCount);
                    }
                }
            }

        }
    }
}
