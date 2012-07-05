using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM_Uber_Tool
{
  #region Enum Definitions

  public enum NPCRace
  {
    Dragonborne,
    Dwarf,
    Eldarin,
    Elf,
    HalfElf,
    Gnome,
    Human,
    Orc,
    HalfOrc,
    Angelic
  };

  public enum NPCGender
  {
    Male,
    Female,
  };

  public enum NPCAge
  {
    VeryYoung,
    Young,
    MiddleAged,
    Aging,
    Elderly,
  };

  public enum NPCFeatures
  {
    TatooArm,
    TattooNeck,
    ScarArm,
    ScarFace,
    HairBeads,
    EarringSingle,
    EarringMultiple,
    NoseRing,
    BraceletArm,
    NeclaceSilver,
    NeclaceGold,
    Spectcles,
    BeltDecorative,
    CloakDecorative,
    HatWideBrimmed,
    Scarf,
    Bandanna,
    WeddingBand,
    RingsGemmed,
    ArmbandSilver,
    ArmbandGold,
  };

  public enum NPCEyeColor
  {
    Blue,
    Brown,
    Green,
    SteelBlue,
    Gray,
    Red,
    Purple,
    Hazel,
  };

  public enum NPCHairColor
  {
    Blonde,
    Brown,
    Black,
    Red,
    Sandy,
    Graying,
    Gray,
    Silver,
  };

  public enum NPCHairLength
  {
    Short,
    ShoulderLength,
    Long,
  };

  public enum NPCHairStyle
  {
    Plain,
    Parted,
    TopKnot,
    Braid,
    PonyTail,
  };

  public enum NPCFacialHair
  {
    None,
    Mustache,
    Beard,
    MustacheAndBeard,
    Chops,
    Goatee,
    Stubble,
  };

  public enum NPCBodySize
  {
    Small,
    Short,
    AverageHeight,
    Tall,
    Towering,
  };

  public enum NPCBodyBuild
  {
    Slender,
    Average,
    Muscular,
    Overweight,
    Athletic,
    Wirey
  };

  public enum NPCType
  {
    Adventurer,
    NonAdventurer,
  };

  public enum NPCTypeProfession
  {
    None = 0,
    Adept,        // NPC Sorcerer
    Aritocrat,    // Politics and whatnot
    GeneralLabor, // commoner
    Waiter,
    Barkeep,
    InnKepper,
    StableMaster,
    Farmer,
    Baker,
    ArmorSmith,
    WeaponSmith,
    Fletcher,
    Bowyer,
    Apothecary,
    Marchant,
    Enchanter,
    Jeweler,
    Herbalist,
    GeneralShopkeep,
  };

  public enum NPCTypeAdventurer
  {
    None = 0,
    Barbarian,
    Bard,
    Cleric,
    Druid,
    Fighter,
    Monk,
    Paladin,
    Ranger,
    Rogue,
    Sorcerer,
    Wizard,
  };

  public enum NPCMentalState
  {
    Alert,
    Worried,
    Distracted,
    Energetic,
    Talkative,
    Fidgety,
    Tired,
    Paranoid,
    Inebriated,
    Euphoric,
  };

  public enum NPCMood
  {
    Content,
    Good,
    Happy,
    Irritable,
    Angry,
  };

  public enum NPCPersonaliy
  {
    EasilyConfused,
    Empathatic,
    Understanding,
    Tolerant,
    QuickWitted,
    Sarcastic,
    Beligerent,
    Talkative,
    Reclusive,
    AntiSocial,
    Thoughtful,
    Generous,
    Cautious,
    Careless,
    Impusive,
  };

  public enum NPCFeats
  {
    Run,
  };

  #endregion

  public static class DmNpcFactory
  {
    private static Random rand = new Random();

    private static bool listsBuilt = false;

    private static string[] descNPCAge            = new string[Enum.GetValues( typeof( NPCAge             ) ).Length];
    private static string[] descNPCBodyBuild      = new string[Enum.GetValues( typeof( NPCBodyBuild       ) ).Length];
    private static string[] descNPCBodySize       = new string[Enum.GetValues( typeof( NPCBodySize        ) ).Length];
    private static string[] descNPCEyeColor       = new string[Enum.GetValues( typeof( NPCEyeColor        ) ).Length];
    private static string[] descNPCFacialHair     = new string[Enum.GetValues( typeof( NPCFacialHair      ) ).Length];
    private static string[] descNPCFeats          = new string[Enum.GetValues( typeof( NPCFeats           ) ).Length];
    private static string[] descNPCFeatures       = new string[Enum.GetValues( typeof( NPCFeatures        ) ).Length];
    private static string[] descNPCGender         = new string[Enum.GetValues( typeof( NPCGender          ) ).Length];
    private static string[] descNPCHairColor      = new string[Enum.GetValues( typeof( NPCHairColor       ) ).Length];
    private static string[] descNPCHairLength     = new string[Enum.GetValues( typeof( NPCHairLength      ) ).Length];
    private static string[] descNPCHairStyle      = new string[Enum.GetValues( typeof( NPCHairStyle       ) ).Length];
    private static string[] descNPCMentalState    = new string[Enum.GetValues( typeof( NPCMentalState     ) ).Length];
    private static string[] descNPCMood           = new string[Enum.GetValues( typeof( NPCMood            ) ).Length];
    private static string[] descNPCPersonaliy     = new string[Enum.GetValues( typeof( NPCPersonaliy      ) ).Length];
    private static string[] descNPCRace           = new string[Enum.GetValues( typeof( NPCRace            ) ).Length];
    private static string[] descNPCType           = new string[Enum.GetValues( typeof( NPCType            ) ).Length];
    private static string[] descNPCTypeAdventurer = new string[Enum.GetValues( typeof( NPCTypeAdventurer  ) ).Length];
    private static string[] descNPCTypeProfession = new string[Enum.GetValues( typeof( NPCTypeProfession  ) ).Length];

    /// <summary>
    /// Builds the descriptions for each enum defined above
    /// </summary>
    private static void BuildDescriptionLists()
    {
      // Age
      descNPCAge[(int)NPCAge.VeryYoung] = "very young";
      descNPCAge[(int)NPCAge.Young] = "young";
      descNPCAge[(int)NPCAge.MiddleAged] = "middle-aged";
      descNPCAge[(int)NPCAge.Aging] = "aging";
      descNPCAge[(int)NPCAge.Elderly] = "elderly";

      // Body Build
      descNPCBodyBuild[(int)NPCBodyBuild.Slender] = "slender";
      descNPCBodyBuild[(int)NPCBodyBuild.Average] = "average-build";
      descNPCBodyBuild[(int)NPCBodyBuild.Muscular] = "muscular";
      descNPCBodyBuild[(int)NPCBodyBuild.Overweight] = "overweight";
      descNPCBodyBuild[(int)NPCBodyBuild.Athletic] = "athletic";
      descNPCBodyBuild[(int)NPCBodyBuild.Wirey] = "wirey";

      // Body Size
      descNPCBodySize[(int)NPCBodySize.Small] = "small";
      descNPCBodySize[(int)NPCBodySize.Short] = "short";
      descNPCBodySize[(int)NPCBodySize.AverageHeight] = "average-height";
      descNPCBodySize[(int)NPCBodySize.Tall] = "tall";
      descNPCBodySize[(int)NPCBodySize.Towering] = "towering";

      // Eye Color
      descNPCEyeColor[(int)NPCEyeColor.Blue] = "blue";
      descNPCEyeColor[(int)NPCEyeColor.Brown] = "brown";
      descNPCEyeColor[(int)NPCEyeColor.Green] = "green";
      descNPCEyeColor[(int)NPCEyeColor.SteelBlue] = "steel-blue";
      descNPCEyeColor[(int)NPCEyeColor.Gray] = "gray";
      descNPCEyeColor[(int)NPCEyeColor.Red] = "red";
      descNPCEyeColor[(int)NPCEyeColor.Purple] = "purple";
      descNPCEyeColor[(int)NPCEyeColor.Hazel] = "hazel";

      // Facial Hair
      descNPCFacialHair[(int)NPCFacialHair.None] = "none";
      descNPCFacialHair[(int)NPCFacialHair.Mustache] = "mustache";
      descNPCFacialHair[(int)NPCFacialHair.Beard] = "beard";
      descNPCFacialHair[(int)NPCFacialHair.MustacheAndBeard] = "mustache and beard";
      descNPCFacialHair[(int)NPCFacialHair.Chops] = "chops";
      descNPCFacialHair[(int)NPCFacialHair.Goatee] = "goatee";
      descNPCFacialHair[(int)NPCFacialHair.Stubble] = "stubble";

      // Feats
      descNPCFeats[(int)NPCFeats.Run] = "run";

      // Features
      descNPCFeatures[(int)NPCFeatures.TatooArm] = "a tattoo on their arm";
      descNPCFeatures[(int)NPCFeatures.TattooNeck] = "a tattoo on their neck";
      descNPCFeatures[(int)NPCFeatures.ScarArm] = "a scar on their arm";
      descNPCFeatures[(int)NPCFeatures.ScarFace] = "a scar across their face";
      descNPCFeatures[(int)NPCFeatures.HairBeads] = "hair beads";
      descNPCFeatures[(int)NPCFeatures.EarringSingle] = "a single earring";
      descNPCFeatures[(int)NPCFeatures.EarringMultiple] = "multiple earrings";
      descNPCFeatures[(int)NPCFeatures.NoseRing] = "a nose ring";
      descNPCFeatures[(int)NPCFeatures.BraceletArm] = "an arm bracelet";
      descNPCFeatures[(int)NPCFeatures.NeclaceSilver] = "a silver necklace";
      descNPCFeatures[(int)NPCFeatures.NeclaceGold] = "a gold necklace";
      descNPCFeatures[(int)NPCFeatures.Spectcles] = "spectcles";
      descNPCFeatures[(int)NPCFeatures.BeltDecorative] = "a decorative belt";
      descNPCFeatures[(int)NPCFeatures.CloakDecorative] = "a decorative cloak";
      descNPCFeatures[(int)NPCFeatures.HatWideBrimmed] = "a wide-brimmed hat";
      descNPCFeatures[(int)NPCFeatures.Scarf] = "a scarf";
      descNPCFeatures[(int)NPCFeatures.Bandanna] = "a bandanna";
      descNPCFeatures[(int)NPCFeatures.WeddingBand] = "a wedding band";
      descNPCFeatures[(int)NPCFeatures.RingsGemmed] = "several gemmed rings";
      descNPCFeatures[(int)NPCFeatures.ArmbandSilver] = "a silver armband";
      descNPCFeatures[(int)NPCFeatures.ArmbandGold] = "a gold armband";

      // Gender
      descNPCGender[(int)NPCGender.Male] = "male";
      descNPCGender[(int)NPCGender.Female] = "female";

      // Hair Color
      descNPCHairColor[(int)NPCHairColor.Blonde] = "blonde";
      descNPCHairColor[(int)NPCHairColor.Brown] = "brown";
      descNPCHairColor[(int)NPCHairColor.Black] = "black";
      descNPCHairColor[(int)NPCHairColor.Red] = "red";
      descNPCHairColor[(int)NPCHairColor.Sandy] = "sandy";
      descNPCHairColor[(int)NPCHairColor.Graying] = "graying";
      descNPCHairColor[(int)NPCHairColor.Gray] = "gray";
      descNPCHairColor[(int)NPCHairColor.Silver] = "silver";

      // Hair Length
      descNPCHairLength[(int)NPCHairLength.Short] = "short";
      descNPCHairLength[(int)NPCHairLength.ShoulderLength] = "shoulder-length";
      descNPCHairLength[(int)NPCHairLength.Long] = "long";

      // Hair Style
      descNPCHairStyle[(int)NPCHairStyle.Plain] = "plain";
      descNPCHairStyle[(int)NPCHairStyle.Parted] = "parted on one side";
      descNPCHairStyle[(int)NPCHairStyle.TopKnot] = "in a top-knot";
      descNPCHairStyle[(int)NPCHairStyle.Braid] = "in a braid";
      descNPCHairStyle[(int)NPCHairStyle.PonyTail] = "in a ponytail";

      // Mental State
      descNPCMentalState[(int)NPCMentalState.Alert] = "alert";
      descNPCMentalState[(int)NPCMentalState.Worried] = "worried";
      descNPCMentalState[(int)NPCMentalState.Distracted] = "distracted";
      descNPCMentalState[(int)NPCMentalState.Energetic] = "energetic";
      descNPCMentalState[(int)NPCMentalState.Talkative] = "talkative";
      descNPCMentalState[(int)NPCMentalState.Fidgety] = "fidgety";
      descNPCMentalState[(int)NPCMentalState.Tired] = "tired";
      descNPCMentalState[(int)NPCMentalState.Paranoid] = "paranoid";
      descNPCMentalState[(int)NPCMentalState.Inebriated] = "inebriated";
      descNPCMentalState[(int)NPCMentalState.Euphoric] = "euphoric";

      // Mood
      descNPCMood[(int)NPCMood.Content] = "content";
      descNPCMood[(int)NPCMood.Good] = "good";
      descNPCMood[(int)NPCMood.Happy] = "happy";
      descNPCMood[(int)NPCMood.Irritable] = "irritable";
      descNPCMood[(int)NPCMood.Angry] = "angry";

      // Personality
      descNPCPersonaliy[(int)NPCPersonaliy.EasilyConfused] = "easily confused";
      descNPCPersonaliy[(int)NPCPersonaliy.Empathatic] = "empathatic";
      descNPCPersonaliy[(int)NPCPersonaliy.Understanding] = "understanding";
      descNPCPersonaliy[(int)NPCPersonaliy.Tolerant] = "tolerant";
      descNPCPersonaliy[(int)NPCPersonaliy.QuickWitted] = "quickwitted";
      descNPCPersonaliy[(int)NPCPersonaliy.Sarcastic] = "sarcastic";
      descNPCPersonaliy[(int)NPCPersonaliy.Beligerent] = "beligerent";
      descNPCPersonaliy[(int)NPCPersonaliy.Talkative] = "talkative";
      descNPCPersonaliy[(int)NPCPersonaliy.Reclusive] = "reclusive";
      descNPCPersonaliy[(int)NPCPersonaliy.AntiSocial] = "antisocial";
      descNPCPersonaliy[(int)NPCPersonaliy.Thoughtful] = "thoughtful";
      descNPCPersonaliy[(int)NPCPersonaliy.Generous] = "generous";
      descNPCPersonaliy[(int)NPCPersonaliy.Cautious] = "cautious";
      descNPCPersonaliy[(int)NPCPersonaliy.Careless] = "careless";
      descNPCPersonaliy[(int)NPCPersonaliy.Impusive] = "impusive";

      // Race
      descNPCRace[(int)NPCRace.Dragonborne] = "dragon-borne";
      descNPCRace[(int)NPCRace.Dwarf] = "dwarf";
      descNPCRace[(int)NPCRace.Eldarin] = "eldarin";
      descNPCRace[(int)NPCRace.Elf] = "elf";
      descNPCRace[(int)NPCRace.HalfElf] = "halfelf";
      descNPCRace[(int)NPCRace.Gnome] = "gnome";
      descNPCRace[(int)NPCRace.Human] = "human";
      descNPCRace[(int)NPCRace.Orc] = "orc";
      descNPCRace[(int)NPCRace.HalfOrc] = "halforc";
      descNPCRace[(int)NPCRace.Angelic] = "angelic";

      // Type
      descNPCType[(int)NPCType.Adventurer] = "adventurer";
      descNPCType[(int)NPCType.NonAdventurer] = "non-adventurer";

      // Class
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.None] = "none";        // they have a profession and are not adventurers
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Barbarian] = "barbarian";
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Bard] = "bard";
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Cleric] = "cleric";
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Druid] = "druid";
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Fighter] = "fighter";
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Monk] = "monk";
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Paladin] = "paladin";
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Ranger] = "ranger";
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Rogue] = "rogue";
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Sorcerer] = "sorcerer";
      descNPCTypeAdventurer[(int)NPCTypeAdventurer.Wizard] = "wizard";

      // Profession
      descNPCTypeProfession[(int)NPCTypeProfession.None] = "none";       // they are an adventurer and have no profession, -or- they are NOT an adventurer and are unemployed (beggars?)
      descNPCTypeProfession[(int)NPCTypeProfession.Adept] = "adept";
      descNPCTypeProfession[(int)NPCTypeProfession.Aritocrat] = "aritocrat";
      descNPCTypeProfession[(int)NPCTypeProfession.GeneralLabor] = "laborer";
      descNPCTypeProfession[(int)NPCTypeProfession.Waiter] = "waiter";
      descNPCTypeProfession[(int)NPCTypeProfession.Barkeep] = "barkeep";
      descNPCTypeProfession[(int)NPCTypeProfession.InnKepper] = "inn kepper";
      descNPCTypeProfession[(int)NPCTypeProfession.StableMaster] = "stable master";
      descNPCTypeProfession[(int)NPCTypeProfession.Farmer] = "farmer";
      descNPCTypeProfession[(int)NPCTypeProfession.Baker] = "baker";
      descNPCTypeProfession[(int)NPCTypeProfession.ArmorSmith] = "armor smith";
      descNPCTypeProfession[(int)NPCTypeProfession.WeaponSmith] = "weapon smith";
      descNPCTypeProfession[(int)NPCTypeProfession.Fletcher] = "fletcher";
      descNPCTypeProfession[(int)NPCTypeProfession.Bowyer] = "bowyer";
      descNPCTypeProfession[(int)NPCTypeProfession.Apothecary] = "apothecary";
      descNPCTypeProfession[(int)NPCTypeProfession.Marchant] = "marchant";
      descNPCTypeProfession[(int)NPCTypeProfession.Enchanter] = "enchanter";
      descNPCTypeProfession[(int)NPCTypeProfession.Jeweler] = "jeweler";
      descNPCTypeProfession[(int)NPCTypeProfession.Herbalist] = "herbalist";
      descNPCTypeProfession[(int)NPCTypeProfession.GeneralShopkeep] = "general shopkeep";

      listsBuilt = true;
    }

    /// <summary>
    /// Gets a paragraph-style description for this NPC
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetDescription( object type )
    {
      if( !listsBuilt )
        BuildDescriptionLists();

      Type dmItemType = type.GetType();

      if( dmItemType == typeof( NPCAge ) )
        return descNPCAge[(int)type];

      else if( dmItemType == typeof( NPCBodyBuild ) )
        return descNPCBodyBuild[(int)type];

      else if( dmItemType == typeof( NPCBodySize ) )
        return descNPCBodySize[(int)type];

      else if( dmItemType == typeof( NPCEyeColor ) )
        return descNPCEyeColor[(int)type];

      else if( dmItemType == typeof( NPCFacialHair ) )
        return descNPCFacialHair[(int)type];

      else if( dmItemType == typeof( NPCFeats ) )
        return descNPCFeats[(int)type];

      else if( dmItemType == typeof( NPCFeatures ) )
        return descNPCFeatures[(int)type];

      else if( dmItemType == typeof( NPCGender ) )
        return descNPCGender[(int)type];

      else if( dmItemType == typeof( NPCHairColor ) )
        return descNPCHairColor[(int)type];

      else if( dmItemType == typeof( NPCHairLength ) )
        return descNPCHairLength[(int)type];

      else if( dmItemType == typeof( NPCHairStyle ) )
        return descNPCHairStyle[(int)type];

      else if( dmItemType == typeof( NPCMentalState ) )
        return descNPCMentalState[(int)type];

      else if( dmItemType == typeof( NPCMood ) )
        return descNPCMood[(int)type];

      else if( dmItemType == typeof( NPCPersonaliy ) )
        return descNPCPersonaliy[(int)type];

      else if( dmItemType == typeof( NPCRace ) )
        return descNPCRace[(int)type];

      else if( dmItemType == typeof( NPCType ) )
        return descNPCType[(int)type];
      
      else if ( dmItemType == typeof( NPCType ) )
        return descNPCType[(int)type];

      else if( dmItemType == typeof( NPCTypeAdventurer ) )
        return descNPCTypeAdventurer[(int)type];

      else if( dmItemType == typeof( NPCTypeProfession ) )
        return descNPCTypeProfession[(int)type];

      // default value : requested a description for a DmItemType we didn't expect
      return "????";
    }

    /// <summary>
    /// Generte a random NPC : name, race, age, gender, profession, equipment and basic stats
    /// </summary>
    /// <returns></returns>
    public static DmNPC GenerateNPC()
    {
      return GenerateNPC( (NPCType)rand.Next( Enum.GetValues( typeof(NPCType) ).Length ) );
    }

    /// <summary>
    /// Generate a random NPC of the specified type (Profession or Adventurer)
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static DmNPC GenerateNPC( NPCType type )
    {
      DmNPC npc = new DmNPC();

      npc.race    = (NPCRace)rand.Next( Enum.GetValues( typeof(NPCRace) ).Length );
      AdjustStatsForRace( npc );

      npc.gender  = (NPCGender)rand.Next( Enum.GetValues( typeof(NPCGender) ).Length );
      npc.name    = DmNameGenerator.GenerateName( npc.race, npc.gender );

      npc.age     = (NPCAge)rand.Next( Enum.GetValues( typeof(NPCAge) ).Length );
      int numFeatures = rand.Next(11)-5;

      for( int i=0; i<numFeatures; i++ )
      {
        NPCFeatures feature = (NPCFeatures)rand.Next( Enum.GetValues( typeof( NPCFeatures ) ).Length );

        while( npc.features.Contains( feature ) )
          feature = (NPCFeatures)rand.Next( Enum.GetValues( typeof( NPCFeatures ) ).Length );

        npc.features.Add( feature );
      }

      npc.eyeColor = (NPCEyeColor)rand.Next( Enum.GetValues( typeof( NPCEyeColor ) ).Length );
      
      npc.hairColor = (NPCHairColor)rand.Next( Enum.GetValues( typeof( NPCHairColor ) ).Length );
      npc.hairStyle = (NPCHairStyle)rand.Next( Enum.GetValues( typeof( NPCHairStyle ) ).Length );
      npc.hairLength= (NPCHairLength)rand.Next( Enum.GetValues( typeof( NPCHairLength ) ).Length );
      
      if( npc.gender == NPCGender.Male )
        npc.facialHair = (NPCFacialHair)rand.Next( Enum.GetValues( typeof( NPCFacialHair ) ).Length );

      npc.bodySize = (NPCBodySize)rand.Next( Enum.GetValues( typeof( NPCBodySize ) ).Length );
      npc.bodyBuild = (NPCBodyBuild)rand.Next( Enum.GetValues( typeof( NPCBodyBuild ) ).Length );

      npc.characterType = type;
      if( npc.characterType == NPCType.Adventurer )
      {
        // make sure we choose something other than 'None'
        NPCTypeAdventurer iType = NPCTypeAdventurer.None;

        while( iType == NPCTypeAdventurer.None )
          iType = (NPCTypeAdventurer)rand.Next( Enum.GetValues( typeof( NPCTypeAdventurer ) ).Length );

        npc.characterClass = iType;
      }
      else
      {
        // unlike adventurer types, Profession can be 'None' - which indicates some sort of beggar or unemployed commoner
        npc.characterProfession= (NPCTypeProfession)rand.Next( Enum.GetValues( typeof( NPCTypeProfession ) ).Length );
      }

      npc.clothing  = GenerateClothing( npc );
      npc.armor     = GenerateArmor( npc );
      npc.weapons   = GenerateWeapons( npc );
      npc.inventory = GenerateInventory( npc );

      npc.mentalState = (NPCMentalState)rand.Next( Enum.GetValues( typeof( NPCMentalState ) ).Length );
      npc.mood        = (NPCMood)rand.Next( Enum.GetValues( typeof( NPCMood ) ).Length );
      npc.personality = (NPCPersonaliy)rand.Next( Enum.GetValues( typeof( NPCPersonaliy ) ).Length );
      
      return npc;
    }

    /// <summary>
    /// Adjusts the base stats for this NOPC based on their race
    /// </summary>
    /// <param name="npc"></param>
    private static void AdjustStatsForRace( DmNPC npc )
    {
      switch( npc.race )
      {
        case NPCRace.Angelic:
        case NPCRace.Dragonborne:
        case NPCRace.Eldarin:
        case NPCRace.Orc:
          // no stat adjustment numbers for these yet...
          return;

        case NPCRace.Dwarf:
          npc.statCON += 2;
          npc.statCHR -= 2;
          break;

        case NPCRace.Elf:
          npc.statCON -= 2;
          npc.statDEX += 2;
          break;

        case NPCRace.Gnome:
          npc.statSTR -= 2;
          npc.statCON += 2;
          break;

        case NPCRace.HalfElf:
          // nothing special
          break;

        case NPCRace.HalfOrc:
          npc.statSTR += 2;
          npc.statINT -= 2;
          npc.statCHR -= 2;
          break;

        case NPCRace.Human:
          // nothing special
          break;
      }
    }

    /// <summary>
    /// PIcks a style of clothing for the NPC 
    /// </summary>
    /// <param name="npc"></param>
    /// <returns></returns>
    private static DmItemClothing GenerateClothing( DmNPC npc )
    {
      //DmItemClothing item;
      List<DmItemTypeClothing> choices = new List<DmItemTypeClothing>();
      switch( npc.characterType )
      {
        case NPCType.Adventurer:
          // for NPC base class types, the clothing is essentially what they've got under their armor.
          // This list is completely arbitary, and purely cosmetic

          // TODO : Except for Wizards and Sorcerers, which cannot wear armor, and Monks that probably don't want to wear armor.
          switch( npc.characterClass )
          {
            case NPCTypeAdventurer.Bard:
              choices.Add( DmItemTypeClothing.ArtisanOutfit );
              choices.Add( DmItemTypeClothing.CourtiersOutfit );
              choices.Add( DmItemTypeClothing.EntertainersOutfit );
              break;

            case NPCTypeAdventurer.Barbarian:
            case NPCTypeAdventurer.Fighter:
              choices.Add( DmItemTypeClothing.ColdWeatherOutfit );
              choices.Add( DmItemTypeClothing.ExplorersOutfit );
              choices.Add( DmItemTypeClothing.PeasantsOutfit );
              choices.Add( DmItemTypeClothing.TravelersOutfit );
              break;

            case NPCTypeAdventurer.Cleric:
              choices.Add( DmItemTypeClothing.NoblesOutfit );
              choices.Add( DmItemTypeClothing.ClericVestments );
              break;

            case NPCTypeAdventurer.Rogue:
            case NPCTypeAdventurer.Ranger:
              choices.Add( DmItemTypeClothing.ColdWeatherOutfit );
              choices.Add( DmItemTypeClothing.ExplorersOutfit );
              choices.Add( DmItemTypeClothing.TravelersOutfit );
              break;

            case NPCTypeAdventurer.Druid:
            case NPCTypeAdventurer.Monk:
              choices.Add( DmItemTypeClothing.NoblesOutfit );
              choices.Add( DmItemTypeClothing.MonksOutfit );
              choices.Add( DmItemTypeClothing.TravelersOutfit );
              break;

            case NPCTypeAdventurer.Paladin:
              choices.Add( DmItemTypeClothing.TravelersOutfit );
              choices.Add( DmItemTypeClothing.NoblesOutfit );
              choices.Add( DmItemTypeClothing.ArtisanOutfit );
              break;

            case NPCTypeAdventurer.Sorcerer:
            case NPCTypeAdventurer.Wizard:
              choices.Add( DmItemTypeClothing.NoblesOutfit );
              choices.Add( DmItemTypeClothing.ArtisanOutfit );
              choices.Add( DmItemTypeClothing.ScholarsOutfit );
              choices.Add( DmItemTypeClothing.CastersRobesElaborate );
              choices.Add( DmItemTypeClothing.CastersRobesFine );
              choices.Add( DmItemTypeClothing.CastersRobesSimple );
              break;
          }
          break;

        case NPCType.NonAdventurer:
          // for non-class NPCs, the outfit is their current (and possibly only) garb.
          switch( npc.characterProfession )
          {
            case NPCTypeProfession.Adept:
            case NPCTypeProfession.Apothecary:
            case NPCTypeProfession.Aritocrat:
            case NPCTypeProfession.Enchanter:
            case NPCTypeProfession.Herbalist:
              choices.Add( DmItemTypeClothing.ArtisanOutfit );
              choices.Add( DmItemTypeClothing.ClericVestments );
              choices.Add( DmItemTypeClothing.NoblesOutfit );
              choices.Add( DmItemTypeClothing.RoyalOutfit );
              choices.Add( DmItemTypeClothing.ScholarsOutfit );
              choices.Add( DmItemTypeClothing.CastersRobesElaborate );
              choices.Add( DmItemTypeClothing.CastersRobesFine );
              choices.Add( DmItemTypeClothing.CastersRobesSimple );
              break;

            case NPCTypeProfession.Baker:
            case NPCTypeProfession.Barkeep:
            case NPCTypeProfession.GeneralLabor:
            case NPCTypeProfession.GeneralShopkeep:
            case NPCTypeProfession.Farmer:
            case NPCTypeProfession.Waiter:
            case NPCTypeProfession.InnKepper:
            case NPCTypeProfession.Marchant:
              choices.Add( DmItemTypeClothing.ArtisanOutfit );
              choices.Add( DmItemTypeClothing.EntertainersOutfit );
              choices.Add( DmItemTypeClothing.ExplorersOutfit );
              choices.Add( DmItemTypeClothing.PeasantsOutfit );
              break;

            case NPCTypeProfession.Bowyer:
            case NPCTypeProfession.Fletcher:
            case NPCTypeProfession.Jeweler:
            case NPCTypeProfession.StableMaster:
            case NPCTypeProfession.ArmorSmith:
            case NPCTypeProfession.WeaponSmith:
              choices.Add( DmItemTypeClothing.PeasantsOutfit );
              break;

            default:  // if profession is 'None', they're unemployed... but they're still not naked.
              choices.Add(DmItemTypeClothing.PeasantsOutfit);
              break;
          }
          break;
      }

      // given the specified list of acceptable clothing for this NPC's class, pick one at random.
      return DmItemFactory.CreateClothing( choices[ rand.Next( choices.Count ) ] );
    }

    /// <summary>
    /// Randomly assigns some armor to the NPC
    /// </summary>
    /// <param name="npc"></param>
    /// <returns></returns>
    private static List<DmItemAbstract> GenerateWeapons( DmNPC npc )
    {
      List<DmItemAbstract>        list    = new List<DmItemAbstract>();

      List<DmItemTypeWeaponClass>  weaponsClasses = new List<DmItemTypeWeaponClass>();
      List<DmItemTypeWeaponType>   weaponsTypes   = new List<DmItemTypeWeaponType>();

      switch( npc.characterClass )
      {
        case NPCTypeAdventurer.Barbarian:
          // weapons : simple
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          weaponsTypes.Add( DmItemTypeWeaponType.Ranged );
          break;

        case NPCTypeAdventurer.Bard:
          // weapons : simple weapons + longsword, rapier, sap, short sword, shortbow, and whip
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          weaponsTypes.Add( DmItemTypeWeaponType.Ranged );

          break;

        case NPCTypeAdventurer.Cleric:
          // weapons : blunt only, (unless exceptions allow)
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsClasses.Add( DmItemTypeWeaponClass.Martial );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          break;

        case NPCTypeAdventurer.Druid:
          // weapons : club, dagger, dart, halfspear, longspear, quarterstaff, scimitar, sickle, shortspear, and sling
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          break;

        case NPCTypeAdventurer.Fighter:
          // weapons : simple + martial
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsClasses.Add( DmItemTypeWeaponClass.Martial );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          weaponsTypes.Add( DmItemTypeWeaponType.Ranged );
          break;

        case NPCTypeAdventurer.Monk:
          // weapons : Simple peasant weapons and club, crossbow (light or heavy), dagger, handaxe, javelin, kama, nunchaku, quarterstaff, shuriken, siangham, sling
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsClasses.Add( DmItemTypeWeaponClass.Martial );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          weaponsTypes.Add( DmItemTypeWeaponType.Ranged );
          break;

        case NPCTypeAdventurer.Paladin:
          // weapons : simple + martial
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsClasses.Add( DmItemTypeWeaponClass.Martial );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          weaponsTypes.Add( DmItemTypeWeaponType.Ranged );
          break;

        case NPCTypeAdventurer.Ranger:
          // weapons : simple + martial
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsClasses.Add( DmItemTypeWeaponClass.Martial );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          weaponsTypes.Add( DmItemTypeWeaponType.Ranged );
          break;

        case NPCTypeAdventurer.Rogue:
          // weapons : crossbow (hand or light), dagger (any type), dart, light mace, sap, shortbow (normal and composite), and short sword,
          //           medium-sized races and up also get : club, heavy crossbow, heavy mace, morningstar, quarterstaff, and rapier
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsClasses.Add( DmItemTypeWeaponClass.Martial );
          weaponsClasses.Add( DmItemTypeWeaponClass.Exotic );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          weaponsTypes.Add( DmItemTypeWeaponType.Ranged );
          break;

        case NPCTypeAdventurer.Sorcerer:
          // weapons : simple
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          weaponsTypes.Add( DmItemTypeWeaponType.Ranged );
          break;

        case NPCTypeAdventurer.Wizard:
          // weapons : club, dagger, heavy crossbow, light crossbow, and quarterstaff
          weaponsClasses.Add( DmItemTypeWeaponClass.Simple );
          weaponsTypes.Add( DmItemTypeWeaponType.Melee );
          weaponsTypes.Add( DmItemTypeWeaponType.Ranged );
          break;

        default: // NPCTypeAdventurer.None : no armor for non-adventurer types
          DmItemWeaponMeleeSimple item = new DmItemWeaponMeleeSimple(DmItemTypeWeaponMeleeSimple.Dirk);
          item.Quality = DmItemQuality.Poor;

          list.Add( item );  // default - beggars have a small, crappy knife.
          break;
      }

      if( weaponsClasses.Count() > 0 )
      {
        int numWeapons = rand.Next( 3 )+1;
        for( int i=0; i<numWeapons; i++ )
        {
          list.Add( DmItemFactory.CreateItem( weaponsClasses[rand.Next( weaponsClasses.Count() )], 
            /* */                             weaponsTypes  [rand.Next( weaponsTypes  .Count() )] 
            /* */                           )
            /* */ );
          // TODO : special conditions for classes with specific lists.
          // TODO : make sure the items in the lists are in the list of weapons enum...
        }
      }

      return list;
    }

    /// <summary>
    /// Randomly assigns weapons to the NPC
    /// </summary>
    /// <param name="npc"></param>
    /// <returns></returns>
    private static List<DmItemAbstract> GenerateArmor( DmNPC npc )
    {
      List<DmItemAbstract> list = new List<DmItemAbstract>();
      
      List<DmItemTypeArmorType> armors = new List<DmItemTypeArmorType>();
      List<DmItemTypeArmorShield> shields = new List<DmItemTypeArmorShield>();

      switch( npc.characterClass )
      {
        case NPCTypeAdventurer.Barbarian:
          // armor   : light, medium
          armors.Add( DmItemTypeArmorType.Light );
          armors.Add( DmItemTypeArmorType.Medium );

          // sheild  : any
          shields.Add( DmItemTypeArmorShield.Buckler );
          shields.Add( DmItemTypeArmorShield.HeavySteel );
          shields.Add( DmItemTypeArmorShield.HeavyWooden );
          shields.Add( DmItemTypeArmorShield.LightSteel );
          shields.Add( DmItemTypeArmorShield.LightWooden );
          shields.Add( DmItemTypeArmorShield.Tower );
          break;

        case NPCTypeAdventurer.Bard:
          // aarmor  : any (light = no casting penalties, though)
          armors.Add( DmItemTypeArmorType.Light );

          // shield  : any except Tower
          shields.Add( DmItemTypeArmorShield.Buckler );
          shields.Add( DmItemTypeArmorShield.HeavySteel );
          shields.Add( DmItemTypeArmorShield.HeavyWooden );
          shields.Add( DmItemTypeArmorShield.LightSteel );
          shields.Add( DmItemTypeArmorShield.LightWooden );
          break;

        case NPCTypeAdventurer.Cleric:
          // armor   : any
          armors.Add( DmItemTypeArmorType.Light );
          armors.Add( DmItemTypeArmorType.Medium );
          armors.Add( DmItemTypeArmorType.Heavy );

          // shield  : any
          shields.Add( DmItemTypeArmorShield.Buckler );
          shields.Add( DmItemTypeArmorShield.HeavySteel );
          shields.Add( DmItemTypeArmorShield.HeavyWooden );
          shields.Add( DmItemTypeArmorShield.LightSteel );
          shields.Add( DmItemTypeArmorShield.LightWooden );
          shields.Add( DmItemTypeArmorShield.Tower );
          break;

        case NPCTypeAdventurer.Druid:
          // armor   : light and medium armors but are prohibited from wearing metal armor (thus, they may wear only padded, leather, or hide armor)
          armors.Add( DmItemTypeArmorType.Light );
          armors.Add( DmItemTypeArmorType.Medium );
          armors.Add( DmItemTypeArmorType.Heavy );

          // sheild  : wooden only
          shields.Add( DmItemTypeArmorShield.Buckler );
          shields.Add( DmItemTypeArmorShield.HeavyWooden );
          shields.Add( DmItemTypeArmorShield.LightWooden );
          break;

        case NPCTypeAdventurer.Fighter:
          // armor   : any
          armors.Add( DmItemTypeArmorType.Light );
          armors.Add( DmItemTypeArmorType.Medium );
          armors.Add( DmItemTypeArmorType.Heavy );

          // shields : any
          shields.Add( DmItemTypeArmorShield.Buckler );
          shields.Add( DmItemTypeArmorShield.HeavySteel );
          shields.Add( DmItemTypeArmorShield.HeavyWooden );
          shields.Add( DmItemTypeArmorShield.LightSteel );
          shields.Add( DmItemTypeArmorShield.LightWooden );
          shields.Add( DmItemTypeArmorShield.Tower );
          break;

        case NPCTypeAdventurer.Monk:
          // armor   : preferred none - penalties induced for wearing anything but simple clothing.
          // shield  : none
          break;

        case NPCTypeAdventurer.Paladin:
          // armor   : any
          armors.Add( DmItemTypeArmorType.Light );
          armors.Add( DmItemTypeArmorType.Medium );
          armors.Add( DmItemTypeArmorType.Heavy );

          // shields : any
          shields.Add( DmItemTypeArmorShield.Buckler );
          shields.Add( DmItemTypeArmorShield.HeavySteel );
          shields.Add( DmItemTypeArmorShield.HeavyWooden );
          shields.Add( DmItemTypeArmorShield.LightSteel );
          shields.Add( DmItemTypeArmorShield.LightWooden );
          shields.Add( DmItemTypeArmorShield.Tower );
          break;

        case NPCTypeAdventurer.Ranger:
          // armor   : light, medium
          armors.Add( DmItemTypeArmorType.Light );
          armors.Add( DmItemTypeArmorType.Medium );

          // shields : any
          shields.Add( DmItemTypeArmorShield.Buckler );
          shields.Add( DmItemTypeArmorShield.HeavySteel );
          shields.Add( DmItemTypeArmorShield.HeavyWooden );
          shields.Add( DmItemTypeArmorShield.LightSteel );
          shields.Add( DmItemTypeArmorShield.LightWooden );
          shields.Add( DmItemTypeArmorShield.Tower );
          break;

        case NPCTypeAdventurer.Rogue:
          // armor   : light
          armors.Add( DmItemTypeArmorType.Light );

          // shields : none
          break;

        case NPCTypeAdventurer.Sorcerer:
          // armor   : none (robes only)
          // shields : none
          break;

        case NPCTypeAdventurer.Wizard:
          // armor   : none (robes only)
          // shields : none
          break;

        default: // NPCTypeAdventurer.None : no armor for non-adventurer types
          break;
      }

      if( armors.Count() > 0 )
      {
        list.Add( DmItemFactory.CreateItem( armors[rand.Next( armors.Count() )] ) );

        // TODO : special case for Druids - no metal armor allowed.
      }

      if( shields.Count() > 0 )
      {
        // arbitrary - if they /can/ have a shield, it doesn't mean they /do/...  40% chance they're packin' protection.
        if( rand.Next(100) < 40 )
          list.Add( new DmItemArmorShield( shields[ rand.Next(shields.Count()) ] ) );

        // TODO : special case for druids?
      }

      return list;
    }

    /// <summary>
    /// Generate some general goods the NPC has with them
    /// </summary>
    /// <param name="npc"></param>
    /// <returns></returns>
    private static List<DmItemAbstract> GenerateInventory( DmNPC npc )
    {
      List<DmItemAbstract> list = new List<DmItemAbstract>();

      // Money :
      //  Adventurers should have a money pouch. Maybe more.
 
      //  Commoners may or may not have one on their person.

      // Packs :
      //  Adventurers should be created with at least one, and it should have a fairly basic set 
      //    (bedrool, food, light source) as well as other goods.)
      //    Additional packs  may be generated, but increasingly less likely.

      //  Commoners may or may not have a pack with them.  If they do, it shall be created completely at random 
      //    (who knows when a farmer might wander into a bar with a single sheet of parchment, an empty scroll case, and a ladder?

      switch( npc.characterType )
      {
        case NPCType.Adventurer:
          {
            // create the money pouch
            DmItemContainer beltpouch = new DmItemContainer( DmItemTypeContainer.BeltPouch );

            // add some random type/amount of money
            beltpouch.Contents.Add( DmItemFactory.CreateItem( DmItemType.Money ) );
            beltpouch.Contents.Add( DmItemFactory.CreateItem( DmItemType.Money ) );
            beltpouch.Contents.Add( DmItemFactory.CreateItem( DmItemType.Money ) );

            // add the filled pouch to the list
            list.Add( beltpouch );


            // Good chance for 1 bag...
            if( rand.Next( 100 ) < 90 )
            {
              list.Add( DmItemFactory.CreateItem( DmItemType.Container ) );

              // if they have one, small chance for a 2nd...
              if( rand.Next( 100 ) < 30 )
              {
                list.Add( DmItemFactory.CreateItem( DmItemType.Container ) );

                // if they have 2, _really_ slim chance for a 3rd.
                if( rand.Next( 100 ) <  5 )
                  list.Add( DmItemFactory.CreateItem( DmItemType.Container ) );
              }
            }
          }
          break;

        default:
          {
            if( npc.characterProfession == NPCTypeProfession.None )
            {
              // beggar?  slim chance for money, nothing else.
              if( rand.Next( 100 ) < 10 )
              {
                // create the money pouch
                DmItemContainer beltpouch = new DmItemContainer( DmItemTypeContainer.BeltPouch );

                // add some random type/amount of money
                // TODO : maybe (just maybe) limit this to make sure we didn;t just give some grubby beggar a fist-ful of platinum.
                //        unless we meant to, so we could return it to it's rightful owner coughtcoughtstealitcoughcough.
                beltpouch.Contents.Add( DmItemFactory.CreateItem( DmItemType.Money ) );

                // add the filled pouch to the list
                list.Add( beltpouch );
              }
            }
            else
            {
              // not a beggar : good chance for money, slim chance for 1 bag of stuff.
              if( rand.Next( 100 ) < 80 )
              {
                // create the money pouch
                DmItemContainer beltpouch = new DmItemContainer( DmItemTypeContainer.BeltPouch );

                // add some random type/amount of money
                beltpouch.Contents.Add( DmItemFactory.CreateItem( DmItemType.Money ) );

                // add the filled pouch to the list
                list.Add( beltpouch );
              }

              // TODO : limit this some?  Or should NPCs be allowed to drag around a bucket and a locked chest?
              if( rand.Next( 100 ) < 20 )
                list.Add( DmItemFactory.CreateItem( DmItemType.Container ) );
            }
          }
          break;
      }

      return list;
    }
  }
}
