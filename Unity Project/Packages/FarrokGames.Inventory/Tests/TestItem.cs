using UnityEngine;

namespace FarrokhGames.Inventory
{
    public class TestItem : IInventoryItem
    {
        bool _canDrop = true;
        private readonly InventoryShape _shape;
        
        public TestItem(Sprite sprite, InventoryShape shape, bool canDrop)
        {
            this.sprite = sprite;
            _shape = shape;
            _canDrop = canDrop;
            position = Vector2Int.zero;
        }

        public Sprite sprite { get; }
        public int width => _shape.width;
        public int height => _shape.height;
        public Vector2Int position { get; set; }
        public bool IsPartOfShape(Vector2Int localPosition) => _shape.IsPartOfShape(localPosition);
        public bool canDrop => _canDrop;
    }
}