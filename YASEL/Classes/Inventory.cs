using System;
using System.Text;
using System.Collections.Generic;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using VRageMath;

namespace Inventory
{
    class Inventory
    {
        static public float CountItems(List<IMyTerminalBlock> invBlocks, string itemType = "", string itemSubtypeName = "")
        {
            float count = 0;
            invBlocks.ForEach(invBlock =>
            {
                if (invBlock is IMyInventoryOwner)
                {
                    for (int i = 0; i < (invBlock as IMyInventoryOwner).InventoryCount; i++)
                    {
                        var items = (invBlock as IMyInventoryOwner).GetInventory(i).GetItems();
                        items.ForEach(item =>
                        {
                            if (item.Content.TypeId.ToString().Contains(itemType) &&
                                item.Content.SubtypeName.Contains(itemSubtypeName))
                                count += (float)item.Amount;
                        });
                    }
                }
            });
            return count;
        }
    }
}