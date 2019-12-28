using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walterlv.Blog.Middlewares
{
    public static class LegacyUrlRedirectionExtensions
    {
        /// <summary>
        /// 将博客中的各种已经遗弃的 URL 永久重定向到新的 URL 规范。
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>。</param>
        /// <returns><see cref="IApplicationBuilder"/>。</returns>
        public static IApplicationBuilder UsePostLegacyUrlRedirection(this IApplicationBuilder app) => app.UseLegacyUrlsRedirection(
            "/dotnet/2014/09/12/CallerMemberName.html", "/post/CallerMemberName",
            "/windows/2014/09/20/windows-dpi-awareness-for-wpf.html", "/post/windows-dpi-awareness-for-wpf",
            "/dotnet/2014/09/22/FileShare-read-file-which-is-written-by-another-process.html", "/post/FileShare-read-file-which-is-written-by-another-process",
            "/dotnet/2014/09/23/FileSystemWatcher-monitor-filesystem-changed.html", "/post/FileSystemWatcher-monitor-filesystem-changed",
            "/windows/2014/09/25/why-two-same-file-can-be-existed-on-desktop.html", "/post/why-two-same-file-can-be-existed-on-desktop",
            "/windows/2014/12/28/decelerate-windows-animation.html", "/post/decelerate-windows-animation",
            "/wpf/2015/03/31/run-desktop-application-above-windows-application.html", "/post/run-desktop-application-above-windows-application",
            "/windows/2015/03/31/sign-for-desktop-application.html", "/post/sign-for-desktop-application",
            "/windows/2015/07/07/associate-with-file-or-protocol.html", "/post/associate-with-file-or-protocol",
            "/wpf/2016/05/09/know-alt-is-pressed-in-key-down-event.html", "/post/know-alt-is-pressed-in-key-down-event",
            "/wpf/2016/07/31/solve-xaml-designer-errors.html", "/post/solve-xaml-designer-errors",
            "/visualstudio/2016/08/01/share-code-with-add-as-link.html", "/post/share-code-with-add-as-link",
            "/wpf/2017/01/16/wpf-render-system.html", "/post/wpf-render-system",
            "/dotnet/2017/01/17/convert-using-type-converter.html", "/post/convert-using-type-converter",
            "/dotnet/2017/01/19/there-is-no-code-of-mine-in-the-stack-trace.html", "/post/there-is-no-code-of-mine-in-the-stack-trace",
            "/windows/2017/09/12/32bit-application-use-large-memory.html", "/post/32bit-application-use-large-memory",
            "/dotnet/2017/09/12/exception-data.html", "/post/exception-data",
            "/wpf/2017/09/12/touch-not-work-in-wpf.html", "/post/touch-not-work-in-wpf",
            "/git/2017/09/13/add-file-to-whole-git-repository.html", "/post/add-file-to-whole-git-repository",
            "/jekyll/2017/09/15/setup-a-jekyll-blog-1.html", "/post/setup-a-jekyll-blog-1",
            "/jekyll/2017/09/15/setup-a-jekyll-blog.html", "/post/setup-a-jekyll-blog",
            "/windows/2017/09/17/find-lost-space-using-space-sniffer.html", "/post/find-lost-space-using-space-sniffer",
            "/jekyll/2017/09/17/force-https-for-github-pages.html", "/post/force-https-for-github-pages",
            "/uwp/2017/09/17/optimize-image-in-uwp.html", "/post/optimize-image-in-uwp",
            "/ime/2017/09/18/date-time-format-using-microsoft-pinyin.html", "/post/date-time-format-using-microsoft-pinyin",
            "/git/2017/09/18/delete-a-file-from-whole-git-history.html", "/post/delete-a-file-from-whole-git-history",
            "/git/2017/09/19/delete-file-using-filter-branch.html", "/post/delete-file-using-filter-branch",
            "/wpf/2017/09/19/why-unload-twice.html", "/post/why-unload-twice",
            "/uwp/2017/09/21/reflection-using-dotnet-native-runtime-directive.html", "/post/reflection-using-dotnet-native-runtime-directive",
            "/dotnet/2017/09/22/dotnet-version.html", "/post/dotnet-version",
            "/dotnet/2017/09/23/install-dotnet35-on-windows-10.html", "/post/install-dotnet35-on-windows-10",
            "/uwp/2017/09/25/binding-update-source-trigger.html", "/post/binding-update-source-trigger",
            "/uwp/2017/09/25/launch-uri-async.html", "/post/launch-uri-async",
            "/post/dotnet/2017/09/26/dispatcher-invoke-async.html", "/post/dispatcher-invoke-async",
            "/post/dotnet/2017/09/26/dispatcher-push-frame.html", "/post/dispatcher-push-frame",
            "/post/vs/2017/09/26/wildcards-in-vs-projects.html", "/post/wildcards-in-vs-projects",
            "/post/powershell/2017/09/28/get-clr-version-via-powershell.html", "/post/get-clr-version-via-powershell",
            "/post/dotnet/2017/09/30/app-context.html", "/post/app-context",
            "/post/win10/2017/10/02/wpf-transparent-blur-in-windows-10.html", "/post/wpf-transparent-blur-in-windows-10",
            "/post/wpf/capture-mouse-failed.html", "/post/capture-mouse-failed",
            "/post/windows/find-wifi-password.html", "/post/find-wifi-password",
            "/post/xaml/how-to-use-dependencyproperty-unsetvalue.html", "/post/how-to-use-dependencyproperty-unsetvalue",
            "/post/jekyll/jekyll-concat.html", "/post/jekyll-concat",
            "/post/git/pull-request-merge-request.html", "/post/pull-request-merge-request",
            "/post/jekyll/raw-in-jekyll.html", "/post/raw-in-jekyll",
            "/post/wpf-add-on-ui.html", "/post/wpf-cross-domain-ui",
            "/windows/2015/04/07/create-shortcut.html", "/post/create-shortcut-file-using-csharp",
            "/post/microsoft-extensions-commandlineutils.html", "/post/mcmaster-extensions-commandlineutils",
            "/post/write-custom-awaiter.html", "/post/write-dispatcher-awaiter-for-ui");
    }
}
