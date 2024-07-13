using System.IO;
using System.Collections.Generic;

namespace ExcelData
{
    public class Level : IDataSheet
    {
        public class Item
        {
            public int levelID;
            public int days;
            public float target;
        }

        private static Level s_Instance;
        private static Level Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new Level();
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

        public string sheetName => "Level";

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
                    int levelIDIndex = sheetHeader.IndexOf("levelID", "int");
                    int daysIndex = sheetHeader.IndexOf("days", "int");
                    int targetIndex = sheetHeader.IndexOf("target", "float");

                    #if UNITY_EDITOR
                    bool promptMismatchColumns = false;
                    #endif
                    for (int i = 0; i < rows; ++i)
                    {
                        Item newItem = new Item();
                        for (int j = 0; j < columns; ++j)
                        {
                            SheetHeader.Item headerItem = headerItems[j];

                            if (j == levelIDIndex)
                            {
                                newItem.levelID = reader.ReadInt32();
                            }
                            else if (j == daysIndex)
                            {
                                newItem.days = reader.ReadInt32();
                            }
                            else if (j == targetIndex)
                            {
                                newItem.target = reader.ReadSingle();
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
                        m_Items.Add(newItem.levelID, newItem);
                    }
                }
            }
            
        }

    }
}
    