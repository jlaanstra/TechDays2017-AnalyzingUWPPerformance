using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechdaysPerformance.Services;
using TechdaysPerformance.Tracing;

namespace TechdaysPerformance.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public MainPageViewModel()
        {
            this.Pivots = new ObservableCollection<SearchViewModel>();

            foreach (string searchString in new[] { "cats", "dogs", "microsoft", "computers" })
            {
                this.Pivots.Add(new SearchViewModel(searchString));
            }
        }

        public async Task LoadPivots()
        {
            var loadActivity = Trace.Instance.StartActivity("LoadPivots");

            try
            {
                foreach (var pivot in Pivots)
                {
                    await pivot.LoadImages();
                }
            }
            finally
            {
                loadActivity.StopActivity("LoadPivots");
            }
        }

        private ObservableCollection<SearchViewModel> pivots;
        public ObservableCollection<SearchViewModel> Pivots
        {
            get { return pivots; }
            private set { this.SetProperty(ref pivots, value); }
        }
    }
}
