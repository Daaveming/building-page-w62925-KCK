using BuildPage.Interfaces;
using BuildPage.Models;

namespace BuildPage.Services
{
    public class CalendarMock : ICalendar
    {
        private readonly DbContextModel _context;
        public CalendarMock(DbContextModel context)
        {
            _context = context;
        }
        public bool AddItem(CalendarModel model)
        {
            if(model == null)
            {
                return false;
            }

            if(model.Date.Hour >= 16)
            {
                _context.Add(model);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<CalendarModel> GetAll()
        {
            return _context.Calendar;
        }

        public CalendarModel ItemDetails(int id)
        {
            return _context.Calendar.FirstOrDefault(x => x.Id == id);
        }

        public void RemoveItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
