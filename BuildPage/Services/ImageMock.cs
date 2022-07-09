using BuildPage.Interfaces;
using BuildPage.Models;

namespace BuildPage.Services
{
    public class ImageMock : IImages
    {
        private readonly DbContextModel _context;
        public ImageMock(DbContextModel context)
        {
            _context = context;
        }
        public void AddImage(ImageModel model)
        {
            _context.Images.Add(model);
            _context.SaveChanges();
        }

        public IQueryable<ImageModel> GetAll()
        {
            return _context.Images;
        }

        public string GetImage(int id)
        {
            var image = _context.Images.SingleOrDefault(x => x.Id == id);
            return image.File;
        }

        public void RemoveImage(int id)
        {
            var image = _context.Images.SingleOrDefault(x => x.Id == id);
            if(image != null)
            {
                _context.Images.Remove(image);
                _context.SaveChanges();
            }  
        }
    }
}
