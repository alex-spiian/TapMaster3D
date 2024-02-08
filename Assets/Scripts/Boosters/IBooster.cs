using DefaultNamespace.Items;

namespace Boosters
{
    public interface IBooster : IItem
    {
        public int Count { get; }
        
        public void SpendItem();
        public void AddItem();


    }
}