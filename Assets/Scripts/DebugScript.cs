using UnityEngine;
using System.Collections;
using System.IO;

public class DebugScript {
    
	public static void Log(string message)
    {
        File.AppendAllText(@"C:\Users\Bjorn\Desktop\Log.txt", message + "\n");
    }
}
