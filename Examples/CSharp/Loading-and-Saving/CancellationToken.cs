using Aspose.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.Loading_and_Saving
{
    class CancellationToken
    {
        public static void Run()
        {
            // ExStart:CancellationTokenSource
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();

            CancellationTokenSource cts = new CancellationTokenSource();
            Scene scene = new Scene();
            cts.CancelAfter(1000);
            try
            {
                scene.Open(MyDir + "document.fbx" , cts.Token);
                Console.WriteLine("Import is done within 1000ms");
            }
            catch (ImportException e)
            {
                if (e.InnerException is OperationCanceledException)
                {
                    Console.WriteLine("It takes too long time to import, import has been canceled.");
                }
            }
            // ExEnd:CancellationTokenSource
        }
    }
}
