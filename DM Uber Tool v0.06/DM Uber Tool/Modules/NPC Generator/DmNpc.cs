using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DM_Uber_Tool
{
  public class DmNPC
  {
    public string   name = "";

    public string   dmNotes = "";

    // Unless modified, basic lvl 1 NPCs all have a flat 10 in everything.
    public int      statSTR = 10,
                    statDEX = 10,
                    statCON = 10,
                    statINT = 10,
                    statWIS = 10,
                    statCHR = 10;

    public int      hp = 0;

    public List<NPCFeats>       feats;

    public NPCAge               age;
    public NPCRace              race;
    public NPCGender            gender;

    public NPCHairLength        hairLength;
    public NPCHairColor         hairColor;
    public NPCHairStyle         hairStyle;

    public NPCFacialHair        facialHair;

    public NPCEyeColor          eyeColor;

    public NPCBodySize          bodySize;
    public NPCBodyBuild         bodyBuild;

    public NPCMentalState       mentalState;
    public NPCMood              mood;
    public NPCPersonaliy        personality;

    public List<NPCFeatures>    features = new List<NPCFeatures>();

    public NPCType              characterType;
    public NPCTypeAdventurer    characterClass;
    public NPCTypeProfession    characterProfession;

    public DmItemClothing       clothing;

    public List<DmItemAbstract> armor     = new List<DmItemAbstract>();
    public List<DmItemAbstract> weapons   = new List<DmItemAbstract>();
    public List<DmItemAbstract> inventory = new List<DmItemAbstract>();
    
    /// <summary>
    /// Constructor.  Any enum values will be set to their default 1st entry (value 0), stats all set to 10.
    /// Nothing else set
    /// </summary>
    public DmNPC()
    {
      // Nothing done for you.  If creating a single NPC without using 
      // DmNpcFactory, you will have to set all values yourself :

      //  name
      //  stats
      //  feats;

      //  age;
      //  race;
      //  gender;

      //  hairLength;
      //  hairColor;
      //  hairStyle;

      //  facialHair;

      //  eyeColor;

      //  bodySize;
      //  bodyBuild;

      //  mentalState;
      //  mood;
      //  personality;

      //  features

      //  characterType;
      //  characterClass;
      //  characterProfession;

      //  clothing;

      //  armor 
      //  weapons
      //  inventory
    }

    /// <summary>
    /// Utility function to capitalize the 1st letter of a string.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private static string Capitalize( string s )
    {
      return s[0].ToString().ToUpper() + s.Substring(1,s.Length-1);
    }

    /// <summary>
    /// Generates a full description of this NPC, including attributes, gear, and attitude.
    /// </summary>
    /// <returns></returns>
    public string GetDescription()
    {
      return string.Format( "Name : {0}\r\n" +
        /* */               "{1}, {2} and {3} {4} {5}, with {6} eyes and {7} {8} hair worn {9}" +
        /* */               "{10}.\r\n\r\n" +
        /* */               "Usually {11}, they are currently {12}, and in a(n) {13} mood.\r\n\r\n" +
        /* */               "{14} by choice\r\n" +
        /* */               "{15} by trade\r\n\r\n" +
        /* */               "They are wearing {16} and {17}.\r\n\r\n" +
        /* */               "{18}",

        /*  0 */            name,

        /*  1 */ Capitalize(DmNpcFactory.GetDescription( age )),

        /*  2 */            DmNpcFactory.GetDescription( bodySize ),
        /*  3 */            DmNpcFactory.GetDescription( bodyBuild ),

        /*  4 */            DmNpcFactory.GetDescription( gender ),
        /*  5 */            DmNpcFactory.GetDescription( race ),

        /*  6 */            DmNpcFactory.GetDescription( eyeColor ),

        /*  7 */            DmNpcFactory.GetDescription( hairLength),
        /*  8 */            DmNpcFactory.GetDescription( hairColor ),
        /*  9 */            DmNpcFactory.GetDescription( hairStyle ),

        /* 10 */            GetFeatureDescriptions(),

        /* 11 */            DmNpcFactory.GetDescription( personality ),
        /* 12 */            DmNpcFactory.GetDescription( mentalState ),
        /* 13 */            DmNpcFactory.GetDescription( mood ),

        /* 14 */ Capitalize(DmNpcFactory.GetDescription( characterType )),
        /* 15 */ Capitalize(( characterType==NPCType.Adventurer ? DmNpcFactory.GetDescription(characterClass) : DmNpcFactory.GetDescription(characterProfession) ) ),
       
        /* 16 */            GetArmorDescriptions(),
        /* 17 */            GetWeaponDescriptions(),
        /* 18 */            GetEquipmentDescriptions()
                          );
    }

    /// <summary>
    /// Gets the paragraphs-tyle armor description for this MPC
    /// </summary>
    /// <returns></returns>
    private string GetArmorDescriptions()
    {
      string tmp = "";

      tmp += clothing.GetDescription(true);

      if( armor.Count() > 0 )
      {
        // TODO : make this handle more than armor and shield... so we can descipr armor parts in painstaking detail for no good reason.
        tmp += " under their " + armor[0].GetDescription(true) + " armor";
        if( armor.Count() == 2 )
          tmp += " and " + armor[1].GetDescription(true);
      }
      return tmp;
    }

    /// <summary>
    /// Gets the paragraph-style weapon list descriptions for this NPC
    /// </summary>
    /// <returns></returns>
    private string GetWeaponDescriptions()
    {
      string tmp = "";

      if( weapons.Count() == 0 )
        tmp = "unarmed";
      else
        for( int i=0; i<weapons.Count(); i++ )
          tmp += (tmp.Length>0 ? (i==weapons.Count()-1 ? " and " : ",") : "" ) + weapons[i].GetDescription(true);

      return "armed with " + tmp;
    }

    /// <summary>
    /// Gets the paragraph-stye equipment list descriptions for this NPC
    /// </summary>
    /// <returns></returns>
    private string GetEquipmentDescriptions()
    {
      string tmp = "";

      for( int i=0; i<inventory.Count(); i++ )
        if( inventory[i] is DmItemContainer )
        {
          DmItemContainer item = (DmItemContainer)inventory[i];
          tmp += (tmp.Length>0 ? "\r\n" : "") + item.GetDescription( false );
        }
        else
          tmp += (tmp.Length>0 ? "\r\n" : "") + ((DmItemContainer)inventory[i]).GetDescription( false );

      return tmp;
    }

    /// <summary>
    /// Gets the paragraph-style features list descriptions for this NPC
    /// </summary>
    /// <returns></returns>
    private string GetFeatureDescriptions()
    {
      if( features.Count > 0 )
      {
        string tmp = "";

        for( int i=0; i<features.Count; i++ )
          tmp += (tmp.Length==0 ? "" : (i==features.Count-1 ? " and " : ", ")) + DmNpcFactory.GetDescription( features[i] );
      
        return " with " + tmp;
      }

      return "";
    }

    /// <summary>
    /// Creates a TreeNode for this NPC, populating it's sub-nodes with the various attributes
    /// </summary>
    /// <returns></returns>
    public TreeNode GetNPCNode()
    {
      // create the node with the NPC name
      TreeNode node = new TreeNode( name );

      // description node (short)
      string shortDescription = string.Format( "{0}, {1}",
        /* */                                  DmNpcFactory.GetDescription( race ),
        /* */                                  DmNpcFactory.GetDescription( gender )
        /* */                                );
      TreeNode descNode = new TreeNode( shortDescription );

      // class/profession node
      TreeNode classNode = new TreeNode( ( characterType==NPCType.Adventurer ? DmNpcFactory.GetDescription(characterClass) : DmNpcFactory.GetDescription(characterProfession) ) );

      // base stats
      TreeNode statsNode = new TreeNode( "Stats" );
      statsNode.Nodes.Add( "STR : " + statSTR.ToString() );
      statsNode.Nodes.Add( "DEX : " + statDEX.ToString() );
      statsNode.Nodes.Add( "CON : " + statCON.ToString() );
      statsNode.Nodes.Add( "INT : " + statINT.ToString() );
      statsNode.Nodes.Add( "WIS : " + statWIS.ToString() );
      statsNode.Nodes.Add( "CHR : " + statCHR.ToString() );

      // clothing description node
      TreeNode clothingNode = new TreeNode( clothing.GetDescription(true) );

      // armor list node
      TreeNode armorNode = new TreeNode( "Armor" );
      for( int i=0; i<armor.Count; i++ )
        //armorNode.Nodes.Add( new TreeNode( armor[i].GetDescription( true ) ) );
        armorNode.Nodes.Add( new TreeNode( DmItemFactory.GetDescription( armor[i].itemName, false ) ) );

      // weapon list node
      TreeNode weaponNode = new TreeNode( "Weapons" );
      for( int i=0; i<weapons.Count; i++ )
        //weaponNode.Nodes.Add( new TreeNode( weapons[i].GetDescription( true ) ) );
        weaponNode.Nodes.Add( new TreeNode( DmItemFactory.GetDescription( weapons[i].itemName, false ) ) );

      // inventory list node
      TreeNode inventoryNode = new TreeNode( "Inventory" );
      for( int i=0; i<inventory.Count; i++ )
        inventoryNode.Nodes.Add( inventory[i].GetDescriptionNode() );

      // add the nodes to the NPC node
      node.Nodes.Add( descNode );
      node.Nodes.Add( classNode );
      node.Nodes.Add( statsNode );
      node.Nodes.Add( clothingNode );
      node.Nodes.Add( armorNode );
      node.Nodes.Add( weaponNode );
      node.Nodes.Add( inventoryNode );

      return node;
    }

    /// <summary>
    /// Will modify stats, HP, and feats selections from DmNpcFactory to level this NPC up by 1.
    /// </summary>
    public void LevelUp()
    {
      // TODO : class-specific level up
    }

  }
}
