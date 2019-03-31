using Highsoft.Web.Mvc.Charts;
using System;
using System.Collections.Generic;

namespace PatientManagementApp.ViewModels
{
    public class StatisticsViewModel
    {
        public List<ColumnSeriesData> correctSerie { get; set; }
        public List<ColumnSeriesData> incorrectSerie { get; set; }
        public List<string> xSerie { get; set; }

        public StatisticsViewModel()
        {
            correctSerie = new List<ColumnSeriesData>();
            incorrectSerie = new List<ColumnSeriesData>();
            xSerie = new List<string>();
        }
    }
}