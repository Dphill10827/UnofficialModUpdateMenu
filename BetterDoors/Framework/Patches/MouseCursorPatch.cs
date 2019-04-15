﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using Microsoft.Xna.Framework;
using StardewValley;

namespace BetterDoors.Framework.Patches
{
    /// <summary>Patches GameLocation.isCollidingPosition to allow for accurate door collision detection.</summary>
    [HarmonyPatch]
    internal class MouseCursorPatch
    {
        /*********
        ** Private methods
        *********/
        /// <summary>Gets the method to patch.</summary>
        /// <returns>The method to patch.</returns>
        [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Method names are defined by Harmony.")]
        private static MethodBase TargetMethod()
        {
            return typeof(Game1).GetMethod(nameof(Game1.drawMouseCursor));
        }

        /// <summary>The method to call before Game1.drawMouseCursor.</summary>
        [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Method names are defined by Harmony.")]
        private static void Prefix()
        {
            Point mousePosition = new Point((Game1.viewport.X + Game1.getOldMouseX()) / 64, (Game1.viewport.Y + Game1.getOldMouseY()) / 64);

            if (BetterDoorsMod.Instance.GetMouseCursor(Game1.currentLocation, mousePosition, out int cursor, out float transparency))
            {
                Game1.mouseCursor = cursor;
                Game1.mouseCursorTransparency = transparency;
            }
        }
    }
}
