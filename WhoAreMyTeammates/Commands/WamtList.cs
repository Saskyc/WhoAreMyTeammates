using PlayerRoles;

namespace WhoAreMyTeammates.Commands
{
    using CommandSystem;
    using Exiled.API.Features;
    using RemoteAdmin;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class WamtList : ICommand
    {
        /// <inheritdoc/>
        public string Command { get; } = "WamtList";

        /// <inheritdoc/>
        public string[] Aliases { get; } = { "WL", "SCPList", "ListSCPs" };

        /// <inheritdoc/>
        public string Description { get; } = "Lists SCPs in the current round";

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Player.Get(sender);
            
            if (!player.IsScp)
            {
                response = "You must be an SCP to run this command!";
                return false;
            }

            var scps = Player.Get(Team.SCPs);
            var scpNames = new List<string>();
            foreach (var scp in scps)
            {
                scpNames.Add(scp.Role.Name);
                if (scp != scps.Last())
                    scpNames.Append(", ");
                else
                    scpNames.Append(".");
            }

            var NameString = string.Join(",", scpNames);
            Player.Get(sender).Broadcast(10, $"<color=red>The Following SCPs are ingame: {NameString}</color>");
            response = $"The Following SCPs are ingame: {NameString}";
            return true;
        }
    }
}