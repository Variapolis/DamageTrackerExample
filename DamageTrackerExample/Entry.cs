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

        private static void PrintDamage(Ped ped, Ped attackerPed, PedDamageInfo damageInfo) =>
            Game.DisplayHelp($"~w~{ped.Model.Name} (~r~{damageInfo.Damage} Dmg~w~) ({(ped.IsAlive ? "~g~Alive" : "~r~Dead")}~w~)" +
                             $"\n~r~{attackerPed?.Model.Name ?? "None"}" +
                             $"\n~y~{damageInfo.WeaponInfo.Hash.ToString()} {damageInfo.WeaponInfo.Type.ToString()} {damageInfo.WeaponInfo.Group.ToString()}" +
                             $"\n~r~{damageInfo.BoneInfo.BoneId.ToString()} {damageInfo.BoneInfo.Limb.ToString()} {damageInfo.BoneInfo.BodyRegion.ToString()}");
    }
}