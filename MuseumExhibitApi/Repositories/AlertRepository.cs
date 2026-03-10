using MuseumExhibitApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MuseumExhibitApi.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private static List<Alert> _alerts = new List<Alert>();
        private static int _nextId = 1;

        public IEnumerable<Alert> GetAll() => _alerts;

        public Alert GetById(int id) => _alerts.FirstOrDefault(a => a.Id == id);

        public void Add(Alert alert)
        {
            alert.Id = _nextId++;
            _alerts.Add(alert);
        }
    }
}