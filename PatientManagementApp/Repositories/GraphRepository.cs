using PatientManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagementApp.Repositories
{
    public class GraphRepository
    {
        private readonly ApplicationDbContext context;

        public GraphRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Complete()
        {
            context.SaveChanges();
        }

        public void AddGraphPoint(GraphData graphData)
        {
            context.GraphData.Add(graphData);
        }

        public void AddGraphData(IEnumerable<GraphData> graphData)
        {
            context.GraphData.AddRange(graphData);
        }

        public void DeleteGraphData(IEnumerable<GraphData> graphData)
        {
            context.GraphData.RemoveRange(graphData);
        }

        public IEnumerable<DateTime> GetXSerie(int patientId)
        {
            return context.GraphData
                .Where(p => p.PatinetId == patientId)
                .Select(p => p.xValue)
                .Distinct()
                .ToList();
        }

        public IEnumerable<GraphData> GetData(int patientId)
        {
            return context.GraphData
                .Where(p => p.PatinetId == patientId)
                .ToList();
        }
    }
}