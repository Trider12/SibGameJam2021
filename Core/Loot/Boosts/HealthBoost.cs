﻿using SibGameJam2021.Core.Managers;

namespace SibGameJam2021.Core.Loot.Boosts
{
    public class HealthBoost : BoostBase
    {
        public override void ApplyBoost()
        {
            GameManager.Instance.Player.MaxHealth += 10f;
        }
    }
}