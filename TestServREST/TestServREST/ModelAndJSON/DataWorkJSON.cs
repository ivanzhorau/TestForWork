using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestServREST.ModelAndJSON
{
    public static class DataWorkJSON
    {
        private static async void WriteJSON(string filename, Lot[] lots)
        {
            string json = JsonSerializer.Serialize<Lot[]>(lots);
            File.WriteAllText(filename, string.Empty);

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {

                byte[] array = System.Text.Encoding.UTF8.GetBytes(json);

                await fs.WriteAsync(array, 0, array.Length);

            }
        }

        public static Lot[] ReadJSON(string filename)
        {
            Lot[] lots;
            using (FileStream fstream = File.OpenRead(filename))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                lots = JsonSerializer.Deserialize<Lot[]>(array);

            }
            return lots;
        }
        public static void Add(string filename, Lot lot)
        {
            Lot[] lots = ReadJSON(filename);
            Lot[] newLots = new Lot[lots.Length + 1];
            for (int i = 0; i < lots.Length; i++)
            {
                newLots[i] = lots[i];
            }
            newLots[lots.Length] = new Lot { Title = "Default",ImagePath = "https://sun9-56.userapi.com/impg/lHHTjendH1AhyStnljXjpJCdk1j8SUOM21BzGw/SVBZ5GCiO9o.jpg?size=1280x960&quality=96&sign=d1a4514d16a8a52c9c5ab28891c9a9c6&type=album" };
            if (lots.Length != 0)
                newLots[lots.Length].Id = newLots[lots.Length-1].Id + 1;
            else
                newLots[lots.Length].Id = 0;
            WriteJSON(filename, newLots);
        }
        public static void Change(string filename, Lot lot)
        {
            Lot[] lots = ReadJSON(filename);
            for (int i = 0; i < lots.Length; i++)
            {
                if (lots[i].Id == lot.Id) {
                    lots[i] = lot;
                }
            }
            WriteJSON(filename, lots);

        }
        public static void Delete(string filename, Lot lot)
        {
            Lot[] lots = ReadJSON(filename);
            Lot[] newLots = new Lot[lots.Length - 1];
            int i = 0;
            for (; lots[i].Id != lot.Id; i++)
            {
                newLots[i] = lots[i];
            }
            for (; i<newLots.Length; i++)
            {
                newLots[i] = lots[i+1];
            }

            WriteJSON(filename, newLots);
        }
    }
}
