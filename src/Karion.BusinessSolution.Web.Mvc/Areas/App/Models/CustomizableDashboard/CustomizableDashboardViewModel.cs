﻿using Karion.BusinessSolution.DashboardCustomization;
using Karion.BusinessSolution.DashboardCustomization.Dto;

namespace Karion.BusinessSolution.Web.Areas.App.Models.CustomizableDashboard
{
    public class CustomizableDashboardViewModel
    {
        public DashboardOutput DashboardOutput { get; }

        public Dashboard UserDashboard { get; }

        public CustomizableDashboardViewModel(
            DashboardOutput dashboardOutput,
            Dashboard userDashboard)
        {
            DashboardOutput = dashboardOutput;
            UserDashboard = userDashboard;
        }
    }
}