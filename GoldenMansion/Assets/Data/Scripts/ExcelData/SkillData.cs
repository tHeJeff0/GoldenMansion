using System.IO;
using System.Collections.Generic;

namespace ExcelData
{
    public class SkillData : IDataSheet
    {
        public class Item
        {
            public int ID;
            public string name;
            public string effectDesc;
            public int descID;
            public int[] conditionValue;
            public int[] effectValue;
        }

        private static SkillData s_Instance;
        private static SkillData Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new SkillData();
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

        public string sheetName => "SkillData";

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
                    int nameIndex = sheetHeader.IndexOf("name", "string");
                    int effectDescIndex = sheetHeader.IndexOf("effectDesc", "string");
                    int descIDIndex = sheetHeader.IndexOf("descID", "int");
                    int conditionValueIndex = sheetHeader.IndexOf("conditionValue", "int[]");
                    int effectValueIndex = sheetHeader.IndexOf("effectValue", "int[]");

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
                            else if (j == nameIndex)
                            {
                                newItem.name = reader.ReadString();
                            }
                            else if (j == effectDescIndex)
                            {
                                newItem.effectDesc = reader.ReadString();
                            }
                            else if (j == descIDIndex)
                            {
                                newItem.descID = reader.ReadInt32();
                            }
                            else if (j == conditionValueIndex)
                            {
                                int l_4 = reader.ReadInt32();
                                newItem.conditionValue = new int[l_4];
                                for(int i_4 = 0; i_4 < l_4; ++i_4)
                                {
                                    newItem.conditionValue[i_4] = reader.ReadInt32();
                                }
                            }
                            else if (j == effectValueIndex)
                            {
                                int l_5 = reader.ReadInt32();
                                newItem.effectValue = new int[l_5];
                                for(int i_5 = 0; i_5 < l_5; ++i_5)
                                {
                                    newItem.effectValue[i_5] = reader.ReadInt32();
                                }
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
    