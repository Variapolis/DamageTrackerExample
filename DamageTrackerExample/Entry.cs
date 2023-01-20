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
            DamageTrackerService.OnPedTookDamage += PrintDamage;
            DamageTrackerService.OnPlayerTookDamage += PrintDamage;
            GameFiber.Hibernate();
        }

        // ReSharper disable once UnusedMember.Global
        public static void OnUnload(bool Exit)
        {
            DamageTrackerService.Stop();
            Game.DisplayNotification("DamageTrackerExample by Variapolis ~r~ Unloaded");
        }

        private static void PrintDamage(Ped ped, PedDamageInfo damageInfo) =>
            Game.DisplayHelp($"~g~{ped.Model.Name} {damageInfo.Damage}dmg ({(ped.IsAlive ? "Alive" : "Dead")})" +
                             $"\n~y~{Enum.GetName(typeof(WeaponHash), damageInfo.WeaponInfo.Hash)} {Enum.GetName(typeof(DamageType), damageInfo.WeaponInfo.Type)} {Enum.GetName(typeof(DamageGroup), damageInfo.WeaponInfo.Group)}" +
                             $"\n~r~{Enum.GetName(typeof(BoneId), damageInfo.BoneInfo.BoneId)} {Enum.GetName(typeof(Limb), damageInfo.BoneInfo.Limb)} {Enum.GetName(typeof(BodyRegion), damageInfo.BoneInfo.BodyRegion)}");
    }
    
    
}