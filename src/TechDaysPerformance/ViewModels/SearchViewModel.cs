using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechdaysPerformance.Services;
using TechdaysPerformance.Tracing;
using Windows.Foundation.Diagnostics;

namespace TechdaysPerformance.ViewModels
{
    public class SearchViewModel : BindableBase
    {
        private readonly ImageService imageService;
        private readonly string searchString;
        private readonly ObservableCollection<string> searchResults;

        public SearchViewModel(string searchString)
        {
            this.imageService = new ImageService();
            this.searchResults = new ObservableCollection<string>();
            this.searchString = searchString;
        }

        public async Task LoadImages()
        {
            var fields = new LoggingFields();
            fields.AddString("searchString", searchString);
            var loadActivity = Trace.Instance.StartActivity("LoadImages", fields);

            try
            {
                SearchResults.Clear();
                foreach (string image in await this.imageService.GetImagesAsync(SearchString))
                {
                    SearchResults.Add(image);
                }
                Trace.Instance.LogMessage($"Added {SearchResults.Count} images for searchString {searchString}.");
            }
            finally
            {
                loadActivity.StopActivity("LoadImages");
            }
        }

        public string SearchString => searchString;

        public ObservableCollection<string> SearchResults => searchResults;
    }
}
