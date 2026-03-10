# museum-exhibit

## Overview
AI-focused security backend for a museum exhibit. Receives SIEM alerts, analyzes them with an AI agent, and generates structured security reports.

## Features
- Receives alerts via HTTP POST endpoint
- AI agent generates:
	- Summary
	- Findings
	- Action Items
- Modular three-layer architecture (Repository, Service, Controller)
- In-memory database for prototyping
- API documentation with Scalar (Swagger UI for .NET 9)
- CI/CD pipeline with GitHub Actions:
	- Linting (dotnet format, Super-Linter)
	- Security scanning (CodeQL)
	- Build verification

## API Usage
**POST** `/api/alert/report`
```json
{
	"description": "Alert description",
	"metadata": "Alert metadata"
}
```
Returns a structured report.

## Getting Started
1. Clone the repo
2. Run `dotnet build` and `dotnet run` in MuseumExhibitApi
3. Access API docs at `/scalar/v1`

## CI/CD
- Automated checks on push to main
- Lint, scan, and build steps

## Future Plans
-
## TO-DO
- [ ] Create API to send a list of alerts to the frontend
- [ ] Create API to send all reported alerts and their reports to the frontend
- [ ] Create Frontend
- Integrate real AI models for report generation
- Add persistent database
- Build frontend dashboard