using System;
using DamageTrackerLib;
using DamageTrackerLib.DamageInfo;
using Rage;
using Rage.Attributes;
using WeaponHash = DamageTrackerLib.DamageInfo.WeaponHash;

[assembly: Plugin("DamageTrackerExample", Description = "A plugin for testing.",
    Author = "Variapolis",
    PrefersSingleInstance = true)]

namespace TestCallouts
{
    // ReSharper disable once UnusedType.Global
    public class EntryPoint
    {
        private static GameFiber GameFiber;

        // ReSharper disable once UnusedMember.Global
        public static void Main()
        {
            Game.DisplayNotification("DamageTrackerExample by Variapolis ~g~Successfully Loaded");
            DamageTrackerService.Start();
            DamageTrackerService.OnPedTookDamage += HandleDamage; // C# Event from DamageTrackerService
            DamageTrackerService.OnPlayerTookDamage += HandleDamage; // C# Event from DamageTrackerService
            GameFiber.Hibernate();
        }

        // ReSharper disable once UnusedMember.Global
        public static void OnUnload(bool Exit)
        {
            DamageTrackerService.Stop();
            Game.DisplayNotification("DamageTrackerExample by Variapolis ~r~ Unloaded");
        }

        // This uses a delegate function from DamageTrackerLib - public delegate void PedTookDamageDelegate(Ped victimPed, Ped attackerPed, PedDamageInfo damageInfo)
        private static void HandleDamage(Ped victim, Ped attacker, PedDamageInfo damageInfo) =>
            Game.DisplayHelp($"~w~Ped: {victim.Model.Name} (~r~{damageInfo.Damage} ~b~{damageInfo.ArmourDamage} ~w~Dmg ({(victim.IsAlive ? "~g~Alive" : "~r~Dead")}~w~) " +
                             $"\n~w~Health: ~g~{victim.Health}/{victim.MaxHealth} Armor: ~b~{victim.Armor})" +
                             $"\n~w~Attacker: ~r~{attacker?.Model.Name ?? "None"}" +
                             $"\n~w~Weapon: ~y~{damageInfo.WeaponInfo.Hash.ToString()} {damageInfo.WeaponInfo.Type.ToString()} {damageInfo.WeaponInfo.Group.ToString()}" +
                             $"\n~w~Bone: ~r~{damageInfo.BoneInfo.BoneId.ToString()} {damageInfo.BoneInfo.Limb.ToString()} {damageInfo.BoneInfo.BodyRegion.ToString()}");
    }
}