## Asynchronous Programming
- There is often a misunderstanding in the asynchronous programming and how to use it. In this project, I am trying to create an example of the correct usage of asynchronous programming and how this concept can benefit our project.
- I apply Tim Corey's method of asynchronous programming here with a few modifications to the code, particularly in utilizing parallel async. I'm not saying that my approach is better, but I simply want to explore async programming from a different perspective and understand this concept better.
- This project is self-documented. Inside this project you will find many comments explaining how the code works.

I want to highlight this code blocks where I implemented the parallel async.
Here I asked the program to download string of content from websites url.
We can download the content asynchronously one by one, but in this case, I download all the contents simultaneously and check which one finishes first and then displaying the result in the textbox.
By using this method, you will notice a significant improvement in the overall execution time, leading to better performance.

```cs
public static async Task DownloadParallelAsync(IProgress<ProgressStatusReport> progress)
{
    HttpClient client = new();
    List<string> websiteUrlList = InitializeData();
    List<WebsiteModel> websiteList = new();
    List<Task<WebsiteModel>> tasks = new();
    ProgressStatusReport progressStatus = new();
    progress.Report(progressStatus); //Trigger the progress update when data has no content so the progress bar will start from zero
    int progressPercentage = 0;

    //Download website asynchronously from website url list and save the task to the IEnumerable
    //Reference link: https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/start-multiple-async-tasks-and-process-them-as-they-complete
    IEnumerable<Task<WebsiteModel>> downloadWebsiteTaskFromQuery = from webisteUrl in websiteUrlList select DownloadWebsiteFromUrlAsync(webisteUrl, client);
    //Set the task to list
    List<Task<WebsiteModel>> downloadWebsiteTask = downloadWebsiteTaskFromQuery.ToList();

    //Check if there are any website task in the list.
    //We will remove the completed task from the list later on
    while (downloadWebsiteTask.Any())
    {
        //Here is how we check if there is any finished task
        Task<WebsiteModel> finishedTask = await Task.WhenAny(downloadWebsiteTask);
        //Get the website model of finished task
        var websiteDownloaded = await finishedTask;
        //Put website model to website list
        websiteList.Add(websiteDownloaded);

        //Update the progress to set the value of progressStatus.PercentageComplete
        //NOTE: We can get the downloaded count by using websiteList.Count(), but we want to separate the progressPercentage just to make it readable
        progressPercentage += 1;
        progressStatus.WebsiteDownloadedList = websiteList;
        progressStatus.PercentageComplete = (progressPercentage * 100) / websiteUrlList.Count;
        //Report progress update, it will trigger the ProgressChanged event handler
        progress.Report(progressStatus);

        //Remove finished task from the list
        downloadWebsiteTask.Remove(finishedTask);
    }
}
```
