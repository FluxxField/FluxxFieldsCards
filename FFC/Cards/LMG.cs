﻿using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;
using FFC.Utilities;

namespace FFC.Cards {
    public class LMG : CustomCard {
        private const float DamageMultiplier = 0.60f;
        private const float ProjectileSpeedMultiplier = 1.30f;
        private const float AttackSpeedMultiplier = 1.15f;
        private const float ReloadSpeedMultiplier = 2.50f;
        private const float MovementSpeedMultiplier = 0.70f;
        private const float RecoilMultiplier = 0.25f;

        protected override string GetTitle() {
            return "LMG";
        }

        protected override string GetDescription() {
            return "Lot more Damage and Ammo, but at a cost";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            UnityEngine.Debug.Log($"[{FFC.AbbrModName}] Setting up {GetTitle()}");

            cardInfo.allowMultiple = false;
            cardInfo.categories = new[] {
                ManageCardCategories.LightGunnerClassUpgradesCategory,
                ManageCardCategories.LMGUpgradeCategory
            };
        }

        public override void OnAddCard(
            Player player,
            Gun gun,
            GunAmmo gunAmmo,
            CharacterData data,
            HealthHandler health,
            Gravity gravity,
            Block block,
            CharacterStatModifiers characterStats
        ) {
            gun.dontAllowAutoFire = false;
            gun.damage *= DamageMultiplier;
            gun.projectileSpeed *= ProjectileSpeedMultiplier;
            gun.attackSpeed *= AttackSpeedMultiplier;
            gun.recoil *= RecoilMultiplier;
            gunAmmo.reloadTimeMultiplier *= ReloadSpeedMultiplier;
            characterStats.movementSpeed *= MovementSpeedMultiplier;
            gunAmmo.maxAmmo += 6;

            characterStats.GetAdditionalData().blacklistedCategories.AddRange(new[] {
                ManageCardCategories.AssaultRifleUpgradeCategory,
                ManageCardCategories.DMRUpgradeCategory
            });
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                ManageCardInfoStats.BuildCardInfoStat("Damage", false, DamageMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Bullet Speed", true, ProjectileSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Max Ammo", true,null, "+6"),
                ManageCardInfoStats.BuildCardInfoStat("Attack Speed", false, AttackSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Reload Speed", false, ReloadSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Movement Speed", false, MovementSpeedMultiplier),
                ManageCardInfoStats.BuildCardInfoStat("Recoil", false, ReloadSpeedMultiplier)
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Rare;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.NatureBrown;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}