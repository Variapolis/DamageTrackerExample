using System;
using DamageTrackerLib;
using DamageTrackerLib.DamageInfo;
using Rage;
using Rage.Attributes;

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
            Game.DisplayHelp($"{ped.Model.Name} was damaged by a {Enum.GetName(typeof(DamageType), damageInfo.WeaponInfo.Type)}");
    }
    
    
}