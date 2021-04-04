﻿using SibGameJam2021.Core.Managers;

namespace SibGameJam2021.Core.Loot.Boosts
{
    public class BounceBoost : BoostBase
    {
        protected override void ApplyBoost()
        {
            GameManager.Instance.Player.BounceBoost++;
        }
    }
}