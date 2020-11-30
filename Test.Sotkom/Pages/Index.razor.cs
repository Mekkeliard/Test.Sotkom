using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Test.Sotkom.ViewModels;

namespace Test.Sotkom.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IndexViewModel DataContext { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await DataContext.InitializeAsync();
            DataContext.PropertyChanged += (s, e) => InvokeAsync(StateHasChanged);

            await base.OnInitializedAsync();
        }
    }
}
