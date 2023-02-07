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
                DamageTrackerService.OnPedTookDamage += HandleDamage;
                DamageTrackerService.OnPlayerTookDamage += HandleDamage;;  
            }
            else
            {
                DamageTrackerService.Stop();
                DamageTrackerService.OnPedTookDamage -= HandleDamage;;
                DamageTrackerService.OnPlayerTookDamage -= HandleDamage;;  
            }
        }

        private static void HandleDamage(Ped victim, Ped attacker, PedDamageInfo damageInfo)
        {
            Game.DisplayHelp(
                $"~w~{victim.Model.Name} (~r~{damageInfo.Damage} Dmg~w~) ({(victim.IsAlive ? "~g~Alive" : "~r~Dead")}~w~)" +
                $"\n~r~{attacker?.Model.Name ?? "None"}" +                $"\n~y~{damageInfo.WeaponInfo.Hash.ToString()} {damageInfo.WeaponInfo.Type.ToString()} {damageInfo.WeaponInfo.Group.ToString()}" +
                $"\n~r~{damageInfo.BoneInfo.BoneId.ToString()} {damageInfo.BoneInfo.Limb.ToString()} {damageInfo.BoneInfo.BodyRegion.ToString()}");
        }

        public override void Finally() => Game.DisplayNotification("DTF LSPDFR Example Unloaded.");
    }
}