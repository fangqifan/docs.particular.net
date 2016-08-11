﻿using System;
using NServiceBus;
using NServiceBus.Persistence;

class Program
{

    static void Main()
    {
        Console.Title = "Samples.NHibernateCustomSagaFinder";
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.NHibernateCustomSagaFinder");
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.EnableInstallers();

        #region NHibernateSetup

        var persistence = busConfiguration.UsePersistence<NHibernatePersistence>();
        persistence.ConnectionString(@"Data Source=.\SqlExpress;Database=NHCustomSagaFinder;Integrated Security=True");

        #endregion

        using (var bus = Bus.Create(busConfiguration).Start())
        {
            bus.SendLocal(new StartOrder
                          {
                              OrderId = "123"
                          });

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
