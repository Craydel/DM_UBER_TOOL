using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM_Uber_Tool
{
  public static class DmNameGenerator
  {
    private static Random rand = new Random();

    private class GrammarPart
    {
      public string         condition = "";
      public List<string>   follow    = new List<string>();
    };

    #region Random Name Grammar (simple)
    private static string[] consonants = {"b","c","d","f","g","h","j","k","l","m","n","p","r","s","t","v","w","x","y","z"};
    private static string[] vowels     = {"a","i","e","o","u","y"};
    private static string[] endings    = {"ith", "ton", "on", "field", "man", "maker"};
    #endregion

    #region Dragonborne
    private static string[] DragonBorneMaleNames = 
    { "Arjhan",
      "Ararn",
      "Balasar",
      "Bharash",
      "Donaar",
      "Duhakal",
      "Duhar ",
      "Duhima",
      "Duhinn",
      "Duhir",
      "Ekala",
      "Ekar",
      "Eran",
      "Erath",
      "Eshark",
      "Eshath",
      "Ghear",
      "Gheark",
      "Gheash",
      "Gheish",
      "Gheith",
      "Ghesh",
      "Harkath",
      "Harkath ",
      "Harkina",
      "Heskan",
      "Kriv",
      "Krivaan",
      "Krivan",
      "Krivath",
      "Kryark",
      "Kryath",
      "Kryinn",
      "Kryir",
      "Lihmath",
      "Medrash",
      "Nadarr",
      "Patrin",
      "Rhogar",
      "Sesakal",
      "Sesharn",
      "Seshath",
      "Seshinn",
      "Seshith ",
      "Shaaan",
      "Shaath",
      "Shaith",
      "Shamash",
      "Shedinath",
      "Shedininn",
      "Shedinn",
      "Shysark",
      "Shysath",
      "Shysik",
      "Shysinn",
      "Thaath",
      "Thuarn",
      "Thuish",
      "Thuith",
      "Tinath",
      "Tinir",
      "Torinn"
    };

    private static string[] DragonBorneFemaleNames = 
    { "Akra",
      "Araan",
      "Arima",
      "Arith",
      "Biri",
      "Daar",
      "Ekala",
      "Harann",
      "Kava",
      "Korinn",
      "Krivima",
      "Kryish ",
      "Mishann",
      "Nala",
      "Perra",
      "Raiann",
      "Shaash",
      "Shysala",
      "Shysath",
      "Sora",
      "Surina",
      "Thava",
      "Thuina",
      "Thuuna",
      "Tinith"
    };
    #endregion
    #region Dwarven
    private static string[] DwarvenMaleNames = 
    { "Adaladin",
      "Alaric",
      "Aldin",
      "Alfginnar",
      "Algrim",
      "Alrik",
      "Argam",
      "Arik",
      "Arngrim",
      "Azaglar",
      "Azram",
      "Baldrick",
      "Balik",
      "Balin",
      "Balzud",
      "Baragor",
      "Bardin",
      "Barik",
      "Barin",
      "Belegar",
      "Belegol",
      "Belegond",
      "Belgar",
      "Belgol",
      "Belgond",
      "Bhatran ",
      "Borgo",
      "Borin",
      "Borri",
      "Brand",
      "Brokki",
      "Brond",
      "Bronn",
      "Budrik",
      "Burlok",
      "Dadrin",
      "Daled",
      "Dammin",
      "Dardar",
      "Dared",
      "Darek",
      "Del",
      "Dimgol",
      "Dimrond",
      "Dimzad",
      "Dolur",
      "Dorin",
      "Dorn",
      "Dorri",
      "Drakki",
      "Drokki",
      "Drong",
      "Drumin",
      "Dumin",
      "Dunaar",
      "Durak",
      "Duregar",
      "Durgim",
      "Durim",
      "Durin",
      "Durrag",
      "Falagrim",
      "Falgrim",
      "Faragrim",
      "Fargrim",
      "Fimbur",
      "Finn",
      "Flakki",
      "Fodrin",
      "Fular",
      "Furgil",
      "Gadrin",
      "Gamar",
      "Garag",
      "Garik",
      "Garil",
      "Garin",
      "Garn",
      "Garuk",
      "Gharth",
      "Gimli",
      "Gomrund",
      "Gorazin",
      "Gorek",
      "Gorem",
      "Gorin",
      "Gorm",
      "Gorrin",
      "Gotrek",
      "Gottri",
      "Grim",
      "Grimbul",
      "Grimdal",
      "Grimli",
      "Grimnir",
      "Grimwold",
      "Grodrik",
      "Grogan",
      "Grogril",
      "Grom",
      "Grond",
      "Groth",
      "Grum",
      "Grumdi",
      "Grun",
      "Grundi",
      "Grung",
      "Grunni",
      "Guddi",
      "Gudrun",
      "Gumli",
      "Gundrik",
      "Gurni",
      "Guttri",
      "Haakon",
      "Hadrin",
      "Haki",
      "Haragin",
      "Harakaz",
      "Harek",
      "Hargin",
      "Harkaz",
      "Hathel",
      "Heganbor",
      "Herger",
      "Holgar",
      "Horgar",
      "Hrungnor",
      "Hugen",
      "Hurgin",
      "Janek",
      "Kadrin",
      "Kaz",
      "Kazador",
      "Kazarik",
      "Kazi",
      "Kazrik",
      "Ketil",
      "Kimril",
      "Kirgi",
      "Klob",
      "Korgan",
      "Kragg",
      "Krudd",
      "Krung",
      "Kurgan",
      "Kurgaz",
      "Logan",
      "Logazor",
      "Loki",
      "Lokri",
      "Lothor",
      "Lunn",
      "Magnund",
      "Morag",
      "Mordin",
      "Mordred",
      "Morek",
      "Morgrim",
      "Morngrim",
      "Mundri",
      "Nirum",
      "Okri",
      "Oldor",
      "Orek",
      "Orgri",
      "Othos",
      "Ragnar",
      "Ragni",
      "Rakyr",
      "Ranulf",
      "Rarek",
      "Rentar",
      "Ril",
      "Rorek",
      "Rornbek",
      "Rornnar",
      "Rukh",
      "Sindri",
      "Skag",
      "Skaggi",
      "Skaldor",
      "Skalf",
      "Skalli",
      "Skorri",
      "Skudd",
      "Skuddi",
      "Smakki",
      "Snaddri",
      "Snarri",
      "Snorri",
      "Storri",
      "Strom",
      "Stromni",
      "Thialfi",
      "Thingrim",
      "Thokas",
      "Thorbal",
      "Thorek",
      "Thorgrim",
      "Thori",
      "Thorin",
      "Thorlek",
      "Throbbi",
      "Throbin",
      "Thrung",
      "Tordo",
      "Trygg",
      "Ulfar",
      "Ulrik",
      "Ulther",
      "Yimdal",
      "Yorri"
    };

    public static string[] DwarvenFemaleNames = 
    { "Adra",
      "Agladeth",
      "Almada",
      "Almauki",
      "Ambrla",
      "Amli",
      "Anadi",
      "Anilu",
      "Aoti",
      "Arani",
      "Arka",
      "Armeralsia",
      "Armermae",
      "Arri",
      "Asabelle ",
      "Badalsia",
      "Badna",
      "Baerilu",
      "Baktu",
      "Balarani",
      "Balarra",
      "Barudeth",
      "Belbrideth",
      "Belbrigret",
      "Belbriil",
      "Belmaeka",
      "Belna",
      "Bethvae",
      "Birda",
      "Birgit ",
      "Birmi",
      "Brodrika ",
      "Brondi",
      "Chaeya",
      "Dagali",
      "Dagba",
      "Dagda",
      "Daglana",
      "Dekli",
      "Dekna",
      "Delamaba",
      "Delamaia",
      "Delamasa",
      "Delba",
      "Deldeth",  
      "Delida",
      "Delil",
      "Delza",
      "Dertain",
      "Dorba",
      "Dunhilda ",
      "Duri",
      "Eiki",
      "Elna",
      "Elra",
      "Erla",
      "Erra",
      "Fragh",
      "Fregar ",
      "Gaeka",
      "Gani",
      "Gerrad",
      "Gerza",
      "Gili",
      "Gilkara",
      "Gima",
      "Gimrim",
      "Ginal",
      "Gurtrud ",
      "Gwariff",
      "Gwarla",
      "Gwenelyn ",
      "Halola",
      "Helgar ",
      "Heltia",
      "Hergar ",
      "Hilda ",
      "Hilna",
      "Huakgi",
      "Huakri",
      "Ilda",
      "Ilgunn",
      "Iodi",
      "Kalina",
      "Kalna",
      "Katalin",
      "Katlin ",
      "Kiha",
      "Krait",
      "Kraoh",
      "Kriadi",
      "Kriba",
      "Luka",
      "Mabnu",
      "Madeth",
      "Mairani",
      "Mairda",
      "Mairra",
      "Mairren",
      "Marlani",
      "Marlina",
      "Marnu",
      "Marul",
      "Mili",
      "Mora",
      "Namna",
      "Niba",
      "Nita",
      "Nitura",
      "Niyma",
      "Niythra",
      "Nola",
      "Noloina",
      "Nolola",
      "Nolora",
      "Nomla",
      "Oami",
      "Ova",
      "Ovna",
      "Ovras",
      "Ovthra",
      "Ramilda",
      "Ramilla",
      "Ranna",
      "Rari",
      "Rega",
      "Reggunn",
      "Regna",
      "Rerna",
      "Rokoila",
      "Rona",
      "Roti",
      "Rubekae",
      "Rubekil",
      "Sibbela",
      "Sigrid ",
      "Sisani",
      "Sisna",
      "Sistu",
      "Skori",
      "Steoshar",
      "Talida",
      "Tanbi",
      "Tandeth",
      "Tangi",
      "Tarri",
      "Tarthra",
      "Tartu",
      "Tefbela",
      "Tefshar",
      "Telak",
      "Teldu",
      "Thogret",
      "Thoma",
      "Tidret",
      "Tili",
      "Tiril",
      "Tirla",
      "Tokotu",
      "Tora",
      "Torka",
      "Tuli",
      "Tumha",
      "Tumna",
      "Tuvakara",
      "Uada",
      "Ulaesli",
      "Ungi",
      "Unka",
      "Vada",
      "Valak",
      "Valla",
      "Valli",
      "Velm",
      "Vonna",
      "Vonra",
      "Werbryn",
      "Werret",
      "Whurla",
      "Yada",
      "Yenbryn",
      "Yerilu",
      "Yerma",
      "Yerna",
      "Yili",
      "Yuanna",
      "Yurak",
      "Yurra",
      "Zintah",
      "Zuthgi"
    };

    public static string[] DwarvenSurnames = 
    { "Anvilcaster",
      "Anvilcrafter",
      "Anvilmaker",
      "Anvilsmith",
      "Armorforger",
      "Armorsmith",
      "Axehelm",
      "Axemaker",
      "Axesmith",
      "Axeworker",
      "Blackaxe",
      "Blackhammer",
      "Blackmaul",
      "Bloodhammer",
      "Bloodpick",
      "Bloodsword",
      "Blueaxe",
      "Bluefoot",
      "Blueheart",
      "Bluepick",
      "Bluespear",
      "Bluespike",
      "Bluesword",
      "Bouldercarver",
      "Bouldercrusher",
      "Bronzecarver",
      "Bronzeforger",
      "Bronzeheart",
      "Bronzemaker",
      "Chertcrusher",
      "Chertdelver",
      "Chertfoot",
      "Cherthelm",
      "Chertsmash",
      "Copperfoot",
      "Copperhand",
      "Copperheart",
      "Copperhelm",
      "Dragonbasher",
      "Dragonbreaker",
      "Dragoncrusher",
      "Dragonhewer",
      "Dragonkiller",
      "Dragonslayer",
      "Dragonspear",
      "Earthcrusher",
      "Earthdig",
      "Earthdigger",
      "Earthtracker",
      "Fardelver",
      "Fardweller",
      "Farminer",
      "Firefoot",
      "Firehair",
      "Fireheart",
      "Firehelm",
      "Firmfist",
      "Firmheart",
      "Flameaxe",
      "Flamefoot",
      "Flamehammer",
      "Flameheart",
      "Flameleg",
      "Flamespear",
      "Flamespike",
      "Foebasher",
      "Foeblade",
      "Foeclub",
      "Foehammer",
      "Foemaul",
      "Foeslayer",
      "Gemhand",
      "Gemmaker",
      "Giantbender",
      "Giantslayer",
      "Goblinhewer",
      "Goblinkiller",
      "Goblinslayer",
      "Granitearm",
      "Graniteblade",
      "Granitedelver",
      "Granitefoot",
      "Granitehammer",
      "Granitepick",
      "Grayfist",
      "Grayfoot",
      "Grayhammer",
      "Grayheart",
      "Graymaul",
      "Graysword",
      "Groundbreaker",
      "Groundcarver",
      "Groundcrusher",
      "Groundcutter",
      "Hammerbeard",
      "Hammerfist",
      "Ironcrafter",
      "Irondig",
      "Ironhammer",
      "Ironleg",
      "Ironoath",
      "Ironworker",
      "Marblecarver",
      "Marblecutter",
      "Metalcarver",
      "Metalmaker",
      "Moonbeard",
      "Oakfoot",
      "Oakheart",
      "Oakoath",
      "Ogrehammer",
      "Ogrepick",
      "Onyxhand",
      "Orcaxe",
      "Orcbasher",
      "Orcblade",
      "Orchammer",
      "Orcslayer",
      "Pickcheek",
      "Pickfist",
      "Pickheart",
      "Quartzfist",
      "Redaxe",
      "Redback",
      "Redbeard",
      "Redfist",
      "Redheart",
      "Ringcaster",
      "Ringcrafter",
      "Rockaxe",
      "Rockcrusher",
      "Rockcutter",
      "Rockdweller",
      "Rockfist",
      "Rockhammer",
      "Rocksword",
      "Shalecrusher",
      "Shaleminer",
      "Silveraxe",
      "Silverbeard",
      "Silverhand",
      "Silversword",
      "Spidercrusher",
      "Spidermaul",
      "Steelaxe",
      "Steelblade",
      "Steelpick",
      "Stoneaxe",
      "Stonebeard",
      "Stonecarver",
      "Stonecrusher",
      "Stonecutter",
      "Stonedig",
      "Stonehammer",
      "Stonehewer",
      "Stoneminer",
      "Stonepick",
      "Stonequarry",
      "Stonesmash",
      "Strongfist",
      "Swordcaster",
      "Thunderaxe",
      "Thunderback",
      "Thunderbeard",
      "Thunderfoot",
      "Thunderhair",
      "Trollaxe",
      "Trollblade",
      "Trollclub",
      "Trollhammer",
      "Trollhewer",
      "Trollkiller",
      "Tunnelcrusher",
      "Tunnelhewer"
    };

    #endregion
    #region Eladrin
    private static string[] EladrinMaleNames = 
    { "Aramil",
      "Erevan",
      "Paelias",
      "Arannis",
      "Berrian",
      "Dayereth",
      "Galinndan",
      "Hadarai",
      "Immeral",
      "Mindartis",
      "Quarion",
      "Riardon",
      "Soveliss"
    };

    private static string[] EladrinFemaleNames = 
    { "Althaea",
      "Bethrynna",
      "Naivara",
      "Valenae",
      "Anastrianna",
      "Andraste",
      "Caelynna",
      "Jelenneth",
      "Leshanna",
      "Meriele",
      "Quelenna",
      "Sariel",
      "Shanairra",
      "Theirastra"
    };
    #endregion
    #region Elven
    private static string[] ElvenMaleNames = 
    {
      "Aecinuionion",
      "Aldion",
      "Alestan",
      "Anwe",
      "Arancion",
      "Aranion",
      "Arthraeus",
      "Arwarthnen",
      "Authion",
      "Authmion",
      "Banmoridan",
      "Baraohdon",
      "Beinion",
      "Belldhorar",
      "Bellohion",
      "Bersidhcan",
      "Caladol",
      "Calaion",
      "Calaron",
      "Caltherdon",
      "Caundir",
      "Caunthandien",
      "Celear",
      "Celeion",
      "Cilion",
      "Cundnibdas",
      "Daeron",
      "Doseurien",
      "Eglerestan",
      "Eidirnaiion",
      "Eirantien",
      "Elion",
      "Eruangolnen",
      "Erumaenun",
      "Erurion",
      "Erutáion",
      "Faebaradion",
      "Galanion",
      "Galanon",
      "Garwë",
      "Glanthil",
      "Habrisdur",
      "Habrisgos",
      "Herron",
      "Iauron",
      "Ionir",
      "Kaondad",
      "Kiirion",
      "Laiquaendien",
      "Lantoldon",
      "Latheyion",
      "Lathgolion",
      "Lithwarthdien",
      "Locien",
      "Malion",
      "Malron",
      "Marfindion",
      "Megildhon",
      "Melimdhon",
      "Melimron",
      "Meririnil",
      "Mingliruionion",
      "Minhasbarton",
      "Minpangorion",
      "Morthitur",
      "Nedordon",
      "Nendhon",
      "Nennon",
      "Nithien",
      "Paraerion",
      "Parenien",
      "Parranthor",
      "Raegton",
      "Rainmornion",
      "Rimeion",
      "Rinnoruionton",
      "Rinsabarien",
      "Risswe",
      "Rûion",
      "Saelion",
      "Siphendur",
      "Taenion",
      "Thanmorir",
      "Thannon",
      "Tharciniole",
      "Tharserthor",
      "Throiothion",
      "Ticadolien",
      "Turdirdil",
      "Upadion",
      "Uruvion",
      "Valtherinbarren",
      "Vaniion",
      "Veassen",
      "Verion",
      "Yalawairon",
      "Yalayail"
    };

    private static string[] ElvenFemaleNames = 
    {
      "Aeranwen",
      "Aerwethwen",
      "Agtoria",
      "Aireda",
      "Aireiel",
      "Aireira",
      "Amamariiel",
      "Amariel",
      "Anaethauiel",
      "Anelya",
      "Anesnostiel",
      "Anesyavea",
      "Anfainiel",
      "Aniel",
      "Belfindtha",
      "Berdethiel",
      "Berlairiel",
      "Bronmoriel",
      "Buisidhiel",
      "Caelwen",
      "Calaniel",
      "Canostae",
      "Caunthea",
      "Caunwen",
      "Cireuraiwen",
      "Cirlaiiel",
      "Daerirya",
      "Daernathiel",
      "Dauthenladiel",
      "Eardirasse",
      "Eccaedra",
      "Elmiriel",
      "Enafainiel",
      "Enamaivsta",
      "Enauialiel",
      "Erudia",
      "Erudwen",
      "Erueryniel",
      "Erulothwen",
      "Eruranna",
      "Erurwen",
      "Faeliel",
      "Falranna",
      "Filestiel",
      "Filethiel",
      "Fillaina",
      "Finbannima",
      "Finladiel",
      "Finuraiiel",
      "Ghilesse",
      "Glanethiel",
      "Goneliel",
      "Gwestien",
      "Gwethmelinnel",
      "Gwilcistiel",
      "Halasathiel",
      "Halasiniel",
      "Ielliel",
      "Isduriel",
      "Janwen",
      "Laerwen",
      "Laiathima",
      "Limoranwen",
      "Losthendaaya",
      "Luthna",
      "Marilwen",
      "Meldethiel",
      "Melethwen",
      "Mellinnanna",
      "Melranna",
      "Melwen",
      "Meruraiea",
      "Minariel",
      "Minlondiel",
      "Mithmethiel",
      "Narien",
      "Nigobelwen",
      "Quangobeliel",
      "Quanliniel",
      "Raediliel",
      "Raerahiniel",
      "Rainnaeiel",
      "Renvaniel",
      "Rodima",
      "Roserininlasa",
      "Runiliel",
      "Talaiel",
      "Talaima",
      "Teidrenthiriel",
      "Tiregethwen",
      "Tirlinnthea",
      "Tirmoriel",
      "Tirrinneth",
      "Titidiriel",
      "Uthlinnnyl",
      "Vanaadhwen",
      "Vanarauniel",
      "Vaniwethiel",
      "Venraniel",
      "Yara"
    };

    private static string[] ElvenSurnames = 
    {
      "Aeradiir",
      "Aeraron",
      "Aerasume",
      "Aeraviel",
      "Aldavathar",
      "Aldaviel",
      "Augloththar",
      "Augtanta",
      "Bermirta",
      "Bertanion",
      "Bertholryl",
      "Celeagaar",
      "Crometh",
      "Dlaryaina",
      "Ealoloth",
      "Elentaure",
      "Elerdiir",
      "Elerren",
      "Faelandaerl",
      "Faelandalan",
      "Faerondaerl",
      "Falarina",
      "Felethdal",
      "Galaagaian",
      "Galadragthor",
      "Galalithdal",
      "Galalithmyr",
      "Galaraithor",
      "Galondhen",
      "Galonmin",
      "Galontaur",
      "Haemin",
      "Haeviel",
      "Isilidhrinial",
      "Isilidiliniel",
      "Isiliglinathar",
      "Isilivandar",
      "Kevabrinthar",
      "Kevathalrin",
      "Kidragil",
      "Laeesina",
      "Laeesta",
      "Laeethtinu",
      "Lanbrinas",
      "Lanbrinthar",
      "Landireth",
      "Lareneththar",
      "Larenladal",
      "Lassnaronel",
      "Lithtaure",
      "Mithendonel",
      "Mithtathiel",
      "Mithtaththir",
      "Nalldel",
      "Nallvathar",
      "Nallviel",
      "Narbrinellyn",
      "Narvansel",
      "Nellynnellyn",
      "Neltholeth",
      "Nhaalthir",
      "Nhaendlhûn",
      "Nhathalsyr",
      "Norolinde",
      "Noromiel",
      "Norotaure",
      "Ondoloth",
      "Ondoos",
      "Orelwa",
      "Orvir",
      "Rhuielon",
      "Rhuiron",
      "Rhuiviel",
      "Roloviel",
      "Runalvaar",
      "Shalandaerl",
      "Shalandalan",
      "Sharondaerl",
      "Taleslal",
      "Talmirina",
      "Talnae",
      "Taltatinu",
      "Tandilindal",
      "Tanethelen",
      "Tanraias",
      "Tathdel",
      "Tathren",
      "Tathtaure",
      "Teldirthar",
      "Teltandar",
      "Toradilinmyr",
      "Toralithsyr",
      "Undomin",
      "Undotaur",
      "Vanlithe",
      "Vantaure",
      "Vanval"
    };
    #endregion
    #region Gnomish
    private static string[] GnomishMaleNames = 
    {
      "Alaedda",
      "Aleavyan",
      "Alu",
      "Alwniver",
      "Alydda",
      "Brwlyan",
      "Bryvyan",
      "Ciniver",
      "Clungwen",
      "Clwlyan",
      "Clwryan",
      "Clylonna",
      "Clynoic",
      "Cussa",
      "Dae",
      "Daecla",
      "Daeniver",
      "Dinoic",
      "Dwnoic",
      "Dyrka",
      "Elaell",
      "Elaevyan",
      "Ely",
      "Elybrylla",
      "Gweryan",
      "Joniver",
      "Locla",
      "Longwen",
      "Meanoic",
      "Merall",
      "Merellyra",
      "Meruryan",
      "Merwcla",
      "Muryan",
      "Mydda",
      "Myna",
      "Naengwen",
      "Nengwen",
      "Nolyan",
      "Rangwen",
      "Reaniver ",
      "Reanoic",
      "Rivyan",
      "Ru",
      "Saengwen",
      "Saeryan",
      "Swryan",
      "Syniver",
      "Ysura"
    };

    private static string[] GnomishFemaleNames = 
    {
      "Alalyan",
      "Bressa",
      "Brullyra",
      "Cacla",
      "Cellyra",
      "Claebrylla",
      "Clalla",
      "Clealonna",
      "Clina",
      "Clolla",
      "Clollyra",
      "Clussa",
      "Cussa",
      "Elillyra",
      "Elossa",
      "Ely",
      "Elybrylla",
      "Gwebrylla",
      "Gwibrylla",
      "Gwicla",
      "Gwongwen",
      "Jaelonna",
      "Jaerka",
      "Jill",
      "Lealonna",
      "Lella",
      "Locla",
      "Lyssa",
      "Maebrylla",
      "Maellyra",
      "Mara",
      "Mea",
      "Mydda",
      "Myna",
      "Naessa",
      "Nalonna",
      "Nillyra",
      "Nira",
      "Nyllyra",
      "Nylonna",
      "Nyna",
      "Nyra",
      "Ralla",
      "Redda",
      "Solla"
    };
    #endregion
    #region Halfling
    private static string[] HalflingMaleNames = 
    {
      "Adalgrim",
      "Adelard",
      "Andwise",
      "Balbo",
      "Bandobras",
      "Berilac",
      "Bingo",
      "Bodo",
      "Bowman",
      "Bungo",
      "Deagol",
      "Dinodas",
      "Doderick",
      "Drogo",
      "Dudo",
      "Erling",
      "Everard",
      "Fastolph",
      "Fastred",
      "Ferdibrand",
      "Ferdinard",
      "Ferumbras",
      "Filibert",
      "Flambard",
      "Folco",
      "Fosco",
      "Fredegar",
      "Frodo",
      "Gerontius",
      "Gorbadoc",
      "Gorbulas",
      "Gorhendad",
      "Gormadoc",
      "Griffo",
      "Gundabald",
      "Halfred",
      "Hamfast",
      "Hamson",
      "Hending",
      "Hildibrand",
      "Hildifons",
      "Hildigrim",
      "Hobson",
      "Hugo",
      "Ilberic",
      "Isembold",
      "Isengar",
      "Isengrim",
      "Isumbras",
      "Largo",
      "Longo",
      "Lotho",
      "Madoc",
      "Marmadas",
      "Marmadoc",
      "Marroc",
      "Matta",
      "Meriadoc",
      "Merimac",
      "Merimas",
      "Milo",
      "Minto",
      "Moro",
      "Mosco",
      "Moto",
      "Mungo",
      "Odo",
      "Odovacar",
      "Olo",
      "Orgulas",
      "Otho",
      "Paladin",
      "Peregrin",
      "Polo",
      "Ponto",
      "Porto",
      "Posco",
      "Reginard",
      "Robin",
      "Rorimac",
      "Rudigar",
      "Rufus",
      "Sadoc",
      "Samwise",
      "Sancho",
      "Saradas",
      "Saradoc",
      "Seredic",
      "Sigismond",
      "Smeagol",
      "Togo",
      "Tolman",
      "Tomba",
      "Wilcome",
      "Wilibald"
    };

    private static string[] HalflingFemaleNames = 
    {
      "Adaldrida",
      "Adamanta",
      "Amaranth",
      "Angelica",
      "Asphodel",
      "Belba",
      "Bell",
      "Belladonna",
      "Berylla",
      "Camellia",
      "Celandine",
      "Chica",
      "Cora",
      "Daisy",
      "Diamond",
      "Donnamira",
      "Dora",
      "Eglantine",
      "Elanor",
      "Esmeralda",
      "Estella",
      "Firiel",
      "Goldilocks",
      "Hanna",
      "Hilda",
      "Lily",
      "Linda",
      "Lobelia",
      "Malva",
      "Marigold",
      "May",
      "Melilot",
      "Menegilda",
      "Mentha",
      "Mimosa",
      "Mirabella",
      "Myrtle",
      "Nina",
      "Pansy",
      "Pearl",
      "Peony",
      "Pervinca",
      "Pimpernel",
      "Poppy",
      "Primrose",
      "Primula",
      "Prisca",
      "Rosa",
      "Rosamunda",
      "Rose",
      "Rosie",
      "Rowan",
      "Ruby",
      "Salvia",
      "Tanta"
    };

    private static string[] HalflingSurnames = 
    {
      "Banks",
      "Boffin",
      "Bolger",
      "Bracegirdle",
      "Brandybuck",
      "Brockhouse",
      "Brown",
      "Brownlock",
      "Bunce",
      "Burrowes",
      "Burrows",
      "Chubb",
      "Cotton",
      "Fairbairn",
      "Gamgee",
      "Goldworthy",
      "Goodbody",
      "Goodchild",
      "Goold",
      "Greenhand",
      "Grubb",
      "Hayward",
      "Hornblower",
      "Longholes",
      "Maggot",
      "Mugwort",
      "Noakes",
      "Overhill",
      "Pott",
      "Proudfoot",
      "Puddifoot",
      "Roper",
      "Rumble",
      "Sackville",
      "Sandheaver",
      "Sandyman",
      "Smallburrow",
      "Took",
      "Tunnelly",
      "Twofoot",
      "Underhill",
      "Whitfoot"
    };
    #endregion
    #region Human
    private static string[] HumamMaleNames =
    {
      "Allan",
      "Aalton",
      "Aarchie",
      "Bennie",
      "Cesar",
      "Chiristian",
      "Damon",
      "Darren",
      "Delbert",
      "Dominic",
      "Ed",
      "Erik",
      "Felipe",
      "Gary",
      "Goeffery",
      "Gerardo",
      "Guy",
      "Hugh",
      "Jamie",
      "Jessie",
      "Johnathan",
      "Jonathan",
      "Julio",
      "Kelly",
      "Kelvin",
      "Lance",
      "Lonnie",
      "Loren",
      "Marco",
      "Mathew",
      "Max",
      "Nelson",
      "Pete",
      "Preston",
      "Randolph",
      "Robin",
      "Rudolph",
      "Rudy",
      "Tyrone"
    };

    private static string[] HumamFemaleNames =
    {
      "Alene",
      "Althea",
      "Annabelle",
      "Annetta",
      "Anspach",
      "Barb",
      "Benita",
      "Breanna",
      "Candida",
      "Carmel",
      "Carmella",
      "Cathrine",
      "Chandra",
      "Christal",
      "Christin",
      "Dahlem",
      "Dalia",
      "Dara",
      "Delgrosso",
      "Delois",
      "Dishon",
      "Dona",
      "Dorris",
      "Earlene",
      "Edwina",
      "Ellie",
      "Ericka",
      "Harriett",
      "India",
      "Ione",
      "Isabell",
      "Jami",
      "Julianne",
      "Kira",
      "Kisha",
      "Lilia",
      "Lorri",
      "Lyn",
      "Malinda",
      "Mandi",
      "Marylou",
      "Matilde",
      "Mayme",
      "Merry",
      "Milagros",
      "Neva",
      "Noreen",
      "Oralia",
      "Penelope",
      "Portia",
      "Rae",
      "Rafaela",
      "Reina",
      "Reta",
      "Roxanna",
      "Roxie",
      "Sandi",
      "Shanda",
      "Shayla",
      "Shiela",
      "Suzan",
      "Tiffani",
      "Verda",
      "Zella",
      "Zoe",
      "Zoila"
    };

    #endregion
    #region Orcish
    private static string[] OrcMaleNames = 
    {
      "Baarz",
      "Badash",
      "Baol",
      "Birt",
      "Brigash",
      "Brilg",
      "Briol",
      "Brurbag",
      "Budush",
      "Bunk",
      "Buunk",
      "Buur",
      "Eraalg",
      "Eraanak",
      "Eradush",
      "Eragar",
      "Erarz",
      "Eridish",
      "Eridush",
      "Erigdush",
      "Erort",
      "Eruugar",
      "Eruurag",
      "Gag",
      "Galo",
      "Gark",
      "Gigdish",
      "Gorg",
      "Graark",
      "Grik",
      "Grodish",
      "Grorag",
      "Grudush",
      "Grurg",
      "Gudish",
      "Gurg",
      "Guugdush",
      "Haarbag",
      "Hank",
      "Hark",
      "Hidish",
      "Higar",
      "Higdush",
      "Hilg",
      "Hink",
      "Hirag",
      "Hirk",
      "Holo",
      "Honk",
      "Hug",
      "Hur",
      "Kadush",
      "Karag",
      "Kilg",
      "Kok",
      "Kor",
      "Korg",
      "Koshnak",
      "Kraanak",
      "Kraashnak",
      "Kraol",
      "Krigash",
      "Krink",
      "Kriurk",
      "Krok",
      "Krork",
      "Krugdish",
      "Krulg",
      "Krur",
      "Kudish",
      "Kugor",
      "Kushnak",
      "Kuugdish",
      "Kuuk",
      "Kuuurk",
      "Pang",
      "Pank",
      "Part",
      "Pigdush",
      "Pilo",
      "Pir",
      "Podash",
      "Pogash",
      "Pogor",
      "Ponak",
      "Praanak",
      "Pralo",
      "Prashnak",
      "Prinak",
      "Prirt",
      "Prolo",
      "Pronak",
      "Proshnak",
      "Prurg",
      "Prurk",
      "Pruurbag",
      "Pruurk",
      "Pudish",
      "Pung",
      "Purk",
      "Puurg",
      "Raaol",
      "Ragash",
      "Rak",
      "Rang",
      "Rar",
      "Ridash",
      "Rigdish",
      "Rigor",
      "Rirz",
      "Rorag",
      "Rur",
      "Rurag",
      "Rurt",
      "Ruulg",
      "Vaagdush",
      "Vaak",
      "Vadash",
      "Vagdish",
      "Vig",
      "Vik",
      "Vog",
      "Vourk",
      "Vraadush",
      "Vraagdush",
      "Vraarug",
      "Vrarbag",
      "Vrarg",
      "Vrirz",
      "Vrogar",
      "Vrolg",
      "Vrudish",
      "Vrugash",
      "Vrugdish",
      "Vrurk",
      "Vugdush",
      "Vunak",
      "Vurg",
      "Vurug",
      "Vuuk"
    };
    #endregion
    #region Angelic
    private static string[] AngelicMaleNames = 
    {
      "Aachiaram",
      "Aarmouriam",
      "Abel",
      "Abitrion",
      "Abrana",
      "Abrisene",
      "Achcha",
      "Achiel",
      "Adaban",
      "Adamas",
      "Adonaiou",
      "Adonein",
      "Aesthesis",
      "Agromauna",
      "Akioreim",
      "Amen",
      "Amiorps",
      "Anaro",
      "Anasimalar",
      "Aol",
      "Arabeei",
      "Arachethopi",
      "Ararim",
      "Arbao",
      "Archendekta",
      "Archentechtha",
      "Areche",
      "Armoupieel",
      "Armozel",
      "Aroer",
      "Arouph",
      "Asaklas",
      "Asmenedas",
      "Asphixix",
      "Astaphaios",
      "Asterechme",
      "Astrops",
      "Athoth",
      "Athuro",
      "Atoimenpsephei",
      "Autogenes",
      "Banen-Ephroum",
      "Bano",
      "Baoum",
      "Barbar",
      "Barbelo",
      "Barias",
      "Barroph",
      "Basiliademe",
      "Bastan",
      "Bathinoth",
      "Bedouk",
      "Belbel",
      "Belias",
      "Beluai",
      "Biblo",
      "Bineborin",
      "Bissoum",
      "Blaomen",
      "Boabel",
      "Cain",
      "Chaaman",
      "Charaner",
      "Charcha",
      "Charcharb",
      "Chnoumeninorin",
      "Choux",
      "Chthaon",
      "Daveithai",
      "Dearcho",
      "Deitharbathas",
      "Diolimodraza",
      "Eilo",
      "Eleleth",
      "Eloaiou",
      "Eloim",
      "Emenun",
      "Entholleia",
      "Ephememphi",
      "Epinoia",
      "Erimacho",
      "Eteraphaope-Abron",
      "Evanthen",
      "Gesole",
      "Gorma-Kaiochlabar",
      "Harmas",
      "Imae",
      "Ipouspoboba",
      "Kalila-Oumbri",
      "Knyx",
      "Koade",
      "Kriman",
      "Krys",
      "Lampno",
      "Leekaphar",
      "Marephnouth",
      "Melceir-Adonein",
      "Meniggesstroeth",
      "Miamai",
      "Michael",
      "Nebrith",
      "Nenentophni",
      "Nous",
      "Odeor",
      "Onorthochrasaei",
      "Oriel",
      "Ormaoth",
      "Oroorrothos",
      "Oudidi",
      "Oummaa",
      "Ouriel",
      "Phikna",
      "Phiouthrom",
      "Phloxopha",
      "Phnene",
      "Phnouth",
      "Phthave",
      "Pisandrioptes",
      "Pronoia",
      "Pserim",
      "Riaramnacho",
      "Richram",
      "Roeror",
      "Sabalo",
      "Sabaoth",
      "Sabbede",
      "Saklas",
      "Samael",
      "Saphasatoel",
      "Senaphim",
      "Seth",
      "Sophia",
      "Sorma",
      "Sostrapal",
      "Synogchouta",
      "Taphreo",
      "Tebar",
      "Thaspomocha",
      "Thopithro",
      "Toechtha",
      "Trachoun",
      "Treneu",
      "Tupelon",
      "Verton",
      "Yabel",
      "Yakouib",
      "Yaltabaoth",
      "Yammeax",
      "Yao",
      "Yave",
      "Yeronumos",
      "Yobel",
      "Yoko",
      "Zabedo"
    };
    #endregion


    /// <summary>
    /// 
    /// </summary>
    /// <param name="race"></param>
    /// <param name="isMale"></param>
    /// <returns></returns>
    public static string GenerateName( NPCRace race, NPCGender gender )
    {
      switch( race )
      {
        case NPCRace.Dragonborne:
          return DragonborneName( gender );

        case NPCRace.Dwarf:
          return DwarvenName( gender );

        case NPCRace.Eldarin:
          return EladrinName( gender );

        case NPCRace.Elf:
          return ElvenName( gender );

        case NPCRace.HalfElf:
          if( rand.Next(2)==0 )
            return ElvenName( gender );
          else
            return HumanName( gender );

        case NPCRace.Gnome:
          return GnomishName( gender );

        case NPCRace.Orc:
          return OrcishName( gender );

        case NPCRace.HalfOrc:
          if( rand.Next(2)==0 )
            return OrcishName( gender );
          else
            return HumanName( gender );

        case NPCRace.Angelic:
          return AngelicName( gender );

        default: // human
          return HumanName( gender );
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isMale"></param>
    /// <returns></returns>
    public static string DragonborneName( NPCGender gender )
    {
      return PickFromList( gender==NPCGender.Male ? DragonBorneMaleNames : DragonBorneFemaleNames );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isMale"></param>
    /// <returns></returns>
    public static string DwarvenName( NPCGender gender )
    {
      return PickFromList( (gender==NPCGender.Male ? DwarvenMaleNames : DwarvenFemaleNames) ) + " " + PickFromList( DwarvenSurnames );
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="isMale"></param>
    /// <returns></returns>
    public static string EladrinName( NPCGender gender )
    {
      return PickFromList( (gender==NPCGender.Male ? EladrinMaleNames : EladrinFemaleNames) );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isMale"></param>
    /// <returns></returns>
    public static string ElvenName( NPCGender gender )
    {
      return PickFromList( (gender==NPCGender.Male ? ElvenMaleNames : ElvenFemaleNames) ) + " " + PickFromList( ElvenSurnames );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isMale"></param>
    /// <returns></returns>
    public static string GnomishName( NPCGender gender )
    {
      return PickFromList( (gender==NPCGender.Male ? GnomishMaleNames : GnomishFemaleNames) );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isMale"></param>
    /// <returns></returns>
    public static string HalflingName( NPCGender gender )
    {
      return PickFromList( (gender==NPCGender.Male ? HalflingMaleNames : HalflingFemaleNames) ) + " " + PickFromList( HalflingSurnames );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isMale"></param>
    /// <returns></returns>
    public static string HumanName( NPCGender gender )
    {
      return PickFromList( (gender==NPCGender.Male ? HumamMaleNames : HumamFemaleNames) );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isMale"></param>
    /// <returns></returns>
    public static string OrcishName( NPCGender gender )
    {
      return PickFromList( OrcMaleNames );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isMale"></param>
    /// <returns></returns>
    public static string AngelicName( NPCGender gender )
    {
      return PickFromList( AngelicMaleNames );
    }



    /// <summary>
    /// Utility Function - return a random entry from the given list
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private static string PickFromList( string[] list )
    {
      return list[ rand.Next( list.Length ) ];
    }

    /*
    //#region Random Generic Name Methods
    //private static string RandomName()
    //{
    //  switch( rand.Next( 3 ) )
    //  {
    //    case 0:
    //      // format CVCEnd
    //      return C().ToUpper() + V() + C() + E();

    //    case 1:
    //      // format CVCCVC
    //      return C().ToUpper() + V() + C() + C() + V() + C();

    //    default:
    //      // format CVCCVVC
    //      return C().ToUpper() + V() + C() + C() + V() + V() + C();
    //  }
    //}
    //private static string C()
    //{
    //  return consonants[rand.Next( consonants.Length )];
    //}
    //private static string V()
    //{
    //  return vowels[rand.Next( vowels.Length )];
    //}
    //private static string E()
    //{
    //  return endings[rand.Next( endings.Length )];
    //}
    //#endregion

    //#region Elven Name Gneration Methods
    //private static string ElvenPrefix( bool maleName )
    //{
    //  string[] prefix = { "Ael:knight",
    //                      "Aer:law,order",
    //                      "Af:ring",
    //                      "Ah:crafty,sly",
    //                      "Al:sea",
    //                      "Am:swan",
    //                      "Ama:beauty,beautiful",
    //                      "An:hand",
    //                      "Ang:glitter",
    //                      "Ansr:rune",
    //                      "Ar:gold,golden",
    //                      "Arì:silver",
    //                      "Arn:south",
    //                      "Aza:life,lives",
    //                      "Bael:guardian",
    //                      "Bes:oath",
    //                      "Cael:archer,arrow",
    //                      "Cal:faith",
    //                      "Cas:herald",
    //                      "Cla:rose",
    //                      "Cor:legend,legendary",
    //                      "Cy:onyx",
    //                      "Dae:white",
    //                      "Dho:falcon",
    //                      "Dre:hound",
    //                      "Du:crescent",
    //                      "Eil:azure,blue",
    //                      "Eir:sharp",
    //                      "El:green",
    //                      "Er:boar",
    //                      "Ev:stag",
    //                      "Fera:champion",
    //                      "Fi:rain",
    //                      "Fir:dark",
    //                      "Fis:light",
    //                      "Gael:pegasus",
    //                      "Gar:owl",
    //                      "Gil:griffin",
    //                      "Ha:free,freedom",
    //                      "Hu:horse",
    //                      "Ia:day",
    //                      "Il:mist",
    //                      "Ja:staff",
    //                      "Jar:dove",
    //                      "Ka:dragon",
    //                      "Kan:eagle",
    //                      "Ker:spell",
    //                      "Keth:wind",
    //                      "Koeh:earth",
    //                      "Kor:black",
    //                      "Ky:ruby",
    //                      "La:night",
    //                      "Laf:moon",
    //                      "Lam:east",
    //                      "Lue:riddle",
    //                      "Ly:wolf",
    //                      "Mai:death,slayer",
    //                      "Mal:war",
    //                      "Mara:priest",
    //                      "My:emerald",
    //                      "Na:ancient",
    //                      "Nai:oak",
    //                      "Nim:deep",
    //                      "Nu:hope,hopeful",
    //                      "Ny:diamond",
    //                      "Py:sapphire",
    //                      "Raer:unicorn",
    //                      "Re:bear",
    //                      "Ren:west",
    //                      "Rhy (Ry): jade",
    //                      "Ru:dream",
    //                      "Rua:star",
    //                      "Rum:meadow",
    //                      "Rid:spear",
    //                      "Sae:wood",
    //                      "Seh:soft",
    //                      "Sel:high",
    //                      "Sha:sun",
    //                      "She:age,time",
    //                      "Si:cat,feline",
    //                      "Sim:north",
    //                      "Sol:history,memory",
    //                      "Sum:water",
    //                      "Syl:faerie",
    //                      "Ta:fox",
    //                      "Tahl:blade",
    //                      "Tha:vigil,vigilance",
    //                      "Tho:true,truth",
    //                      "Ther:sky",
    //                      "Thro:lore,sage",
    //                      "Tia:magic",
    //                      "Tra:tree",
    //                      "Ty,Try: crystal",
    //                      "Uth:wizard",
    //                      "Ver:peace",
    //                      "Vil:finger,point",
    //                      "Von:ice",
    //                      "Ya:bridge,path,way",
    //                      "Za:royal",
    //                      "Zy:ivory"
    //                    };
    //  // grab one
    //  string pre = prefix[ rand.Next( prefix.Length ) ];

    //  // get only the name portion, not the meaning
    //  pre = pre.Split(':')[0];
    //  pre = pre.Split(',')[ rand.Next( pre.Split(',').Length ) ];

    //  // return the name portion
    //  return pre;
    //}
    //private static string ElvenSuffix( bool maleName)
    //{
    //  // -suffix : meaning
    //  // or
    //  // -suffix / -siffixFemale : meaning

    //  string []suffixes = { "-ae, -nae : whisper",
    //                        "-ael : great",
    //                        "-aer / -aera : singer, song",
    //                        "-aias / -aia : mate, husband / wife",
    //                        "-ah / -aha : wand",
    //                        "-aith / -aira : home",
    //                        "-al / -ala, -la, -lae, -llae : harmony",
    //                        "-ali : shadow",
    //                        "-am / -ama : strider",
    //                        "-an / -ana, -a, -ani, -uanna : make, maker",
    //                        "-ar / -ara, -ra : man / woman",
    //                        "-ari, -ri : spring",
    //                        "-aro, -ro : summer",
    //                        "-as, -ash, -sah : bow, fletcher",
    //                        "-ath : by, of, with",
    //                        "-avel : sword",
    //                        "-brar, -abrar, -ibrar : craft, crafter",
    //                        "-dar, -adar, -odar : world",
    //                        "-deth, -eath, -eth : eternal",
    //                        "-dre : charm, charming",
    //                        "-drim, -drimme, -udrim : flight, flyer",
    //                        "-dul : glade",
    //                        "-ean : ride, rider",
    //                        "-el, -ele / el, -ela) : hawk",
    //                        "-emar : honor",
    //                        "-en : autumn",
    //                        "-er, -erl, -ern : winter",
    //                        "-ess, -esti : elves, elvin",
    //                        "-evar : flute",
    //                        "-fel, -afel, -efel : lake",
    //                        "-hal, -ahal, -ihal : pale, weak",
    //                        "-har, -ihar, -uhar : wisdom, wise",
    //                        "-hel, -ahel, -ihel : sadness, tears",
    //                        "-ian / ianna, -ia, -ii, -ion : lord / lady",
    //                        "-iat : fire",
    //                        "-ik : might, mighty",
    //                        "-il, -iel, -ila, -lie : gift, giver",
    //                        "-im : duty",
    //                        "-in, -inar, -ine : sibling, brother / sister",
    //                        "-ir, -ira, -ire : dusk",
    //                        "-is, -iss, -ist : scribe, scroll",
    //                        "-ith, -lath, -lith, -lyth : child, young",
    //                        "-kash, -ashk, -okash: fate",
    //                        "-ki : void",
    //                        "-lan, -olan / -lanna, -lean, -ola: son / daughter",
    //                        "-lam, -ilam,ulam: fair",
    //                        "-lar, -lirr: shine",
    //                        "-las : wild",
    //                        "-lian / -lia : master / mistress",
    //                        "-lis, -elis, -lys: breeze",
    //                        "-lon, -ellon: chief",
    //                        "-lyn, -llinn, -lihn: bolt, ray",
    //                        "-mah / -ma, -mahs: mage",
    //                        "-mil, -imil, -umil: bond, promise",
    //                        "-mus : ally, companion",
    //                        "-nal, -inal, -onal: distant, far",
    //                        "-nes : heart",
    //                        "-nin, -nine, -nyn: rite, ritual",
    //                        "-nis, -anis: dawn",
    //                        "-on/-onna: Keep/Keeper",
    //                        "-or, -oro: Flower",
    //                        "-oth, -othi: gate",
    //                        "-que : forgotten, lost",
    //                        "-quis : branch, limb",
    //                        "-rah, -rae, -raee: beast",
    //                        "-rad, -rahd: leaf",
    //                        "-rail/-ria, -aral, -ral, -ryl: hunt, hunter",
    //                        "-ran, -re, -reen: binding, shackles",
    //                        "-reth, -rath: arcane",
    //                        "-ro, -ri, -ron: walker, walks",
    //                        "-ruil, -aruil,eruil: noble",
    //                        "-sal, -isal, -sali: honey, sweet",
    //                        "-san : drink, wine",
    //                        "-sar, -asar, -isar: quest, seeker",
    //                        "-sel, -asel, -isel: mountain",
    //                        "-sha, -she, -shor: ocean",
    //                        "-spar : fist",
    //                        "-tae, -itae: beloved, love",
    //                        "-tas, -itas: wall, ward",
    //                        "-ten, -iten: spinner",
    //                        "-thal, -ethal / -tha, -etha: heal, healer, healing",
    //                        "-thar, -ethar, -ithar: friend",
    //                        "-ther, -ather, -thir: armor, protection",
    //                        "-thi, -ethil, -thil: wing",
    //                        "-thus, -aethus /-thas, -aethas: harp, harper",
    //                        "-ti, -eti, -til: eye, sight",
    //                        "-tril, -atril  /-tria, -atri, -atria: dance, dancer",
    //                        "-ual, -lua: holy",
    //                        "-uath, -luth, -uth: lance",
    //                        "-us / -ua : cousin, kin",
    //                        "-van / -vanna : forest",
    //                        "-var, -avar / -vara, -avara: father / mother",
    //                        "-vain, -avain: spirit",
    //                        "-via, -avia: good fortune, luck",
    //                        "-vin, -avin: storm",
    //                        "-wyn : music, muscian",
    //                        "-ya : helm",
    //                        "-yr / -yn : bringer",
    //                        "-yth : folk, people",
    //                        "-zair, -azair /-zara, -ezara: lightning"
    //                      };

    //  // pick one
    //  string suffix = suffixes[ rand.Next( suffixes.Length ) ];

    //  // strip out the name part
    //  suffix = suffix.Split(':')[0].Trim();

    //  // look for male/female sections
    //  if( suffix.Split( '/' ).Length == 2 )
    //  {
    //    if( maleName )
    //      suffix = suffix.Split( '/' )[0];
    //    else
    //      suffix = suffix.Split( '/' )[1];
    //  }

    //  // pick one of the options
    //  suffix = suffix.Split(',')[ rand.Next(suffix.Split(',').Length) ];

    //  // no spaces, remove the hyphen
    //  return suffix.Trim().Replace("-","");
    //}
    //private static string ElvenHousePrefix()
    //{
    //  return "";
    //}
    //private static string ElvenHouseSuffix()
    //{
    //  return "";
    //}
    //public static string ElvenName( bool maleName )
    //{
    //  // shamelessly stolen from http://www.angelfire.com/rpg2/vortexshadow/names.html

    ////D10  Result
    ////1-4  Roll once on Table 2 and once on Table 3
    ////5-7  Roll once on Table 2 and twice on Table 3
    ////8-9  Roll once on Table 2 and once on Table 3 for a first name, then once on Table 2 and twice on Table 3 for a second name
    ////10  Roll once on Table 3, add an apostrophe, then roll once on Table 2 and twice on Table 3

    //  int roll = rand.Next(10);

    //  if( roll <= 3 )
    //    return ElvenPrefix( maleName ) + ElvenSuffix( maleName );
    //  if( roll <= 6 )
    //    return ElvenPrefix( maleName ) + ElvenSuffix( maleName ) + ElvenSuffix( maleName );
    //  if( roll <= 8 )
    //    return ElvenPrefix( maleName ) + ElvenSuffix( maleName ) + " "  + ElvenPrefix( maleName ) + ElvenSuffix( maleName ) + ElvenSuffix( maleName );
      
    //  return ElvenSuffix( maleName ) + "-" + ElvenPrefix( maleName ) + ElvenSuffix( maleName ) + ElvenSuffix( maleName );

    ////Optional Table 1B (D10)
    ////D10  Result
    ////1-3  Roll once on Table 4 and once on Table 5
    ////4-5  Roll once on Table 4 and twice on Table 5
    ////6-7  Roll once on Table 4 and once on Table 5, add an apostrophe, then roll again on Table 3.
    ////8-9  Roll once on Table 4 and once on Table 5 for a first name, then roll on Table 1 again for a second name.
    ////10  Roll once on Table 5, add an apostrophe, then roll once on Table 4 and once on Table 5

     
    ////Table 2 - Prefixes (D100)
    ////{1} Ael: knight 
    ////{2} Aer : law, order
    ////{3} Af : ring 
    ////{4} Ah : crafty, sly 
    ////{5} Al : sea 
    ////{6} Am : swan
    ////{7} Ama : beauty, beautiful 
    ////{8} An : hand 
    ////{9} Ang : glitter 
    ////{10} Ansr : rune 
    ////{11} Ar : gold, golden 
    ////{12} Arì : silver 
    ////{13} Arn : south 
    ////{14} Aza : life, lives 
    ////{15} Bael : guardian 
    ////{16} Bes : oath 
    ////{17} Cael : archer, arrow 
    ////{18} Cal : faith 
    ////{19} Cas : herald 
    ////{20} Cla : rose 
    ////{21} Cor : legend, legendary 
    ////{22} Cy : onyx 
    ////{23} Dae : white 
    ////{24} Dho : falcon 
    ////{25} Dre : hound 
    ////{26} Du : crescent 
    ////{27} Eil : azure, blue 
    ////{28} Eir : sharp 
    ////{29} El : green 
    ////{30} Er : boar 
    ////{31} Ev : stag 
    ////{32} Fera : champion 
    ////{33} Fi : rain 
    ////{34} Fir : dark
    ////{35} Fis : light 
    ////{36} Gael : pegasus 
    ////{37} Gar : owl 
    ////{38} Gil : griffin 
    ////{39} Ha : free, freedom 
    ////{40} Hu : horse 
    ////{41} Ia : day 
    ////{42} Il : mist 
    ////{43} Ja : staff 
    ////{44} Jar : dove 
    ////{45} Ka : dragon 
    ////{46} Kan : eagle 
    ////{47} Ker : spell 
    ////{48} Keth : wind 
    ////{49} Koeh : earth 
    ////{50} Kor : black  {51} Ky : ruby 
    ////{52} La : night 
    ////{53} Laf : moon 
    ////{54} Lam : east 
    ////{55} Lue : riddle 
    ////{56} Ly : wolf 
    ////{57} Mai : death, slayer 
    ////{58} Mal : war 
    ////{59} Mara : priest 
    ////{60} My : emerald 
    ////{61} Na : ancient 
    ////{62} Nai : oak 
    ////{63} Nim : deep 
    ////{64} Nu : hope, hopeful 
    ////{65} Ny : diamond 
    ////{66} Py : sapphire 
    ////{67} Raer : unicorn 
    ////{68} Re : bear 
    ////{69} Ren : west 
    ////{70} Rhy (Ry): jade 
    ////{71} Ru : dream 
    ////{72} Rua : star 
    ////{73} Rum : meadow 
    ////{74} Rid : spear
    ////{75} Sae : wood 
    ////{76} Seh : soft 
    ////{77} Sel : high 
    ////{78} Sha : sun 
    ////{79} She : age, time 
    ////{80} Si : cat, feline 
    ////{81} Sim : north 
    ////{82} Sol : history, memory 
    ////{83} Sum : water 
    ////{84} Syl : faerie 
    ////{85} Ta : fox 
    ////{86} Tahl : blade 
    ////{87} Tha : vigil, vigilance 
    ////{88} Tho : true, truth 
    ////{89} Ther : sky 
    ////{90} Thro : lore, sage 
    ////{91} Tia : magic 
    ////{92} Tra : tree 
    ////{93} Ty (Try): crystal 
    ////{94} Uth : wizard 
    ////{95} Ver : peace 
    ////{96} Vil : finger, point 
    ////{97} Von : ice 
    ////{98} Ya : bridge, path, way 
    ////{99} Za : royal 
    ////{100} Zy : ivory

    ////Table 3 - Suffixes (D100)
    ////{1} -ae (-nae) : whisper 
    ////{2} -ael : great 
    ////{3} -aer / -aera : singer, song 
    ////{4} -aias / -aia : mate, husband / wife 
    ////{5} -ah / -aha : wand 
    ////{6} -aith / -aira : home 
    ////{7} -al / -ala (-la; -lae; -llae) : harmony 
    ////{8} -ali : shadow 
    ////{9} -am / -ama : strider 
    ////{10} -an / -ana (-a; -ani; -uanna) : make, maker 
    ////{11} -ar / -ara (-ra) : man / woman 
    ////{12} -ari (-ri) : spring 
    ////{13} -aro (-ro) : summer 
    ////{14} -as (-ash; -sah) : bow, fletcher 
    ////{15} -ath : by, of, with 
    ////{16} -avel : sword 
    ////{17} -brar (-abrar; -ibrar) : craft, crafter 
    ////{18} -dar (-adar; -odar) : world 
    ////{19} -deth (-eath; -eth) : eternal 
    ////{20} -dre : charm, charming 
    ////{21} -drim (-drimme; -udrim) : flight, flyer 
    ////{22} -dul : glade 
    ////{23} -ean : ride, rider 
    ////{24} -el (ele / -ela) : hawk 
    ////{25} -emar : honor 
    ////{26} -en : autumn 
    ////{27} -er (-erl; -ern) : winter 
    ////{28} -ess (-esti) : elves, elvin 
    ////{29} -evar : flute 
    ////{30} -fel (-afel; -efel) : lake 
    ////{31} -hal (-ahal; -ihal) : pale, weak 
    ////{32} -har (-ihar; -uhar) : wisdom, wise 
    ////{33} -hel (-ahel; -ihel) : sadness, tears 
    ////{34} -ian / ianna (-ia; -ii; -ion) : lord / lady 
    ////{35} -iat : fire 
    ////{36} -ik : might, mighty 
    ////{37} -il (-iel; -ila; -lie) : gift, giver 
    ////{38} -im : duty 
    ////{39} -in (-inar; -ine) : sibling, brother / sister 
    ////{40} -ir (-ira; -ire) : dusk 
    ////{41} -is (-iss; -ist) : scribe, scroll 
    ////{42} -ith (-lath; -lith; -lyth) : child, young 
    ////{43} -kash (-ashk; -okash) : fate 
    ////{44} -ki : void 
    ////{45} -lan / -lanna (-lean; -olan / -ola) : son / daughter 
    ////{46} -lam (-ilam; -ulam) : fair 
    ////{47} -lar (-lirr) : shine 
    ////{48} -las : wild 
    ////{49} -lian / -lia : master / mistress 
    ////{50} -lis (-elis; -lys) : breeze  {51} -lon (-ellon) : chief 
    ////{52} -lyn (-llinn; -lihn) : bolt, ray 
    ////{53} -mah / -ma (-mahs) : mage 
    ////{54} -mil (-imil; -umil) : bond, promise 
    ////{55} -mus : ally, companion 
    ////{56} -nal (-inal; -onal) : distant, far 
    ////{57} -nes : heart 
    ////{58} -nin (-nine; -nyn) : rite, ritual 
    ////{59} -nis (-anis) : dawn 
    ////{60} -on/onna: Keep/Keeper
    ////{61} -or (oro): Flower
    ////{62} -oth (-othi) : gate 
    ////{63} -que : forgotten, lost 
    ////{64} -quis : branch, limb
    ////{65} -rah(-rae; -raee) : beast 
    ////{66} -rad(-rahd) : leaf 
    ////{67} -rail/-ria (-aral; -ral; -ryl) : hunt, hunter 
    ////{68} -ran (-re; -reen) : binding, shackles
    ////{69} -reth (-rath) : arcane
    ////{70} -ro (-ri; -ron) : walker, walks 
    ////{71} -ruil (-aruil; -eruil) : noble 
    ////{72} -sal (-isal; -sali) : honey, sweet 
    ////{73} -san : drink, wine 
    ////{74} -sar (-asar; -isar) : quest, seeker 
    ////{75} -sel (-asel; -isel) : mountain 
    ////{76} -sha (-she; -shor) : ocean 
    ////{77} -spar : fist 
    ////{78} -tae (-itae) : beloved, love 
    ////{79} -tas (-itas) : wall, ward 
    ////{80} -ten (-iten) : spinner 
    ////{81} -thal /-tha (-ethal / -etha) : heal, healer, healing 
    ////{82} -thar (-ethar; -ithar) : friend 
    ////{83} -ther (-ather; -thir) : armor, protection 
    ////{84} -thi (-ethil; -thil) : wing 
    ////{85} -thus /-thas (-aethus / -aethas) : harp, harper 
    ////{86} -ti (-eti;-til) : eye, sight 
    ////{87} -tril /-tria (-atri; -atril / -atria) : dance, dancer 
    ////{88} -ual (-lua) : holy 
    ////{89} -uath (-luth; -uth) : lance 
    ////{90} -us /-ua : cousin, kin 
    ////{91} -van /-vanna : forest 
    ////{92} -var /-vara (-avar / -avara) : father / mother 
    ////{93} -vain (-avain) : spirit 
    ////{94} -via (-avia) : good fortune, luck 
    ////{95} -vin (-avin) : storm 
    ////{96} -wyn : music, muscian 
    ////{97} -ya : helm 
    ////{98} -yr / -yn : bringer 
    ////{99} -yth : folk, people
    ////{100} -zair /-zara (-azair / -ezara) : lightning

    ////Table 4 - House Name Prefixes(D100)
    ////{1-3} Alean : The noble line of
    ////{4-6} Alea : Traders in
    ////{7-10} Arabi : Daughters of
    ////{11-13} Arkenea : Mages of
    ////{14-16} Auvrea : Blood of the
    ////{17-20} Baequi : Blessed by
    ////{21-23} Banni : Holder's of
    ////{24-26} Cyred : Warriors from
    ////{27-30} Dirth : Victors of
    ////{31-33} Dryear : Champions of 
    ////{34-36} Dwin’ : Walkers in
    ////{37-40} Eyllis : Lands of
    ////{41-43} Eyther : The Forests of
    ////{44-46} Freani : Friends to
    ////{47-50} Gysse : Clan of   
    ////{51-53} Heasi : Those above
    ////{54-56} Hlae : Seers of
    ////{57-60} Hunith : The sisterhood of
    ////{61-63} Kennyr : Sworn to
    ////{64-66} Kille : People of
    ////{67-70} Maern : Defenders from
    ////{71-73} Melith : Mothers of
    ////{74-76} Myrth : Honoured of
    ////{77-80} Norre : Sacred to
    ////{81-83} Orle : Guild of
    ////{84-86} Oussea : Heirs to
    ////{87-90} Rilynn : House of
    ////{91-93} Teasen' : Trackers of
    ////{94-96} Tyr : Mistresses of
    ////{97-00} Tyrnea : Children of

    ////Table 5 - House Name Suffixes (D100)
    ////{1-3} -altin : The branch
    ////{4-6} -anea : The night
    ////{7-10} -annia : The willow
    ////{11-13} -aear : Water
    ////{14-16} -arnith : Fire
    ////{17-20} -atear : The way
    ////{21-23} -athem : The dragons
    ////{24-26} -dlues : The bow
    ////{27-30} -elrvis : The leaves
    ////{31-33} -eplith : The forest
    ////{34-36} -ettln : Magic
    ////{37-40} -ghymn : The forgotten ways
    ////{41-43} -itryn : History
    ////{44-46} -lylth : The blade
    ////{47-50} -mitore : The moon  {51-53} -nddare : The winds
    ////{54-56} -neldth : The arcane
    ////{57-60} -rae : Powers of Light
    ////{61-63} -raheal : The gods
    ////{64-66} -rretyn : The heavens
    ////{67-70} -sithek : Adamantite
    ////{71-73} -thym : Challenges
    ////{74-76} -tlarn : Mysteries
    ////{77-80} -tlithar : Victory
    ////{81-83} -tylar : The healers
    ////{84-86} -undlin : The lover’s kiss
    ////{87-90} -urdrenn : The light
    ////{91-93} -valsa : Silken weaver 
    ////{94-96} -virrea : Success
    ////{97-100} -zea : The crystal growth
    //}
    //#endregion

    //#region Dwarven Names
    //private static string DwarvenName( NPCGender gender )
    //{
    //  if( isMale )
    //    return DwarvenMaleName() + " " + DwarvenSurname();
    //  else
    //    return DwarvenFemaleName() + " " + DwarvenSurname();
    //}
    //private static string DwarvenSurname()
    //{
    //  //List<GrammarPart> currentGrammar = CreateGrammar( DwarvenSurnames );
    //  //return GenerateNameFromGammar( currentGrammar );
    //  return "";
    //}
    //private static string DwarvenFemaleName()
    //{
    //  List<GrammarPart> currentGrammar = CreateGrammar( DwarvenFemaleNames );
    //  return GenerateNameFromGammar( currentGrammar );
    //}
    //private static string DwarvenMaleName()
    //{
    //  List<GrammarPart> currentGrammar = CreateGrammar( DwarvenMaleNames );
    //  return GenerateNameFromGammar( currentGrammar );
    //}
    //private static string GenerateNameFromGammar( List<GrammarPart> currentGrammar )
    //{
    //  GrammarPart p = null;
    //  while( p==null || p.follow.Count == 0 )
    //  {
    //    p = currentGrammar[rand.Next( currentGrammar.Count )];
    //  }

    //  string cur = p.condition;
    //  int times  = rand.Next( 2, 4 );
    //  string tmp = p.condition;

    //  for( int i=0; i<times; i++ )
    //  {
    //    if( p.follow.Count > 0 )
    //      cur = p.follow[rand.Next( p.follow.Count )];
    //    else
    //      break;

    //    tmp += cur;
    //    p = FindPart( currentGrammar, cur );

    //    if( p==null )
    //      break;
    //  }

    //  return tmp;
    //}
    //#endregion

    //#region CreateGrammar
    //private static GrammarPart FindPart( List<GrammarPart> list, string s )
    //{
    //  for( int i=0; i<list.Count; i++ )
    //    if( list[i].condition == s )
    //      return list[i];

    //  return null;
    //}
    //private static List<GrammarPart> CreateGrammar( string[] names )
    //{
    //  List<GrammarPart> parts = new List<GrammarPart>();

    //  for( int n=0; n<names.Length; n++ )
    //  {
    //    for( int i=0; i<names[n].Length; i++ )
    //    {
    //      if( i+2 >= names[n].Length )
    //        break;

    //      string condition = names[n].Substring(i,2).ToLower();

    //      // find the grammarPart that defines this letter
    //      GrammarPart part = FindPart( parts, condition );

    //      if( part == null )
    //      {
    //        part = new GrammarPart();
    //        part.condition = condition;
    //        parts.Add( part );
    //      }
    //      if( i+3 < names[n].Length )
    //        part.follow.Add( names[n].Substring( i+2, 2 ) );
    //    }
    //  }

    //  return parts;
    //}
    //#endregion
     */
  }
}