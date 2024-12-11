using DamageTrackerLib;
using DamageTrackerLib.DamageInfo;
using LSPD_First_Response.Mod.API;
using Rage;

namespace DamageTrackerLSPDFRExample
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            Functions.OnOnDutyStateChanged += HandleDutyChanged;
            Game.DisplayNotification("DTF LSPDFR Example Loaded.");
        }

        private void HandleDutyChanged(bool onduty)
        {
            if (onduty)
            {
                DamageTrackerService.Start();
                DamageTrackerService.OnPedTookDamage += HandlePedDamage;
                DamageTrackerService.OnPlayerTookDamage += HandlePedDamage;
                DamageTrackerService.OnVehicleTookDamage += HandleVehDamage;
            }
            else
            {
                DamageTrackerService.Stop();
                DamageTrackerService.OnPedTookDamage -= HandlePedDamage;
                DamageTrackerService.OnPlayerTookDamage -= HandlePedDamage;
                DamageTrackerService.OnVehicleTookDamage -= HandleVehDamage;
            }
        }

        private static void HandlePedDamage(Ped victim, Ped attacker, PedDamageInfo damageInfo) =>
            Game.DisplayHelp($"~w~Ped: {victim.Model.Name} (~r~{damageInfo.Damage} ~b~{damageInfo.ArmourDamage} ~w~Dmg ({(victim.IsAlive ? "~g~Alive" : "~r~Dead")}~w~) " +
                             $"\n~w~Health: ~g~{victim.Health}/{victim.MaxHealth} Armor: ~b~{victim.Armor})" +
                             $"\n~w~Attacker: ~r~{attacker?.Model.Name ?? "None"}" +
                             $"\n~w~Weapon: ~y~{damageInfo.WeaponInfo.Hash.ToString()} {damageInfo.WeaponInfo.Type.ToString()} {damageInfo.WeaponInfo.Group.ToString()}" +
                             $"\n~w~Bone: ~r~{damageInfo.BoneInfo.BoneId.ToString()} {damageInfo.BoneInfo.Limb.ToString()} {damageInfo.BoneInfo.BodyRegion.ToString()}");

        private static void HandleVehDamage(Vehicle vehicle, Ped attacker, VehDamageInfo damageInfo) =>
            Game.DisplayHelp($"~w~Vehicle: {vehicle.Model.Name} (~r~{damageInfo.Damage} ~w~Dmg ({(vehicle.IsAlive ? "~g~Alive" : "~r~Dead")}~w~) " +
                             $"\n~w~Health: ~g~{vehicle.Health}/{vehicle.MaxHealth})" +
                             $"\n~w~Attacker: ~r~{attacker?.Model.Name ?? "None"}" +
                             $"\n~w~Weapon: ~y~{damageInfo.WeaponInfo.Hash.ToString()} {damageInfo.WeaponInfo.Type.ToString()} {damageInfo.WeaponInfo.Group.ToString()}" +
                             $"\n~w~Coll Loc: ~r~({damageInfo.LastCollisionPosition.X}, {damageInfo.LastCollisionPosition.Y}, {damageInfo.LastCollisionPosition.Z})");
        
        public override void Finally() => Game.DisplayNotification("DTF LSPDFR Example Unloaded.");
    }
}