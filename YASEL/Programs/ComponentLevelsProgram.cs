using System;
using System.Text;
using System.Collections.Generic;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using VRageMath;
using VRage.Game.ModAPI.Ingame;

namespace TestProgram
{
    using ProgramExtensions;
    using Graph;
    using TextPanelExtensions;
    using InventoryExtensions;

    class ComponentLevelsProgram : MyGridProgram
    {
        IMyTerminalBlock lcdListTargetValues, lcdGraph;
        List<IMyInventory> componentCargo;
        Dictionary<string, double> currentValues = new Dictionary<string, double>();
       
        void Main(string argument)
        {
            if (lcdListTargetValues == null)
            {
                lcdListTargetValues = this.GetBlock("LCDList", true);
                if (lcdListTargetValues == null)
                    throw new Exception("Unable to load LCD with target values");
            }
            if (lcdGraph == null)
            {
                lcdGraph = this.GetBlock("LCDGraph", true);
                if (lcdGraph == null)
                    throw new Exception("Unable to load LCD for graph");
            }
            if (componentCargo == null)
            {
                componentCargo = this.GetBlockGroup("Cargo Components").GetInventories();
                if (componentCargo == null || componentCargo.Count == 0)
                    throw new Exception("Unable to get cargo inventories");
            }
            var targetValues = (lcdListTargetValues as IMyTextPanel).GetValueList();
            var tvEnum = targetValues.GetEnumerator();
            while(tvEnum.MoveNext())
            {
                if (currentValues.ContainsKey(tvEnum.Current.Key))
                    currentValues[tvEnum.Current.Key] = componentCargo.CountItems(tvEnum.Current.Key);
                else
                    currentValues.Add(tvEnum.Current.Key, componentCargo.CountItems(tvEnum.Current.Key));
            }
            string graph = Graph.PrepareBarGraph(targetValues, currentValues, 0.43);
            (lcdGraph as IMyTextPanel).WritePublicText(graph);
        }

    }

}