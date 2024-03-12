using WinFo.Service;
using WinFo.Service.Usage;

IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

IInstalledProgramService ips = sf.CreateInstalledProgramService();

var progras = ips.GetInstalledPrograms();

Console.WriteLine(progras.Count);
