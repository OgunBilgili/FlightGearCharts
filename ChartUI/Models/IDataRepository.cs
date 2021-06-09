using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChartUI.Models
{
    public interface IDataRepository
    {
        IEnumerable<FlightGear> GetAllDatas();
        List<FlightGear> GetAllDatasAsList(DateTime dateTime);
        List<DateTime> GetDates();
        void SavetoDatabase();

        void SavetoDatabaseNew(string data);
    }
}
