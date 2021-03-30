﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Pages.Content
{
    public partial class Dashboard 
    {
        [Parameter]
        public int ProductCount { get; set; }
        [Parameter]
        public int BrandCount { get; set; }
        [Parameter]
        public int UserCount { get; set; }
        [Parameter]
        public int RoleCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();        
            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await hubConnection.StartAsync();
            }
            hubConnection.On("UpdateDashboard", async () =>
            {
                await LoadDataAsync();
                StateHasChanged();
            });

        }

        private async Task LoadDataAsync()
        {
            var data = await _dashboardManager.GetDataAsync();
            if (data.Succeeded)
            {
                ProductCount = data.Data.ProductCount;
                BrandCount = data.Data.BrandCount;
                UserCount = data.Data.UserCount;
                RoleCount = data.Data.RoleCount;
            }
        }
        [CascadingParameter] public HubConnection hubConnection { get; set; }
        
    }
}
