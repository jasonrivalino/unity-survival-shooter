using UnityEngine;

namespace CompleteProject
{
    public class BossEffect : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
        public int attackDamage = 10;               // The amount of health taken away per attack.
        private float defaultSpeed;
        private int defaultShotgunDamage;
        private int defaultKatanaDamage;
        private int defaultRiffleDamage;


        Animator anim;                              // Reference to the animator component.
        GameObject player;                          // Reference to the player GameObject.\
        PlayerHealth playerHealth;                  // Reference to the player's health.
        EnemyHealth enemyHealth;                    // Reference to this enemy's health.
        PlayerWeaponManager playerWeaponManager;                              // Reference to the PlayerWeaponManager
        PlayerMovement speedPlayer;                 // Reference to the speed variable in PlayerMovement.cs
        bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
        float timer;                                // Timer for counting up to the next attack.


        void Awake()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent<Animator>();
            playerWeaponManager = player.GetComponent<PlayerWeaponManager>();
            speedPlayer = player.GetComponent<PlayerMovement>();
        }


        void OnTriggerEnter(Collider other)
        {
            // If the entering collider is the player...
            if (other.gameObject == player)
            {
                // ... the player is in range.
                playerInRange = true;

                // If the player is in range, the player's speed will be decreased.
                speedPlayer.ApplyBossEffect();
                // Debug.Log("speedPlayer: " + speedPlayer.speed);

                // Decreasing weapon's damage
                playerWeaponManager.ApplyBossEffect();
            }
        }


        void OnTriggerExit(Collider other)
        {
            // If the exiting collider is the player...
            if (other.gameObject == player)
            {
                // ... the player is no longer in range.
                playerInRange = false;
                speedPlayer.UnApplyBossEffect();

                // Resetting weapon's damage
                playerWeaponManager.UnApplyBossEffect();
            }
        }


        void Update()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                // ... attack.
                Attack();
            }

            // If the player has zero or less health...
            if (playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger("PlayerDead");
            }
        }


        void Attack()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if (playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}
