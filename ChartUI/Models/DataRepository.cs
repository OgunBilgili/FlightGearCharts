using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChartUI.Models
{
    public class DataRepository : IDataRepository
    {
        private readonly AppDbContext context;
        public DateTime dateValue;

        public DataRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<FlightGear> GetAllDatas()
        {
            return context.FlightGearDatas;
        }

        public List<FlightGear> GetAllDatasAsList(DateTime dateTime)
        {
            return (from c in context.FlightGearDatas where c.FlightDate == dateTime select c).ToList();
        }

        public List<DateTime> GetDates()
        {
            var result = context.FlightGearDatas.Select(m => m.FlightDate).Distinct();
            return result.ToList();

        }

        public void SavetoDatabase()
        {
            string filename = @"C:\Users\ogunb\KTUN Launch Control\KTUN Launch Control\CSV\KtunLC.csv";

            if (File.Exists(filename))
            {
                using (var reader = new StreamReader(@"C:\Users\ogunb\KTUN Launch Control\KTUN Launch Control\CSV\KtunLC.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        FlightGear gd = new FlightGear();
                        gd.Altitude = float.Parse(values[0]);
                        gd.Roll = float.Parse(values[3]);
                        gd.Pitch = float.Parse(values[4]);
                        gd.Yaw = float.Parse(values[5]);
                        gd.Speed = float.Parse(values[6]);
                        DateTime.TryParse(values[7], out dateValue);
                        gd.FlightDate = dateValue;

                        context.FlightGearDatas.Add(gd);
                        context.SaveChanges();
                    }

                    reader.Close();
                    File.Delete(@"C:\Users\ogunb\KTUN Launch Control\KTUN Launch Control\CSV\KtunLC.csv");
                }
            }
            else
            {
                //File has been read already
            }
        }

        public void SavetoDatabaseNew(string data)
        {
            var line = data;
            var values = line.Split(',');

            FlightGear gd = new FlightGear();
            gd.Altitude = float.Parse(values[0]);
            gd.Roll = float.Parse(values[1]);
            gd.Pitch = float.Parse(values[2]);
            gd.Yaw = float.Parse(values[3]);
            gd.Speed = float.Parse(values[4]);
            DateTime.TryParse(values[5], out dateValue);
            gd.FlightDate = dateValue;
            gd.aAltitude = float.Parse(values[6]);
            gd.aRoll = float.Parse(values[7]);
            gd.aPitch = float.Parse(values[8]);
            gd.aHeading = float.Parse(values[9]);
            gd.aSpeed = float.Parse(values[10]);

            context.FlightGearDatas.Add(gd);
            context.SaveChanges();
        }

    }
}
