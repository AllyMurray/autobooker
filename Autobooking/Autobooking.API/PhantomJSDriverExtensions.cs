using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace AutoBooking.Api
{
    // ReSharper disable once InconsistentNaming
    internal static class PhantomJSDriverExtensions
    {

        /// <summary>
        /// Waits for the ajax call to complete then retuens, throws a timeout exception if the timeout period is exceeded.
        /// </summary>
        /// <param name="phantomDriver"></param>
        /// <param name="timeout">The number of seconds before a timeout exception is thrown.</param>
        internal static void WaitForAjax(this PhantomJSDriver phantomDriver, int timeout = 10)
        {
            var stopWatch = new Stopwatch();

            while (stopWatch.Elapsed.TotalSeconds < timeout)
            {
                var ajaxIsComplete = (bool)(phantomDriver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete) return;
            }

            throw new TimeoutException($"The request did not complete in the given timeout period of ({timeout} seconds.)");
        }
    }
}
