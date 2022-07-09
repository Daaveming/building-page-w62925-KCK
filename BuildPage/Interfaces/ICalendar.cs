using BuildPage.Models;

namespace BuildPage.Interfaces
{
    public interface ICalendar
    {
        IQueryable<CalendarModel> GetAll();
        bool AddItem(CalendarModel model);
        void RemoveItem(int id);
        CalendarModel ItemDetails(int id);
    }
}
