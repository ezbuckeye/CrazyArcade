﻿using CrazyArcade.Final;
using CrazyArcade.UI;
using CrazyArcade.UI.GUI_Compositions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyArcade.PlayerStateMachine.PlayerItemInteractions
{
    public class ItemContainer
    {
        /*
         * This will contain the stats here, obtained by picking up items on the ground
         */
        public Dictionary<string, ItemModifier> ItemBox = new();
        private List<ItemModifier> guiList = new();
        private static readonly int defaultBlastLength = 1;
        private static readonly int defaultBombMaximum = 1;
        private static readonly int defaultSpeed = 3;
        public int BombModifier;
        public int BlastModifier;
        public int SpeedModifier;
        public bool CanKick;
        private int itemCount = 0;
        private bool isPirate;
        private Vector2 anchorPoint = new(50, 50);
        public ItemContainer(bool isPirate)
        {
            ResetStats();
            this.isPirate = isPirate;
        }
        public void AddItem(ItemModifier item)
        {
            if (ItemBox.ContainsKey(item.Name))
            {
                ItemModifier currentItem = ItemBox[item.Name];

				if (currentItem.CurrentCount < currentItem.MaxCount)
                {
					currentItem.CurrentCount++;
                    UpdateGuiItemCount(currentItem);
                }
            }
            else
            {
                ItemBox.Add(item.Name, item);
                guiList.Add(item);
                itemCount++;
                item.ItemContainer = this;
                GenerateGuiElement(item, itemCount);
            }
            RecalculateStats();
        }
        public void RemoveOneItem(ItemModifier item)
        {
            if (ItemBox.ContainsKey(item.Name))
            {
                item.CurrentCount--;
                if (item.CurrentCount <= 0)
                {
                    guiList.Remove(ItemBox[item.Name]);
                    ItemBox.Remove(item.Name);
                    UI_Singleton.RemoveComposition(item.Name);
                    itemCount--;
                } 
                else
                {
                    UpdateGuiItemCount(item);
                }
            }
            RecalculateStats();
        }
        public void RemoveEntireItem(ItemModifier item)
        {
            ItemBox.Remove(item.Name);
            RecalculateStats();
        }
        private void ResetStats()
        {
            BombModifier = defaultBombMaximum;
            BlastModifier = defaultBlastLength;
            SpeedModifier = defaultSpeed;
            CanKick = false;
        }
        private void RecalculateStats()
        {
            ResetStats();
            foreach (var mod in ItemBox)
            {
                mod.Value.ModifyStat();
            }
        }
        private void GenerateGuiElement(ItemModifier item, int count)
        {
            if (isPirate) return;
            UI_Singleton.AddPreDesignedComposite(new ItemCountComposition(item.Name, item.ItemRep));
            UpdateGuiItemCount(item);
            UI_Singleton.MoveCompositePosition(item.Name, anchorPoint + new Vector2((count % 2) * 50, ((count-1) / 2) * 50));
        }
        private void UpdateGuiItemCount(ItemModifier item)
        {
            if (isPirate) return;
            UI_Singleton.ChangeComponentText(item.Name, "itemCount", "X" + item.CurrentCount.ToString());
        }
    }
}
