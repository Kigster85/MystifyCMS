﻿using Client.Static;
using Shared.Models;
using System.Net.Http.Json;

namespace Client.Services
{
    internal sealed class InMemoryDatabaseCache
    {
        private readonly HttpClient _httpClient;

        public InMemoryDatabaseCache(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private List<Category> _categories = null;

        internal List<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                NotifyCategoriesDataChanged();
            }
        
        }
        private bool _gettingCategoriesFromDatabaseAndCaching = false;

        internal async Task GetCategoriesFromDatabaseAndCache()
        {
            //Only Allow one Get request to run at a time
            if(_gettingCategoriesFromDatabaseAndCaching == false)
            {
            _gettingCategoriesFromDatabaseAndCaching = true;
            _categories = await _httpClient.GetFromJsonAsync<List<Category>>(APIEndpoints.s_categories);
            _gettingCategoriesFromDatabaseAndCaching = false;
            }

        }

        internal event Action OnCategoriesDataChanged;

        private void NotifyCategoriesDataChanged() => OnCategoriesDataChanged?.Invoke();
    }
}
