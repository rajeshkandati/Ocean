﻿namespace BlazorAppShared.Pages {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BlazorAppShared.Model;
    using BlazorAppShared.Services;
    using Microsoft.AspNetCore.Components;
    using Oceanware.Ocean.ValidationRules;
    using Oceanware.Ocean.Blazor;
    using Microsoft.JSInterop;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "ET001:Type name does not match file name", Justification = "Waiting on Blazor 3.1 RTM to support partial classes so we wouldn't have to do this.")]
    public class AutoCompleteDemoBase : ComponentBase {

        [Inject]
        protected AddressService AddressService { get; set; }

        [Inject] 
        IJSRuntime JSRuntime { get; set; }

        public Person Person { get; set; }

        public IEnumerable<KeyValuePair<String, String>> States { get; set; }

        public AutoCompleteDemoBase() {
            this.States = StateAbbreviationNameTool.Instance.GetStateAbbreviationAndNames();
            this.Person = new Person {
                ActiveRuleSet = ValidationConstants.Insert,
                AddressLineOne = "200 Noll Plaza",
                City = "Huntington",
                State = "IN",
                Zip = "46750"
            };
        }

        protected void SelectedItemChanged(CityStateZip selectedItem) {
            this.Person.State = selectedItem.State;
            this.Person.Zip = selectedItem.Zip;
            Interop.Focus(JSRuntime, "phone");
        }

        protected async Task<IEnumerable<CityStateZip>> SearchByCity(string searchText) {
            var items = await this.AddressService.SearchByCityAsync(searchText);
            return items;
        }

        public void HandleValidSubmit() {
            Console.WriteLine("OnValidSubmit");
        }
    }
}
