using System;
using System.ComponentModel;
using DefaultNamespace.Inventory;

namespace DefaultNamespace
{
    public static class GlobalConstants
    {
        public const string CURRENT_LEVEL = "CurrentLevel";
        public const string lAST_COMPLETED_LEVEL = "LastCompletedLevel";

        public static readonly int BLACK_HOLE = Convert.ToInt32(ItemsType.BlackHole);
        public static readonly int LASER = Convert.ToInt32(ItemsType.Laser);
        public static readonly int ROCKET = Convert.ToInt32(ItemsType.Rocket);

    }
}