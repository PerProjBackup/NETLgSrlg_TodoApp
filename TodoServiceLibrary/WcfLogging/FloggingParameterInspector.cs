using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace TodoServiceLibrary.WcfLogging
{
  // usage /performance are globally tracked here
  // WCF Power Topics: Custon Behaviors (Miguel Castro)
  // parameterinspector -> operationbehavior -> service behavior -> behavior extension
  public class FloggingParameterInspector : IParameterInspector
  {
     string _serviceName;

    public FloggingParameterInspector(string serviceName)
    {
      _serviceName = serviceName;
    }

    public object BeforeCall(string operationName, object[] inputs)
    {
      var details = new Dictionary<string, object>();
      if (inputs != null)
        for (int i = 0; i < inputs.Count(); i++)
          details.Add($"input-{i}:", inputs[i] != null ? GetDetails(inputs[i]) : "");

      return WcfLogger.GetWcfLogEntry(operationName, details,
          serviceName: _serviceName);
    }

    private string GetDetails(object input)
    {
      var props = input.GetType().GetProperties();
      var sb = new StringBuilder();
      foreach (var prop in props)
        sb.Append($"{prop.Name}:{prop.GetValue(input).ToString()} -- ");

      return sb.ToString().Substring(0, sb.Length-4);
    }

    public void AfterCall(string operationName, object[] outputs,
      object returnValue, object correlationState)
    {
      var logEntry = correlationState as FlogDetail;
      if (logEntry == null) return;
      logEntry.ElapsedMilliseconds = 
          (DateTime.Now - logEntry.Timestamp).Milliseconds;
      WcfLogger.LogIt(logEntry, "Performance");
    }
  }
}