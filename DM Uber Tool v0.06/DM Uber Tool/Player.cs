using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace DM_Uber_Tool
{
  public enum PlayerType
  {
    UNKNOWN,
    PlayerCharater,
    NPC,
    Monster,
  }

  public enum Regions
  {
    Any,
    Cave,
    Mountain,
    Plains,
    Forrest,
    Swamp,
    Town,
    Dungeon,
  }

  
  public class Player
  {
    private PlayerType type;

    private string charName;
    private string race;
    private string charSize;
    private string playerClass;
    private string level;
    private string gender;
    private string age;
    private string height;
    private string weight;
    private string diety;
    private string alignmnt;
    private string armor;
    private string weapons;
    private string notableItems;
    private string strength;
    private string dexterity;
    private string constitution;
    private string intelligence;
    private string wisdom;
    private string charisma;
    private string speed;
    private string overhead;
    private string offground;
    private string pushdrag;
    private string fortitude;
    private string reflex;
    private string will;
    private string savemod;
    private string armorclass;
    private string flatfoot;
    private string touch;
    private string acmod;
    private string damage;
    private string hitpoints;
    private string subdual;

    private List<DmItemAbstract> inventory;

    /// <summary>
    /// Constructor
    /// Blank Template Player sheet.  Knows nothing, is nothing, no defaults.
    /// </summary>
    public Player()
    {
      Init(); // set all values to defaults (blank strings) - see definition below
    }

    /// <summary>
    /// Constructor
    /// Fill player template with save file data
    /// </summary>
    /// <param name="characterName"></param>
    public Player( string characterName )
    {
      Init(); // set all values to defaults (blank strings) - see definition below
      Load( characterName );
    }

    #region Public Gets/Sets

    #region Base Stats
    public PlayerType Type
    {
      get { return type; }
      set { type = value; }
    }
    public string CharName
    {
      get { return charName; }
      set { charName = value; }
    }
    public string Race
    {
      get { return race; }
      set { race = value; }
    }
    public string CharSize
    {
      get { return charSize; }
      set { charSize = value; }
    }
    public string PlayerClass
    {
      get { return playerClass; }
      set { playerClass = value; }
    }
    public string Level
    {
      get { return level; }
      set { level = value; }
    }
    public string Gender
    {
      get { return gender; }
      set { gender = value; }
    }
    public string Age
    {
      get { return age; }
      set { age = value; }
    }
    public string Height
    {
      get { return height; }
      set { height = value; }
    }
    public string Weight
    {
      get { return weight; }
      set { weight = value; }
    }
    public string Diety
    {
      get { return diety; }
      set { diety = value; }
    }
    public string Alignmnt
    {
      get { return alignmnt; }
      set { alignmnt = value; }
    }
    public string Armor
    {
      get { return armor; }
      set { armor = value; }
    }
    public string Weapons
    {
      get { return weapons; }
      set { weapons = value; }
    }
    public string NotableItems
    {
      get { return notableItems; }
      set { notableItems = value; }
    }
    public string Strength
    {
      get { return strength; }
      set { strength = value; }
    }
    public string Dexterity
    {
      get { return dexterity; }
      set { dexterity = value; }
    }
    public string Constitution
    {
      get { return constitution; }
      set { constitution = value; }
    }
    public string Intelligence
    {
      get { return intelligence; }
      set { intelligence = value; }
    }
    public string Wisdom
    {
      get { return wisdom; }
      set { wisdom = value; }
    }
    public string Charisma
    {
      get { return charisma; }
      set { charisma = value; }
    }
    public string Speed
    {
      get { return speed; }
      set { speed = value; }
    }
    public string Overhead
    {
      get { return overhead; }
      set { overhead = value; }
    }
    public string Offground
    {
      get { return offground; }
      set { offground = value; }
    }
    public string Pushdrag
    {
      get { return pushdrag; }
      set { pushdrag = value; }
    }
    public string Fortitude
    {
      get { return fortitude; }
      set { fortitude = value; }
    }
    public string Reflex
    {
      get { return reflex; }
      set { reflex = value; }
    }
    public string Will
    {
      get { return will; }
      set { will = value; }
    }
    public string Savemod
    {
      get { return savemod; }
      set { savemod = value; }
    }
    public string Armorclass
    {
      get { return armorclass; }
      set { armorclass = value; }
    }
    public string FlatFoot
    {
      get { return flatfoot; }
      set { flatfoot = value; }
    }
    public string Touch
    {
      get { return touch; }
      set { touch = value; }
    }
    public string Acmod
    {
      get { return acmod; }
      set { acmod = value; }
    }
    public string Damage
    {
      get { return damage; }
      set { damage = value; }
    }
    public string Hitpoints
    {
      get { return hitpoints; }
      set { hitpoints = value; }
    }
    public string Subdual
    {
      get { return subdual; }
      set { subdual = value; }
    }

    public List<DmItemAbstract> Inventory
    {
      get
      {
        return inventory;
      }
      set
      {
        inventory = value;
      }
    }

    #endregion

    #region Derived Modifiers
    public string StrengthModifier
    {
      get
      {
        if (strength != "")
        {
          return ((int.Parse(strength) - 10) / 2).ToString();
        }
        else
        {
          return "";
        }
      }
    }
    public string DexterityModifier
    {
      get
      {
        if (dexterity != "")
        {
          return ((int.Parse(dexterity) - 10) / 2).ToString();
        }
        else
        {
          return "";
        }
      }
    }
    public string ConstitutionModifier
    {
      get
      {
        if (constitution != "")
        {
          return ((int.Parse(constitution) - 10) / 2).ToString();
        }
        else
        {
          return "";
        }
      }
    }
    public string IntelligenceModifier
    {
      get
      {
        if (intelligence != "")
        {
          return ((int.Parse(intelligence) - 10) / 2).ToString();
        }
        else
        {
          return "";
        }
      }
    }
    public string WisdomModifier
    {
      get
      {
        if (wisdom != "")
        {
          return ((int.Parse(wisdom) - 10) / 2).ToString();
        }
        else
        {
          return "";
        }
      }
    }
    public string CharismaModifier
    {
      get
      {
        if (charisma != "")
        {
          return ((int.Parse(charisma) - 10) / 2).ToString();
        }
        else
        {
          return "";
        }
      }
    }
    #endregion

    #endregion


    /// <summary>
    /// Sets all class properties to default values
    /// Used for all constructors.
    /// </summary>
    private void Init()
    {
      type          = PlayerType.UNKNOWN;

      charName      = String.Empty;
      race          = String.Empty;
      charSize      = String.Empty;
      playerClass   = String.Empty;
      level         = String.Empty;
      gender        = String.Empty;
      age           = String.Empty;
      height        = String.Empty;
      weight        = String.Empty;
      diety         = String.Empty;
      alignmnt      = String.Empty;
      armor         = String.Empty;
      weapons       = String.Empty;
      notableItems  = String.Empty;
      strength      = String.Empty;
      dexterity     = String.Empty;
      constitution  = String.Empty;
      intelligence  = String.Empty;
      wisdom        = String.Empty;
      charisma      = String.Empty;
      speed         = String.Empty;
      overhead      = String.Empty;
      offground     = String.Empty;
      pushdrag      = String.Empty;
      fortitude     = String.Empty;
      reflex        = String.Empty;
      will          = String.Empty;
      savemod       = String.Empty;
      armorclass    = String.Empty;
      flatfoot      = String.Empty;
      touch         = String.Empty;
      acmod         = String.Empty;
      damage        = String.Empty;
      hitpoints     = String.Empty;
      subdual       = String.Empty;

      inventory     = new List<DmItemAbstract>();
    }

    /// <summary>
    /// Load a character sheet from a file
    /// </summary>
    /// <param name="characterName"></param>
    public void Load(string characterName)
    {
      TextReader reader = new StreamReader(@"characters\" + characterName + ".txt");

      string buffer = String.Empty;

      // one line at a time, until we reach end of file.
      // 'buffer' is the contents of the next line in the file each time through the loop.
      while ((buffer = reader.ReadLine()) != null)
      {
        // Read file format : 
        //  FIELD:Value Of Field
        //  FIELD:Another value
        //  FIELD:Yet another value!!1!
        //  etc

        string field = buffer.Split('\t')[0];  // colon-delimited array, 0-based, 1st value : the field name
        string val   = buffer.Split('\t')[1];  // colon-delimited array, 0-based, 2nd value : the field value

        // for each field we find, determine which Player value to set
        switch(field)
        {
          case "charName": charName = val; break;
          case "race": race = val; break;
          case "charSize": charSize = val; break;
          case "playerClass": playerClass = val; break;
          case "level": level = val; break;
          case "gender": gender = val; break;
          case "age": age = val; break;
          case "height": height = val; break;
          case "weight": weight = val; break;
          case "diety": diety = val; break;
          case "alignmnt": alignmnt = val; break;
          case "armor": armor = val; break;
          case "weapons": weapons = val; break;
          case "notableItems": notableItems = val; break;
          case "strength": strength = val; break;
          case "dexterity": dexterity = val; break;
          case "constitution": constitution = val; break;
          case "intelligence": intelligence = val; break;
          case "wisdom": wisdom = val; break;
          case "charisma": charisma = val; break;
          case "speed": speed = val; break;
          case "overhead": overhead = val; break;
          case "offground": offground = val; break;
          case "pushdrag": pushdrag = val; break;
          case "fortitude": fortitude = val; break;
          case "reflex": reflex = val; break;
          case "will": will = val; break;
          case "savemod": savemod = val; break;
          case "armorclass": armorclass = val; break;
          case "flatfoot": flatfoot = val; break;
          case "touch": touch = val; break;
          case "acmod": acmod = val; break;
          case "damage": damage = val; break;
          case "hitpoints": hitpoints = val; break;
          case "subdual": subdual = val; break;
            // 
            // TODO : read/save inventory
            //
          default: /* unexpected field name?  Do nothing - don't crash.*/ break;
        }
      }

      reader.Close();
    }

    /// <summary>
    /// Save a character sheet to a file.
    /// Will overwrite any existing file of the same name without asking...
    /// </summary>
    /// <param name="characterName"></param>
    public void Save()
    {
      StreamWriter output = new StreamWriter(@"characters\" + charName + ".txt");

      output.WriteLine("charName\t" + charName);
      output.WriteLine("race\t" + race);
      output.WriteLine("charSize\t" + charSize);
      output.WriteLine("playerClass\t" + playerClass);
      output.WriteLine("level\t" + level);
      output.WriteLine("gender\t" + gender);
      output.WriteLine("age\t" + age);
      output.WriteLine("height\t" + height);
      output.WriteLine("weight\t" + weight);
      output.WriteLine("diety\t" + diety);
      output.WriteLine("alignmnt\t" + alignmnt);
      output.WriteLine("armor\t" + armor);
      output.WriteLine("weapons\t" + weapons);
      output.WriteLine("notableItems\t" + notableItems);
      output.WriteLine("strength\t" + strength);
      output.WriteLine("dexterity\t" + dexterity);
      output.WriteLine("constitution\t" + constitution);
      output.WriteLine("intelligence\t" + intelligence);
      output.WriteLine("wisdom\t" + wisdom);
      output.WriteLine("charisma\t" + charisma);
      output.WriteLine("speed\t" + speed);
      output.WriteLine("overhead\t" + overhead);
      output.WriteLine("offground\t" + offground);
      output.WriteLine("pushdrag\t" + pushdrag);
      output.WriteLine("fortitude\t" + fortitude);
      output.WriteLine("reflex\t" + reflex);
      output.WriteLine("will\t" + will);
      output.WriteLine("savemod\t" + savemod);
      output.WriteLine("armorclass\t" + armorclass);
      output.WriteLine("flatfoot\t" + flatfoot);
      output.WriteLine("touch\t" + touch);
      output.WriteLine("acmod\t" + acmod);
      output.WriteLine("damage\t" + damage);
      output.WriteLine("hitpoints\t" + hitpoints);
      output.WriteLine("subdual\t" + subdual);
      //
      // TODO : write inventory
      //
      output.Flush();
      output.Close();
    }

  }
}
