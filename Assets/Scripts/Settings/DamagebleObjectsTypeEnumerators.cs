namespace Chebureck.Settings
{
    public enum BasicDamagebleObjectsTypeEnumerators
    {
        Unknown,

        Enemy,
        Rock,
        Tree,
        Ore,
        Furniture,
        PeacefullAnimal,
    }

    public enum ExtendedDamagebleObjectsTypeEnumerators
    {
        Unknown,

        #region Enemy
        SlimeWarrior,
        SlimeMage,
        SkeletonWarrior,
        SkeletonWizard,
        Shark,
        #endregion

        #region Rock
        Rock,
        #endregion

        #region Tree
        Tree,
        #endregion

        #region Ore
        Copper,
        Iron,
        Gold,
        Steel,
        Crystal,
        Mithril,
        DragonSteel,
        #endregion

        #region Furniture
        temp,
        #endregion

        #region PeacefullAnimal
        Sheep,
        Cow,
        Boar,
        Bird,
        Fish,
        #endregion
    }

    // FOR FUTURE
    //public enum BossDamagebleObjectsTypeEnumerators
    //{
    //    Unknown,

    //    SlimeKing,
    //    SkeletonKing,
    //    ZombieKing,
    //    SharkKing
    //}
}