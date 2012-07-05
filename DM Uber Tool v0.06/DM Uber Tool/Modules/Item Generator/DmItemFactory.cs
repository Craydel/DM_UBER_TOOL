using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM_Uber_Tool
{
  /**
   * Remeber!!
   *  When adding new items to the list definitions, be sure 
   *  to add the description to the list under 'CreateLists()'
   *  
   * Order is not important, but keeping things organized wouldn't hurt
   */

  #region Item Type and Enchantment Enums

  // base item types
  public enum DmItemType
  {
    Money=0,
    Container,  // bag, pouch, pack, etc
    Food,       // food ration, beer, ale, beef stick, etc
    Equipment,  // rope, bedroll, tarp, torch, general adventuring crap
    Weapon,     // simple, martial, exotic / melee or ranged
    Clothing,   // A cure for nudity!
    Armor,      // light/cloth, leather, chain, splint, steel, etc.
  };

  // quality description
  public enum DmItemQuality
  {
    Poor=0,   // gray
    Average,    // white
    Good,       // blue
    Masterwork, // epic!!1!
  };



  // Weapon Enchantments
  public enum DmItemEnchantmentWeapon
  {
    None=0,
    Strength,
    Constitution,
    Dexterity,
    Intellect,
    Wisdom,
    Charisma,
    AttackBonus, // + to hit
    DamageBonus, // if hit landed, + damage
  };

  // Armor Enchantments
  public enum DmItemEnchantmentArmor
  {
    None=0,
    Strength,
    Constitution,
    Dexterity,
    Intellect,
    Wisdom,
    Charisma,
    AttackBonus, // + to hit
    DamageBonus, // if hit landed, + damage
  };

  // Clothing Enchantments
  public enum DmItemEnchantmentClothing
  {
    None=0,
    //Strength,
    Constitution,
    Dexterity,
    Intellect,
    Wisdom,
    Charisma,
    //AttackBonus, // + to hit
    //DamageBonus, // if hit landed, + damage
  };

  // Consumable Enchantments (spells?)
  public enum DmItemEnchantmentConsumable
  {
    None=0,
    CureMinor,
    CureMajor,
    Levitate,
    Darkness,
    Light,
    Bless,
    Sleep,
    DetectMagic,
  };



  // Money (coin types - not quantity or value)
  public enum DmItemTypeMoney
  {
    Copper=0,
    Silver,
    Gold,
    Platinum,
  };

  // Containers (things that can hold other things, contents not included)
  public enum DmItemTypeContainer
  {
    BeltPouch=0,
    SmallSack,
    LargeSack,
    Backpack,
    ScrollCase,
    Barrel,
    Chest,
    BucketWooden,
    BasketWicker,
  };

  // Consumables (foods, drinks and their containers)
  public enum DmItemTypeFood
  {
    TrailRations=0,
    AleSkein,
    WineBottle,
    WhiskeyFlask,
    Bread,
    Cheese,
    DriedMeat,
    Bacon,
  };

  // Equipment (general store inventory)
  public enum DmItemTypeEquipment
  {
    Bedroll=0,
    Bell,
    BlanketWinter,
    BlocAndTackle,
    Caltrops,
    Candle,
    Canvas,
    Chain10ft,
    Chalk,
    Crowbar,
    Firewood,
    Fishhook,
    FishingNet25sqft,
    Flask,
    FlintAndSteel,
    GrapplingHook,
    Hammer,
    Ink,
    Inkpen,
    JugClay,
    Ladder10ft,
    LampCommon,
    LanternBullseye,
    LanternHooded,
    LockSimple,
    LockAverage,
    LockGood,
    LockAmazing,
    Manacles,
    Mirror,
    MugClay,
    Oil,
    Paper,
    Parchment,
    PickMiners,
    PitcherClay,
    Piton,
    Pole10ft,
    PotIron,
    RamPortable,
    RopeHempen,
    RopeSilk,
    SealingWax,
    SewingNeedle,
    SignalWhistle,
    SignetRing,
    Sledge,
    Soap,
    Shovel,
    Spyglass,
    Tent,
    Torch,
    Vial,
    Waterskin,
    Whetstone,
  };

  // Clothing (robes, tunics, trousers and the like.  Not armor, per se.)
  public enum DmItemTypeClothing
  {
    ArtisanOutfit=0,
    CastersRobesSimple,
    CastersRobesFine,
    CastersRobesElaborate,
    ClericVestments,
    ColdWeatherOutfit,
    CourtiersOutfit,
    EntertainersOutfit,
    ExplorersOutfit,
    MonksOutfit,
    NoblesOutfit,
    PeasantsOutfit,
    RoyalOutfit,
    ScholarsOutfit,
    TravelersOutfit,
  };



  // Weapon types
  public enum DmItemTypeWeaponType
  {
    Melee,
    Ranged,
    Ammo,
  };

  // Weapon classes
  public enum DmItemTypeWeaponClass
  {
    Simple,
    Martial,
    Exotic,
  };

  // Melee Weapons
  public enum DmItemTypeWeaponMeleeSimple
  {
    BlackKnife,
    BrassKnuckles,
    CarvetDagger,
    Club,
    Dagger,
    DaggerPunching,
    Dirk,
    FightingClaw,
    FixedRazor,
    Garrote,
    GoldenMelonHammer,
    Gut_blade,
    IronBrush,
    IronFlute,
    IronPipe,
    LadiesChain,
    LashingStaff,
    LeechingDagger,
    LightMace,
    Mace_Chained,
    MaceHeavy,
    Machete,
    MastersHand,
    Morningstar,
    PushKnife,
    Quarterstaff,
    Rake,
    Shortstaff,
    Shortspear,
    Sickle,
    SmallClub,
    TaMoHiddenDaggers,
  };
  public enum DmItemTypeWeaponMeleeMartial
  {
    BasketHiltedBacksword,
    BattleStaff,
    Battleaxe,
    Bush_knife,
    ChokePike,
    Cinqueda,
    Claw,
    Claymore,
    Dadao,
    DartMace,
    DragonWhiskersFork,
    ExecutionersSword,
    Falchion,
    FangedBlade,
    FencingSaber,
    GhostHeadBroadsword,
    Glaive,
    GoldCoinSpade,
    Greataxe,
    Greatclub,
    Greatsword,
    Guisarme,
    Halberd,
    Handaxe,
    HeavyFlail,
    HeavyLance,
    HeavyPick,
    HookedSpear,
    LightFlail,
    LightLance,
    LightPick,
    Longspear,
    Longsword,
    LucernHammer,
    Maul,
    MonksCudgel,
    NightlingCleaver,
    Pike,
    Ranseur,
    Rapier,
    Sap,
    Scimitar,
    Scythe,
    ShortSword,
    SmallFlail,
    SmallLongsword,
    SmallRapier,
    Straight_sword,
    ThreePointDoubleBladedSword,
    TigerFork,
    Trident,
    WarFork,
    Warhammer,
    Widowmaker,
    WolfSpear,
    WolfTeethClub,
    WolfTeethSpikedTrident,
  };
  public enum DmItemTypeWeaponMeleeExotic
  {
    Axe_Hammer,
    BalledChain,
    BaneSpear,
    BastardSword,
    BattleGauntlet,
    Bloodaxe,
    BucklerBlade,
    ButterflySword,
    CatGloves,
    Chain_and_Dagger,
    ChainedAxe,
    CicadaWingSword,
    ClawBracer,
    CombatHook,
    CrushingAxe,
    DaggerScholarsBrush,
    DireFlail,
    DoubleBladedSword,
    DoubleChainedAxe,
    DoubleFlyingSword,
    DoubleHeaded,
    DoubleHeadedSword,
    DoubleMace,
    DoubleScimitar,
    DragonHeadStick,
    DuckBlade,
    Duom,
    DwarvenUrgrosh,
    DwarvenWaraxe,
    EmeiPincer,
    FlyingWeight,
    FourSectionSickle,
    Fullblade,
    GnomeBattlepick,
    GnomeHooked_Hammer,
    Gyrspike,
    HeavenandSunandMoonSword,
    HeavenLotusPhoenixSword,
    Hokk,
    Hookflail,
    HorseHackingSword,
    HorseHalberd,
    JumpSpear,
    Kama,
    KamaHalfling,
    Kopesh,
    Kukri,
    Manti,
    MercurialGreatsword,
    MercurialLongsword,
    MeteorHammer,
    MonksSpade,
    MotherandSonHammer,
    NineTeethHammer,
    Nunchaku,
    NunchakuHalfling,
    OrcDoubleAxe,
    PantherClaw,
    Pen,
    PoisonedHairpin,
    PoleSword,
    Ribbon,
    RibbonSword,
    RingBlade,
    RoosterBlade,
    Sai,
    Sapara,
    ScarfChain,
    Siangham,
    SianghamHalfling,
    SkyLance,
    SnakeRing,
    SpikedChain,
    StumpKnife,
    SwordGauntlet,
    Three_SectionStaff,
    TigerClaws,
    TigerHook,
    Tonfa,
    Trip_bag,
    TripleDagger,
    Two_BladedSword,
    UnicornHornSword,
    WarCleaver,
    WarFan,
    WarMaul,
    WaterPartingShield,
    Whip,
    Whip_Dagger,
    WindandFireWheel,
    WolfTeethHammer,
  };

  // Ranged Weapons
  public enum DmItemTypeWeaponRangedSimple
  {
    FlaskLauncher,
    Halfspear,
    HeavyCrossbow,
    Javelin,
    LightCrossbow,
    SandSling,
    Sling,
    ThrowingKnives,
  };
  public enum DmItemTypeWeaponRangedMartial
  {
    BarbedArrows,
    CompositeShortbow,
    LightHammer,
    Longbow,
    Shortbow,
    ThrowingAxe,
  };
  public enum DmItemTypeWeaponRangedExotic
  {
    Blowgun,
    Chakram,
    DualCrossbow,
    DuelingCloak,
    ElvenDoubleBow,
    Flutegun,
    Fukimi_Bari,
    GnomeCalculus,
    GreatCrossbow,
    HalflingSkiprock,
    HandCrossbow,
    Harpoon,
    Net,
    OrcShotput,
    RazorDiskLauncher,
    RepeatingCrossbow,
    Shuriken,
    Sling_stick,
    SmallNet,
    SpinningJavalin,
    ThrowingIron,
    Two_BallBolas,
  };

  // Ammo
  public enum DmItemTypeWeaponAmmoSimple
  {
    ArrowPiercing,
    Dart,
    LightCrossbowBolts_10,
    SlingBullet_10,
    SpikedBullets,
  };
  public enum DmItemTypeWeaponAmmoMartial
  {
    AlchemistsArrow,
    Arrow_20,
    BluntArrow,
    FlightArrow,
    SignalArrow,
    ThunderingArrow,
    TumblingBolt,
  };
  public enum DmItemTypeWeaponAmmoExotic
  {
    BlowgunDarts,
    HandCrossbowBolt_10,
    RazorDisk,
  };



  // Armor types
  public enum DmItemTypeArmorType
  {
    Light=0,
    Medium,
    Heavy,
    Shield,
    Extra,
  };

  // armor pieces
  public enum DmItemTypeArmorPiece
  {
    Chest,      // main armor piece - plates, tunics, robes
    Head,       // helms. caps, cowls
    Hand,       // gloves, gauntlets, handguards
    Arm,        // bracers, banles
    Shoulder,   // spaulders, clasps
    Leg,        // leggings, pants,
    Foot,       // boots, sandals
    Shin,       // shinguards (in addition to leggings?)
    Cloak,      // Decorative or functional
  };
   
  // armors
  public enum DmItemTypeArmorLight
  {
    Padded=0,
    Leather,
    StuddedLeather,
    ChainShirt,
  };
  public enum DmItemTypeArmorMedium
  {
    Hide = 0,
    ScaleMail,
    ChainMail,
    BreastPlate,
  };
  public enum DmItemTypeArmorHeavy
  {
    SplintMail=0,
    BandedMail,
    HalfPlate,
    FullPlate,
  };
  public enum DmItemTypeArmorShield
  {
    Buckler=0,
    LightWooden,
    LightSteel,
    HeavyWooden,
    HeavySteel,
    Tower,
  };
  public enum DmItemTypeArmorExtra
  {
    ArmorSpikes=0,
    LockedGauntlet,
    ShieldSpikes,
  };

  #endregion


  /// <summary>
  /// DmItemFactory - your one stop shop to generate any of that crap listed above.
  /// </summary>
  public static class DmItemFactory
  {
    // the everpresent Random Number Generator
    private static Random rand = new Random();

    // flag to indicate whether the string description lists have been generated.
    private static bool   listsCreated = false;

    // description arrays - each enum above has a list of user-friendly descriptions that go with each entry.
    // populated by a one-time call to CreateLists()
    #region Item Description List Declarations

    private static string[] descItemEnchantmentArmor        = new string[Enum.GetValues( typeof( DmItemEnchantmentArmor       ) ).Length];
    private static string[] descItemEnchantmentClothing     = new string[Enum.GetValues( typeof( DmItemEnchantmentClothing    ) ).Length];
    private static string[] descItemEnchantmentConsumable   = new string[Enum.GetValues( typeof( DmItemEnchantmentConsumable  ) ).Length];
    private static string[] descItemEnchantmentWeapon       = new string[Enum.GetValues( typeof( DmItemEnchantmentWeapon      ) ).Length];

    private static string[] descItemType                    = new string[Enum.GetValues( typeof( DmItemType                   ) ).Length];

    private static string[] descItemTypeMoney               = new string[Enum.GetValues( typeof( DmItemTypeMoney              ) ).Length];
    private static string[] descItemTypeContainer           = new string[Enum.GetValues( typeof( DmItemTypeContainer          ) ).Length];
    private static string[] descItemTypeFood                = new string[Enum.GetValues( typeof( DmItemTypeFood               ) ).Length];
    private static string[] descItemTypeEquipment           = new string[Enum.GetValues( typeof( DmItemTypeEquipment          ) ).Length];

    private static string[] descItemTypeWeaponType          = new string[Enum.GetValues( typeof( DmItemTypeWeaponType         ) ).Length];
    private static string[] descItemTypeWeaponClass         = new string[Enum.GetValues( typeof( DmItemTypeWeaponClass        ) ).Length];

    private static string[] descItemTypeWeaponMeleeSimple   = new string[Enum.GetValues( typeof( DmItemTypeWeaponMeleeSimple  ) ).Length];
    private static string[] descItemTypeWeaponMeleeMartial  = new string[Enum.GetValues( typeof( DmItemTypeWeaponMeleeMartial ) ).Length];
    private static string[] descItemTypeWeaponMeleeExotic   = new string[Enum.GetValues( typeof( DmItemTypeWeaponMeleeExotic  ) ).Length];

    private static string[] descItemTypeWeaponRangedSimple  = new string[Enum.GetValues( typeof( DmItemTypeWeaponRangedSimple ) ).Length];
    private static string[] descItemTypeWeaponRangedMartial = new string[Enum.GetValues( typeof( DmItemTypeWeaponRangedMartial) ).Length];
    private static string[] descItemTypeWeaponRangedExotic  = new string[Enum.GetValues( typeof( DmItemTypeWeaponRangedExotic ) ).Length];

    private static string[] descItemTypeWeaponAmmoSimple    = new string[Enum.GetValues( typeof( DmItemTypeWeaponAmmoSimple   ) ).Length];
    private static string[] descItemTypeWeaponAmmoMartial   = new string[Enum.GetValues( typeof( DmItemTypeWeaponAmmoMartial  ) ).Length];
    private static string[] descItemTypeWeaponAmmoExotic    = new string[Enum.GetValues( typeof( DmItemTypeWeaponAmmoExotic   ) ).Length];

    private static string[] descItemTypeClothing            = new string[Enum.GetValues( typeof( DmItemTypeClothing           ) ).Length];

    private static string[] descItemTypeArmorType           = new string[Enum.GetValues( typeof( DmItemTypeArmorType          ) ).Length];

    private static string[] descItemTypeArmorLight          = new string[Enum.GetValues( typeof( DmItemTypeArmorLight         ) ).Length];
    private static string[] descItemTypeArmorMedium         = new string[Enum.GetValues( typeof( DmItemTypeArmorMedium        ) ).Length];
    private static string[] descItemTypeArmorHeavy          = new string[Enum.GetValues( typeof( DmItemTypeArmorHeavy         ) ).Length];
    private static string[] descItemTypeArmorShield         = new string[Enum.GetValues( typeof( DmItemTypeArmorShield        ) ).Length];
    private static string[] descItemTypeArmorExtra          = new string[Enum.GetValues( typeof( DmItemTypeArmorExtra         ) ).Length];

    private static string[] descItemQuality                 = new string[Enum.GetValues( typeof( DmItemQuality                ) ).Length];

    #endregion


    /// <summary>
    /// Populates the description list arrays for every possible item (or, at least I hope that's what it does)
    /// When a new item is defined in the enums above, the description NEEDS to be added to this definition list 
    /// in the appropriate spot.
    /// </summary>
    private static void CreateLists()
    {
      // Weapon enchants
      descItemEnchantmentWeapon[(int)DmItemEnchantmentWeapon.None]                                 = "None";
      descItemEnchantmentWeapon[(int)DmItemEnchantmentWeapon.Strength]                             = "Strength";
      descItemEnchantmentWeapon[(int)DmItemEnchantmentWeapon.Constitution]                         = "Constitution";
      descItemEnchantmentWeapon[(int)DmItemEnchantmentWeapon.Dexterity]                            = "Dexterity";
      descItemEnchantmentWeapon[(int)DmItemEnchantmentWeapon.Intellect]                            = "Intellect";
      descItemEnchantmentWeapon[(int)DmItemEnchantmentWeapon.Wisdom]                               = "Wisdom";
      descItemEnchantmentWeapon[(int)DmItemEnchantmentWeapon.Charisma]                             = "Charisma";
      descItemEnchantmentWeapon[(int)DmItemEnchantmentWeapon.AttackBonus]                          = "Attack Bonus";
      descItemEnchantmentWeapon[(int)DmItemEnchantmentWeapon.DamageBonus]                          = "Damage Bonus";

      // Armor enchants
      descItemEnchantmentArmor[(int)DmItemEnchantmentArmor.None]                                   = "None";
      descItemEnchantmentArmor[(int)DmItemEnchantmentArmor.Strength]                               = "Strength";
      descItemEnchantmentArmor[(int)DmItemEnchantmentArmor.Constitution]                           = "Constitution";
      descItemEnchantmentArmor[(int)DmItemEnchantmentArmor.Dexterity]                              = "Dexterity";
      descItemEnchantmentArmor[(int)DmItemEnchantmentArmor.Intellect]                              = "Intellect";
      descItemEnchantmentArmor[(int)DmItemEnchantmentArmor.Wisdom]                                 = "Wisdom";
      descItemEnchantmentArmor[(int)DmItemEnchantmentArmor.Charisma]                               = "Charisma";
      descItemEnchantmentArmor[(int)DmItemEnchantmentArmor.AttackBonus]                            = "Attack Bonus";
      descItemEnchantmentArmor[(int)DmItemEnchantmentArmor.DamageBonus]                            = "Damage Bonus";

      // Clothing enchants
      descItemEnchantmentClothing[(int)DmItemEnchantmentClothing.None]                             = "None";
      descItemEnchantmentClothing[(int)DmItemEnchantmentClothing.Constitution]                     = "Constitution";
      descItemEnchantmentClothing[(int)DmItemEnchantmentClothing.Dexterity]                        = "Dexterity";
      descItemEnchantmentClothing[(int)DmItemEnchantmentClothing.Intellect]                        = "Intellect";
      descItemEnchantmentClothing[(int)DmItemEnchantmentClothing.Wisdom]                           = "Wisdom";
      descItemEnchantmentClothing[(int)DmItemEnchantmentClothing.Charisma]                         = "Charisma";

      // Consumable enchants (spells, scrolls, wands, and the like)
      descItemEnchantmentConsumable[(int)DmItemEnchantmentConsumable.None]                         = "None";
      descItemEnchantmentConsumable[(int)DmItemEnchantmentConsumable.CureMinor]                    = "Cure Minor";
      descItemEnchantmentConsumable[(int)DmItemEnchantmentConsumable.CureMajor]                    = "Cure Major";
      descItemEnchantmentConsumable[(int)DmItemEnchantmentConsumable.Levitate]                     = "Levitate";
      descItemEnchantmentConsumable[(int)DmItemEnchantmentConsumable.Darkness]                     = "Darkness";
      descItemEnchantmentConsumable[(int)DmItemEnchantmentConsumable.Light]                        = "Light";
      descItemEnchantmentConsumable[(int)DmItemEnchantmentConsumable.Bless]                        = "Bless";
      descItemEnchantmentConsumable[(int)DmItemEnchantmentConsumable.Sleep]                        = "Sleep";
      descItemEnchantmentConsumable[(int)DmItemEnchantmentConsumable.DetectMagic]                  = "Detect Magic";

      // Item Quality Description
      descItemQuality[(int)DmItemQuality.Poor]                                                     = "Poor";
      descItemQuality[(int)DmItemQuality.Average]                                                  = "Average";
      descItemQuality[(int)DmItemQuality.Good]                                                     = "Good";
      descItemQuality[(int)DmItemQuality.Masterwork]                                               = "Masterwork";

      // Basic item types
      descItemType[(int)DmItemType.Money]                                                          = "Money";
      descItemType[(int)DmItemType.Container]                                                      = "Container";
      descItemType[(int)DmItemType.Food]                                                           = "Food";
      descItemType[(int)DmItemType.Equipment]                                                      = "Equipment";
      descItemType[(int)DmItemType.Weapon]                                                         = "Weapon";
      descItemType[(int)DmItemType.Clothing]                                                       = "Clothing";
      descItemType[(int)DmItemType.Armor]                                                          = "Armor";

      // Money / Coin types
      descItemTypeMoney[(int)DmItemTypeMoney.Copper]                                               = "Copper";
      descItemTypeMoney[(int)DmItemTypeMoney.Silver]                                               = "Silver";
      descItemTypeMoney[(int)DmItemTypeMoney.Gold]                                                 = "Gold";
      descItemTypeMoney[(int)DmItemTypeMoney.Platinum]                                             = "Platinum";

      // Container types
      descItemTypeContainer[(int)DmItemTypeContainer.BeltPouch]                                    = "Belt Pouch";
      descItemTypeContainer[(int)DmItemTypeContainer.SmallSack]                                    = "Small Sack";
      descItemTypeContainer[(int)DmItemTypeContainer.LargeSack]                                    = "Large Sack";
      descItemTypeContainer[(int)DmItemTypeContainer.Backpack]                                     = "Backpack";
      descItemTypeContainer[(int)DmItemTypeContainer.ScrollCase]                                   = "Scroll Case";
      descItemTypeContainer[(int)DmItemTypeContainer.Barrel]                                       = "Barrel";
      descItemTypeContainer[(int)DmItemTypeContainer.Chest]                                        = "Chest";
      descItemTypeContainer[(int)DmItemTypeContainer.BucketWooden]                                 = "Bucket Wooden";
      descItemTypeContainer[(int)DmItemTypeContainer.BasketWicker]                                 = "Basket Wicker";

      // Food types
      descItemTypeFood[(int)DmItemTypeFood.TrailRations]                                           = "Trail Rations";
      descItemTypeFood[(int)DmItemTypeFood.AleSkein]                                               = "Ale Skein";
      descItemTypeFood[(int)DmItemTypeFood.WineBottle]                                             = "Wine Bottle";
      descItemTypeFood[(int)DmItemTypeFood.WhiskeyFlask]                                           = "Whiskey Flask";
      descItemTypeFood[(int)DmItemTypeFood.Bread]                                                  = "Bread";
      descItemTypeFood[(int)DmItemTypeFood.Cheese]                                                 = "Cheese";
      descItemTypeFood[(int)DmItemTypeFood.DriedMeat]                                              = "Dried Meat";
      descItemTypeFood[(int)DmItemTypeFood.Bacon]                                                  = "Bacon";

      // General Equipment
      descItemTypeEquipment[(int)DmItemTypeEquipment.Bedroll]                                      = "Bedroll";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Bell]                                         = "Bell";
      descItemTypeEquipment[(int)DmItemTypeEquipment.BlanketWinter]                                = "Winter Blanket";
      descItemTypeEquipment[(int)DmItemTypeEquipment.BlocAndTackle]                                = "Bloc & Tackle";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Caltrops]                                     = "Caltrops";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Candle]                                       = "Candle";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Canvas]                                       = "Canvas";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Chain10ft]                                    = "Chain (10ft)";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Chalk]                                        = "Chalk";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Crowbar]                                      = "Crowbar";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Firewood]                                     = "Firewood";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Fishhook]                                     = "Fishhook";
      descItemTypeEquipment[(int)DmItemTypeEquipment.FishingNet25sqft]                             = "Fishing Net (25 sqft)";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Flask]                                        = "Flask";
      descItemTypeEquipment[(int)DmItemTypeEquipment.FlintAndSteel]                                = "Flint & Steel";
      descItemTypeEquipment[(int)DmItemTypeEquipment.GrapplingHook]                                = "Grappling Hook";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Hammer]                                       = "Hammer";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Ink]                                          = "Ink";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Inkpen]                                       = "Inkpen";
      descItemTypeEquipment[(int)DmItemTypeEquipment.JugClay]                                      = "Clay Jug";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Ladder10ft]                                   = "Ladder (10ft)";
      descItemTypeEquipment[(int)DmItemTypeEquipment.LampCommon]                                   = "Common Lamp";
      descItemTypeEquipment[(int)DmItemTypeEquipment.LanternBullseye]                              = "Bullseye Lantern";
      descItemTypeEquipment[(int)DmItemTypeEquipment.LanternHooded]                                = "Hooded Lantern";
      descItemTypeEquipment[(int)DmItemTypeEquipment.LockSimple]                                   = "Simple Lock";
      descItemTypeEquipment[(int)DmItemTypeEquipment.LockAverage]                                  = "Average Lock";
      descItemTypeEquipment[(int)DmItemTypeEquipment.LockGood]                                     = "Good Lock";
      descItemTypeEquipment[(int)DmItemTypeEquipment.LockAmazing]                                  = "Amazing Lock";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Manacles]                                     = "Manacles";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Mirror]                                       = "Mirror";
      descItemTypeEquipment[(int)DmItemTypeEquipment.MugClay]                                      = "Clay Mug";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Oil]                                          = "Oil";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Paper]                                        = "Paper";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Parchment]                                    = "Parchment";
      descItemTypeEquipment[(int)DmItemTypeEquipment.PickMiners]                                   = "PickMiners";
      descItemTypeEquipment[(int)DmItemTypeEquipment.PitcherClay]                                  = "Clay Pitcher";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Piton]                                        = "Piton";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Pole10ft]                                     = "Pole (10ft)";
      descItemTypeEquipment[(int)DmItemTypeEquipment.PotIron]                                      = "Iron Pot";
      descItemTypeEquipment[(int)DmItemTypeEquipment.RamPortable]                                  = "Ram Portable";
      descItemTypeEquipment[(int)DmItemTypeEquipment.RopeHempen]                                   = "Hempen Rope";
      descItemTypeEquipment[(int)DmItemTypeEquipment.RopeSilk]                                     = "Silk Rope";
      descItemTypeEquipment[(int)DmItemTypeEquipment.SealingWax]                                   = "Sealing Wax";
      descItemTypeEquipment[(int)DmItemTypeEquipment.SewingNeedle]                                 = "Sewing Needle";
      descItemTypeEquipment[(int)DmItemTypeEquipment.SignalWhistle]                                = "Signal Whistle";
      descItemTypeEquipment[(int)DmItemTypeEquipment.SignetRing]                                   = "Signet Ring";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Sledge]                                       = "Sledge";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Soap]                                         = "Soap";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Shovel]                                       = "Shovel";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Spyglass]                                     = "Spyglass";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Tent]                                         = "Tent";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Torch]                                        = "Torch";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Vial]                                         = "Vial";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Waterskin]                                    = "Waterskin";
      descItemTypeEquipment[(int)DmItemTypeEquipment.Whetstone]                                    = "Whetstone";



      // Weapon Types
      descItemTypeWeaponType[(int)DmItemTypeWeaponType.Melee]                                      = "Melee";
      descItemTypeWeaponType[(int)DmItemTypeWeaponType.Ranged]                                     = "Ranged";
      descItemTypeWeaponType[(int)DmItemTypeWeaponType.Ammo]                                       = "Ammo";

      // Weapon Classes
      descItemTypeWeaponClass[(int)DmItemTypeWeaponClass.Simple]                                   = "Simple";
      descItemTypeWeaponClass[(int)DmItemTypeWeaponClass.Martial]                                  = "Martial";
      descItemTypeWeaponClass[(int)DmItemTypeWeaponClass.Exotic]                                   = "Exotic";


      //
      //                                                                                               [0]   [1]      [2]                [3]         [4]    [5]   [6]        [7]         [8]          [9]
      // Weapons : description is comma separated to include stats :                               = "Name, Price, Damage/Damage, Crit roll/Damage, range, weight, size, damage type, WeaponClass, weaponType";
      //           All fields should be used, even if blank or "--"
      //           DmItemWeaponAbstract will handle the description 
      //             based on the array created from Split(',').

      // Simple Melee Weapons
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.BlackKnife]                   = "                        Black Knife,      2 gp,         1d3,19-20 / x2,    10 ft.,  0.5 lbs.,    Tiny,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.BrassKnuckles]                = "                     Brass Knuckles,      5 sp,         1d4,        x2,          ,    1 lbs., Unarmed,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.CarvetDagger]                 = "                      Carvet Dagger,     40 gp,      1d4 +1,19-20 / x2,    10 ft.,    1 lbs.,    Tiny,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Club]                         = "                               Club,          ,         1d6,        x2,    10 ft.,    3 lbs.,  Medium,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Dagger]                       = "                             Dagger,      2 gp,         1d4,19-20 / x2,    10 ft.,    1 lbs.,    Tiny,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.DaggerPunching]               = "                    Dagger Punching,      2 gp,         1d4,        x3,          ,    2 lbs.,    Tiny,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Dirk]                         = "                               Dirk,      2 gp,         1d4,19-20 / x2,    10 ft.,    1 lbs.,    Tiny,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.FightingClaw]                 = "                      Fighting Claw,      5 gp,         d16,        x3,          ,    2 lbs.,   Small,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.FixedRazor]                   = "                        Fixed Razor,      3 sp,         1d3,        x2,          ,  0.5 lbs., Unarmed,                 Slashing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Garrote]                      = "                            Garrote,      2 sp,         1d2,          ,          , 0.25 lbs.,    Tiny,                 Slashing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.GoldenMelonHammer]            = "                Golden Melon Hammer,     15 gp,         1d8,        x3,          ,   14 lbs.,  Medium,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Gut_blade]                    = "                          Gut-blade,      5 gp,         1d4,18-20 / x2,          ,    1 lbs.,    Tiny,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.IronBrush]                    = "                         Iron Brush,      2 gp,         1d4,        x3,          ,    1 lbs.,    Tiny,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.IronFlute]                    = "                         Iron Flute,      5 gp,         1d4,        x2,          ,    1 lbs.,   Small,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.IronPipe]                     = "                          Iron Pipe,      5 gp,         1d6,        x2,          ,    2 lbs.,  Medium,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.LadiesChain]                  = "                       Ladies Chain,      5 gp,         1d3,        x2,          , 0.25 lbs., Unarmed,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.LashingStaff]                 = "                      Lashing Staff,      3 gp,         1d8,        x2,          ,    5 lbs.,   Large,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.LeechingDagger]               = "                    Leeching Dagger,     10 gp,         1d4,19-20 / x2,          ,    1 lbs.,    Tiny,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.LightMace]                    = "                         Light Mace,      5 gp,         1d6,        x2,          ,    6 lbs.,   Small,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Mace_Chained]                 = "                       Mace-Chained,     75 gp,         1d8,        x2,          ,   12 lbs.,  Medium,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.MaceHeavy]                    = "                         Mace Heavy,     12 gp,         1d8,        x2,          ,   12 lbs.,  Medium,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Machete]                      = "                            Machete,      5 gp,         1d6,        x2,    10 ft.,    2 lbs.,   Small,                 Slashing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.MastersHand]                  = "                       Masters Hand,     12 gp,         1d8,        x2,          ,    5 lbs.,   Large,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Morningstar]                  = "                        Morningstar,      8 gp,         1d8,        x2,          ,    8 lbs.,  Medium,   Bludgeoning & Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.PushKnife]                    = "                         Push Knife,      1 gp,         1d3,        x3,          ,  0.5 lbs., Unarmed,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Quarterstaff]                 = "                       Quarterstaff,          ,   1d6 / 1d6,        x2,          ,    4 lbs.,   Large,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Rake]                         = "                               Rake,     10 gp,         1d8,        x3,          ,    7 lbs.,   Large,     Bludgeoning Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Shortstaff]                   = "                        Short staff,          ,   1d4 / 1d4,        x2,          ,    2 lbs.,  Medium,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Shortspear]                   = "                         Shortspear,      2 gp,         1d8,        x3,    20 ft.,    5 lbs.,   Large,                 Piercing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.Sickle]                       = "                             Sickle,      6 gp,         1d6,        x2,          ,    3 lbs.,   Small,                 Slashing,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.SmallClub]                    = "                         Small Club,          ,         1d4,        x2,    10 ft.,    2 lbs.,   Small,              Bludgeoning,    Simple,     Melee";
      descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple.TaMoHiddenDaggers]            = "               Ta Mo Hidden Daggers,      6 gp,         1d4,19-20 / x2,    10 ft.,    2 lbs.,    Tiny,                 Piercing,    Simple,     Melee";

      // Martial Melee Weapons
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.BasketHiltedBacksword]      = "            Basket Hilted Backsword,     20 gp,  1d6 or 1d4,18-20 / x2,          ,    6 lbs.,  Medium,  Slashing or Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.BattleStaff]                = "                       Battle Staff,     10 gp,   1d8 / 1d8,        x2,          ,   15 lbs.,   Large,              Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Battleaxe]                  = "                          Battleaxe,     10 gp,         1d8,        x3,          ,    7 lbs.,  Medium,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Bush_knife]                 = "                         Bush-knife,     10 gp,         1d6,19-20 / x2,          ,    4 lbs.,   Small,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.ChokePike]                  = "                         Choke Pike,      8 gp,         1d8,        x3,          ,   14 lbs.,   Large,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Cinqueda]                   = "                           Cinqueda,     15 gp,         2d3,        x3,          ,    3 lbs.,   Small,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Claw]                       = "                               Claw,     15 gp,         1d8,        x2,          ,    7 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Claymore]                   = "                           Claymore,     60 gp,        1d12,19-20 / x2,          ,   15 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Dadao]                      = "                              Dadao,     25 gp,         1d6,19-20 / x2,          ,    5 lbs.,  Medium,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.DartMace]                   = "                          Dart Mace,     65 gp,1d8/1d4 dart,        x2,    20 ft.,   12 lbs.,  Medium,  Bludgeoning or Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.DragonWhiskersFork]         = "               Dragon Whiskers Fork,     20 gp,         2d4,        x2,    20 ft.,    7 lbs.,   Large,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.ExecutionersSword]          = "                Executioner's Sword,     75 gp,        1d12,        x4,          ,   18 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Falchion]                   = "                           Falchion,     75 gp,         2d4,18-20 / x2,          ,   16 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.FangedBlade]                = "                       Fanged Blade,    450 gp,      1d8 +1,19-20 / x2,          ,    3 lbs.,  Medium,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.FencingSaber]               = "                      Fencing Saber,     20 gp,         d16,18-20 / x2,          ,    3 lbs.,   Small,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.GhostHeadBroadsword]        = "              Ghost Head Broadsword,     80 gp,        1d10,18-20 / x2,          ,   17 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Glaive]                     = "                             Glaive,      8 gp,        1d10,        x3,          ,   15 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.GoldCoinSpade]              = "                    Gold Coin Spade,     20 gp,         d18,        x2,          ,    7 lbs.,   Large,  Bludgeoning or Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Greataxe]                   = "                           Greataxe,     20 gp,        1d12,        x3,          ,   20 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Greatclub]                  = "                          Greatclub,      5 gp,        1d10,        x2,          ,   10 lbs.,   Large,              Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Greatsword]                 = "                         Greatsword,     50 gp,         2d6,19-20 / x2,          ,   15 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Guisarme]                   = "                           Guisarme,      9 gp,         2d4,        x3,          ,   15 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Halberd]                    = "                            Halberd,     10 gp,        1d10,        x3,          ,   15 lbs.,   Large,      Piercing & Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Handaxe]                    = "                            Handaxe,      6 gp,         1d6,        x3,          ,    5 lbs.,   Small,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.HeavyFlail]                 = "                        Heavy Flail,     15 gp,        1d10,19-20 / x2,          ,   20 lbs.,   Large,              Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.HeavyLance]                 = "                        Heavy Lance,     10 gp,         1d8,        x3,          ,   10 lbs.,  Medium,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.HeavyPick]                  = "                         Heavy Pick,      8 gp,         1d6,        x4,          ,    6 lbs.,  Medium,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.HookedSpear]                = "                       Hooked Spear,      4 gp,         1d8,        x3,    20 ft.,    5 lbs.,   Large,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.LightFlail]                 = "                        Light Flail,      8 gp,         1d8,        x2,          ,    5 lbs.,  Medium,              Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.LightLance]                 = "                        Light Lance,      6 gp,         1d6,        x3,          ,    5 lbs.,   Small,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.LightPick]                  = "                         Light Pick,      4 gp,         1d4,        x4,          ,    4 lbs.,   Small,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Longspear]                  = "                          Longspear,      5 gp,         1d8,        x3,          ,    9 lbs.,   Large,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Longsword]                  = "                          Longsword,     15 gp,         1d8,19-20 / x2,          ,    4 lbs.,  Medium,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.LucernHammer]               = "                      Lucern Hammer,     12 gp,         2d4,        x4,          ,   10 lbs.,   Large,   Piercing & Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Maul]                       = "                               Maul,     15 gp,        1d10,        x3,          ,   20 lbs.,   Large,              Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.MonksCudgel]                = "                      Monk's Cudgel,     12 gp,         2d4,        x2,          ,   13 lbs.,   Large,              Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.NightlingCleaver]           = "                  Nightling Cleaver,     35 gp,        1d10,18-20 / x2,          ,   20 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Pike]                       = "                               Pike,      7 gp,         1d8,        x3,          ,   13 lbs.,   Large,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Ranseur]                    = "                            Ranseur,     10 gp,         2d4,        x3,          ,   15 lbs.,   Large,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Rapier]                     = "                             Rapier,     20 gp,         1d6,18-20 / x2,          ,    3 lbs.,  Medium,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Sap]                        = "                                Sap,      1 gp,         1d6,        x2,          ,    3 lbs.,   Small,              Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Scimitar]                   = "                           Scimitar,     15 gp,         1d6,18-20 / x2,          ,    3 lbs.,  Medium,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Scythe]                     = "                             Scythe,     18 gp,         2d4,        x4,          ,   12 lbs.,   Large,      Piercing & Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.ShortSword]                 = "                        Short Sword,     10 gp,         1d6,19-20 / x2,          ,    3 lbs.,   Small,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.SmallFlail]                 = "                        Small Flail,      8 gp,         1d6,        x2,          ,    3 lbs.,   Small,              Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.SmallLongsword]             = "                    Small Longsword,     15 gp,         1d6,19-20 / x2,          ,    3 lbs.,   Small,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.SmallRapier]                = "                       Small Rapier,     20 gp,         1d4,18-20 / x2,          ,    2 lbs.,   Small,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Straight_sword]             = "                     Straight-sword,     20 gp,         1d6,19-20 / x2,          ,    2 lbs.,  Medium,     Slashing or Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.ThreePointDoubleBladedSword]= "    Three Point Double Bladed Sword,     15 gp,         2d4,        x3,    20 ft.,   10 lbs.,   Large,     Slashing or Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.TigerFork]                  = "                         Tiger Fork,     15 gp,        1d10,        x2,    20 ft.,   15 lbs.,   Large,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Trident]                    = "                            Trident,     15 gp,         1d8,        x2,    10 ft.,    5 lbs.,  Medium,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.WarFork]                    = "                           War Fork,     20 gp,        1d12,19-20 / x2,          ,   15 lbs.,   Large,           Piercing and S,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Warhammer]                  = "                          Warhammer,     12 gp,         1d8,        x3,          ,    8 lbs.,  Medium,              Bludgeoning,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.Widowmaker]                 = "                        Widow maker,     35 gp,        1d12,19-20 / x2,          ,   25 lbs.,   Large,                 Slashing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.WolfSpear]                  = "                         Wolf Spear,     50 gp,         1d8,        x3,          ,   20 lbs.,   Large,                 Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.WolfTeethClub]              = "                    Wolf Teeth Club,      8 gp,         1d8,        x3,          ,    5 lbs.,   Large, Bludgeoning and Piercing,   Martial,     Melee";
      descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial.WolfTeethSpikedTrident]     = "          Wolf Teeth Spiked Trident,     20 gp,         2d4,        x2,    20 ft.,    7 lbs.,   Large,                 Piercing,   Martial,     Melee";

      // Exotic Melee Weapons
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Axe_Hammer]                   = "                         Axe-Hammer,     55 gp,   1d8 / 1d8,        x3,          ,    8 lbs.,  Medium,   Bludgeoning & Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.BalledChain]                  = "                       Balled Chain,     20 gp,   1d8 / 1d8,        x2,          ,   18 lbs.,   Large,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.BaneSpear]                    = "                         Bane Spear,     50 gp,   1d8 / 1d8,    x3/ x2,    20 ft.,    7 lbs.,   Large,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.BastardSword]                 = "                      Bastard Sword,     35 gp,        1d10,19-20 / x2,          ,   10 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.BattleGauntlet]               = "                    Battle Gauntlet,     25 gp,         1d6,        x2,          ,    5 lbs.,   Small,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Bloodaxe]                     = "                           Bloodaxe,     50 gp,         2d8,        x3,          ,   20 lbs.,   Large,     Slashing or Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.BucklerBlade]                 = "                      Buckler Blade,     30 gp,         1d6,        x3,          ,    4 lbs.,   Small,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.ButterflySword]               = "                    Butterfly Sword,     10 gp,         1d6,19-20 / x2,          ,    2 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.CatGloves]                    = "                         Cat Gloves,      5 gp,         1d4,        x2,          ,    2 lbs.,    Tiny,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Chain_and_Dagger]             = "                   Chain-and-Dagger,      4 gp,         1d4,19-20 / x2,          ,    4 lbs.,  Medium,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.ChainedAxe]                   = "                        Chained Axe,     15 gp,         1d8,        x3,          ,   15 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.CicadaWingSword]              = "                  Cicada Wing Sword,    120 gp,   1d6 / 1d6,19-20 / x2,          ,   10 lbs.,   Large,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.ClawBracer]                   = "                        Claw Bracer,     30 gp,         1d4,19-20 / x2,          ,    2 lbs.,    Tiny,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.CombatHook]                   = "                        Combat Hook,     10 gp,         1d6,        x3,          ,    3 lbs.,   Small,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.CrushingAxe]                  = "                       Crushing Axe,     75 gp,   1d8 / 1d8,    x3/ x2,          ,   25 lbs.,   Large,  Piercing or Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DaggerScholarsBrush]          = "              Dagger Scholars Brush,      2 gp,         1d3,        x2,    10 ft.,  0.5 lbs.,    Tiny,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DireFlail]                    = "                         Dire Flail,     90 gp,   1d8 / 1d8,        x2,          ,   20 lbs.,   Large,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DoubleBladedSword]            = "                Double Bladed Sword,     60 gp,        1d10,19-20 / x2,          ,    8 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DoubleChainedAxe]             = "                 Double Chained Axe,     30 gp,   1d8 / 1d8,        x3,          ,   20 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DoubleFlyingSword]            = "                Double Flying Sword,     18 gp,   1d6 / 1d6,        x2,          ,    4 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DoubleHeaded]                 = "                      Double Headed,      5 gp,   1d8 / 1d8,        x3,    20 ft.,    6 lbs.,   Large,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DoubleHeadedSword]            = "                Double Headed Sword,     40 gp,        1d10,        x3,          ,   15 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DoubleMace]                   = "                        Double Mace,     70 gp,   1d8 / 1d8,        x2,          ,   22 lbs.,   Large,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DoubleScimitar]               = "                    Double Scimitar,    125 gp,   1d6 / 1d6,18-20 / x2,          ,   15 lbs.,   Large,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DragonHeadStick]              = "                  Dragon Head Stick,     10 gp,   1d4 / 1d4,        x2,          ,    3 lbs.,  Medium,  Bludgeoning or Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DuckBlade]                    = "                         Duck Blade,     12 gp,         1d6,        x3,          ,    2 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Duom]                         = "                               Duom,     20 gp,         1d8,        x3,          ,    8 lbs.,   Large,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DwarvenUrgrosh]               = "                    Dwarven Urgrosh,     50 gp,   1d8 / 1d6,        x3,          ,   15 lbs.,   Large,      Slashing & Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.DwarvenWaraxe]                = "                     Dwarven Waraxe,     30 gp,        1d10,        x3,          ,   15 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.EmeiPincer]                   = "                        Emei Pincer,      2 gp,         1d3,        x3,          ,  0.2 lbs.,    Tiny,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.FlyingWeight]                 = "                      Flying Weight,      5 gp,         1d6,19-20 / x2,    10 ft.,  1.5 lbs.,  Medium,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.FourSectionSickle]            = "                Four Section Sickle,     12 gp,         2d4,        x3,          ,   11 lbs.,   Large,  Bludgeoning or Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Fullblade]                    = "                          Fullblade,    100 gp,         2d8,19-20 / x2,          ,   23 lbs.,    Huge,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.GnomeBattlepick]              = "                   Gnome Battlepick,     10 gp,         1d6,        x4,          ,    5 lbs.,   Small,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.GnomeHooked_Hammer]           = "                Gnome Hooked-Hammer,     20 gp,   1d6 / 1d4,    x3/ x4,          ,    6 lbs.,  Medium,   Bludgeoning & Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Gyrspike]                     = "                           Gyrspike,     90 gp,   1d8 / 1d8,19-20 / x2,          ,  120 lbs.,   Large,   Slashing & Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.HeavenandSunandMoonSword]     = "      Heaven and Sun and Moon Sword,     12 gp,   1d6 / 1d6,19-20 / x2,          ,   10 lbs.,   Large,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.HeavenLotusPhoenixSword]      = "         Heaven Lotus Phoenix Sword,     35 gp,   1d8 / 1d8,        x3,    20 ft.,   11 lbs.,   Large,     Piercing or Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Hokk]                         = "                               Hokk,     10 gp,         1d6,        x3,          ,    3 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Hookflail]                    = "                         Hook flail,     15 gp,         1d8,19-20 / x2,          ,   20 lbs.,   Large,  Bludgeoning or Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.HorseHackingSword]            = "                Horse Hacking Sword,     30 gp,   1d6 / 1d4,        x2,          ,    8 lbs.,   Large,  Slashing or Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.HorseHalberd]                 = "                      Horse Halberd,      5 gp,         1d6,        x2,          ,    2 lbs.,   Small,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.JumpSpear]                    = "                         Jump Spear,      8 gp,         1d8,        x3,          ,    7 lbs.,   Large,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Kama]                         = "                               Kama,      2 gp,         1d6,        x2,          ,    2 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.KamaHalfling]                 = "                      Kama Halfling,      2 gp,         1d4,        x2,          ,    1 lbs.,    Tiny,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Kopesh]                       = "                             Kopesh,     20 gp,         1d8,19-20 / x2,          ,   12 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Kukri]                        = "                              Kukri,      8 gp,         1d4,18-20 / x2,          ,    3 lbs.,    Tiny,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Manti]                        = "                              Manti,     15 gp,         1d8,        x3,          ,    9 lbs.,   Large,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.MercurialGreatsword]          = "               Mercurial Greatsword,    600 gp,         2d6,        x4,          ,   17 lbs.,   Large,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.MercurialLongsword]           = "                Mercurial Longsword,    400 gp,         1d8,        x4,          ,    6 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.MeteorHammer]                 = "                      Meteor Hammer,      8 gp,   1d6 / 1d6,19-20 / x2,          ,    3 lbs.,  Medium,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.MonksSpade]                   = "                       Monk's Spade,     30 gp,  1d10 / 2d4,        x2,          ,   11 lbs.,   Large,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.MotherandSonHammer]           = "              Mother and Son Hammer,      7 gp,   1d8 / 1d8,19-20 / x2,          ,    7 lbs.,   Large,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.NineTeethHammer]              = "                  Nine Teeth Hammer,     18 gp,         1d6,        x3,          ,    3 lbs.,  Medium,     Slashing or Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Nunchaku]                     = "                           Nunchaku,      2 gp,         1d6,        x2,          ,    2 lbs.,   Small,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.NunchakuHalfling]             = "                  Nunchaku Halfling,      2 gp,         1d4,        x2,          ,    1 lbs.,    Tiny,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.OrcDoubleAxe]                 = "                     Orc Double Axe,     60 gp,   1d8 / 1d8,        x3,          ,   25 lbs.,   Large,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.PantherClaw]                  = "                       Panther Claw,     75 gp,         1d4,        x3,          ,    3 lbs.,    Tiny,      Piercing & Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Pen]                          = "                                Pen,     15 gp,         1d6,18-20 / x2,          ,    5 lbs.,   Large,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.PoisonedHairpin]              = "                   Poisoned Hairpin,     15 gp,         1d2,        x2,          ,    .1 lbs,    Tiny,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.PoleSword]                    = "                         Pole Sword,     10 gp,         1d8,19-20 / x2,     5 ft.,    5 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Ribbon]                       = "                             Ribbon,      1 gp,         1d3,        x2,    20 ft.,  0.2 lbs.,    Tiny,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.RibbonSword]                  = "                       Ribbon Sword,     15 gp,         1d8,        x3,          ,    3 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.RingBlade]                    = "                         Ring Blade,     25 gp,         1d6,        x3,          ,    4 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.RoosterBlade]                 = "                      Rooster Blade,     16 gp,         1d6,        x3,          ,    2 lbs.,   Small,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Sai]                          = "                                Sai,      1 gp,         1d4,        x2,          ,    2 lbs.,   Small,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Sapara]                       = "                             Sapara,     15 gp,         1d6,19-20 / x2,          ,    6 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.ScarfChain]                   = "                        Scarf Chain,     10 gp,         1d4,        x3,          ,    1 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Siangham]                     = "                           Siangham,      3 gp,         1d6,        x2,          ,    1 lbs.,   Small,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.SianghamHalfling]             = "                  Siangham Halfling,      2 gp,         1d4,        x2,          ,    1 lbs.,    Tiny,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.SkyLance]                     = "                          Sky Lance,   1060 gp,        1d12,        x3,          ,   10 lbs.,  Medium,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.SnakeRing]                    = "                         Snake Ring,     14 gp,         1d4,19-20 / x2,          ,    2 lbs.,   Small,     Slashing or Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.SpikedChain]                  = "                       Spiked Chain,     25 gp,         2d4,        x2,          ,   15 lbs.,   Large,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.StumpKnife]                   = "                        Stump Knife,      8 gp,         1d4,19-20 / x2,          ,    2 lbs.,    Tiny,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.SwordGauntlet]                = "                     Sword Gauntlet,     55 gp,         1d6,19-20 / x2,          ,    3 lbs.,   Small,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Three_SectionStaff]           = "                Three-Section Staff,      4 gp,         1d8,        x3,          ,    8 lbs.,   Large,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.TigerClaws]                   = "                        Tiger Claws,      5 gp,         1d4,        x2,          ,    2 lbs.,    Tiny,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.TigerHook]                    = "                         Tiger Hook,     15 gp,         1d6,19-20 / x2,          ,    3 lbs.,  Medium,     Slashing or Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Tonfa]                        = "                              Tonfa,      2 sp,         1d6,        x2,          ,    2 lbs.,   Small,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Trip_bag]                     = "                           Trip-bag,      5 gp,1d8 _subdual,        x2,          ,   20 lbs.,   Large,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.TripleDagger]                 = "                      Triple Dagger,     10 gp,         1d4,19-20 / x2,          ,    1 lbs.,    Tiny,                 Piercing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Two_BladedSword]              = "                   Two-Bladed Sword,    100 gp,   1d8 / 1d8,19-20 / x2,          ,   30 lbs.,   Large,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.UnicornHornSword]             = "                 Unicorn Horn Sword,     15 gp,         1d6,19-20 / x2,          ,    2 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.WarCleaver]                   = "                        War Cleaver,     50 gp,         2d4,19-20 / x2,          ,   10 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.WarFan]                       = "                            War Fan,     30 gp,         1d6,        x3,          ,    3 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.WarMaul]                      = "                           War Maul,     75 gp,         2d8,        x3,          ,   30 lbs.,   Large,              Bludgeoning,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.WaterPartingShield]           = "               Water Parting Shield,     12 gp,         1d6,19-20 / x2,          ,    3 lbs.,  Medium,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Whip]                         = "                               Whip,      1 gp,         1d2,        x2,    15 ft.,    2 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.Whip_Dagger]                  = "                        Whip-Dagger,     25 gp,         1d6,19-20 / x2,    15 ft.,    3 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.WindandFireWheel]             = "                Wind and Fire Wheel,     16 gp,         1d6,        x3,          ,    2 lbs.,   Small,                 Slashing,    Exotic,     Melee";
      descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic.WolfTeethHammer]              = "                  Wolf Teeth Hammer,     12 gp,         1d8,        x2,          ,    5 lbs.,   Large,  Bludgeoning or Piercing,    Exotic,     Melee";

      // Simple Ranged Weapons
      descItemTypeWeaponRangedSimple[(int)DmItemTypeWeaponRangedSimple.FlaskLauncher]              = "                     Flask Launcher,     50 gp,            ,19-20 / x2,    60 ft.,    8 lbs.,  Medium,                 Slashing,    Simple,    Ranged";
      descItemTypeWeaponRangedSimple[(int)DmItemTypeWeaponRangedSimple.Halfspear]                  = "                          Halfspear,      1 gp,         1d6,        x3,    20 ft.,    3 lbs.,  Medium,                 Piercing,    Simple,    Ranged";
      descItemTypeWeaponRangedSimple[(int)DmItemTypeWeaponRangedSimple.HeavyCrossbow]              = "                     Heavy Crossbow,     50 gp,        1d10,19-20 / x2,   120 ft.,    9 lbs.,  Medium,                 Piercing,    Simple,    Ranged";
      descItemTypeWeaponRangedSimple[(int)DmItemTypeWeaponRangedSimple.Javelin]                    = "                            Javelin,      1 gp,         1d6,        x2,    30 ft.,    2 lbs.,  Medium,                 Piercing,    Simple,    Ranged";
      descItemTypeWeaponRangedSimple[(int)DmItemTypeWeaponRangedSimple.LightCrossbow]              = "                     Light Crossbow,     35 gp,         1d8,19-20 / x2,    80 ft.,    6 lbs.,   Small,                 Piercing,    Simple,    Ranged";
      descItemTypeWeaponRangedSimple[(int)DmItemTypeWeaponRangedSimple.SandSling]                  = "                         Sand Sling,      2 gp,            ,          ,    15 ft.,   10 lbs.,    Tiny,                         ,    Simple,    Ranged";
      descItemTypeWeaponRangedSimple[(int)DmItemTypeWeaponRangedSimple.Sling]                      = "                              Sling,          ,         1d4,        x2,    50 ft.,    0 lbs.,   Small,              Bludgeoning,    Simple,    Ranged";
      descItemTypeWeaponRangedSimple[(int)DmItemTypeWeaponRangedSimple.ThrowingKnives]             = "                    Throwing Knives,      1 gp,         1d3,        x2,    10 ft.,  0.5 lbs.,    Tiny,                 Piercing,    Simple,    Ranged";

      // Martial Ranged Weapons
      descItemTypeWeaponRangedMartial[(int)DmItemTypeWeaponRangedMartial.BarbedArrows]             = "                      Barbed Arrows,      2 gp,         1d8,19-20 / x2,          ,   30 lbs.,  Medium,                 Piercing,   Martial,    Ranged";
      descItemTypeWeaponRangedMartial[(int)DmItemTypeWeaponRangedMartial.CompositeShortbow]        = "                 Composite Shortbow,     75 gp,         1d6,        x3,    70 ft.,    2 lbs.,  Medium,                 Piercing,   Martial,    Ranged";
      descItemTypeWeaponRangedMartial[(int)DmItemTypeWeaponRangedMartial.LightHammer]              = "                       Light Hammer,      1 gp,         1d4,        x2,    20 ft.,    2 lbs.,   Small,              Bludgeoning,   Martial,    Ranged";
      descItemTypeWeaponRangedMartial[(int)DmItemTypeWeaponRangedMartial.Longbow]                  = "                            Longbow,     75 gp,         1d8,        x3,   100 ft.,    3 lbs.,  Medium,                 Piercing,   Martial,    Ranged";
      descItemTypeWeaponRangedMartial[(int)DmItemTypeWeaponRangedMartial.Shortbow]                 = "                           Shortbow,     30 gp,         1d6,        x3,    60 ft.,    2 lbs.,  Medium,                 Piercing,   Martial,    Ranged";
      descItemTypeWeaponRangedMartial[(int)DmItemTypeWeaponRangedMartial.ThrowingAxe]              = "                       Throwing Axe,      8 gp,         1d6,        x2,    10 ft.,    4 lbs.,   Small,                 Slashing,   Martial,    Ranged";

      // Exotic Ranged Weapons
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.Blowgun]                    = "                            Blowgun,      1 gp,            ,        x2,    30 ft.,  0.5 lbs.,   Small,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.Chakram]                    = "                            Chakram,     15 gp,         1d4,        x3,    30 ft.,    2 lbs.,   Small,                 Slashing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.DualCrossbow]               = "                      Dual Crossbow,    150 gp,         1d8,19-20 / x2,    80 ft.,    9 lbs.,  Medium,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.DuelingCloak]               = "                      Dueling Cloak,     15 gp,            ,        x2,    10 ft.,   30 lbs.,  Medium,                         ,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.ElvenDoubleBow]             = "                   Elven Double Bow,   1000 gp,         1d8,        x3,    90 ft.,    3 lbs.,   Large,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.Flutegun]                   = "                          Flute gun,     15 gp,            ,        x2,    30 ft.,   30 lbs.,   Small,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.Fukimi_Bari]                = "                        Fukimi-Bari,      1 gp,         1d1,        x2,     5 ft.,  0.1 lbs.,    Tiny,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.GnomeCalculus]              = "                     Gnome Calculus,     50 gp,            ,          ,    50 ft.,    2 lbs.,   Small,                         ,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.GreatCrossbow]              = "                     Great Crossbow,    100 gp,        1d12,19-20 / x2,   150 ft.,   15 lbs.,   Large,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.HalflingSkiprock]           = "                  Halfling Skiprock,      3 gp,         1d3,        x2,    10 ft., 0.25 lbs.,    Tiny,              Bludgeoning,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.HandCrossbow]               = "                      Hand Crossbow,    100 gp,         1d4,19-20 / x2,    30 ft.,    3 lbs.,    Tiny,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.Harpoon]                    = "                            Harpoon,     15 gp,        1d10,        x2,    30 ft.,   10 lbs.,   Large,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.Net]                        = "                                Net,     20 gp,            ,          ,    10 ft.,   10 lbs.,  Medium,                         ,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.OrcShotput]                 = "                        Orc Shotput,     10 gp,         2d6,19-20 / x3,    10 ft.,   15 lbs.,   Large,              Bludgeoning,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.RazorDiskLauncher]          = "                Razor Disk Launcher,      1 gp,        1d10,19-20 / x2,    30 ft.,   20 lbs.,  Medium,                 Slashing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.RepeatingCrossbow]          = "                 Repeating Crossbow,    250 gp,         1d8,19-20 / x2,    80 ft.,   16 lbs.,  Medium,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.Shuriken]                   = "                           Shuriken,      1 gp,         1d1,        x2,    10 ft.,  0.1 lbs.,    Tiny,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.Sling_stick]                = "                        Sling-stick,     10 gp,         1d4,        x2,    50 ft.,   10 lbs.,   Small,Bludgeoning or per missle,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.SmallNet]                   = "                          Small Net,     20 gp,            ,          ,    10 ft.,    5 lbs.,   Small,                         ,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.SpinningJavalin]            = "                   Spinning Javalin,      2 gp,         1d8,19-20 / x2,    50 ft.,    2 lbs.,  Medium,                 Piercing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.ThrowingIron]               = "                      Throwing Iron,      8 gp,         1d6,        x3,    10 ft.,    3 lbs.,   Small,                 Slashing,    Exotic,    Ranged";
      descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic.Two_BallBolas]              = "                     Two_Ball Bolas,      5 gp,         1d4,        x2,    10 ft.,    2 lbs.,   Small,              Bludgeoning,    Exotic,    Ranged";

      // Simple Weapon Ammo
      descItemTypeWeaponAmmoSimple[(int)DmItemTypeWeaponAmmoSimple.ArrowPiercing]                  = "                     Arrow Piercing,      2 gp,         1d8,        x3,          ,   30 lbs.,    Size,                 Piercing,    Simple,      Ammo";
      descItemTypeWeaponAmmoSimple[(int)DmItemTypeWeaponAmmoSimple.Dart]                           = "                               Dart,      5 sp,         1d4,        x2,    20 ft.,  0.5 lbs.,   Small,                 Piercing,    Simple,      Ammo";
      descItemTypeWeaponAmmoSimple[(int)DmItemTypeWeaponAmmoSimple.LightCrossbowBolts_10]          = "          Light Crossbow Bolts (10),      1 gp,            ,          ,          ,    1 lbs.,        ,                         ,    Simple,      Ammo";
      descItemTypeWeaponAmmoSimple[(int)DmItemTypeWeaponAmmoSimple.SlingBullet_10]                 = "                  Sling Bullet (10),      1 sp,            ,          ,          ,    5 lbs.,        ,                         ,    Simple,      Ammo";
      descItemTypeWeaponAmmoSimple[(int)DmItemTypeWeaponAmmoSimple.SpikedBullets]                  = "                     Spiked Bullets,      5 sp,      1d4 +1,        x2,          ,    5 lbs.,    Size,              Bludgeoning,    Simple,      Ammo";

      // Martial Weapon Ammo
      descItemTypeWeaponAmmoMartial[(int)DmItemTypeWeaponAmmoMartial.AlchemistsArrow]              = "                  Alchemist's Arrow,     75 gp,            ,        x2,          ,  0.2 lbs.,        ,                         ,   Martial,      Ammo";
      descItemTypeWeaponAmmoMartial[(int)DmItemTypeWeaponAmmoMartial.Arrow_20]                     = "                         Arrow (20),      1 gp,            ,          ,          ,    3 lbs.,        ,                 Piercing,   Martial,      Ammo";
      descItemTypeWeaponAmmoMartial[(int)DmItemTypeWeaponAmmoMartial.BluntArrow]                   = "                        Blunt Arrow,      5 sp,   1d6 / 1d8,        x2,          ,  0.2 lbs.,        ,              Bludgeoning,   Martial,      Ammo";
      descItemTypeWeaponAmmoMartial[(int)DmItemTypeWeaponAmmoMartial.FlightArrow]                  = "                       Flight Arrow,      8 gp,            ,        x2,   +25 ft.,  0.2 lbs.,        ,                 Piercing,   Martial,      Ammo";
      descItemTypeWeaponAmmoMartial[(int)DmItemTypeWeaponAmmoMartial.SignalArrow]                  = "                       Signal Arrow,      5 sp,            ,        x2,          ,  0.2 lbs.,        ,              Bludgeoning,   Martial,      Ammo";
      descItemTypeWeaponAmmoMartial[(int)DmItemTypeWeaponAmmoMartial.ThunderingArrow]              = "                   Thundering Arrow,      2 gp,            ,          ,          ,  0.3 lbs.,        ,                         ,   Martial,      Ammo";
      descItemTypeWeaponAmmoMartial[(int)DmItemTypeWeaponAmmoMartial.TumblingBolt]                 = "                      Tumbling Bolt,      5 gp,            ,        x2,          ,  0.2 lbs.,        ,                         ,   Martial,      Ammo";

      // Exotic Weapon Ammo
      descItemTypeWeaponAmmoExotic[(int)DmItemTypeWeaponAmmoExotic.BlowgunDarts]                   = "                      Blowgun Darts,      1 sp,            ,        x2,          ,  0.1 lbs.,   Small,                         ,    Exotic,      Ammo";
      descItemTypeWeaponAmmoExotic[(int)DmItemTypeWeaponAmmoExotic.HandCrossbowBolt_10]            = "            Hand Crossbow Bolt (10),      1 gp,            ,          ,          ,    1 lbs.,        ,                         ,    Exotic,      Ammo";
      descItemTypeWeaponAmmoExotic[(int)DmItemTypeWeaponAmmoExotic.RazorDisk]                      = "                         Razor Disk,      5 gp,            ,          ,          ,   10 lbs.,  Medium,                         ,    Exotic,      Ammo";



      // Clothing types
      descItemTypeClothing[(int)DmItemTypeClothing.ArtisanOutfit]                                  = "an artisan outfit";
      descItemTypeClothing[(int)DmItemTypeClothing.CastersRobesElaborate]                          = "elaborate robes";
      descItemTypeClothing[(int)DmItemTypeClothing.CastersRobesFine]                               = "fine robes";
      descItemTypeClothing[(int)DmItemTypeClothing.CastersRobesSimple]                             = "simple robes";
      descItemTypeClothing[(int)DmItemTypeClothing.ClericVestments]                                = "a cleric vestments";
      descItemTypeClothing[(int)DmItemTypeClothing.ColdWeatherOutfit]                              = "a cold Weather outfit";
      descItemTypeClothing[(int)DmItemTypeClothing.CourtiersOutfit]                                = "a courtiers outfit";
      descItemTypeClothing[(int)DmItemTypeClothing.EntertainersOutfit]                             = "an entertainers outfit";
      descItemTypeClothing[(int)DmItemTypeClothing.ExplorersOutfit]                                = "an explorers outfit";
      descItemTypeClothing[(int)DmItemTypeClothing.MonksOutfit]                                    = "a monks outfit";
      descItemTypeClothing[(int)DmItemTypeClothing.NoblesOutfit]                                   = "a nobles outfit";
      descItemTypeClothing[(int)DmItemTypeClothing.PeasantsOutfit]                                 = "a peasants outfit";
      descItemTypeClothing[(int)DmItemTypeClothing.RoyalOutfit]                                    = "a royal outfit";
      descItemTypeClothing[(int)DmItemTypeClothing.ScholarsOutfit]                                 = "a scholars outfit";
      descItemTypeClothing[(int)DmItemTypeClothing.TravelersOutfit]                                = "a travelers outfit";



      // Armor Types
      descItemTypeArmorType[(int)DmItemTypeArmorType.Light]                                        = "Light";
      descItemTypeArmorType[(int)DmItemTypeArmorType.Medium]                                       = "Medium";
      descItemTypeArmorType[(int)DmItemTypeArmorType.Heavy]                                        = "Heavy";
      descItemTypeArmorType[(int)DmItemTypeArmorType.Shield]                                       = "Shield";
      descItemTypeArmorType[(int)DmItemTypeArmorType.Extra]                                        = "Extra";

      // Light armors
      descItemTypeArmorLight[(int)DmItemTypeArmorLight.Padded]                                     = "Padded";
      descItemTypeArmorLight[(int)DmItemTypeArmorLight.Leather]                                    = "Leather";
      descItemTypeArmorLight[(int)DmItemTypeArmorLight.StuddedLeather]                             = "Studded Leather";
      descItemTypeArmorLight[(int)DmItemTypeArmorLight.ChainShirt]                                 = "Chain Shirt";

      // Medium armors
      descItemTypeArmorMedium[(int)DmItemTypeArmorMedium.Hide]                                     = "Hide";
      descItemTypeArmorMedium[(int)DmItemTypeArmorMedium.ScaleMail]                                = "Scale Mail";
      descItemTypeArmorMedium[(int)DmItemTypeArmorMedium.ChainMail]                                = "Chain Mail";
      descItemTypeArmorMedium[(int)DmItemTypeArmorMedium.BreastPlate]                              = "Breast Plate";

      // Heavy armors
      descItemTypeArmorHeavy[(int)DmItemTypeArmorHeavy.SplintMail]                                 = "Splint Mail";
      descItemTypeArmorHeavy[(int)DmItemTypeArmorHeavy.BandedMail]                                 = "Banded Mail";
      descItemTypeArmorHeavy[(int)DmItemTypeArmorHeavy.HalfPlate]                                  = "Half Plate";
      descItemTypeArmorHeavy[(int)DmItemTypeArmorHeavy.FullPlate]                                  = "Full Plate";

      // Sheild (armors)
      descItemTypeArmorShield[(int)DmItemTypeArmorShield.Buckler]                                  = "Buckler";
      descItemTypeArmorShield[(int)DmItemTypeArmorShield.LightWooden]                              = "Light Wooden Shield";
      descItemTypeArmorShield[(int)DmItemTypeArmorShield.LightSteel]                               = "Light Steel Shield";
      descItemTypeArmorShield[(int)DmItemTypeArmorShield.HeavyWooden]                              = "Heavy Wooden Shield";
      descItemTypeArmorShield[(int)DmItemTypeArmorShield.HeavySteel]                               = "Heavy Steel Shield";
      descItemTypeArmorShield[(int)DmItemTypeArmorShield.Tower]                                    = "Tower Shield";

      // Armor enhancements
      descItemTypeArmorExtra[(int)DmItemTypeArmorExtra.ArmorSpikes]                                = "Armor Spikes";
      descItemTypeArmorExtra[(int)DmItemTypeArmorExtra.LockedGauntlet]                             = "Locked Gauntlet";
      descItemTypeArmorExtra[(int)DmItemTypeArmorExtra.ShieldSpikes]                               = "Shield Spikes";
    }

    /// <summary>
    /// GetDescription will take any of the DmItemXXXX enum values, 
    ///   determine which enum list it comes from, then return the 
    ///   description of that value from the associated string[] array.
    /// </summary>
    /// <param name="type">A single value from any of the DmItemTyeXXX enums</param>
    /// <returns>The description associated with that enum value</returns>
    public static string GetDescription( object type )
    {
      return GetDescription( type, false );
    }

    /// <summary>
    /// GetDescription will take any of the DmItemXXXX enum values, 
    ///   determine which enum list it comes from, then return the 
    ///   description of that value from the associated string[] array.
    /// </summary>
    /// <param name="type">A single value from any of the DmItemTyeXXX enums</param>
    /// <param name="fullDesc">true = Return the full description (include penalties, stats, dmaage, etc) as a comma separated string, false = return the name of the item only.</param>
    /// <returns>The description associated with that enum value</returns>
    public static string GetDescription( object type, bool fullDesc )
    {
      if( !listsCreated )
        CreateLists();

      Type dmItemType = type.GetType();

      // -------------------------------------------------------------------- enchantment descriptions
      if( dmItemType == typeof( DmItemEnchantmentArmor ) )
        return descItemEnchantmentArmor[(int)type].Trim();

      else if( dmItemType == typeof( DmItemEnchantmentClothing ) )
        return descItemEnchantmentClothing[(int)type].Trim();

      else if( dmItemType == typeof( DmItemEnchantmentConsumable ) )
        return descItemEnchantmentConsumable[(int)type].Trim();

      else if( dmItemType == typeof( DmItemEnchantmentWeapon ) )
        return descItemEnchantmentWeapon[(int)type].Trim();

        // ------------------------------------------------------------------ type and quality descriptions
      else if( dmItemType == typeof( DmItemQuality ) )
        return descItemQuality[(int)type].Trim();

      else if( dmItemType == typeof( DmItemType ) )
        return descItemType[(int)type].Trim();

        // ------------------------------------------------------------------ armor class and type descriptions
      else if( dmItemType == typeof( DmItemTypeArmorType ) )
        return descItemTypeArmorType[(int)type].Trim();

        // ------------------------------------------------------------------ armor descriptions - will use the fullDesc flag
      else if( dmItemType == typeof( DmItemTypeArmorExtra ) )
        if( fullDesc )
          return descItemTypeArmorExtra[(int)type].Trim();
        else
          return descItemTypeArmorExtra[(int)type].Split( ',' )[0].Trim();

      else if( dmItemType == typeof( DmItemTypeArmorHeavy ) )
        if( fullDesc )
          return descItemTypeArmorHeavy[(int)type].Trim();
        else
          return descItemTypeArmorHeavy[(int)type].Split( ',' )[0].Trim();

      else if( dmItemType == typeof( DmItemTypeArmorLight ) )
        if( fullDesc )
          return descItemTypeArmorLight[(int)type].Trim();
        else
          return descItemTypeArmorLight[(int)type].Split( ',' )[0].Trim();

      else if( dmItemType == typeof( DmItemTypeArmorMedium ) )
        if( fullDesc )
          return descItemTypeArmorMedium[(int)type].Trim();
        else
          return descItemTypeArmorMedium[(int)type].Split( ',' )[0].Trim();

      else if( dmItemType == typeof( DmItemTypeArmorShield ) )
        if( fullDesc )
          return descItemTypeArmorShield[(int)type].Trim();
        else
          return descItemTypeArmorShield[(int)type].Split( ',' )[0].Trim();

      // ------------------------------------------------------------------ clothing descriptions
      else if( dmItemType == typeof( DmItemTypeClothing ) )
        return descItemTypeClothing[(int)type].Trim();

      // ------------------------------------------------------------------ container descriptions
      else if( dmItemType == typeof( DmItemTypeContainer ) )
        if( fullDesc )
          return descItemTypeContainer[(int)type].Trim();
        else
          return descItemTypeContainer[(int)type].Split(',')[0].Trim();

        // ------------------------------------------------------------------ equipment descriptions
      else if( dmItemType == typeof( DmItemTypeEquipment ) )
        return descItemTypeEquipment[(int)type].Trim();

        // ------------------------------------------------------------------ food descriptions
      else if( dmItemType == typeof( DmItemTypeFood ) )
        return descItemTypeFood[(int)type].Trim();

        // ------------------------------------------------------------------ money descriptions
      else if( dmItemType == typeof( DmItemTypeMoney ) )
        return descItemTypeMoney[(int)type].Trim();

        // ------------------------------------------------------------------ weapon type and class descriptions
      else if( dmItemType == typeof( DmItemTypeWeaponType ) )
        return descItemTypeWeaponType[(int)type].Trim();

      else if( dmItemType == typeof( DmItemTypeWeaponClass ) )
        return descItemTypeWeaponClass[(int)type].Trim();

        // ------------------------------------------------------------------ ammo descriptions - will use the fullDesc flag
      else if( dmItemType == typeof( DmItemTypeWeaponAmmoExotic ) )
        if( fullDesc )
          return descItemTypeWeaponAmmoExotic[(int)type].Trim();
        else
          return descItemTypeWeaponAmmoExotic[(int)type].Split( ',' )[0].Trim();

      else if( dmItemType == typeof( DmItemTypeWeaponAmmoMartial ) )
        if( fullDesc )
          return descItemTypeWeaponAmmoMartial[(int)type].Trim();
        else
          return descItemTypeWeaponAmmoMartial[(int)type].Split( ',' )[0].Trim();

      else if( dmItemType == typeof( DmItemTypeWeaponAmmoSimple ) )
        if( fullDesc )
          return descItemTypeWeaponAmmoSimple[(int)type].Trim();
        else
          return descItemTypeWeaponAmmoSimple[(int)type].Split( ',' )[0].Trim();

        // ------------------------------------------------------------------ melee weapon descriptions - will use the fullDesc flag
      else if( dmItemType == typeof( DmItemTypeWeaponMeleeExotic ) )
        if( fullDesc )
          return descItemTypeWeaponMeleeExotic[(int)type].Trim();
        else
          return descItemTypeWeaponMeleeExotic[(int)type].Split( ',' )[0].Trim();

      else if( dmItemType == typeof( DmItemTypeWeaponMeleeMartial ) )
        if( fullDesc )
          return descItemTypeWeaponMeleeMartial[(int)type].Trim();
        else
          return descItemTypeWeaponMeleeMartial[(int)type].Split( ',' )[0].Trim();

      else if( dmItemType == typeof( DmItemTypeWeaponMeleeSimple ) )
        if( fullDesc )
          return descItemTypeWeaponMeleeSimple[(int)type].Trim();
        else
          return descItemTypeWeaponMeleeSimple[(int)type].Split( ',' )[0].Trim();

        // ------------------------------------------------------------------ ranged weapon descriptions - will use the fullDesc flag
      else if( dmItemType == typeof( DmItemTypeWeaponRangedExotic ) )
        if( fullDesc )
          return descItemTypeWeaponRangedExotic[(int)type].Trim();
        else
          return descItemTypeWeaponRangedExotic[(int)type].Split( ',' )[0].Trim();

      else if( dmItemType == typeof( DmItemTypeWeaponRangedMartial ) )
        if( fullDesc )
          return descItemTypeWeaponRangedMartial[(int)type].Trim();
        else
          return descItemTypeWeaponRangedMartial[(int)type].Split( ',' )[0].Trim();

      else if( dmItemType == typeof( DmItemTypeWeaponRangedSimple ) )
        if( fullDesc )
          return descItemTypeWeaponRangedSimple[(int)type].Trim();
        else
          return descItemTypeWeaponRangedSimple[(int)type].Split( ',' )[0].Trim();

      // default value : requested a description for a DmItemType we didn't expect or forgot to code.
      return "????";
    }



    /* ------------------------------------------------------------------------------------- *\
     *            The only functions visible to other parts of the program                   *
     *            These methods will create the item by various calls to the other           *
     *            methods below                                                              *
    \* ------------------------------------------------------------------------------------- */
    #region Public CreateItem(...) Methods

    /// <summary>
    /// Create a single random item
    /// </summary>
    /// <returns></returns>
    public static DmItemAbstract CreateItem()
    {
      if( !listsCreated )
        CreateLists();

      // get number of possible items to create
      int num = Enum.GetValues(typeof(DmItemType)).Length;

      // pick one at random
      DmItemType type = (DmItemType)( rand.Next( num ) );

      // create an item of that type
      return CreateItem( type );
    }

    /// <summary>
    /// Create a single random item of a specified basic type (i.e.: a weapon, or general equipment)
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static DmItemAbstract CreateItem( DmItemType type )
    {
      if( !listsCreated )
        CreateLists();

      switch( type )
      {
        case DmItemType.Money:
          return CreateMoney();

        case DmItemType.Food:
          return CreateFoodItem();

        case DmItemType.Equipment:
          return CreateEquipment();

        case DmItemType.Clothing:
          return CreateClothing();

        case DmItemType.Weapon:
          return CreateWeapon();

        case DmItemType.Armor:
          return CreateArmor();

        default: // CreateContianer by default
          return CreateContainer();
      }
    }

    /// <summary>
    /// Create an armor item
    /// </summary>
    /// <param name="iType"></param>
    /// <returns></returns>
    public static DmItemAbstract CreateItem( DmItemTypeArmorType iType )
    {
      return CreateArmor( iType );
    }

    /// <summary>
    /// Create a weapon of the specified type and class
    /// </summary>
    /// <param name="iClass"></param>
    /// <param name="iType"></param>
    /// <returns></returns>
    public static DmItemAbstract CreateItem( DmItemTypeWeaponClass iClass, DmItemTypeWeaponType iType )
    {
      return CreateWeapon( iClass, iType );
    }

    #endregion


    /* ------------------------------------------------------------------------------------- *\
     *            Utility functions used by several item creation methods                    *
    \* ------------------------------------------------------------------------------------- */
    #region Quality and Enchantment selectors
    /// <summary>
    /// Determine the quality of an item at random
    /// 20% - poor
    /// 60% - average
    /// 15% - good
    ///  5% - masterwork
    /// </summary>
    /// <returns></returns>
    private static DmItemQuality PickQuality()
    {
      int diceRoll = rand.Next( 100 )+1;

      // 20% poor quality junk
      if( diceRoll <= 20 )
        return DmItemQuality.Poor;

      // 20-80 = 60% average quality stuff
      if( diceRoll <= 80 )
        return DmItemQuality.Average;

      // 80-95 = 15% good quality treasure
      if( diceRoll <= 95 )
        return DmItemQuality.Good;

      // 95-100 = 5% masterwork quality : chance to be enchanted.  Handled per each item's generation
      return DmItemQuality.Masterwork;
    }

    /// <summary>
    /// Picks a random Armor enchantment
    /// </summary>
    /// <returns></returns>
    private static DmItemEnchantmentArmor PickEnchantmentArmor()
    {
      return (DmItemEnchantmentArmor)(rand.Next( Enum.GetValues( typeof( DmItemEnchantmentArmor ) ).Length ));
    }

    /// <summary>
    /// Picks a random Clothing enchantment
    /// </summary>
    /// <returns></returns>
    private static DmItemEnchantmentClothing PickEnchantmentClothing()
    {
      return (DmItemEnchantmentClothing)(rand.Next( Enum.GetValues( typeof( DmItemEnchantmentClothing ) ).Length ));
    }

    /// <summary>
    /// Picks a random Consumable enchantment or spell
    /// </summary>
    /// <returns></returns>
    private static DmItemEnchantmentConsumable PickEnchantmentConsumable()
    {
      return (DmItemEnchantmentConsumable)(rand.Next( Enum.GetValues( typeof( DmItemEnchantmentConsumable ) ).Length ));
    }

    /// <summary>
    /// Picks a random Weapon enchantment
    /// </summary>
    /// <returns></returns>
    private static DmItemEnchantmentWeapon PickEnchantmentWeapon()
    {
      return (DmItemEnchantmentWeapon)(rand.Next( Enum.GetValues( typeof( DmItemEnchantmentWeapon ) ).Length ));
    }
    #endregion


    /* ------------------------------------------------------------------------------------- *\
     *            Indiviual methods that will return the requested DmItem object             *
    \* ------------------------------------------------------------------------------------- */
    #region Indivdual Creators

    #region Money
    /// <summary>
    /// Create a single money item, and assign a value
    /// </summary>
    /// <returns></returns>
    private static DmItemMoney CreateMoney()
    {
      DmItemMoney item = null;

      // TODO : probably generate copper and silver more often than gold and plat... right now, it's equal chance for all.
      item = new DmItemMoney( (DmItemTypeMoney)(rand.Next( Enum.GetValues( typeof( DmItemTypeMoney ) ).Length )) );

      // 1-10 coppers, or
      // 1-10 silvers, or
      // 1-10 gold, or
      // 1-10 plat.
      item.Cost = rand.Next( 10 ) + 1;

      return item;
    }
    #endregion

    #region Food
    /// <summary>
    /// Create a single food item
    /// </summary>
    /// <returns></returns>
    private static DmItemFood CreateFoodItem()
    {
      return new DmItemFood( (DmItemTypeFood)(rand.Next( Enum.GetValues( typeof( DmItemTypeFood ) ).Length )) );
    }
    #endregion

    #region Equipment
    /// <summary>
    /// Create a random equipment item (bedrool, tent, rope, etc).
    /// </summary>
    /// <returns></returns>
    private static DmItemEquipment CreateEquipment()
    {
      DmItemEquipment item = null;

      item = new DmItemEquipment( (DmItemTypeEquipment)(rand.Next( Enum.GetValues( typeof( DmItemTypeEquipment ) ).Length )) );

      // can this chuck of gear be enchanted?  If so, set about a per-item-type random enchant.
      switch( item.Type )
      {
        case DmItemTypeEquipment.Parchment:
          if( rand.Next( 100 ) >= 95 )
          {
            item.Quality = DmItemQuality.Masterwork;
            item.Enchantment = PickEnchantmentConsumable();
          }
          break;
      }

      return item;
    }
    #endregion

    #region Containers
    /// <summary>
    /// create a pack, and some number of things it contains
    /// </summary>
    /// <returns></returns>
    private static DmItemContainer CreateContainer()
    {
      DmItemContainer item = null;

      // pick type of container
      int choices = Enum.GetValues( typeof( DmItemTypeContainer ) ).Length;
      DmItemTypeContainer choice  = (DmItemTypeContainer)rand.Next( choices );

      // create the item of that type
      item = new DmItemContainer( choice );

      // based on the type of container, pick a number of items it contains
      int numItems = 0;
      switch( choice )
      {
        case DmItemTypeContainer.ScrollCase:
          numItems = rand.Next(3)+1;
          for( int i=0; i<numItems; i++ )
          {
            DmItemEquipment tmpItem = new DmItemEquipment( DmItemTypeEquipment.Parchment );
            if( rand.Next( 100 ) > 50 )
            {
              tmpItem.Quality = DmItemQuality.Masterwork;
              tmpItem.Enchantment = PickEnchantmentConsumable();
            }
            item.Contents.Add( tmpItem );
          }
          break;

        case DmItemTypeContainer.SmallSack:
          numItems = rand.Next( 2, 4 );
          item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Money ) );
          item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Food ) );
          for( int i=0; i<numItems; i++ )
            item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Equipment ) );
          break;

        case DmItemTypeContainer.LargeSack:
          numItems = rand.Next( 3, 7 );
          item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Money ) );
          item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Food ) );
          for( int i=0; i<numItems; i++ )
            item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Equipment ) );
          break;

        case DmItemTypeContainer.Backpack:
          numItems = rand.Next( 6, 8 );
          item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Money ) );
          item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Food ) );
          for( int i=0; i<numItems; i++ )
            item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Equipment ) );
          break;

        case DmItemTypeContainer.BasketWicker:
          numItems = rand.Next( 2, 4 );
          for( int i=0; i<numItems; i++ )
            item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Food ) );
          break;

        case DmItemTypeContainer.BucketWooden:
          numItems = rand.Next( 2, 4 );
          for( int i=0; i<numItems; i++ )
            item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Food ) );
          break;

        case DmItemTypeContainer.Chest:
          numItems = rand.Next( 10, 20 );
          item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Money ) );
          item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Food ) );
          for( int i=0; i<numItems; i++ )
            item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Equipment ) );
          break;

        case DmItemTypeContainer.Barrel:
          numItems = rand.Next( 3, 8 );
          for( int i=0; i<numItems; i++ )
            item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Equipment ) );
          break;

        default: // BeltPouch contents by default
          // only money
          item.Contents.Add( DmItemFactory.CreateItem( DmItemType.Money ) );
          break;
      }

      return item;
    }
    #endregion

    #region General clothing
    /// <summary>
    /// Create a set of clothing
    /// </summary>
    /// <returns></returns>
    public static DmItemClothing CreateClothing()
    {
      // generate a random type of clothing
      return CreateClothing( (DmItemTypeClothing)(rand.Next( Enum.GetValues( typeof( DmItemTypeClothing ) ).Length )) );
    }

    /// <summary>
    /// Create a set of clothing of a specified type
    /// </summary>
    /// <returns></returns>
    public static DmItemClothing CreateClothing( DmItemTypeClothing cType )
    {
      DmItemClothing item = null;

      // set item type
      item = new DmItemClothing( cType );

      item.Quality = PickQuality();

      if( item.Quality == DmItemQuality.Masterwork )
        if( rand.Next( 100 ) > 50 )
          item.Enchantment = PickEnchantmentClothing();

      return item;
    }
    #endregion

    #region Weapons
    /// <summary>
    /// Create a random weapon
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeapon()
    {
      return CreateWeapon( (DmItemTypeWeaponClass)rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponClass ) ).Length ), 
        /* */              (DmItemTypeWeaponType )rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponType  ) ).Length ) 
        /* */            );
    }

    /// <summary>
    /// Create a weapon of the specified type (simple, martial, exotic / melee, ranged)
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeapon( DmItemTypeWeaponClass iClass, DmItemTypeWeaponType iType )
    {
      DmItemAbstract item = null;

      switch( iClass )
      {
        case DmItemTypeWeaponClass.Simple:
          switch( iType )
          {
            case DmItemTypeWeaponType.Melee:
              item = CreateWeaponMeleeSimple();
              break;

            case DmItemTypeWeaponType.Ranged:
              item = CreateWeaponRangedSimple();
              break;

            case DmItemTypeWeaponType.Ammo:
              item = CreateWeaponAmmoSimple();
              break;
          }
          break;

        case DmItemTypeWeaponClass.Martial:
          switch( iType )
          {
            case DmItemTypeWeaponType.Melee:
              item = CreateWeaponMeleeSimple();
              break;

            case DmItemTypeWeaponType.Ranged:
              item = CreateWeaponRangedSimple();
              break;

            case DmItemTypeWeaponType.Ammo:
              item = CreateWeaponAmmoMartial();
              break;
          }
          break;

        case DmItemTypeWeaponClass.Exotic:
          switch( iType )
          {
            case DmItemTypeWeaponType.Melee:
              item = CreateWeaponMeleeSimple();
              break;

            case DmItemTypeWeaponType.Ranged:
              item = CreateWeaponRangedSimple();
              break;

            case DmItemTypeWeaponType.Ammo:
              item = CreateWeaponAmmoExotic();
              break;
          }
          break;
      }

      // in case something didn't go right, and item didnt get set
      if( item == null )
        item = CreateWeaponMeleeSimple();

      // set quality
      item.Quality = PickQuality();

      // check for enchantments
      if( item.Quality == DmItemQuality.Masterwork )
        if( rand.Next( 100 ) > 50 )
          item.Enchantment = PickEnchantmentArmor();

      // return the created item
      return item;
    }

    #region Melee Weeapons
    /// <summary>
    /// Create a random melee weapon
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponMelee()
    {
      switch( rand.Next( 10 ) )
      {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
          return CreateWeaponMeleeSimple();

        case 5:
        case 6:
        case 7:
        case 8:
          return CreateWeaponMeleeMartial();

        default:
          return CreateWeaponMeleeExotic();
      }
    }

    /// <summary>
    /// Create a simple melee weapon
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponMeleeSimple()
    {
      int choice = rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponMeleeSimple ) ).Length );
      return new DmItemWeaponMeleeSimple( (DmItemTypeWeaponMeleeSimple)choice );
    }

    /// <summary>
    /// Create a martial melee weapon
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponMeleeMartial()
    {
      int choice = rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponMeleeMartial ) ).Length );
      return new DmItemWeaponMeleeMartial( (DmItemTypeWeaponMeleeMartial)choice );
    }

    /// <summary>
    /// Create an exotic melee weapon
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponMeleeExotic()
    {
      int choice = rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponMeleeExotic ) ).Length );
      return new DmItemWeaponMeleeExotic( (DmItemTypeWeaponMeleeExotic)choice );
    }
    #endregion Melee Weapons

    #region Ranged Weapons
    /// <summary>
    /// Create a random ranged weapon
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponRanged()
    {
      switch( rand.Next( 10 ) )
      {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
          return CreateWeaponRangedSimple();

        case 5:
        case 6:
        case 7:
        case 8:
          return CreateWeaponRangedMartial();

        default:
          return CreateWeaponRangedExotic();
      }
    }

    /// <summary>
    /// Create a simple ranged weapon
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponRangedSimple()
    {
      int choice = rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponRangedSimple ) ).Length );
      return new DmItemWeaponRangedSimple( (DmItemTypeWeaponRangedSimple)choice );
    }

    /// <summary>
    /// Create a martial ranged weapon
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponRangedMartial()
    {
      int choice = rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponRangedMartial ) ).Length );
      return new DmItemWeaponRangedMartial( (DmItemTypeWeaponRangedMartial)choice );
    }

    /// <summary>
    /// Create an exotic ranged weapon
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponRangedExotic()
    {
      int choice = rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponRangedExotic ) ).Length );
      return new DmItemWeaponRangedExotic( (DmItemTypeWeaponRangedExotic)choice );
    }
    #endregion Ranged Weapons

    #region Weapon Ammo
    /// <summary>
    /// Create a random ammunition item
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponAmmo()
    {
      switch( rand.Next( 10 ) )
      {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
          return CreateWeaponAmmoSimple();

        case 5:
        case 6:
        case 7:
        case 8:
          return CreateWeaponAmmoMartial();

        default:
          return CreateWeaponAmmoExotic();
      }
    }

    /// <summary>
    /// Create simple ammo
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponAmmoSimple()
    {
      int choice = rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponAmmoSimple ) ).Length );
      return new DmItemWeaponAmmoSimple( (DmItemTypeWeaponAmmoSimple)choice );
    }

    /// <summary>
    /// Create martial ammo
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponAmmoMartial()
    {
      int choice = rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponAmmoMartial ) ).Length );
      return new DmItemWeaponAmmoMartial( (DmItemTypeWeaponAmmoMartial)choice );
    }

    /// <summary>
    /// Create exotic ammo
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateWeaponAmmoExotic()
    {
      int choice = rand.Next( Enum.GetValues( typeof( DmItemTypeWeaponAmmoExotic ) ).Length );
      return new DmItemWeaponAmmoExotic( (DmItemTypeWeaponAmmoExotic)choice );
    }
    #endregion Weapon Ammo

    #endregion weapons

    #region Armor
    /// <summary>
    /// Create some armor of a random type
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateArmor()
    {
      return CreateArmor( (DmItemTypeArmorType)rand.Next( Enum.GetValues( typeof( DmItemTypeArmorType ) ).Length ) );
    }

    /// <summary>
    /// Creates a random set of armor
    /// </summary>
    /// <param name="iType"></param>
    /// <returns></returns>
    private static DmItemAbstract CreateArmor( DmItemTypeArmorType iType )
    {
      DmItemAbstract item = null;

      switch( iType )
      {
        case DmItemTypeArmorType.Medium:
          item = CreateArmorMedium();
          break;

        case DmItemTypeArmorType.Heavy:
          item = CreateArmorHeavy();
          break;

        case DmItemTypeArmorType.Shield:
          item = CreateArmorShield();
          break;

        case DmItemTypeArmorType.Extra:
          item = CreateArmorExtra();
          break;

        default: //DmItemTypeArmorType.Light:
          item = CreateArmorLight();
          break;
      }

      item.Quality = PickQuality();

      if( item.Quality == DmItemQuality.Masterwork )
        if( rand.Next( 100 ) > 50 )
          item.Enchantment = PickEnchantmentArmor();

      return item;
    }

    /// <summary>
    /// Create Light armor
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateArmorLight()
    {
      return new DmItemArmorLight( (DmItemTypeArmorLight)(rand.Next( Enum.GetValues( typeof( DmItemTypeArmorLight ) ).Length )) );
    }

    /// <summary>
    /// Create Medium armor
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateArmorMedium()
    {
      return new DmItemArmorMedium( (DmItemTypeArmorMedium)(rand.Next( Enum.GetValues( typeof( DmItemTypeArmorMedium ) ).Length )) );
    }

    /// <summary>
    /// Create Heavy armor
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateArmorHeavy()
    {
      return new DmItemArmorHeavy( (DmItemTypeArmorHeavy)(rand.Next( Enum.GetValues( typeof( DmItemTypeArmorHeavy ) ).Length )) );
    }

    /// <summary>
    /// Create a Sheild
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateArmorShield()
    {
      return new DmItemArmorShield( (DmItemTypeArmorShield)(rand.Next( Enum.GetValues( typeof( DmItemTypeArmorShield ) ).Length )) );
    }

    /// <summary>
    /// Create an armor enhancement (spikes, shained, etc)
    /// </summary>
    /// <returns></returns>
    private static DmItemAbstract CreateArmorExtra()
    {
      return new DmItemArmorExtra( (DmItemTypeArmorExtra)(rand.Next( Enum.GetValues( typeof( DmItemTypeArmorExtra ) ).Length )) );
    }
    #endregion Armor

    #endregion


  } // end ItemFactory class

}