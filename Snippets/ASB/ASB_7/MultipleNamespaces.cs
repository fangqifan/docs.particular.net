﻿using NServiceBus;

class MultipleNamespaces
{
    public void SingleNamespaceStrategyWithAddNamespace(EndpointConfiguration endpointConfiguration)
    {
        #region single_namespace_partitioning_strategy_with_add_namespace

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        var partitioning = transport.NamespacePartitioning();
        partitioning.UseStrategy<SingleNamespacePartitioning>();
        partitioning.AddNamespace(
            name: "default",
            connectionString: "Endpoint=sb://namespace.servicebus.windows.net;SharedAccessKeyName=[shared access key name];SharedAccessKey=[shared access key]");

        #endregion
    }

    public void SingleNamespaceStrategyWithDefaultConnectionString(EndpointConfiguration endpointConfiguration)
    {
        #region single_namespace_partitioning_strategy_with_default_connection_string

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.ConnectionString(
            connectionString: "Endpoint=sb://namespace.servicebus.windows.net;SharedAccessKeyName=[shared access key name];SharedAccessKey=[shared access key]");

        #endregion
    }

    public void RoundRobinNamespacePartitioning(EndpointConfiguration endpointConfiguration)
    {
        #region round_robin_partitioning_strategy

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        var partitioning = transport.NamespacePartitioning();
        partitioning.AddNamespace(
            name: "namespace1",
            connectionString: "Endpoint=sb://namespace1.servicebus.windows.net;SharedAccessKeyName=[shared access key name];SharedAccessKey=[shared access key]");
        partitioning.AddNamespace(
            name: "namespace2",
            connectionString: "Endpoint=sb://namespace2.servicebus.windows.net;SharedAccessKeyName=[shared access key name];SharedAccessKey=[shared access key]");
        partitioning.AddNamespace(
            name: "namespace3",
            connectionString: "Endpoint=sb://namespace3.servicebus.windows.net;SharedAccessKeyName=[shared access key name];SharedAccessKey=[shared access key]");

        #endregion
    }

    public void FailOverNamespacePartitioning(EndpointConfiguration endpointConfiguration)
    {
        #region fail_over_partitioning_strategy

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        var partitioning = transport.NamespacePartitioning();
        partitioning.UseStrategy<FailOverNamespacePartitioning>();
        partitioning.AddNamespace(
            name: "primary",
            connectionString: "Endpoint=sb://primary.servicebus.windows.net;SharedAccessKeyName=[shared access key name];SharedAccessKey=[shared access key]");
        partitioning.AddNamespace(
            name: "secondary",
            connectionString: "Endpoint=sb://secondary.servicebus.windows.net;SharedAccessKeyName=[shared access key name];SharedAccessKey=[shared access key]");

        #endregion
    }

    void NamespaceRoutingRegistration(EndpointConfiguration endpointConfiguration)
    {
        #region namespace_routing_registration

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        var routing = transport.NamespaceRouting();
        routing.AddNamespace(
            name: "destination1",
            connectionString: "Endpoint=sb://destination1.servicebus.windows.net;SharedAccessKeyName=[shared access key name];SharedAccessKey=[shared access key]");
        routing.AddNamespace(
            name: "destination2",
            connectionString: "Endpoint=sb://destination2.servicebus.windows.net;SharedAccessKeyName=[shared access key name];SharedAccessKey=[shared access key]");

        #endregion
    }

    void NamespaceRoutingSendOptions(IEndpointInstance endpointInstance)
    {
        string destination;
        #region namespace_routing_send_options_full_connectionstring

        destination = "sales@Endpoint=sb://destination1.servicebus.windows.net;SharedAccessKeyName=[shared access key name];SharedAccessKey=[shared access key]";
        endpointInstance.Send(destination, new MyMessage());

        #endregion

        #region namespace_routing_send_options_named

        destination = "sales@destination1";
        endpointInstance.Send(destination, new MyMessage());

        #endregion
    }

    void DefaultNamespaceName(EndpointConfiguration endpointConfiguration)
    {
        #region default_namespace_name

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.DefaultNamespaceName("myname");

        #endregion
    }

    public class MyMessage :
        ICommand { }
}