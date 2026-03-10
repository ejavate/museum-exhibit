using MuseumExhibitApi.Models;
using System.Collections.Generic;

namespace MuseumExhibitApi.Repositories
{
    public interface IAlertRepository
    {
        IEnumerable<Alert> GetAll();
        Alert GetById(int id);
        void Add(Alert alert);
    }
}