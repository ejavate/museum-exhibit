using MuseumExhibitApi.Models;
using System.Collections.Generic;

namespace MuseumExhibitApi.Repositories
{
    public interface IReportRepository
    {
        IEnumerable<Report> GetAll();
        Report GetById(int id);
        void Add(Report report);
        IEnumerable<Report> GetByAlertId(int alertId);
    }
}