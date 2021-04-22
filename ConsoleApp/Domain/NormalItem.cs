namespace ConsoleApp.Domain
{
    public class NormalItem : ItemCollaborator
    {
        public override void Update()
        {
            UpdateQuality();
            base.UpdateSellIn();
            UpdateQualityForAgedItems();
        }

        protected override void UpdateQuality()
        {
            if (Quality > 0) Quality -= 1;
        }

        protected override void UpdateQualityForAgedItems()
        {
            if(SellIn < 0)
            {
                UpdateQuality();
            }
        }
    }
}
