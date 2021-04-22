using ConsoleApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class GlidedRose
    {
        IList<ItemCollaborator> Items;
        public GlidedRose(IList<ItemCollaborator> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach(var item in Items)
            {
                item.Update();
            }
        }
    }
}
