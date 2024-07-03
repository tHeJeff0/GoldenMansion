using System.IO;
using System.Collections.Generic;

namespace ExcelData
{
    public class RoomData : IDataSheet
    {
        public class Item
        {
            public int roomID;
            public string name;
            public int basicRent;
            public int unlockCost;
            public int effectID;
            public string effectDesc;
        }

        private static RoomData s_Instance;
        private static RoomData Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new RoomData();
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

        public string sheetName => "RoomData";

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
                    int roomIDIndex = sheetHeader.IndexOf("roomID", "int");
                    int nameIndex = sheetHeader.IndexOf("name", "string");
                    int basicRentIndex = sheetHeader.IndexOf("basicRent", "int");
                    int unlockCostIndex = sheetHeader.IndexOf("unlockCost", "int");
                    int effectIDIndex = sheetHeader.IndexOf("effectID", "int");
                    int effectDescIndex = sheetHeader.IndexOf("effectDesc", "string");

                    #if UNITY_EDITOR
                    bool promptMismatchColumns = false;
                    #endif
                    for (int i = 0; i < rows; ++i)
                    {
                        Item newItem = new Item();
                        for (int j = 0; j < columns; ++j)
                        {
                            SheetHeader.Item headerItem = headerItems[j];

                            if (j == roomIDIndex)
                            {
                                newItem.roomID = reader.ReadInt32();
                            }
                            else if (j == nameIndex)
                            {
                                newItem.name = reader.ReadString();
                            }
                            else if (j == basicRentIndex)
                            {
                                newItem.basicRent = reader.ReadInt32();
                            }
                            else if (j == unlockCostIndex)
                            {
                                newItem.unlockCost = reader.ReadInt32();
                            }
                            else if (j == effectIDIndex)
                            {
                                newItem.effectID = reader.ReadInt32();
                            }
                            else if (j == effectDescIndex)
                            {
                                newItem.effectDesc = reader.ReadString();
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
                        m_Items.Add(newItem.roomID, newItem);
                    }
                }
            }
            
        }

    }
}
    