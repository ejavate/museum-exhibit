using MuseumExhibitApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MuseumExhibitApi.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private static List<Report> _reports = new List<Report>();
        private static int _nextId = 1;

        public IEnumerable<Report> GetAll() => _reports;

        public Report GetById(int id) => _reports.FirstOrDefault(r => r.Id == id);

        public void Add(Report report)
        {
            report.Id = _nextId++;
            _reports.Add(report);
        }

        public IEnumerable<Report> GetByAlertId(int alertId) => _reports.Where(r => r.AlertId == alertId);
    }
}