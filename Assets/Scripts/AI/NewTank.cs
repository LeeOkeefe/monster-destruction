﻿using Objects.Destructible.Definition;
using UnityEngine;

namespace AI
{
    internal sealed class NewTank : Tank
    {
        private void Start()
        {
            InitializeHealth();
        }

        void Update()
        {
            turret.transform.LookAt(PlayerTransform);

            if (!CanShootPlayer)
            {
                timeTillShoot -= Time.deltaTime;
            }

            if (IsPlayerInRange(distanceToAttackTarget) && CanShootPlayer)
            {
                Attack();
            }
        }

        // Deals damage to the building it collides with, and itself
        // Checks whether it is dead and adds score/destroys itself
        //
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Building"))
                return;

            var destructibleObject = other.gameObject.GetComponent<DestructibleObject>();
            destructibleObject.currentHealth -= environmentalDamage;

            HandleDeath();
        }
    }
}