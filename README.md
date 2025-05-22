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
```
