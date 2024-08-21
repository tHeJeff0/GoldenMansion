using System.IO;
using System.Collections.Generic;

namespace ExcelData
{
    public class GuestMBTIData : IDataSheet
    {
        public class Item
        {
            public int ID;
            public int personaID;
            public string name;
            public int nameID;
            public string effectDesc;
            public int descID;
            public int skillID;
            public string iconRoute;
        }

        private static GuestMBTIData s_Instance;
        private static GuestMBTIData Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new GuestMBTIData();
                    s_Instance.Init();
                    DataService.RegisterSheet(s_Instance);
                }
                return s_Instance;
            }
        }

        public static Item GetItem(int key)
        {
            Instance.m_Items.TryGetValue(key, out Item foundItem);
            #if UNITY_EDITOR
            if (foundItem == null)
            {
                UnityEngine.Debug.LogWarningFormat("{0} do not contains item of key '{1}'.", Instance.sheetName, key);
            }
            #endif
            return foundItem;
        }

        public static IEnumerable<KeyValuePair<int, Item>> GetDict()
        {
            return Instance.m_Items;
        }

        private Dictionary<int, Item> m_Items = new Dictionary<int, Item>();

        public string sheetName => "GuestMBTIData";

        private void Init()
        {
            byte[] bytes = DataService.GetSheetBytes(sheetName);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using(BinaryReader reader = new BinaryReader(ms))
                {
                    reader.ReadString(); //sheetName

                    //Read header
                    SheetHeader sheetHeader = new SheetHeader();
                    sheetHeader.ReadFrom(reader);
                    List<SheetHeader.Item> headerItems = sheetHeader.items;

                    int columns = headerItems.Count;
                    int rows = reader.ReadInt32();

                    //Get Item indices
                    int IDIndex = sheetHeader.IndexOf("ID", "int");
                    int personaIDIndex = sheetHeader.IndexOf("personaID", "int");
                    int nameIndex = sheetHeader.IndexOf("name", "string");
                    int nameIDIndex = sheetHeader.IndexOf("nameID", "int");
                    int effectDescIndex = sheetHeader.IndexOf("effectDesc", "string");
                    int descIDIndex = sheetHeader.IndexOf("descID", "int");
                    int skillIDIndex = sheetHeader.IndexOf("skillID", "int");
                    int iconRouteIndex = sheetHeader.IndexOf("iconRoute", "string");

                    #if UNITY_EDITOR
                    bool promptMismatchColumns = false;
                    #endif
                    for (int i = 0; i < rows; ++i)
                    {
                        Item newItem = new Item();
                        for (int j = 0; j < columns; ++j)
                        {
                            SheetHeader.Item headerItem = headerItems[j];

                            if (j == IDIndex)
                            {
                                newItem.ID = reader.ReadInt32();
                            }
                            else if (j == personaIDIndex)
                            {
                                newItem.personaID = reader.ReadInt32();
                            }
                            else if (j == nameIndex)
                            {
                                newItem.name = reader.ReadString();
                            }
                            else if (j == nameIDIndex)
                            {
                                newItem.nameID = reader.ReadInt32();
                            }
                            else if (j == effectDescIndex)
                            {
                                newItem.effectDesc = reader.ReadString();
                            }
                            else if (j == descIDIndex)
                            {
                                newItem.descID = reader.ReadInt32();
                            }
                            else if (j == skillIDIndex)
                            {
                                newItem.skillID = reader.ReadInt32();
                            }
                            else if (j == iconRouteIndex)
                            {
                                newItem.iconRoute = reader.ReadString();
                            }
                            else
                            {
                                DataService.ReadAndDrop(reader, headerItem.valType);
                                #if UNITY_EDITOR
                                if (!promptMismatchColumns)
                                {
                                    UnityEngine.Debug.LogWarningFormat("Data sheet '{0}' find mismatch columns for '{1}({2})'.", sheetName, headerItem.name, headerItem.valType);
                                    promptMismatchColumns = true;
                                }
                                #endif
                            }
                        }
                        m_Items.Add(newItem.ID, newItem);
                    }
                }
            }
            
        }

    }
}
    