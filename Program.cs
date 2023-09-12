// See https://aka.ms/new-console-template for more information
using NLog;

string path = Directory.GetCurrentDirectory() + "\\nlog.config";
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

