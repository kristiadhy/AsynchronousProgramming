using AsynchronousProgramming.DataModel;

namespace AsynchronousProgramming.Method;
internal static class BasicMethod
{
    public static List<string> InitializeData()
    {
        List<string> websiteUrl = [
        "https://www.bbc.com/news",
        "https://www.google.com",
        "https://www.microsoft.com",
        "https://www.amazon.com",
        "https://www.facebook.com",
        "https://www.twitter.com",
        ];

        return websiteUrl;
    }

    public static async Task DownloadAsync(IProgress<ProgressStatusReport> progress)
    {
        HttpClient client = new();
        List<string> websiteUrlList = InitializeData();
        List<WebsiteModel> websiteList = [];
        ProgressStatusReport progressStatus = new();
        progress.Report(progressStatus); //Trigger the progress update when data has no content so the progress bar will start from zero
        int progressPercentage = 0;

        //Loop the initialized website urls
        //1.BBC
        //2.Google
        //3.Microsoft
        //4.Amazon
        //...
        foreach (string websiteUrl in websiteUrlList)
        {
            //Get the result of each downloaded
            var websiteContent = await DownloadWebsiteFromUrlAsync(websiteUrl, client);
            //Put it in the website list
            websiteList.Add(websiteContent);

            //Update the progress to set the value of progressStatus.PercentageComplete
            //NOTE: We can get the downloaded count by using websiteList.Count(), but we want to separate the progressPercentage just to make it readable
            progressPercentage += 1;
            progressStatus.WebsiteDownloadedList = websiteList;
            progressStatus.PercentageComplete = (progressPercentage * 100) / websiteUrlList.Count;

            //Report progress update, it will trigger the ProgressChanged event handler
            progress.Report(progressStatus);
        }
    }

    public static async Task DownloadParallelAsync(IProgress<ProgressStatusReport> progress)
    {
        HttpClient client = new();
        List<string> websiteUrlList = InitializeData();
        List<WebsiteModel> websiteList = [];

        ProgressStatusReport progressStatus = new();
        progress.Report(progressStatus); //Trigger the progress update when data has no content so the progress bar will start from zero
        int progressPercentage = 0;

        List<Task<WebsiteModel>> taskWebsiteModels = [];
        foreach (string websiteUrl in websiteUrlList)
        {
            //Get the result of each downloaded
            var taskWebsiteModel = DownloadWebsiteFromUrlAsync(websiteUrl, client);
            taskWebsiteModels.Add(taskWebsiteModel);
        }

        while (taskWebsiteModels.Count > 0)
        {
            var finishedTask = await Task.WhenAny(taskWebsiteModels);
            taskWebsiteModels.Remove(finishedTask);

            var websiteContent = await finishedTask;
            websiteList.Add(websiteContent);

            progressPercentage += 1;
            progressStatus.WebsiteDownloadedList = websiteList;
            progressStatus.PercentageComplete = (progressPercentage * 100) / websiteUrlList.Count;
            progress.Report(progressStatus);
        }
    }

    public static async Task<WebsiteModel> DownloadWebsiteFromUrlAsync(string websiteURL, HttpClient client)
    {
        WebsiteModel website = new();
        website.WebsiteUrl = websiteURL;
        website.WebsiteData = await client.GetStringAsync(websiteURL);
        return website;
    }

    public static List<string> GetReportResult(List<WebsiteModel> websites)
    {
        List<string> resultTexts = [];
        foreach (var site in websites)
        {
            string resultText = $"{site.WebsiteUrl} : {site.WebsiteData.Length}";
            resultTexts.Add(resultText);
        }
        return resultTexts;
    }
}
