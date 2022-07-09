using BuildPage.Models;

namespace BuildPage.Interfaces
{
    public interface IImages
    {
        void AddImage(ImageModel model);
        void RemoveImage(int id);
        IQueryable<ImageModel> GetAll();
        string GetImage(int id);
    }
}
