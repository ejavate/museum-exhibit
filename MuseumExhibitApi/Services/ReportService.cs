using MuseumExhibitApi.Models;
using MuseumExhibitApi.Repositories;
using System.Collections.Generic;

namespace MuseumExhibitApi.Services
{
    public class ReportService
    {
        private readonly IAlertRepository _alertRepo;
        private readonly IReportRepository _reportRepo;

        public ReportService(IAlertRepository alertRepo, IReportRepository reportRepo)
        {
            _alertRepo = alertRepo;
            _reportRepo = reportRepo;
        }

        #region Public Methods
        public Alert AddAlert(string description, string metadata)
        {
            var alert = new Alert { Description = description, Metadata = metadata };
            _alertRepo.Add(alert);
            return alert;
        }

        public async Task<Report> GenerateReportForAlertAsync(int alertId, string description, string metadata, string? apiUrl = null, Dictionary<string, string>? headers = null)
        {
            var summary = await GetSummaryAsync(description, metadata, apiUrl, headers);
            var findings = await GetFindingsAsync(description, metadata, apiUrl, headers);
            var actionItems = await GetActionItemsAsync(description, metadata, apiUrl, headers);
            var report = new Report
            {
                AlertId = alertId,
                Summary = summary,
                Findings = findings,
                ActionItems = actionItems
            };
            _reportRepo.Add(report);
            return report;
        }

        public IEnumerable<Alert> GetAllAlerts() => _alertRepo.GetAll();

        public IEnumerable<Report> GetAllReports() => _reportRepo.GetAll();

        public IEnumerable<Report> GetReportsByAlertId(int alertId) => _reportRepo.GetByAlertId(alertId);

        public async Task<string> GetSummaryAsync(string description, string metadata, string? apiUrl = null, Dictionary<string, string>? headers = null)
        {
            if (string.IsNullOrEmpty(apiUrl))
                return "This is a generic summary of the alert.";
            var prompt = "Summarize the following alert.";
            return await CallAIAsync(apiUrl, prompt, description, metadata, headers);
        }

        public async Task<string> GetFindingsAsync(string description, string metadata, string? apiUrl = null, Dictionary<string, string>? headers = null)
        {
            if (string.IsNullOrEmpty(apiUrl))
                return "These are generic findings about the alert.";
            var prompt = "Analyze the alert and provide findings.";
            return await CallAIAsync(apiUrl, prompt, description, metadata, headers);
        }

        public async Task<string> GetActionItemsAsync(string description, string metadata, string? apiUrl = null, Dictionary<string, string>? headers = null)
        {
            if (string.IsNullOrEmpty(apiUrl))
                return "These are generic recommended action items.";
            var prompt = "Suggest recommended action items for the alert.";
            return await CallAIAsync(apiUrl, prompt, description, metadata, headers);
        }
        #endregion

        #region Private Helpers
        private async Task<string> CallAIAsync(string apiUrl, string prompt, string description, string metadata, Dictionary<string, string>? headers)
        {
            using var client = new System.Net.Http.HttpClient();
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            var payload = new
            {
                prompt,
                description,
                metadata
            };
            var content = new System.Net.Http.StringContent(System.Text.Json.JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            // Assume the response is a single string
            return json;
        }
        #endregion
    }
}