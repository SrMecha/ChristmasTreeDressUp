using ChristmasTreeDressUp.Interfaces.Types;

namespace ChristmasTreeDressUp.Utility
{
    public static class EntityManager
    {
        public static List<IEntity> ToRemove { get; private set; } = new();
        public static List<IEntity> ToAddOnTop { get; private set; } = new();
        public static List<IEntity> ToAddOnBottom { get; private set; } = new();
        public static List<IEntity> Entities { get; private set; } = new();

        public static bool HaveTag(string tag)
        {
            return Entities.Any(x => x.Tag == tag);
        }

        public static void AddEntity(IEntity entity, bool onTop = true)
        {
            if (onTop)
                ToAddOnTop.Add(entity);
            else
                ToAddOnBottom.Add(entity);
        }

        public static void ClerEntities()
        {
            for (int i = Entities.Count - 1; i >= 0; i--)
            {
                Entities[i].Dispose();
                Entities.RemoveAt(i);
            }
        }

        public static void RemoveWithTag(string tag)
        {
            for (int i = Entities.Count - 1; i >= 0; i--)
            {
                if (Entities[i].Tag == tag)
                {
                    ToRemove.Add(Entities[i]);
                }
            }
        }

        public static void Remove(IEntity entity)
        {
            ToRemove.Add(entity);
        }

        public static void Update()
        {
            foreach (var entity in ToAddOnTop)
                Entities.Add(entity);
            ToAddOnTop.Clear();
            foreach (var entity in ToAddOnBottom)
                Entities.Insert(0, entity);
            ToAddOnBottom.Clear();
            foreach (var entity in ToRemove)
            {
                entity.Dispose();
                Entities.Remove(entity);
            }
            ToRemove.Clear();
        }
    }
}
