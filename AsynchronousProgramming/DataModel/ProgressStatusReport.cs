using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProgramming.DataModel;
internal class ProgressStatusReport
{
    public int PercentageComplete { get; set; } = 0;
    public List<WebsiteModel> WebsiteDownloadedList { get; set; } = [];
}
